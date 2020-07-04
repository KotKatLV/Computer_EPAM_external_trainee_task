namespace Computer_EPAM_Task.Interfaces
{
    internal interface IComputer
    {
        // Сведения о ОС
        (string, string) GetInfoAboutOS();

        // Сведения о центральном процессоре
        (string, string) GetInfoAboutCPU();

        // Сведения о видеокарте
        (string, string, string, string) GetInfoAboutGPU();

        // Сведения о ОЗУ
        (string, string) GetInfoAboutRAM();

        // Запуск программы
        void RunProgram(string progName);

        // Проигрвание музыки включения ОС
        void PlayLoadSound();

        // Проигрвание музыки выключения ОС
        void PlayShutDownSound();
    }
}