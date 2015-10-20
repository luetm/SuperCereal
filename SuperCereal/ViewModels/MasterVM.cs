using Omu.ValueInjecter;
using PropertyChanged;
using SuperCereal.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using Common.Async;
using GalaSoft.MvvmLight.Command;
using SuperCereal.Tools;

namespace SuperCereal.ViewModels
{
    [ImplementPropertyChanged]
    public class MasterVM
    {
        public DataViewerVM DataText { get; set; }
        public DataViewerVM DataHex { get; set; }

        public PortListVM PortList { get; set; }
        public PortDetailVM PortDetail { get; set; }

        public FileDumpVM FileDump { get; set; }

        public RelayCommand<string> SendCommand { get; set; }


        private Dictionary<string, DeviceState> _states;

        public MasterVM(bool live)
        {
            try
            {
                if (!live)
                    return;

                PortList = new PortListVM(this);
                FileDump = new FileDumpVM(this);

                SendCommand = new RelayCommand<string>(Send);

                RefreshPorts();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.GenerateInfo());
            }
        }

        private void RefreshPorts()
        {
            _states = new Dictionary<string, DeviceState>();
            foreach (var port in PortList.Ports)
            {
                var serialPort = new SerialPort(port.PortName)
                {
                    RtsEnable = true,
                    DtrEnable = true,
                    Encoding = port.Encoding,
                    Parity = port.Parity,
                    Handshake = port.Handshake,
                    BaudRate = port.BaudRate,
                    DataBits = port.DataBits,
                    StopBits = port.StopBits,
                };

                var state = new DeviceState
                {
                    SerialPort = serialPort,
                    Port = port,
                    Hex = new DataViewerVM(this, ViewerType.Hex),
                    Text = new DataViewerVM(this, ViewerType.Text),
                };
                state.BackgroundTask = Task.Run(() => Work(state));
                _states[port.PortName] = state;
            }
        }

        public MasterVM()
        {
            GenerateTestData();
        }

        public void OnSelectedPortChanged(Port value)
        {
            if (_states == null || value == null)
                return;

            var state = _states[value.PortName];
            PortDetail = new PortDetailVM(this, value, state.SerialPort);
            DataText = state.Text;
            DataHex = state.Hex;
        }

        private async void Work(DeviceState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));


            while (!state.KillSignal)
            {
                try
                {
                    if (state.SerialPort.IsOpen && state.SerialPort.BytesToRead > 0)
                    {
                        while (state.SerialPort.BytesToRead > 0)
                        {
                            var text = state.SerialPort.ReadExisting();
                            state.Text.Append(text, state.Port.Encoding);
                            state.Hex.Append(text, state.Port.Encoding);
                        }
                    }
                    await Task.Delay(500);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.GenerateInfo());
                }
            }
        }

        private void Send(string text)
        {
            try
            {
                if (PortList.SelectedPort == null)
                    return;

                text = TextHelper.Convert(text.Replace("\n", "").Replace("\r", ""));

                var state = _states[PortList.SelectedPort.PortName];
                state.SerialPort.Write(text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.GenerateInfo());
            }
        }

        public void Clear()
        {
            DataHex.Clear();
            DataText.Clear();
        }

        public void SendBytes(byte[] raw)
        {
            try
            {
                if (PortList.SelectedPort == null)
                    return;

                var state = _states[PortList.SelectedPort.PortName];
                state.SerialPort.Write(raw, 0, raw.Length);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.GenerateInfo());
            }
        }

        private void GenerateTestData()
        {
            DataText = new DataViewerVM(ViewerType.Text);
            DataHex = new DataViewerVM(ViewerType.Hex);

            PortList = new PortListVM();
            PortDetail = new PortDetailVM();
        }
    }
}
