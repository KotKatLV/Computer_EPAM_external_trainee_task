namespace Computer_EPAM_Task.Interfaces
{
    interface IComputer
    {
        // Сведения о ОС
        (string, string) GetInfoAboutOS();

        // Сведения о центральном процессоре
        (string, string) GetInfoAboutCPU();

        // Сведения о видеокарте
        (string, string, string, string) GetInfoAboutGPU();

        // Сведения о ОЗУ
        (string, string) GetInfoAboutRAM();

        void RunProgram(string progName);
    }
}