using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}