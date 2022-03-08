using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInfoLib.Windows;
using SystemInfoLib.Windows.HardDisk;
using SystemInfoLib.Windows.Processor;
using SystemInfoLib.Windows.Ram;

namespace SearchModels.Models
{
    public class DeviceInfo
    {
        public string MachineName { get; set; }
        public string ProcName { get; set; }
        public string ProcSpeed { get; set; }
        public int ProcCores { get; set; }
        public string Memory { get; set; }
        public string FreeSpace { get; set; }
        public string OSName { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Machine Name : {MachineName}\n");
            sb.Append($"Processor Name : {ProcName}\n");
            sb.Append($"Processor Speed : {ProcSpeed}\n");
            sb.Append($"Processor Cores : {ProcCores}\n");
            sb.Append($"RAM : {Memory}\n");
            sb.Append($"HDD Freespace : {FreeSpace}\n");
            sb.Append($"OS : {OSName}\n");

            return sb.ToString();
        }
        public static DeviceInfo GetDeviceInfo()
        {
            var newInfo = new DeviceInfo();
            newInfo.OSName = Environment.OSVersion.ToString();
            newInfo.MachineName = Environment.MachineName;
            // Processor:
            //Console.WriteLine("Processor Related:" + Environment.NewLine);
            newInfo.ProcName = Processor.Name;
            //Console.WriteLine("    Processor Identifier: " + Processor.Identifier + Environment.NewLine);
            //Console.WriteLine("    Processor Level: " + Processor.ProcessorLevel + Environment.NewLine);
            //Console.WriteLine("    Processor Revision: " + Processor.ProcessorRevision + Environment.NewLine);
            //Console.WriteLine("    Processor Architecture: " + Processor.ProcessorArchitecture + Environment.NewLine);
            newInfo.ProcCores = Processor.NumberOfLogicalCores;
            newInfo.ProcSpeed = Processor.ClockSpeed + " MHz";
            //Console.WriteLine("    Vendor Identifier: " + Processor.VendorIdentifier + Environment.NewLine + Environment.NewLine);

            // BIOS:
            //Console.WriteLine("BIOS Related:" + Environment.NewLine);
            //Console.WriteLine("    Motherboard Manufacturer: " + Bios.MotherboardManufacturer + Environment.NewLine);
            //Console.WriteLine("    Motherboard Model: " + Bios.MotherboardModel + Environment.NewLine);
            //Console.WriteLine("    Motherboard Version: " + Bios.MotherboardVersion + Environment.NewLine);
            //Console.WriteLine("    BIOS Release Date: " + Bios.BiosReleaseDate + Environment.NewLine);
            //Console.WriteLine("    BIOS Vendor: " + Bios.BiosVendor + Environment.NewLine);
            //Console.WriteLine("    BIOS Version: " + Bios.BiosVersion + Environment.NewLine + Environment.NewLine);

            // System:
            //Console.WriteLine("System Related:" + Environment.NewLine);
            //Console.WriteLine("    64-Bit Operating System: " + OS.Is64BitOperatingSystem + Environment.NewLine);
            //Console.WriteLine("    Windows Edition: " + OS.WindowsEdition + Environment.NewLine);
            //Console.WriteLine("    Windows Product Key: " + OS.ProductKey + Environment.NewLine);
            //Console.WriteLine("    Current Build Number: " + OS.CurrentBuild + Environment.NewLine);
            ////Console.WriteLine("    CSD Version: " + OS.CSDVersion + Environment.NewLine);
            //Console.WriteLine("    System Directory: " + OS.SystemFolder + Environment.NewLine + Environment.NewLine);

            // RAM
            //Console.WriteLine("RAM Related:" + Environment.NewLine);
            //Console.WriteLine("    Total Physical Memory (in bytes): " + Ram.TotalPhysicalMemory + Environment.NewLine);
            newInfo.Memory = Ram.TotalPhysicalMemory.ToMegaBytes().ToString("n2") + " Mb";

            // Harddisk
            //Console.WriteLine("HardDisk Related:" + Environment.NewLine);
            newInfo.FreeSpace = HardDisk.GetFreeSpace(@"C:\", DiskSpaceFlags.TotalNumberOfFreeBytes).ToString("n2") + " bytes";
            return newInfo;
        }
    }
}
