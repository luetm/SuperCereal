using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SuperCereal.ViewModels;

namespace SuperCereal.Model
{
    public class DeviceState
    {
        public SerialPort SerialPort { get; set; }
        public Port Port { get; set; }
        public DataViewerVM Hex { get; set; }
        public DataViewerVM Text { get; set; }
        public Task BackgroundTask { get; set; }
        public bool KillSignal { get; set; }
    }
}
