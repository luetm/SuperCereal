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
    public class PortListVM
    {
        private readonly MasterVM _masterVM;

        public IEnumerable<Port> Ports { get; set; }
        public Port SelectedPort { get; set; }

        public PortListVM()
        {
            GenerateTestData();
        }

        public PortListVM(MasterVM masterVM)
        {
            _masterVM = masterVM;

            Ports = SerialPort.GetPortNames().Select(n => new Port
            {
                Encoding = Encoding.ASCII,
                Handshake = Handshake.None,
                Parity = Parity.Even,
                BaudRate = 9600,
                StopBits = StopBits.One,
                DataBits = 8,
                PortName = n,
            }).ToList();
        }

        protected void OnSelectedPortChanged()
        {
            _masterVM.OnSelectedPortChanged(SelectedPort);
        }

        private void GenerateTestData()
        {
            Ports = new List<Port>
            {
                new Port { IsConnected = false, PortName = "COM1", BaudRate = 9600},
                new Port { IsConnected = false, PortName = "COM3", BaudRate = 921600},
                new Port { IsConnected = true, PortName = "COM4", BaudRate = 19200},
            };
        }
    }
}
