using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.StartupShutdownStages.Loaders
{
    // Начальный этап загрузки операционной системы. Менеджер загрузки ОС
    internal class LoaderOfTheThirdLevel : IPCState
    {
        // BIOS подтверждает наличие загрузочного загрузчика или загрузчика в этом первом секторе загрузочного диска и загружает этот загрузчик RAM
        internal bool IsBootLoaderActive { get; private set; } = true;

        internal LoaderOfTheThirdLevel() { }

        public bool TurnOff() => true;

        public bool TurnOn() => LoadingBootloaderIntoRAM();

        // Загрузка загрузчика в память
        private bool LoadingBootloaderIntoRAM() => IsBootLoaderActive;
    }
}