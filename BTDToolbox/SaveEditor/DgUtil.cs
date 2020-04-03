using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DgDecryptor
{
    //this was made by Vadmeme on github
    //https://github.com/Vadmeme/BTDSaveEditor

    class DgUtil
    {

        public static long nk_crc32(byte[] message)
        {
            unchecked
            { 
                long crc;
                int POLY = (int)0xdb710641;
                long v;

                crc = 0;

                for (int i = 0; i < message.Length; i++)
                {
                    v = message[i];
                    v ^= crc;
                    v &= 0xff;

                    for (int k = 0; k < 8; k++)
                    {
                        if ((v & 1) != 0) { v ^= POLY; };
                        v >>= 1;
                    }

                    crc >>= 8;
                    crc &= 0x00ffffff;
                    crc ^= v;
                }

                return crc & 0x00ffffffffL;
            }
        }

        public static JObject nk_decrypt(byte[] data_in)
        {
            string header = "";
            string nk_crc = "";
            for (int i = 0; i < 6; i++)
            {
                header += (char)data_in[i];
            }

            for (int i = 6; i < 6 + 8; i++)
            {
                nk_crc += (char)data_in[i];
            }

            if (!header.Equals("DGDATA"))
            {
                return null;
            }

            byte[] save = data_in.Skip(6 + 8).ToArray();

            if (!header.Equals("DGDATA"))
            {
                return null;
            }

            for (int counter = 0; counter < save.Length; counter++)
            {

                int v = counter / 6;
                v *= 6;

                v -= (counter & 0xff);
                v -= 0x15;

                save[counter] += (byte)v;
            }

            string crc_mine = nk_crc32(save).ToString("X").ToLower();
            while (crc_mine.Length < 8)
            {
                crc_mine = "0" + crc_mine;
            }

            if (!nk_crc.Equals(crc_mine))
            {
                return null;
            }

            string json = Encoding.ASCII.GetString(save);
            return JObject.Parse(json);
        }

        public static byte[] nk_encrypt(JObject o)
        {
            byte[] save = Encoding.ASCII.GetBytes(o.ToString(Formatting.None));

            byte[] heading = new byte[6 + 8];

            heading[0] = (byte)'D';
            heading[1] = (byte)'G';
            heading[2] = (byte)'D';
            heading[3] = (byte)'A';
            heading[4] = (byte)'T';
            heading[5] = (byte)'A';

            string crc = nk_crc32(save).ToString("X").ToLower();

            for (int counter = 0; counter < save.Length; counter++)
            {

                int v = counter / 6;
                v *= 6;

                v -= (counter & 0xff);
                v -= 0x15;

                save[counter] -= (byte)v;
            }

            while (crc.Length < 8) crc = "0" + crc;

            byte[] crc_bytes = Encoding.ASCII.GetBytes(crc);

            heading[6] = crc_bytes[0];
            heading[7] = crc_bytes[1];
            heading[8] = crc_bytes[2];
            heading[9] = crc_bytes[3];
            heading[10] = crc_bytes[4];
            heading[11] = crc_bytes[5];
            heading[12] = crc_bytes[6];
            heading[13] = crc_bytes[7];

            byte[] merge = new byte[heading.Length + save.Length];

            for (int i = 0; i < heading.Length; i++)
            {
                merge[i] = heading[i];
            }
            for (int i = 0; i < save.Length; i++)
            {
                merge[heading.Length + i] = save[i];
            }

            return merge;
        }

    }
}
