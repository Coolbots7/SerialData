using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SerialData
{
    public struct SerialHeader
    {
        public char Type;
        public byte NumBytes;

        public SerialHeader(char type)
        {
            this.Type = type;
            this.NumBytes = 0;
        }
    };

    public class SerialData
    {
        private readonly int MESSAGE_SIZE = 62;

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

        private Action<SerialHeader, byte[], UInt32> dataReceivedHandler;

        public bool IsOpen()
        { 
            return (serialPort.IsOpen);
        }

        public string[] AvailableCOMPorts()
        {
            return SerialPort.GetPortNames();
        }

        public SerialData(Action<SerialHeader, byte[], UInt32> handler)
        {
            this.dataReceivedHandler = handler;
            this.serialPort = new SerialPort();
        }

        public bool Open()
        {
            Console.Write("Connecting...");
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
                //serialPort.Encoding = Encoding.Unicode;
                serialPort.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed");
                Console.WriteLine("Error: " + e.Message);
                return false;
            }

            Console.WriteLine("OK");

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
            if (serialPort.BytesToRead >= (Marshal.SizeOf(typeof(SerialHeader)) + this.MESSAGE_SIZE))
            {
                byte[] headerBuffer = new byte[Marshal.SizeOf(typeof(SerialHeader))];

                serialPort.Read(headerBuffer, 0, Marshal.SizeOf(typeof(SerialHeader)));

                GCHandle handle = GCHandle.Alloc(headerBuffer, GCHandleType.Pinned);
                SerialHeader header = (SerialHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(SerialHeader));
                handle.Free();

                byte[] messageBuffer = new byte[this.MESSAGE_SIZE];

                serialPort.Read(messageBuffer, 0, this.MESSAGE_SIZE);

                this.dataReceivedHandler(header, messageBuffer, (UInt32)this.MESSAGE_SIZE);
            }

            //this.dataReceivedHandler(buffer, bytesRead);
        }

        public bool Write(SerialHeader Header, object message, byte NumBytes)
        {
            int writeBufferSize = Marshal.SizeOf(typeof(SerialHeader)) + this.MESSAGE_SIZE;
            Header.NumBytes = NumBytes;
            try
            {
                byte[] writeBuffer = new byte[writeBufferSize];

                //Add Header to witeBuffer
                IntPtr headerPtr = Marshal.AllocHGlobal(Marshal.SizeOf(Header));
                Marshal.StructureToPtr(Header, headerPtr, false);

                Marshal.Copy(headerPtr, writeBuffer, 0, Marshal.SizeOf(Header));


                //Add message to writeBuffer
                //BinaryFormatter bf = new BinaryFormatter();
                //using (var ms = new MemoryStream())
                //{
                //    bf.Serialize(ms, message);
                //    Array.Copy(ms.ToArray(), 0, writeBuffer, Marshal.SizeOf(typeof(SerialHeader)), this.MESSAGE_SIZE);
                //}

                IntPtr messagePtr = Marshal.AllocHGlobal(Marshal.SizeOf(message));
                Marshal.StructureToPtr(message, messagePtr, false);

                Marshal.Copy(messagePtr, writeBuffer, Marshal.SizeOf(typeof(SerialHeader)), Marshal.SizeOf(message));


                //Write writeBuffer to Serial Port
                serialPort.Write(writeBuffer, 0, writeBufferSize);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
