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
            if (System.Windows.MessageBox.Show("确认退出？", "提示", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
            {
                Application.Current.Shutdown(-1);
            }
        }
    }
}
