using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerialData;
using SerialDataForm;
using System.Runtime.InteropServices;

namespace RobotMotorSerialView
{
    public struct TestStruct
    {
        public char Char;
        public Int32 Integer;
        public float Float;

    }
    public partial class SerialDatTest : Form
    {
        private SerialData.SerialData SerialData;
        private SerialDataForm.SerialDataForm SerialDataForm;

        public SerialDatTest()
        {
            InitializeComponent();

            this.SerialData = new SerialData.SerialData(this.SerialDataHandler);
            this.SerialDataForm = new SerialDataForm.SerialDataForm(this.SerialData);
            this.SerialDataForm.Show();
        }

        delegate void SerialDataHandlerCallback(SerialData.SerialHeader Header, byte[] messageBuffer, UInt32 NumBytes);
        private void SerialDataHandler(SerialData.SerialHeader Header, byte[] messageBuffer, UInt32 NumBytes)
        {

            switch (Header.Type)
            {
                case '@':
                    Console.WriteLine("Got bytes read header: " + Header.Type + " - " + Header.NumBytes);
                    Int32 bytes = System.BitConverter.ToInt32(messageBuffer, 0);
                    Console.WriteLine("\tBytes: " + bytes);
                    break;
                case '!':
                    Console.WriteLine("Got test 1 header: " + Header.Type + " - " + Header.NumBytes);
                    //Int32 message = System.BitConverter.ToInt32(messageBuffer, 0);
                    float message = System.BitConverter.ToSingle(messageBuffer, 0);
                    Console.WriteLine("\tMessage: " + message);
                    break;
                case '#':
                    Console.WriteLine("Got test 2 message header: " + Header.Type + " - " + Header.NumBytes);
                    //Int32 message = System.BitConverter.ToInt32(messageBuffer, 0);
                    Int32 randomVariable = System.BitConverter.ToInt32(messageBuffer, 0);
                    Console.WriteLine("\tMessage: " + randomVariable);
                    break;
                case '$':
                    Console.WriteLine("Got test 3 message header: " + Header.Type + " - " + Header.NumBytes);
                    GCHandle messageHandle = GCHandle.Alloc(messageBuffer, GCHandleType.Pinned);
                    TestStruct Struct = (TestStruct)Marshal.PtrToStructure(messageHandle.AddrOfPinnedObject(), typeof(TestStruct));
                    messageHandle.Free();

                    Console.WriteLine("\tChar: " + Struct.Char);
                    Console.WriteLine("\tInteger: " + Struct.Integer);
                    Console.WriteLine("\tFloat: " + Struct.Float);
                    break;
                default:
                    Console.WriteLine("Unknown Header type: " + Header.Type);
                    Console.WriteLine("\t" + (byte)Header.Type);
                    Console.WriteLine("\t" + messageBuffer.Length);
                    Console.Write("\t");
                    foreach (byte b in messageBuffer)
                    {
                        Console.Write(b + ", ");
                    }
                    Console.WriteLine();
                    break;
            };
        }

        private void Test1Btn_Click(object sender, EventArgs e)
        {
            SerialHeader Header = new SerialHeader('!');
            float message = (float)12.0;
            this.SerialData.Write(Header, message, (byte)Marshal.SizeOf(message));
        }

        private void Test2Button_Click(object sender, EventArgs e)
        {
            SerialHeader Header = new SerialHeader('#');
            Int32 message = 20;
            this.SerialData.Write(Header, message, (byte)Marshal.SizeOf(message));
        }

        private void Test3Button_Click(object sender, EventArgs e)
        {
            SerialHeader Header = new SerialHeader('$');
            TestStruct Struct = new TestStruct();
            Struct.Char = 'S';
            Struct.Integer = 20;
            Struct.Float = (float)0.3;

            this.SerialData.Write(Header, Struct, (byte)Marshal.SizeOf(Struct));
        }


    }
}
