using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace FileStealerUSB2PC
{

    class Program
    {
        public static string dest = @"C:\USB Files";
        public static string destLog = @"C:\USB Files\Log.txt";
        public static int FilesNum = 0;
        public static int DirsNum = 0;
        public static Int64 FileSizes = 0;

        static void Main(string[] args)
        {
            //alaki Alaki = new alaki();
            //Alaki.sth();

            Start();

            Console.ReadLine();
        }

        static void Start()
        {
            if (Directory.GetDirectories(@"C:\").Contains("USB Files") == false)
                Directory.CreateDirectory(dest);


            List<string> USBdrivesLoc = usbDriveLoc();


            if (USBdrivesLoc.Count() == 0)
            {
                Console.WriteLine("No USB Connected!!!");
                Console.ReadKey();
                Environment.Exit(0);
            }




            foreach (string DriveLoc in USBdrivesLoc)
            {
                //time
                DateTime start = DateTime.Now;
                Console.WriteLine(string.Format("Start: {0}:{1}:{2}", start.Hour, start.Minute, start.Second));

                string DriveLabel = usbDriveLabel(DriveLoc);

                Console.WriteLine(string.Format("Working... | \"{0} ({1})\"" , DriveLabel, DriveLoc.Replace("\\", "")));
                Copy(DriveLoc);
                Console.WriteLine(string.Format("Done!      | \"{0} ({1})\"", DriveLabel, DriveLoc.Replace("\\", "")));

                DateTime end = DateTime.Now;
                Console.WriteLine(string.Format("end  : {0}:{1}:{2}\n", end.Hour, end.Minute, end.Second));

                TimeSpan Time = end - start;

                Result(DriveLoc, DriveLabel, Time);
            }

            Console.WriteLine("All done.");

        }

        public static List<string> usbDriveLoc()
        {
            List<string> driveLocs = new List<string>();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                    driveLocs.Add(drive.Name);
            }

            return driveLocs;
        }

        public static string usbDriveLabel(string DriveLoc)
        {
            string driveLabel = "";
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.Name == DriveLoc)
                {
                    driveLabel = drive.VolumeLabel;
                }
            }
            return driveLabel;
        }


        public static void Copy(string USBDrive)
        {
            string SourcePath = USBDrive;
            string Destinationpath = dest + "\\" + usbDriveLabel(USBDrive) + "\\";

            foreach (string dirpath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirpath.Replace(SourcePath, Destinationpath));
                DirsNum++;
            }
            foreach (string newpath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newpath, newpath.Replace(SourcePath, Destinationpath), true);
                FilesNum++;
                FileInfo fi = new FileInfo(newpath);
                FileSizes += fi.Length;
            }
        }

        public static string SizeFixResult(Int64 size)
        {
            string sizeFile = "";
            if ((size / 1024 / 1024 / 1024) > 0)
            {
                sizeFile = (size / 1024 / 1024 / 1024).ToString() + " GB";
            }
            else if ((size / 1024 / 1024) > 0)
            {
                sizeFile = (size / 1024 / 1024).ToString() + " MB";
            }
            else if ((size / 1024) > 0)
            {
                sizeFile = (size / 1024).ToString() + " KB";
            }
            else
            {
                sizeFile = size.ToString() + " B";
            }
            return sizeFile;
        }

        public static void Result(string DriveLoc, string DriveLabel, TimeSpan Time)
        {
            string files; string folders;

            if (File.Exists(destLog) == false)
            {
                File.WriteAllText(destLog, "USB Drives Results:\n\n");
            }

            if (FilesNum == 1 || FilesNum == 0)
            {
                files = "File";
            }
            else
            {
                files = "Files";
            }

            if (DirsNum == 1 || DirsNum == 0)
            {
                folders = "Folder";
            }
            else
            {
                folders = "Folder";
            }

            DateTime start = DateTime.Today;

            string to_write = string.Format("{0} ({1}) >>> ({2}) {3}, ({4}) {5}, ({6}) Total Size, (+{7}) seconds, ({8}/{9}/{10}) Date\n", DriveLabel, DriveLoc, FilesNum, files, DirsNum, folders, SizeFixResult(FileSizes),Time.TotalSeconds,start.Year,start.Month,start.Day);
            File.AppendAllText(destLog, to_write);
        }
    }
}