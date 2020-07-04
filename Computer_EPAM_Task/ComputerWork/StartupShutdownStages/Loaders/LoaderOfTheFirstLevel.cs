using Computer_EPAM_Task.Interfaces;

namespace Computer_EPAM_Task.ComputerWork.StartupShutdownStages.Loaders
{
    // Загрузчик 1-го уровня. Master Boot Record
    internal class LoaderOfTheFirstLevel : IPCState
    {
        public bool TurnOff() => true;

        // Если был успешно пройден этап самотестирования это означает, что процессы первого этапа заввершены успешно и мы переходим дальше
        public bool TurnOn() => POST();

        // Самотестирование: проверяем аппаратные сбои, один сигнал после POST сигнализирует, что все в порядке.
        private bool POST() => true;
    }
}