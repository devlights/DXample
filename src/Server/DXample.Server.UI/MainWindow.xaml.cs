using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

using DevExpress.Xpf.Core;

namespace DXample.Server.UI
{
    public partial class MainWindow : DXWindow
    {
        internal ServiceHost _host;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainViewModel.Create();
        }

        private async void DXWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            try
            {
                await StartHostAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
            }
        }

        private async void DXWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await StopHostAsync();
        }


        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            try
            {
                await StartHostAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
            }
        }

        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            try
            {
                await StopHostAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnStart.IsEnabled = false;
                btnStop.IsEnabled = true;
            }
        }

        protected internal async Task StartHostAsync()
        {
            await StopHostAsync();

            await Task.Run(() =>
            {
                _host = new ServiceHost(typeof(DataService));
                _host.Open();
            });
        }

        protected internal async Task StopHostAsync()
        {
            if (_host == null)
            {
                return;
            }

            if (_host.State == CommunicationState.Opened)
            {
                await Task.Run(() =>
                {
                    _host.Close();
                    _host = null;
                });
            }
        }
    }
}