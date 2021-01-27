using Panuon.UI.Silver;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SoftwarePack
{
    /// <summary>
    /// Close.xaml 的交互逻辑
    /// </summary>
    public partial class Close : UserControl
    {
        public Close()
        {
            InitializeComponent();
        }

        private void image_min_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window win = Window.GetWindow(this);
            win.WindowState = WindowState.Minimized;
        }

        private void image_close_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Window win = Window.GetWindow(this);
            if (MessageBoxX.Show("确认要退出吗？", "退出", Application.Current.MainWindow, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown(-1);
            }
        }
    }
}
