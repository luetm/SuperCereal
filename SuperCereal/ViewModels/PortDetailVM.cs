using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Async;
using GalaSoft.MvvmLight.Command;
using PropertyChanged;
using SuperCereal.Model;

namespace SuperCereal.ViewModels
{
    [ImplementPropertyChanged]
    public class PortDetailVM
    {
        private MasterVM _masterVM;
        private readonly SerialPort _serialPort;

        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand DisconnectCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }

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
            _serialPort = serialPort;

            Port = port;
            Port.IsConnected = serialPort.IsOpen;

            ConnectCommand = new RelayCommand(Connect);
            DisconnectCommand = new RelayCommand(Disconnect);
            ClearCommand = new RelayCommand(Clear);
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

        private void Connect()
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
                Port.IsConnected = true;
            }
        }

        private void Disconnect()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                Port.IsConnected = false;
            }
        }

        private void Clear()
        {
            _masterVM.Clear();
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
