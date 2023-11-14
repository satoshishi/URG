namespace URG.SCIP.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // http://sourceforge.net/p/urgnetwork/wiki/cs_sample_jp/
    public class SCIP_Reader
    {
        /// <summary>
        /// read MD command
        /// </summary>
        /// <param name="get_command">received command</param>
        /// <param name="time_stamp">timestamp data</param>
        /// <param name="distances">distance data</param>
        /// <returns>is successful</returns>
        public static bool MD(string get_command, ref long time_stamp, ref List<long> distances)
        {
            string[] split_command = get_command.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (split_command[1].StartsWith("00"))
            {
                return true;
            }
            else if (split_command[1].StartsWith("99"))
            {
                time_stamp = Decode(split_command[2], 4);
                DistanceData(split_command, 3, ref distances);
                return true;
            }

            return false;
        }

        public static bool GD(string get_command, ref long time_stamp, ref List<long> distances)
        {
            string[] split_command = get_command.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (split_command[1].StartsWith("00"))
            {
                time_stamp = Decode(split_command[2], 4);
                DistanceData(split_command, 3, ref distances);
                return true;
            }

            return false;
        }

        /// <summary>
        /// read distance data
        /// </summary>
        /// <param name="lines">lines</param>
        /// <param name="start_line">start_line</param>
        /// <param name="distances">distances</param>
        /// <returns>bool</returns>
        public static bool DistanceData(string[] lines, int start_line, ref List<long> distances)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = start_line; i < lines.Length; ++i)
            {
                sb.Append(lines[i][..^1]);
            }

            return Decode_array(sb.ToString(), 3, ref distances);
        }

        /// <summary>
        /// Decode part of string
        /// </summary>
        /// <param name="data">encoded string</param>
        /// <param name="size">encode size</param>
        /// <param name="offset">Decode start position</param>
        /// <returns>Decode result</returns>
        public static long Decode(string data, int size, int offset = 0)
        {
            long value = 0;

            for (int i = 0; i < size; ++i)
            {
                value <<= 6;
                value |= (long)data[offset + i] - 0x30;
            }

            return value;
        }

        /// <summary>
        /// Decode multiple data
        /// </summary>
        /// <param name="data">encoded string</param>
        /// <param name="size">encode size</param>
        /// <param name="decodedData">decode data</param>
        /// <returns>Decode result</returns>
        public static bool Decode_array(string data, int size, ref List<long> decodedData)
        {
            for (int pos = 0; pos <= data.Length - size; pos += size)
            {
                decodedData.Add(Decode(data, size, pos));
            }

            return true;
        }

        public static bool ME(string get_command, ref long time_stamp, ref List<long> distances, ref List<long> strengths)
        {
            string[] split_command = get_command.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (split_command[1].StartsWith("00"))
            {
                return true;
            }
            else if (split_command[1].StartsWith("99"))
            {
                time_stamp = Decode(split_command[2], 4);
                DistanceStrengthData(split_command, 3, ref distances, ref strengths);
                return true;
            }

            return false;
        }

        public static bool DistanceStrengthData(string[] lines, int start_line, ref List<long> distances, ref List<long> strengths)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = start_line; i < lines.Length; ++i)
            {
                sb.Append(lines[i][..^1]);
            }

            return Decode_array(sb.ToString(), 3, ref distances, ref strengths);
        }

        public static bool Decode_array(string data, int size, ref List<long> decodedData, ref List<long> stDecoded_data)
        {
            for (int pos = 0; pos <= data.Length - (size * 2); pos += size * 2)
            {
                decodedData.Add(Decode(data, size, pos));
                stDecoded_data.Add(Decode(data, size, pos + size));
            }

            return true;
        }
    }
}
