using Computer_EPAM_Task.DataRepositories.DBRepositories.EF_Core.Repositories;
using Computer_EPAM_Task.Enums;
using Computer_EPAM_Task.Interfaces;
using Computer_EPAM_Task.Models;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Computer_EPAM_Task
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const Visibility _hidden = Visibility.Hidden;
        private const Visibility _visible = Visibility.Visible;
        private const Visibility _collapsed = Visibility.Collapsed;
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private readonly IRepository<Video> _videos = new VideoRepository();

        public MainWindow() => InitializeComponent();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _dispatcherTimer.Tick += DispatcherTimer_Tick;
                _dispatcherTimer.Interval = new TimeSpan(0, 0, 8);
            }
            catch(ArgumentOutOfRangeException aoofre)
            {
                MessageBox.Show(aoofre.Message);
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (winLoad.Visibility == _visible)
            {
                EventsAfterLoad();
                MessageBox.Show("Операционная система запущена и готова к использованию", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            winShuttigDown.Visibility = _hidden;
            _dispatcherTimer.Stop();
        }

        /// <summary>
        /// Действия после загрузки ОС
        /// </summary>
        private void EventsAfterLoad()
        {
            PlayLoadSound();
            MakeMainOptionsEnabled();
            winShuttigDown.Visibility = _hidden;
            winLoad.Visibility = _hidden;
            _dispatcherTimer.Stop();       
        }

        /// <summary>
        /// Проигрвание музыки загрузки ОС
        /// </summary>
        private void PlayLoadSound()
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
            catch(FileNotFoundException fnfe)
            {
                MessageBox.Show(fnfe.Message);
            }
            catch(InvalidOperationException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Проигрвание музыки выключения ОС
        /// </summary>
        private void PlayShutDownSound()
        {
            try
            {
                using (SoundPlayer sp = new SoundPlayer())
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

        /// <summary>
        ///  Установка допустимости взаимодействия для основных кнопок
        /// </summary>
        private void MakeMainOptionsEnabled()
        {
            runProgram.IsEnabled = true;
            powerOffMenu.IsEnabled = true;
            addInfo.IsEnabled = true;
            infoAboutPC.IsEnabled = true;
        }

        /// <summary>
        /// Действия после запуска ПК
        /// </summary>
        private void EventsAfterRunnigPC() 
        {
            _dispatcherTimer.Start();
            powerOnBtn.Visibility = _hidden;
            winLoad.Visibility = _visible;
        }

        /// <summary>
        /// Запуск ПК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EventsAfterRunnigPC();
                grid.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Img/Others/windows10.jpeg")));
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Выключение ПК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PowerOffBtn_Click(object sender, RoutedEventArgs e)
        {
            EventsAfterTurningOffPC();
            powerOffMenu.IsEnabled = false;
            runProgram.IsEnabled = false;
            addInfo.IsEnabled = false;
            infoAboutPC.IsEnabled = false;

        }

        /// <summary>
        /// Действия после выключения ПК
        /// </summary>
        private void EventsAfterTurningOffPC()
        {
            _dispatcherTimer.Start();
            PlayShutDownSound();

            grid.Background = new SolidColorBrush(Color.FromArgb(100, 205, 130, 130));

            GPUDockPanel.Visibility = _hidden;
            webBrowser.Visibility = _hidden;
            OSDockPanel.Visibility = _hidden;
            CPUDockPanel.Visibility = _hidden;
            RAMDockPanel.Visibility = _hidden;

            grid.Visibility = _visible;
            winShuttigDown.Visibility = _visible;
            powerOnBtn.Visibility = _visible;
        }

        /// <summary>
        /// Запуск программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunProgram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Выберите программу | *.exe"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    Process.Start(openFileDialog.FileName);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Показ видео в webBrowser
        /// </summary>
        /// <param name="url"></param>
        private void ShowFrameInBrowser(string url)
        {
            try
            {
                webBrowser.NavigateToString(CreateHTMLForWebBrowser(url));
                webBrowser.Visibility = _visible;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Создание разметки для браузера
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string CreateHTMLForWebBrowser(string url)
        {
            string html = "<html><head>";
            html += "<meta content='IE=Edge' http-equiv='X-UA-Compatible'/>";
            html += $"<iframe id='video' src= '{url}' width='716' height='320' frameborder='0' allowfullscreen></iframe>";
            html += "</body></html>";
            return html;
        }

        /// <summary>
        /// Устройство компьютера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowFrameInBrowser((await _videos.TryGetAsync((int)VideoNames.OSWork)).URL);
                ActionsToMakeVisibleBrowser();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Действия для отображения браузера
        /// </summary>
        private void ActionsToMakeVisibleBrowser()
        {
            OSDockPanel.Visibility = _hidden;
            GPUDockPanel.Visibility = _hidden;
            OSDockPanel.Visibility = _hidden;
            CPUDockPanel.Visibility = _hidden;
            RAMDockPanel.Visibility = _hidden;
            grid.Visibility = _visible;
            webBrowser.Visibility = _visible;
        }

        /// <summary>
        /// Работа процессора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowFrameInBrowser((await _videos.TryGetAsync((int)VideoNames.CPUWork)).URL);
                ActionsToMakeVisibleBrowser();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Работа видеокарты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowFrameInBrowser((await _videos.TryGetAsync((int)VideoNames.GPUWork)).URL);
                ActionsToMakeVisibleBrowser();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Работа ОЗУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowFrameInBrowser((await _videos.TryGetAsync((int)VideoNames.RAMWork)).URL);
                ActionsToMakeVisibleBrowser();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Работа ОС
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowFrameInBrowser((await _videos.TryGetAsync((int)VideoNames.OSWork)).URL);
                ActionsToMakeVisibleBrowser();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Сведения о ОС
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoAboutOS_Click(object sender, RoutedEventArgs e)
        {
            grid.Visibility = _collapsed;
            CPUDockPanel.Visibility = _hidden;
            GPUDockPanel.Visibility = _hidden;
            RAMDockPanel.Visibility = _hidden;
            OSDockPanel.Visibility = _visible;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, Version FROM Win32_OperatingSystem"))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    nameOS.Text = item["Caption"].ToString();
                    versionOS.Text = item["Version"].ToString();
                    break;
                }
            }
        }

        /// <summary>
        /// Сведения о ЦП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoAboutCPU_Click(object sender, RoutedEventArgs e)
        {
            grid.Visibility = _collapsed;
            OSDockPanel.Visibility = _collapsed;
            GPUDockPanel.Visibility = _hidden;
            RAMDockPanel.Visibility = _hidden;
            CPUDockPanel.Visibility = _visible;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name, NumberOfCores FROM Win32_Processor"))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    nameCPU.Text = item["Name"].ToString();
                    numOfCPUCores.Text = item["NumberOfCores"].ToString();
                }
            }
        }

        /// <summary>
        /// Сведения о видеокарте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoAboutGraphicsCard_Click(object sender, RoutedEventArgs e)
        {
            grid.Visibility = _collapsed;
            OSDockPanel.Visibility = _collapsed;
            CPUDockPanel.Visibility = _collapsed;
            RAMDockPanel.Visibility = _collapsed;
            GPUDockPanel.Visibility = _visible;

            byte tmp = 0;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption, VideoProcessor FROM Win32_VideoController"))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    switch (tmp++)
                    {
                        case 0:
                            nameIntegratedGPU.Text = item["Caption"].ToString();
                            integratedGPUType.Text = item["VideoProcessor"].ToString();
                            break;
                        default:
                            nameEmbeddedGPU.Text = item["Caption"].ToString();
                            embeddedGPUType.Text = item["VideoProcessor"].ToString();
                            break;
                    }

                }
                tmp = 0;
            }
        }

        /// <summary>
        /// Сведения о ОЗУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoAboutRAM_Click(object sender, RoutedEventArgs e)
        {
            grid.Visibility = _collapsed;
            OSDockPanel.Visibility = _collapsed;
            CPUDockPanel.Visibility = _collapsed;
            GPUDockPanel.Visibility = _collapsed;
            RAMDockPanel.Visibility = _visible;

            double capacity = 0;
            double freq = 0;

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Capacity, Speed FROM Win32_PhysicalMemory"))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    capacity += Math.Round(Convert.ToDouble(item.Properties["Capacity"].Value) / 1024 / 1024 / 1024, 2);
                    if (freq < Convert.ToDouble(item["Speed"].ToString().Substring(0, 4)))
                    {
                        freq = Convert.ToDouble(item["Speed"].ToString().Substring(0, 4));
                    }
                }
                RAMCapacity.Text = capacity.ToString();
                maxFreq.Text = freq.ToString();
            }
        }
    }
}