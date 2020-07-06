using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.LoadShutdownProcess
{
    class BootLoader : IComputerState
    {
        public IComputerState load()
        {
            // Загружаем Boot Loader
            // Изменяем модель памяти
            // длинный режим
            return new Kernel();
        }
    }
}