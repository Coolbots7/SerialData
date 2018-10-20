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

namespace SerialDataForm
{
    public partial class SerialDataForm : Form
    {
        public static readonly string[] BaudRates = new string[] { "300", "1200", "2400", "4800", "9600", "19200", "38400", "57600", "74880", "115200", "230400", "250000" };

        private SerialData.SerialData SerialData;

        //public SerialDataForm()
        //{
        //    Console.WriteLine("SerialDataForm class requires 
        //    Environment.Exit(-1);
        //}

        public SerialDataForm(SerialData.SerialData serialData )
        {
            InitializeComponent();
            this.SerialData = serialData;

            //Set COM Ports dropdown list
            this.UpdateCOMPortDropdown();

            //Set baud rate dropdown list
            foreach (string baudRate in BaudRates)
            {
                this.BaudRateSelection.Items.Add(baudRate);
            }
            this.BaudRateSelection.SelectedIndex = BaudRates.Length - 3;

            //Set connected state text box
            SerialConnectionStateText.TextAlign = HorizontalAlignment.Center;
            SerialConnectionStateText.BackColor = Color.Red;
            SerialConnectionStateText.Text = "Disconnected";
        }

        private void UpdateCOMPortDropdown()
        {
            this.COMPortSelection.Items.Clear();
            foreach (string port in this.SerialData.AvailableCOMPorts())
            {
                this.COMPortSelection.Items.Add(port);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            this.UpdateCOMPortDropdown();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Connecting..");
            this.SerialData.COMPort = this.COMPortSelection.GetItemText(this.COMPortSelection.SelectedItem);
            this.SerialData.BaudRate = Convert.ToInt32(this.BaudRateSelection.GetItemText(this.BaudRateSelection.SelectedItem));
            if (!this.SerialData.IsOpen() && this.SerialData.Open())
            {
                SerialConnectionStateText.BackColor = Color.Green;
                SerialConnectionStateText.Text = "Connected";
                ConnectButton.Text = "Disconnect";
            }

            else if (this.SerialData.IsOpen() && this.SerialData.Close())
            {
                SerialConnectionStateText.BackColor = Color.Red;
                SerialConnectionStateText.Text = "Disconnected";
                ConnectButton.Text = "Connect";
            }
        }
    }
}
