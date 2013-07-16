using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace GTLibrary.Utils
{
    public class SystemInfo
    {
        #region Variables

        private static SystemInfo instance;
        //private double availableMemory;

        private double cpuLoad;
        private ulong installedMemory;
        private double memLoad;
        private PerformanceCounter pcCPU;
        private PerformanceCounter pcMem;
        private Process process;

        #endregion

        private bool errorGettingCPULoad;
        private bool errorGettingMemLoad;

        private SystemInfo()
        {
        }

        public static SystemInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemInfo();
                }

                return instance;
            }
        }


        public static int CPUSpeed()
        {
            try
            {
                RegistryKey registrykeyHKLM = Registry.LocalMachine;
                string keyPath = @"HARDWARE\DESCRIPTION\System\CentralProcessor\0";
                RegistryKey registrykeyCPU = registrykeyHKLM.OpenSubKey(keyPath, false);
                string MHz = registrykeyCPU.GetValue("~MHz").ToString();
                var ProcessorNameString = (string) registrykeyCPU.GetValue("ProcessorNameString");
                registrykeyCPU.Close();
                registrykeyHKLM.Close();
                return (Convert.ToInt32(MHz));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string CPUName()
        {
            try
            {
                RegistryKey registrykeyHKLM = Registry.LocalMachine;
                string keyPath = @"HARDWARE\DESCRIPTION\System\CentralProcessor\0";
                RegistryKey registrykeyCPU = registrykeyHKLM.OpenSubKey(keyPath, false);

                if (registrykeyCPU != null)
                {
                    var processorNameString = (string) registrykeyCPU.GetValue("ProcessorNameString");
                    registrykeyCPU.Close();
                    registrykeyHKLM.Close();
                    return processorNameString;
                }
                else
                {
                    return "Unknown";
                }
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }

        public double GetCPULoad()
        {
            if (process == null)
                process = Process.GetCurrentProcess();

            if (pcCPU == null && errorGettingCPULoad == false)
            {
                try
                {
                    pcCPU = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                    pcCPU.NextValue();
                }
                catch (Exception ex)
                {
                    errorGettingCPULoad = true;
                    Console.Out.WriteLine("Error in SystemInfo.GetCPULoad(), message: " + ex.Message);
                }
            }

            if (pcCPU != null)
            {
                cpuLoad = pcCPU.NextValue();
                return Math.Round(cpuLoad/Environment.ProcessorCount, 0);
            }
            return 0;
        }

        public double GetMemLoad()
        {
            if (process == null)
                process = Process.GetCurrentProcess();

            if (pcMem == null && errorGettingMemLoad == false)
            {
                try
                {
                    pcMem = new PerformanceCounter("Memory", "Available MBytes");
                    pcMem.NextValue();
                }
                catch (Exception ex)
                {
                    errorGettingMemLoad = true;
                    Console.Out.WriteLine("Error in SystemInfo.GetMemLoad(), message: " + ex.Message);
                }
            }

            if (pcMem != null)
            {
                pcMem.NextValue();

                memLoad = process.PrivateMemorySize64/1024/1024; // Kb/Mb.
                return memLoad;
            }
            return 0;
        }

        public ulong GetTotalMemory()
        {
            if (installedMemory == 0)
            {
                var memStatus = new MEMORYSTATUSEX();
                if (GlobalMemoryStatusEx(memStatus))
                    installedMemory = memStatus.ullTotalPhys;
            }

            return installedMemory/1024/1024;
        }

        public string GetIPAddress()
        {
            try
            {
                string strHostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                return addr[addr.Length - 1].ToString();
            }
            catch (Exception)
            {
                return "127.0.0.1";
            }
        }

        #region Get physical memory size

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;

            public MEMORYSTATUSEX()
            {
                dwLength = (uint) Marshal.SizeOf(this);
            }
        }

        #endregion
    }
}