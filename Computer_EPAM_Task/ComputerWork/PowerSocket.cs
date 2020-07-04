namespace Computer_EPAM_Task.ComputerWork
{
    internal static class PowerSocket
    {
        // Состояние электричества
        private static bool State { get; set; }

        static PowerSocket() { }

        public static bool IsElectricity => State;

        internal static void SetElectricityState(bool state) => State = state;
    }
}