using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.StartupShutdownStages.Loaders
{
    // Загрузчик 2-го уровня. Partition Boot Sector
    internal class LoaderOfTheSecondLevel : IPCState
    {
        // Передача управления исполняемому коду, записанному в PBS (Partition Boot Sector - загрузочный сектор активного раздела)
        internal bool IsPBSActive { get; private set; } = true;

        public LoaderOfTheSecondLevel() { }

        public bool TurnOff() => true;

        public bool TurnOn() => IsPBSActive;
    }
}