namespace Computer_EPAM_Task.Interfaces
{
    internal interface IComputer
    {
        bool StartPC();

        bool ShutdownPC();

        (string, string) GetInfoAboutOS();

        (string, string) GetInfoAboutCPU();

        (string, string, string, string) GetInfoAboutGPU();

        (string, string) GetInfoAboutRAM();

        void RunProgram(string progName);

        void PlayLoadSound();

        void PlayShutDownSound();
    }
}