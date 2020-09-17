using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace FileStealerUSB2PC
{
    class alaki
    {
        public static string destLog = @"C:\USB Files\Log.txt";
        public void sth()
        {

            TimeSpan interval = new TimeSpan(0, 0, 2);
            Thread.Sleep(interval);
            //start.Year + "/" + start.Month + "/" + start.Day


            DateTime start = DateTime.Today;


            Console.WriteLine(string.Format("Start: {0}:{1}:{2}", start.Hour, start.Minute, start.Second));


            //DateTime end = DateTime.Now;
            //Console.WriteLine(string.Format("End: {0}:{1}:{2}", end.Hour, end.Minute, end.Second));

            //TimeSpan sth = end - start;
            Console.WriteLine(start.Year+"/"+start.Month+"/"+start.Day);

            //Console.WriteLine(string.Format("{0}:{1}", sth.TotalSeconds,sth.Seconds));

            //string filename = @"D:\MY Projects\C#\Templates\QR_Code_Generator_In_Csahrp.zip";

            //FileInfo fi = new FileInfo(filename);

            //Int64 size = (Int64)(fi.Length);

            //Console.WriteLine(SizeFixResult(size));


            //DateTime now = DateTime.Now;

            //string sec = "";

            //if ((now.Second.ToString()).Length == 1)
            //    sec = "0"+now.Second.ToString();
            //else
            //    sec = now.Second.ToString();

            //Console.WriteLine(string.Format("{0}:{1}:{2}",now.Hour,now.Minute,now.Second));

        }

    }
}