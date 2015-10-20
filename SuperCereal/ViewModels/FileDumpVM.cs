using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PropertyChanged;
using SuperCereal.Tools;

namespace SuperCereal.ViewModels
{
    [ImplementPropertyChanged]
    public class FileDumpVM
    {
        private MasterVM _masterVM;

        public RelayCommand BrowseCommand { get; set; }
        public RelayCommand DumpCommand { get; set; }

        public string File { get; set; }
        public bool CanDump { get; set; }
        public string Preview { get; set; }
        public string PreviewHex { get; set; }
        public byte[] Raw { get; set; }


        public FileDumpVM()
        {
            GenerateTestData();
        }

        public FileDumpVM(MasterVM masterVM)
        {
            _masterVM = masterVM;

            BrowseCommand = new RelayCommand(Browse);
            DumpCommand = new RelayCommand(Dump);
        }

        private void Browse()
        {
            Task.Run(() =>
            {
                try
                {
                    var dialog = new OpenFileDialog
                    {
                        Multiselect = false,
                        CheckFileExists = true,
                        CheckPathExists = true,
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    };

                    if (dialog.ShowDialog() == true) // yes, it's Nullable<bool>
                    {
                        var fi = new FileInfo(dialog.FileName);
                        File = fi.FullName;

                        if (fi.Exists)
                        {
                            var preview = System.IO.File.ReadAllText(fi.FullName);
                            Preview = TextHelper.ConvertBack(preview)
                            .Replace("[CR]", "[CR]\n")
                            .Replace("[LF]", "[LF]\n");

                            Raw = System.IO.File.ReadAllBytes(fi.FullName);

                            var text = BitConverter.ToString(Raw).Replace("-", "");
                            text = Regex.Replace(text, "(.{2})", "$1 ");
                            text = Regex.Replace(text, "(.{" + 48 + "})", "$1\n");

                            PreviewHex = text;

                            CanDump = true;
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.GenerateInfo());
                    CanDump = false;
                }
            });
        }

        private void Dump()
        {
            _masterVM.SendBytes(Raw);
        }

        private void GenerateTestData()
        {
            File = @"C:\Test\file.bin";
            CanDump = true;
            Preview = "[SOH][CR]\nStormtrooper[CR]\nStormtrooper[CR]\nWHAZAAAAAA[CR]\n[EOT]";
            PreviewHex = "b6 06 55 1a  f3 8a 94 7d\n";
            PreviewHex += "c1 1b ea fb  a9 fb b2 a3\n";
            PreviewHex += "71 00 fd 54  8c 2f 20 0b\n";
            PreviewHex += "6c 90 28 0a  89 0b 2e ee ";
        }
    }
}
