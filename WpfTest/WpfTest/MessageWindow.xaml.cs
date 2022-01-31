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
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        private bool mIsMouseDown = false;
        private Point mWindowLocation;     // Window的location
        private Point mMouseOffset;      // 鼠标的按下位置
        private System.Timers.Timer mTimer = null;
        public MessageWindow()
        {
            //初始位置居中
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();

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

        public void setText(string title, int delayCloseMs = 0)
        {
            labelTitle.Content = title;
            if (delayCloseMs != 0)
            {
                mTimer = new System.Timers.Timer(delayCloseMs);
                mTimer.Interval = delayCloseMs;
                mTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerEvent);
                mTimer.Start();
            }
        }

        public void OnTimerEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //this.BeginInvoke(new Action(() =>
            //{
            //    this.DialogResult = DialogResult.Abort;
            //    mTimer.Stop();
            //    this.Close();
            //}));
        }

        public void setCancelBtn(bool state)
        {
            if (state)
            {
                cancelBtn.Visibility = Visibility.Visible;
            }
            else
            {
                cancelBtn.Visibility = Visibility.Hidden;
            }
        }

        private void OnWindowMouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                mIsMouseDown = true;
                mWindowLocation = this.GetWindowLocation();
                mMouseOffset = this.PointToScreen(Mouse.GetPosition(this));
                this.DragMove();  //zx拖拽
            }
        }

        /// <summary>
        /// zx 获取窗体坐标
        /// </summary>
        /// <returns></returns>
        private Point GetWindowLocation()
        {
            //首先获取当前窗体的左上角和右下角坐标
            Point ptLeftUp = new Point(0, 0);
            Point ptRightDown = new Point(this.ActualWidth, this.ActualHeight);

            //转换获取到这个窗口相对于屏幕两个坐标
            ptLeftUp = this.PointToScreen(ptLeftUp);
            ptRightDown = this.PointToScreen(ptRightDown);

            //获取窗体在屏幕的实际宽高
            double Width = ptRightDown.X - ptLeftUp.X;
            double Height = ptRightDown.X - ptLeftUp.X;

            return new Point(Width, Height);
        }

        //zx进行改动后有错误，故暂时注释，此处代码不影响程序
        //private void OnWindowMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (mIsMouseDown)
        //    {
        //        Point pt = Mouse.GetPosition(null);
        //        double x = mMouseOffset.X - pt.X;
        //        double y = mMouseOffset.Y - pt.Y;

        //        //this.Location = new Point(mFormLocation.X - x, mFormLocation.Y - y);
        //        this.Left = mWindowLocation.X - x;
        //        this.Top = mWindowLocation.Y - y;
        //    }
        //}

        private void OnFormMouseUp(object sender, MouseEventArgs e)
        {
            mIsMouseDown = false;
        }


        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ////文字内容太小时，就居中显示，Form高度适中就可以
            //if (this.labelTitle.Height < 50)
            //{
            //    labelTitle.Location = new Point((this.Width - labelTitle.Width) / 2, 25);
            //    labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            //    this.Height = 120;
            //}
            //else
            //{
            //    this.Height = labelTitle.Height + 100;
            //    labelTitle.TextAlign = ContentAlignment.MiddleLeft;
            //}


        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.DialogResult = false;
            this.Close();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            this.DialogResult = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != mTimer)
            {
                mTimer.Stop();
            }
        }
    }
}
