using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.LoadShutdownProcess
{
    class LoadMBR : IComputerState
    {
        public IComputerState load()
        {
            // Поиск и загрузка загрузочного устройства
            return new BootLoader();
        }
    }
}