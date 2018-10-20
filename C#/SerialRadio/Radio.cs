using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SerialRadio
{
    public class Radio
    {
        public static readonly string[] BaudRates = new string[] {"300", "1200", "2400", "4800", "9600", "19200", "38400", "57600", "74880", "115200", "230400", "250000"};

        private SerialComm serialComm = null;

        public int BaudRate
        {
            get
            {
                return this.serialComm.BaudRate;
            }

            set
            {
                this.serialComm.BaudRate = value;
            }
        }
        public string COMPort
        {
            get
            {
                return serialComm.COMPort;
            }

            set
            {
                serialComm.COMPort = value;
            }
        }
        public bool Open()
        {
            return this.serialComm.Open();
        }
        public bool Close()
        {
            return this.serialComm.Close();
        }
        public bool IsOpen
        {
            get
            {
                return this.serialComm.IsOpen();
            }
        }
        public string[] AvailableCOMPorts
        {
            get
            {
                return this.serialComm.AvailableCOMPorts();
            }
        }

        private List<RadioMessage> Messages;

        private SerialComm serialConnection;

        public Radio()
        {
            this.Messages = new List<RadioMessage>();
            this.serialComm = new SerialComm(SerialReader);
        }

        public bool Write(RadioHeader header, object Message, int MessageSize)
        {
            try
            {
                byte[] buffer = new byte[Marshal.SizeOf(typeof(RadioHeader)) + MessageSize];

                IntPtr headerPointer = Marshal.AllocHGlobal(Marshal.SizeOf(header));
                Marshal.StructureToPtr(header, headerPointer, false);

                Marshal.Copy(headerPointer, buffer, 0, Marshal.SizeOf(header));

                BinaryFormatter bf = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    bf.Serialize(ms, Message);
                    Array.Copy(ms.ToArray(), 0, buffer, Marshal.SizeOf(typeof(RadioHeader)), MessageSize);
                }

                this.serialComm.Write(buffer, Marshal.SizeOf(buffer));
            }
            catch
            {
                return false;
            }
            return true;

        }

        public byte[] Read()
        {
            byte[] message = this.Messages[0].Message;
            this.Messages.RemoveAt(0);
            return message;
        }

        public RadioHeader Peek()
        {
            return this.Messages[0].Header;
        }

        private void SerialReader(byte[] buffer, int numBytes)
        {
            //Read message header
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            RadioHeader header = (RadioHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(RadioHeader));
            handle.Free();

            byte[] messageBuffer = new byte[numBytes - Marshal.SizeOf(typeof(RadioHeader))];
            Array.Copy(buffer, Marshal.SizeOf(typeof(RadioHeader)), messageBuffer, 0, numBytes - Marshal.SizeOf(typeof(RadioHeader)));

            //add to message queue
            this.Messages.Add(new RadioMessage { Header = header, Message = messageBuffer });
        }

        private struct RadioMessage
        {
            public RadioHeader Header;
            public byte[] Message;
        };

        
    }

    public struct RadioHeader
    {
        public char Type;
        public byte FromNode;
    };

    public class SerialComm
    {
        private SerialPort serialPort;
        private int baudRate;
        private int readTimeout = 500;
        private int writeTimeout = 500;
        private string portName;

        public int BaudRate
        {
            get
            {
                return this.baudRate;
            }

            set
            {
                this.baudRate = value;
            }
        }
        public string COMPort
        {
            get
            {
                return this.portName;
            }

            set
            {
                this.portName = value;
            }
        }

        private Action<byte[], int> dataReceivedHandler;

        public bool IsOpen()
        { 
            return (serialPort.IsOpen);
        }

        public string[] AvailableCOMPorts()
        {
            return SerialPort.GetPortNames();
        }

        public SerialComm(Action<byte[], int> handler)
        {
            this.dataReceivedHandler = handler;
            this.serialPort = new SerialPort();
        }

        public bool Open()
        {
            try
            {
                serialPort.BaudRate = this.baudRate;
                serialPort.ReadTimeout = this.readTimeout;
                serialPort.WriteTimeout = this.writeTimeout;
                serialPort.PortName = this.portName;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceivedFunction);
                serialPort.Open();
            }
            catch
            {
                return false;
            }

            return true;

        }

        public bool Close()
        {
            try
            {
                serialPort.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void dataReceivedFunction(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[serialPort.BytesToRead];

            int bytesRead = serialPort.Read(buffer, 0, serialPort.BytesToRead);

            this.dataReceivedHandler(buffer, bytesRead);
        }

        public bool Write(byte[] message, int numBytes)
        {
            try
            {
                serialPort.Write(message, 0, numBytes);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }

    
}
