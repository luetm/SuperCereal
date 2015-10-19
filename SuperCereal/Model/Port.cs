using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace SuperCereal.Model
{
    [ImplementPropertyChanged]
    public class Port
    {
        public string PortName { get; set; }
        public bool IsConnected { get; set; }

        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public Parity Parity { get; set; }
        public Handshake Handshake { get; set; }
        public Encoding Encoding { get; set; }
    }
}
