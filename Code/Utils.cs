using System;
using System.Globalization;

namespace MegaDownload.Code
{
    public static class Utils
    {
        public static void ConsoleClearLine()
        {
            var cursorTop = Console.CursorTop;
            Console.SetCursorPosition(0, cursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, cursorTop);
        }

        public static string GetHumanSize(long value, string suffix = null)
        {
            string scale;
            double dvalue;

            long abs = (value < 0 ? -value : value);
            if (abs < 0x400)
            {
                scale = " B";
                dvalue = value;
            }
            else if (abs < 0x100000)
            {
                scale = " KB";
                dvalue = value / 1024d;
            }
            else if (abs < 0x40000000)
            {
                scale = " MB";
                dvalue = (value >> 10) / 1024d;
            }
            else if (abs < 0x10000000000)
            {
                scale = " GB";
                dvalue = (value >> 20) / 1024d;
            }
            else if (abs < 0x4000000000000)
            {
                scale = " TB";
                dvalue = (value >> 30) / 1024d;
            }
            else if (abs < 0x1000000000000000)
            {
                scale = " PB";
                dvalue = (value >> 40) / 1024d;
            }
            else
            {
                scale = " EB";
                dvalue = (value >> 50) / 1024d;
            }
            
            return string.Concat(dvalue.ToString("0.00", CultureInfo.InvariantCulture), scale, suffix);
        }

        public static string GetHumanProgress(double value)
        {            
            return string.Concat(value.ToString("0.00", CultureInfo.InvariantCulture), "%");
        }
    }
}
