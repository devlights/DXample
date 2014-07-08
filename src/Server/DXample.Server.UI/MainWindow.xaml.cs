using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;

namespace DXample.Server.UI
{
    public partial class MainWindow : DXWindow
    {
        internal ServiceHost _host;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModelSource.Create(() => new MainViewModel());
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