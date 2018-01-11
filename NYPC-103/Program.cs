using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NYPC_103
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.UTF8;
            //Console.InputEncoding = Encoding.UTF8;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            while (true)
            {
                string input = Console.ReadLine();
                if (input.StartsWith("end"))
                {
                    break;
                }
                else if (input.StartsWith("set"))
                {
                    string key = input.Substring(4, input.Substring(4).IndexOf(' '));
                    string value = input.Substring(input.LastIndexOf(' ') + 1);
                    dict[key] = value;
                }
                else if (input.StartsWith("print"))
                {
                    string output = input.Substring(6);
                    StringBuilder sb_output = new StringBuilder(output);
                    string[] keys = dict.Keys.ToArray();
                    foreach (string key in keys)
                    {
                        int start = 0;
                        List<int> pos_list = new List<int>();
                        while (true)
                        {
                            int index = output.Substring(start).IndexOf("{" + key + "}");
                            if (index > -1)
                            {
                                start += index + ("{" + key + "}").Length;
                                pos_list.Add(start);
                            }
                            else
                            {
                                break;
                            }
                        }
                        foreach (int pos in pos_list)
                        {
                            int uTempCode = Convert.ToUInt16(dict[key][dict[key].Length - 1]) - 0xAC00;
                            int fp = uTempCode / (21 * 28);
                            uTempCode = uTempCode % (21 * 28);
                            int mp = uTempCode / 28;
                            int nUniCode = uTempCode % 28;
                            int lp = nUniCode;
                            if (output[pos] == '은' || output[pos] == '는')
                            {
                                sb_output[pos] = (lp == 0) ? '는' : '은';
                            }
                            if (output[pos] == '이' || output[pos] == '가')
                            {
                                sb_output[pos] = (lp == 0) ? '가' : '이';
                            }
                            if (output[pos] == '을' || output[pos] == '를')
                            {
                                sb_output[pos] = (lp == 0) ? '를' : '을';
                            }
                            output = sb_output.ToString();
                        }
                        output = output.Replace("{" + key + "}", dict[key]);
                        sb_output = new StringBuilder(output);
                    }
                    Console.WriteLine(output);
                }
            }
        }
    }
}
