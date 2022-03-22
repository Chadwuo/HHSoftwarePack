using Panuon.UI.Silver;
using Panuon.UI.Silver.Core;
using SevenZip;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SoftwarePack
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 根目录
        /// </summary>
        private string rootDirPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 版本信息
        /// </summary>
        private string verStr = "© 2021 Micahh MIT license";

        /// <summary>
        /// 是否压缩完成
        /// </summary>
        private bool IsCompleteCompressor;

        /// <summary>
        /// MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Environment.Is64BitProcess)
            {
                SevenZipBase.SetLibraryPath(rootDirPath + @"\x64\7z.dll");
            }
            else
            {
                SevenZipBase.SetLibraryPath(rootDirPath + @"\x86\7z.dll");
            }

            // 是否存在打包核心库
            if (Directory.Exists(Path.Combine(rootDirPath, "SoftSetupCore")))
            {
                // skin资源图片
                LoadImgageResources();
            }
            else
            {
                if (File.Exists(Path.Combine(rootDirPath, "SoftSetupCore.7z")))
                {
                    SevenZipExtractor zipExtractor = new SevenZipExtractor(Path.Combine(rootDirPath, "SoftSetupCore.7z"));
                    zipExtractor.BeginExtractArchive(Path.Combine(rootDirPath));

                    // 解压完成  
                    zipExtractor.ExtractionFinished += (obj, eventArgs) =>
                    {
                        LoadImgageResources();
                    };
                }
            }

            lbl_Ver.Content = verStr;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Begin dragging the window
            this.DragMove();
        }

        /// <summary>
        /// 按钮动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void img_form_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string filterStr = "图片文件(*.png)|*.png";
            string TargetPath = string.Empty;

            Image img = sender as Image;
            switch (img.Name)
            {
                case "imgSoftIcon":
                    filterStr = "图标文件(*.ico)|*.ico";
                    TargetPath = SkinPath.SoftIcon;
                    break;
                case "imgSoftUninstIcon":
                    filterStr = "图标文件(*.ico)|*.ico";
                    TargetPath = SkinPath.SoftUninstIcon;
                    break;
                case "img_TopLogo":
                    TargetPath = SkinPath.TopLogo;
                    break;
                case "img_Carousel_1":
                    TargetPath = SkinPath.Carousel_1;
                    break;
                case "img_Carousel_2":
                    TargetPath = SkinPath.Carousel_2;
                    break;
                case "img_Carousel_3":
                    TargetPath = SkinPath.Carousel_3;
                    break;
                case "img_Uninstall_1":
                    TargetPath = SkinPath.Uninstall_1;
                    break;
                case "img_Uninstall_2":
                    TargetPath = SkinPath.Uninstall_2;
                    break;
                case "img_Uninstall_3":
                    TargetPath = SkinPath.Uninstall_3;
                    break;

                default:
                    break;
            }

            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = "请选择资源文件";
            dialog.Filter = filterStr;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo newImg = new FileInfo(dialog.FileName);
                newImg.CopyTo(Path.Combine(rootDirPath, TargetPath), true);
                LoadImgageResources(sender, Path.Combine(rootDirPath, TargetPath));
            }

        }

        /// <summary>
        /// 加载资源文件
        /// </summary>
        private void LoadImgageResources(object sender = null, string imgUrl = null)
        {
            try
            {
                //skin资源图片
                if (string.IsNullOrEmpty(imgUrl) || sender == null)
                {
                    imgSoftIcon.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.SoftIcon));
                    imgSoftUninstIcon.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.SoftUninstIcon));

                    img_TopLogo.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.TopLogo));
                    img_Carousel_1.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.Carousel_1));
                    img_Carousel_2.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.Carousel_2));
                    img_Carousel_3.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.Carousel_3));

                    img_Uninstall_1.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.Uninstall_1));
                    img_Uninstall_2.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.Uninstall_2));
                    img_Uninstall_3.Source = this.GetBitmapImage(Path.Combine(rootDirPath, SkinPath.Uninstall_3));
                }
                else
                {
                    Image img = sender as Image;
                    img.Source = this.GetBitmapImage(imgUrl);
                }
            }
            catch
            {
            }

        }

        /// <summary>
        /// 读取图片内容
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private BitmapImage GetBitmapImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            return bitmap.Clone();
        }


        /// <summary>
        /// 打开主程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void img_OpenMainSoftPath_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = "请选择主执行程序";
            dialog.Filter = "可执行文件(*.exe)|*.exe";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtMainSoftPath.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// 选择路径按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void img_btnSoftPath_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择程序所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    System.Windows.MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                txtSoftPath.Text = dialog.SelectedPath;
            }

        }

        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!InitProductInfo(out string productInfo))
                {
                    return;
                }

                // 写入到脚本中
                File.WriteAllText(rootDirPath + @"SoftSetupCore\SetupScripts\runtime\setup.nsi", productInfo, Encoding.Unicode);

                if (IsCompleteCompressor)
                {
                    if (MessageBoxX.Show("是否需要用新参数重新构建你的应用程序？", "询问", Application.Current.MainWindow, MessageBoxButton.YesNo, new MessageBoxXConfigurations() { MessageBoxIcon = MessageBoxIcon.Question }) == MessageBoxResult.Yes)
                    {
                        CompressorSoftware();
                        btnBuild.IsEnabled = false;
                        btnLastStep.IsEnabled = false;
                    }

                }
                else
                {
                    CompressorSoftware();
                    btnBuild.IsEnabled = false;
                    btnLastStep.IsEnabled = false;
                }

                step1.Visibility = Visibility.Hidden;
                step2.Visibility = Visibility.Visible;

               
            }
            catch (Exception ex)
            {
                MessageBoxX.Show($"异常错误：{ex.Message}", "错误", Application.Current.MainWindow, MessageBoxButton.OK, new MessageBoxXConfigurations() { MessageBoxIcon = MessageBoxIcon.Error });
            }
        }

        /// <summary>
        /// 压缩应用程序
        /// </summary>
        private void CompressorSoftware()
        {
            // 打包app
            SevenZipCompressor compressor = new SevenZipCompressor();
            compressor.BeginCompressDirectory(txtSoftPath.Text, rootDirPath + @"SoftSetupCore\SetupScripts\app.7z");

            compressor.Compressing += (sender, eventArgs) =>
            {
                lbl_Ver.Content = "正在构建你的应用程序，请稍等..." + $"已完成:{eventArgs.PercentDone}%";
            };

            // 压缩完成  
            compressor.CompressionFinished += (sender, eventArgs) =>
            {
                btnBuild.IsEnabled = true;
                btnLastStep.IsEnabled = true;
                IsCompleteCompressor = true;
                lbl_Ver.Content = "准备工作已全部完成，开始生成属于你的安装包吧";
            };
        }

        /// <summary>
        /// 运行打包按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuild_Click(object sender, RoutedEventArgs e)
        {
            // build
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.FileName = Path.Combine(rootDirPath, @"SoftSetupCore\run-build.bat");
            Process.Start(startInfo);

            lbl_Ver.Content = "请等待命令执行完成，请勿关闭程序";
        }

        /// <summary>
        /// 读取产品信息
        /// </summary>
        /// <param name="productInfoData"></param>
        /// <returns></returns>
        private bool InitProductInfo(out string productInfoData)
        {
            string softName = txtSoftName.Text;
            string softVersion = txtSoftVersion.Text;

            productInfoData = string.Empty;
            if (string.IsNullOrEmpty(txtSoftName.Text))
            {
                MessageBoxX.Show("请输入应用程序名称", "提示", Application.Current.MainWindow, MessageBoxButton.OK);
                return false;
            }

            // 版本号验证
            if (!Regex.IsMatch(softVersion, @"^([1-9]\d|[1-9])(\.([1-9]\d|\d)){3}$"))
            {
                MessageBoxX.Show("请输入正确的版本号", "提示", Application.Current.MainWindow, MessageBoxButton.OK);
                return false;
            }

            if (string.IsNullOrEmpty(txtSoftPath.Text))
            {
                MessageBoxX.Show("请选择应用程序路径", "提示", Application.Current.MainWindow, MessageBoxButton.OK);
                return false;
            }
            if (string.IsNullOrEmpty(txtMainSoftPath.Text))
            {
                MessageBoxX.Show("请选择应用程序主启动程序", "提示", Application.Current.MainWindow, MessageBoxButton.OK);
                return false;
            }

            FileInfo exeInfo = new FileInfo(txtMainSoftPath.Text);
            string mainEXEName = exeInfo.Name.Split('.')[0];
            productInfoData = $"# ====================== 自定义宏 产品信息==============================" +
                              $"\r\n!define PRODUCT_NAME                \"{softName}\"" +
                              $"\r\n!define PRODUCT_PATHNAME            \"{mainEXEName}\"  #安装卸载项用到的KEY" +
                              $"\r\n!define INSTALL_APPEND_PATH         \"{mainEXEName}\"	  #安装路径追加的名称 " +
                              $"\r\n!define INSTALL_DEFALT_SETUPPATH    \"\"       #默认生成的安装路径  " +
                              $"\r\n!define EXE_NAME                    \"{mainEXEName}.exe\"" +
                              $"\r\n!define PRODUCT_VERSION             \"{softVersion}\"  #ProductVersion必须是X.X.X.X" +
                              $"\r\n!define PRODUCT_PUBLISHER           \"Micahh\"" +
                              $"\r\n!define PRODUCT_LEGAL               \"Micahh Copyright（c）2021\"" +
                              $"\r\n!define INSTALL_OUTPUT_NAME         \"{softName}_Setup_{softVersion}.exe\"" +
                              "\r\n# ====================== 自定义宏 安装信息==============================" +
                              "\r\n!define INSTALL_7Z_PATH             \"..\\app.7z\"" +
                              "\r\n!define INSTALL_7Z_NAME             \"app.7z\"" +
                              "\r\n!define INSTALL_RES_PATH            \"skin.zip\"" +
                              "\r\n!define INSTALL_LICENCE_FILENAME    \"license.rtf\"" +
                              "\r\n!define INSTALL_ICO                 \"logo.ico\"" +
                              "\r\n!define UNINSTALL_ICO               \"uninst.ico\"" +
                              "\r\n!include \"ui_setup.nsh\"" +
                              "\r\nRequestExecutionLevel admin" +
                              "\r\nName \"${PRODUCT_NAME}\"" +
                              "\r\nOutFile \"..\\..\\..\\Output\\${INSTALL_OUTPUT_NAME}\"" +
                              "\r\nInstallDir \"1\"" +
                              "\r\nIcon              \"${INSTALL_ICO}\"" +
                              "\r\nUninstallIcon     \"${UNINSTALL_ICO}\"";

            return true;
        }

        /// <summary>
        /// 上一步按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLastStep_Click(object sender, RoutedEventArgs e)
        {
            step1.Visibility = Visibility.Visible;
            step2.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 打开资源文件所在文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_OpenResource_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(rootDirPath, @"SoftSetupCore\SetupScripts\runtime\skin\form\"));
        }

        /// <summary>
        /// 打开输出文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_OpenOutput_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(rootDirPath, @"Output\"));
        }

        /// <summary>
        /// 版本按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbl_Ver_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //调用系统默认的浏览器 
            Process.Start("explorer.exe", "https://github.com/micahh28");
        }
    }

    /// <summary>
    /// 皮肤路径
    /// </summary>
    public class SkinPath
    {
        public const string SoftIcon = @"SoftSetupCore\SetupScripts\runtime\logo.ico";
        public const string SoftUninstIcon = @"SoftSetupCore\SetupScripts\runtime\uninst.ico";
        public const string TopLogo = @"SoftSetupCore\SetupScripts\runtime\skin\form\bgtop.png";
        public const string Carousel_1 = @"SoftSetupCore\SetupScripts\runtime\skin\form\step1.png";
        public const string Carousel_2 = @"SoftSetupCore\SetupScripts\runtime\skin\form\step2.png";
        public const string Carousel_3 = @"SoftSetupCore\SetupScripts\runtime\skin\form\step3.png";
        public const string Uninstall_1 = @"SoftSetupCore\SetupScripts\runtime\skin\form\uninstall_bg1.png";
        public const string Uninstall_2 = @"SoftSetupCore\SetupScripts\runtime\skin\form\uninstall_bg2.png";
        public const string Uninstall_3 = @"SoftSetupCore\SetupScripts\runtime\skin\form\uninstall_bg3.png";
    }
}
