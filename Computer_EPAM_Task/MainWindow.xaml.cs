using Computer_EPAM_Task.DataRepositories.DBRepositories.EF_Core.Repositories;
using Computer_EPAM_Task.Enums;
using Computer_EPAM_Task.Interfaces;
using Computer_EPAM_Task.Models;
using Microsoft.Win32;
using System;
using System.IO;
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
        private readonly IRepository<Video> _videos = new VideoRepository();
        private readonly IComputer _computer = new Computer.Computer();
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

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
                _computer.PlayLoadSound();
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
                _computer.PlayShutDownSound();
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
                if (_computer.StartPC())
                {
                    EventsAfterRunnigPC();
                    grid.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Img/Others/windows10.jpeg")));
                }
                else
                    throw new Exception("Ошибка при запуске ПК");
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
            _computer.ShutdownPC();
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
                    _computer.RunProgram(openFileDialog.FileName);
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
            try
            {
                grid.Visibility = _collapsed;
                CPUDockPanel.Visibility = _hidden;
                GPUDockPanel.Visibility = _hidden;
                RAMDockPanel.Visibility = _hidden;
                OSDockPanel.Visibility = _visible;
                nameOS.Text = _computer.GetInfoAboutOS().Item1;
                versionOS.Text = _computer.GetInfoAboutOS().Item2;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Сведения о ЦП
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoAboutCPU_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                grid.Visibility = _collapsed;
                OSDockPanel.Visibility = _collapsed;
                GPUDockPanel.Visibility = _hidden;
                RAMDockPanel.Visibility = _hidden;
                CPUDockPanel.Visibility = _visible;

                nameCPU.Text = _computer.GetInfoAboutCPU().Item1;
                numOfCPUCores.Text = _computer.GetInfoAboutCPU().Item2;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Сведения о видеокарте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoAboutGraphicsCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                grid.Visibility = _collapsed;
                OSDockPanel.Visibility = _collapsed;
                CPUDockPanel.Visibility = _collapsed;
                RAMDockPanel.Visibility = _collapsed;
                GPUDockPanel.Visibility = _visible;

                nameIntegratedGPU.Text = _computer.GetInfoAboutGPU().Item1;
                integratedGPUType.Text = _computer.GetInfoAboutGPU().Item2;
                nameEmbeddedGPU.Text = _computer.GetInfoAboutGPU().Item3;
                embeddedGPUType.Text = _computer.GetInfoAboutGPU().Item4;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Сведения о ОЗУ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoAboutRAM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                grid.Visibility = _collapsed;
                OSDockPanel.Visibility = _collapsed;
                CPUDockPanel.Visibility = _collapsed;
                GPUDockPanel.Visibility = _collapsed;
                RAMDockPanel.Visibility = _visible;

                RAMCapacity.Text = _computer.GetInfoAboutRAM().Item1;
                maxFreq.Text = _computer.GetInfoAboutRAM().Item2;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}