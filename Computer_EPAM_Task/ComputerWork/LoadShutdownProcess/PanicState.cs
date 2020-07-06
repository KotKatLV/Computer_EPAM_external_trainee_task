using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.LoadShutdownProcess
{
    // На случай критической ошибки ядра операционной системы
    class PanicState : IComputerState
    {
        public IComputerState load()
        {
            System.Threading.Thread.Sleep(500);
            throw new System.Exception("Критическая ошибка операционной системы.");
        }
    }
}