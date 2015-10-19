using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using SuperCereal.Model;

namespace SuperCereal.ViewModels
{
    [ImplementPropertyChanged]
    public class PortDetailVM
    {
        private MasterVM _masterVM;

        public Port Port { get; set; }

        public int[] BaudRates { get; set; }
        public Handshake[] Handshakes { get; set; }
        public Parity[] Parities { get; set; }
        public int[] ByteLengths { get; set; }
        public string[] PortNames { get; set; }
        public StopBits[] StopBitss { get; set; }
        public Encoding[] Encodings { get; set; }

        public PortDetailVM()
        {
            MakeCollections();
            GenerateTestData();
        }

        public PortDetailVM(MasterVM masterVM, Port port, SerialPort serialPort)
        {
            MakeCollections();
            _masterVM = masterVM;

            Port = port;
            Port.IsConnected = serialPort.IsOpen;
        }

        private void MakeCollections()
        {
            BaudRates = new[]
            {
                110, 300, 600, 1200, 2400, 4800, 9600, 14400,
                19200, 28800, 38400, 56000, 57600, 115200,
                128000, 153600, 230400, 256000, 460800, 921600
            };

            Handshakes = new[] { Handshake.None, Handshake.RequestToSend, Handshake.RequestToSendXOnXOff, Handshake.XOnXOff };
            Parities = new[] { Parity.None, Parity.Odd, Parity.Even, Parity.Mark, Parity.Space };
            ByteLengths = new[] { 7, 8 };
            PortNames = SerialPort.GetPortNames();
            StopBitss = new[] { StopBits.None, StopBits.One, StopBits.OnePointFive, StopBits.Two };
            Encodings = new[] { Encoding.ASCII, Encoding.Default, Encoding.UTF8, Encoding.UTF7, Encoding.UTF32, Encoding.BigEndianUnicode, Encoding.Unicode };
        }

        private void GenerateTestData()
        {
            PortNames = new[] { "COM1", "COM2" };
            Port = new Port
            {
                BaudRate = 9600,
                Handshake = Handshake.XOnXOff,
                Parity = Parity.Even,
                DataBits = 8,
                PortName = "COM1",
                IsConnected = true,
                StopBits = StopBits.OnePointFive,
                Encoding = Encoding.ASCII,
            };
        }
    }
}
