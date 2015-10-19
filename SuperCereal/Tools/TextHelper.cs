using System.Text;

namespace SuperCereal.Tools
{
    public static class TextHelper
    {
        public static string Soh => Encoding.ASCII.GetString(new byte[] { 0x01 });
        public static string Stx => Encoding.ASCII.GetString(new byte[] { 0x02 });
        public static string Etx => Encoding.ASCII.GetString(new byte[] { 0x03 });
        public static string Eot => Encoding.ASCII.GetString(new byte[] { 0x04 });
        public static string Enq => Encoding.ASCII.GetString(new byte[] { 0x05 });
        public static string Ack => Encoding.ASCII.GetString(new byte[] { 0x06 });
        public static string Cr => Encoding.ASCII.GetString(new byte[] { 0x0D });
        public static string Sp => Encoding.ASCII.GetString(new byte[] { 0x20 });
        public static string Etb => Encoding.ASCII.GetString(new byte[] { 0x17 });
        public static string Lf => Encoding.ASCII.GetString(new byte[] { 0x0A });


        public static byte SohByte => 0x01;
        public static byte StxByte => 0x02;
        public static byte EtxByte => 0x03;
        public static byte EotByte => 0x04;
        public static byte EnqByte => 0x05;
        public static byte AckByte => 0x06;
        public static byte CrByte => 0x0D;
        public static byte SpByte => 0x20;
        public static byte LfByte => 0x0A;

        public static char[] All => new[]
        {
            Soh[0],
            Stx[0],
            Etx[0],
            Eot[0],
            Enq[0],
            Ack[0],
            Cr[0] ,
            Sp[0] ,
            Etb[0],
            Lf[0],
        };

        public static string Convert(string input)
        {
            var output = input;

            output = output.Replace("[SOH]", Soh);
            output = output.Replace("[STX]", Stx);
            output = output.Replace("[ETX]", Etx);
            output = output.Replace("[EOT]", Eot);
            output = output.Replace("[ENQ]", Enq);
            output = output.Replace("[ACK]", Ack);
            output = output.Replace("[CR]", Cr);
            output = output.Replace("[SP]", Sp);
            output = output.Replace("[ETB]", Etb);
            output = output.Replace("[LF]", Lf);

            return output;
        }

        public static string ConvertBack(string input)
        {
            var output = input;

            output = output.Replace(Soh, "[SOH]");
            output = output.Replace(Stx, "[STX]");
            output = output.Replace(Etx, "[ETX]");
            output = output.Replace(Eot, "[EOT]");
            output = output.Replace(Enq, "[ENQ]");
            output = output.Replace(Ack, "[ACK]");
            output = output.Replace(Etb, "[ETB]");
            output = output.Replace(Cr, "[CR]");
            output = output.Replace(Sp, "[SP]");
            output = output.Replace(Lf, "[LF]");

            return output;
        }
    }
}
