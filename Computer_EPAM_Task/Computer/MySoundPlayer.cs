using Computer_EPAM_Task.Interfaces;
using System;
using System.IO;
using System.Media;
using System.Windows;

namespace Computer_EPAM_Task.Computer
{
    internal class MySoundPlayer : ISoundPlayer
    {
        private SoundPlayer sp;

        public void PlayLoadSound()
        {
            try
            {
                using (SoundPlayer sp = new SoundPlayer())
                {
                    sp.Stream = Properties.Resources.loadMus;
                    sp.Play();
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
                using (sp = new SoundPlayer())
                {
                    sp.Stream = Properties.Resources.shutDownMus;
                    sp.Play();
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