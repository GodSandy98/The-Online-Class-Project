using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for LocalVedioWhenShare.xaml
    /// </summary>
    public partial class LocalVedioWhenShare : Window
    {
        public LocalVedioWhenShare(string teacherName)
        {
            InitializeComponent();
            this.localUserLabel.Content = teacherName;
            //任务栏右键退出功能
            this.Loaded += delegate
            {
                HwndSource source = (HwndSource)PresentationSource.FromDependencyObject(this);
                source.AddHook(WindowProc);
            };
        }

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x112 && ((ushort)wParam & 0xfff0) == 0xf060)
            {
                Console.WriteLine("Close reason: User closing from menu");
                System.Windows.Application.Current.Shutdown();
            }
            return IntPtr.Zero;
        }

        public IntPtr getHandle()
        {
            return remoteVideoPanel1.Handle;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            //实现拖拽功能
            this.MouseDown += (x, y) =>
            {
                if (y.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            //初始化
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
