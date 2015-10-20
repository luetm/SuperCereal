using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PropertyChanged;
using SuperCereal.Model;

namespace SuperCereal.ViewModels
{
    [ImplementPropertyChanged]
    public class DataViewerVM
    {
        private readonly ViewerType _viewerType;
        private MasterVM _masterVM;
        
        public string Data { get; set; }

        public DataViewerVM()
        {
            GenerateTestData();
        }

        public DataViewerVM(ViewerType viewerType)
        {
            _viewerType = viewerType;
        }

        public DataViewerVM(MasterVM masterVM, ViewerType type)
        {
            _masterVM = masterVM;
            _viewerType = type;
        }

        public void Append(string data, Encoding encoding)
        {
            if (_viewerType == ViewerType.Hex)
            {
                var text = Data?.Replace(" ", "").Replace("\n", "").Replace("\r", "") + BitConverter.ToString(encoding.GetBytes(data)).Replace("-", "");
                text = Regex.Replace(text, "(.{2})", "$1 ");
                text = Regex.Replace(text, "(.{" + 48 + "})", "$1\n");

                Data = text;
            }
            else if (_viewerType == ViewerType.Text)
            {
                Data += data;
            }
        }

        public void Clear()
        {
            Data = "";
        }

        private void GenerateTestData()
        {
            Data =  "b6 06 55 1a  f3 8a 94 7d\n";
            Data += "c1 1b ea fb  a9 fb b2 a3\n";
            Data += "71 00 fd 54  8c 2f 20 0b\n";
            Data += "6c 90 28 0a  89 0b 2e ee ";
        }
    }
}
