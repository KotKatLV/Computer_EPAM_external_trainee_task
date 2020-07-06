using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.StartupShutdownStages.Loaders
{
    // Загрузка ядра операционной системы
    internal class BootLoaderOSKernel : IPCState
    {
        // Когда загрузчик находится в памяти, BIOS передает свою работу загрузчику, который, в свою очередь, начинает загрузку операционной системы в память.
        internal bool IsReadyToLoadOSIntoRAM { get; private set; } = true;

        public BootLoaderOSKernel() { }

        private bool LoadOSKernel() => IsReadyToLoadOSIntoRAM;

        public bool TurnOn() => LoadOSKernel();

        public bool TurnOff() => IsReadyToLoadOSIntoRAM = true;
    }
}