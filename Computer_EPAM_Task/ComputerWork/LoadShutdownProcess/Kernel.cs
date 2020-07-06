using Computer_EPAM_Task.Interfaces;
using System;

namespace Computer_EPAM_Task.ComputerWork.LoadShutdownProcess
{
    class Kernel : IComputerState
    {
        private static Random random;

        public IComputerState load()
        {
            random = new Random();

            // Даем волю псевдослучайному генератору определить возможность запуска ПК
            switch (random.Next(1, 3))
            {
                case 1:
                    // Запуск ядра ОС
                    return new Kernel();
                default:
                    // Критическая ошибка ОС
                    return new PanicState();
            }
        }
    }
}