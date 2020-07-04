namespace Computer_EPAM_Task.Interfaces
{
    internal interface ISoundPlayer
    {
        /// <summary>
        /// Проигрвание музыки включения ОС
        /// </summary>
        void PlayLoadSound();

        /// <summary>
        /// Проигрвание музыки выключения ОС
        /// </summary>
        void PlayShutDownSound();
    }
}