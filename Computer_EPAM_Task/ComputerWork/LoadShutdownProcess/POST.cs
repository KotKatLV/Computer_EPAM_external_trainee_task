using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.LoadShutdownProcess
{
    class POST : IComputerState
    {
        public IComputerState load()
        {
            if (PowerSocket.IsElectricity)
            {
                // Начало процедуры POST
                return new LoadMBR();
            }
            else return null;
        }
    }
}