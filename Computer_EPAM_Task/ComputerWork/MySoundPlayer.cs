using Computer_EPAM_Task.Interfaces;
using System;
using System.IO;
using System.Media;
using System.Windows;

namespace Computer_EPAM_Task.Computer
{
    internal class MySoundPlayer : ISoundPlayer
    {
        private SoundPlayer soundPlayer;

        public void PlayLoadSound()
        {
            try
            {
                using (soundPlayer = new SoundPlayer())
                {
                    soundPlayer.Stream = Properties.Resources.loadMus;
                    soundPlayer.Play();
                }
            }
            catch (TimeoutException te)
            {
                MessageBox.Show(te.Message);
            }
            catch (FileNotFoundException fnfe)
            {
                MessageBox.Show(fnfe.Message);
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void PlayShutDownSound()
        {
            try
            {
                using (soundPlayer = new SoundPlayer())
                {
                    soundPlayer.Stream = Properties.Resources.shutDownMus;
                    soundPlayer.Play();
                }
            }
            catch (TimeoutException te)
            {
                MessageBox.Show(te.Message);
            }
            catch (FileNotFoundException fnfe)
            {
                MessageBox.Show(fnfe.Message);
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}