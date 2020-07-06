using Computer_EPAM_Task.ComputerWork;
using Computer_EPAM_Task.ComputerWork.LoadShutdownProcess;
using Computer_EPAM_Task.Interfaces;
using System;
using System.Diagnostics;
using System.Management;

namespace Computer_EPAM_Task.Computer
{
    internal class Computer : IComputer, IComputerState
    {
        private ManagementObjectSearcher _searcher;
        private readonly ISoundPlayer _soundPlayer = new MySoundPlayer();
        private IComputerState state;

        public Computer() { }

        public Computer(ManagementObjectSearcher searcher, ISoundPlayer soundPlayer)
        {
            _searcher = searcher ?? throw new ArgumentNullException(nameof(searcher));
            _soundPlayer = soundPlayer ?? throw new ArgumentNullException(nameof(soundPlayer));
        }

        public (string, string) GetInfoAboutCPU()
        {
            using (_searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name, NumberOfCores FROM Win32_Processor"))
            {
                foreach (ManagementObject item in _searcher.Get())
                {
                    return (item["Name"].ToString(), item["NumberOfCores"].ToString());
                }
            }

            throw new Exception("При считывании данных о центральном процессоре произошла ошибка");
        }

        public (string, string, string, string) GetInfoAboutGPU()
        {
            byte tmp = 0;
            string tmp1 = null;
            string tmp2 = null;

            using (_searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, VideoProcessor FROM Win32_VideoController"))
            {
                foreach (ManagementObject item in _searcher.Get())
                {
                    switch (tmp++)
                    {
                        case 0:
                            tmp1 = item["Caption"].ToString();
                            tmp2 = item["VideoProcessor"].ToString();
                            break;
                        default:
                            return (tmp1, tmp2, item["Caption"].ToString(), item["VideoProcessor"].ToString());
                    }
                }

                throw new Exception("При считывании данных о видеокарте произошла ошибка");
            }
        }

        public (string, string) GetInfoAboutOS()
        {
            using (_searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, Version FROM Win32_OperatingSystem"))
            {
                foreach (ManagementObject item in _searcher.Get())
                {
                    return (item["Caption"].ToString(), item["Version"].ToString());
                }
            }

            throw new Exception("При считывании данных о операционной системе произошла ошибка");
        }

        public (string, string) GetInfoAboutRAM()
        {
            double capacity = 0;
            double freq = 0;

            using (_searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Capacity, Speed FROM Win32_PhysicalMemory"))
            {
                foreach (ManagementObject item in _searcher.Get())
                {
                    capacity += Math.Round(Convert.ToDouble(item.Properties["Capacity"].Value) / 1024 / 1024 / 1024, 2);
                    if (freq < Convert.ToDouble(item["Speed"].ToString().Substring(0, 4)))
                    {
                        freq = Convert.ToDouble(item["Speed"].ToString().Substring(0, 4));
                    }
                }

                return (capacity.ToString(), freq.ToString());
            }

            throw new Exception("При считывании данных о оперативной памяти произошла ошибка");
        }

        public void PlayLoadSound() => _soundPlayer.PlayLoadSound();

        public void PlayShutDownSound() => _soundPlayer.PlayShutDownSound();

        public void RunProgram(string progName) => Process.Start(progName);

        public bool StartPC()
        {
            // Включаем элекртичество
            PowerSocket.SetElectricityState(true);

            // Начинаем процесс включения ПК 
            return load() is Kernel;
        }

        public bool ShutdownPC()
        {
            // Выключаем электричество
            PowerSocket.SetElectricityState(false);
            return true;
        }

        public IComputerState load()
        {
            state = new POST();

            while (true)
            {
                state = state.load();
                System.Threading.Thread.Sleep(200);

                if (state is Kernel)
                {
                    state = state.load();
                    return state is Kernel ? state : state = state.load();
                }
            }
        }
    }
}