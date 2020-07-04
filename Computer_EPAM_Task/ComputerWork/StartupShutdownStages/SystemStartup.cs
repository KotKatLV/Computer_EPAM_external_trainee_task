using Computer_EPAM_Task.Enums;
using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.StartupShutdownStages
{
    // Включение компьютера, POST, BootMonitor
    internal class SystemStartup : IPCState
    {
        // Начальный этап загрузки операционной системы после включения компьютера начинается в BIOS. 
        internal BootDevice BootDevice { get; private set; } = BootDevice.HDD;

        // Кнопка питания активирует питание ПК, отправляя питание на материнскую плату и другие компоненты.
        private bool SendingPowerToMotherboardAndComponents() => PowerSocket.IsElectricity;

        // В настройках BIOS мы указываем загрузочное устройство, или ряд загрузочных устройств в порядке их приоритета
        // По умолчанию я установил HDD
        // Также проверяется, что было распределено питание по всем аппартным частям ПК
        public bool TurnOn() => SendingPowerToMotherboardAndComponents();

        public bool TurnOff() => true;
    }
}