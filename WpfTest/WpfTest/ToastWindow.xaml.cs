using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using ManageLiteAV;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for ToastWindow.xaml
    /// </summary>
    public partial class ToastWindow : Window
    {
        private System.Timers.Timer mTimer = null;
        private ITRTCCloud mTRTCCloud;
        private MainWindow mMainWindow;

        public ToastWindow(ITRTCCloud mTRTCCloud, MainWindow mMainWindow)
        {
            InitializeComponent();

            this.Closed += new EventHandler(OnDisposed);
            //任务栏右键退出功能
            this.Loaded += delegate
            {
                HwndSource source = (HwndSource)PresentationSource.FromDependencyObject(this);
                source.AddHook(WindowProc);
            };

            // 显示在顶部
            this.Top = 0;
            //仍需重新计算
            this.Left = (SystemParameters.PrimaryScreenWidth - 230) /2;
            //显示在最上层
            this.Topmost = true;

            //int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.ClientSize.Width) / 2;
            //int y = 0;
            //this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            //this.Location = (Point)new Size(x, y);         //窗体的起始位置为(x,y)

            //成员变量
            this.mMainWindow = mMainWindow;
            this.mTRTCCloud = mTRTCCloud;
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

        private void OnDisposed(object sender, EventArgs e)
        {
            //清理资源
            if (mTimer != null)
            {
                mTimer.Stop();
            }
        }

        public void SetText(string content, int time = 0)
        {
            this.textLabel.Content = content;
            if (time != 0)
            {
                mTimer = new System.Timers.Timer(time);
                mTimer.Interval = time;
                mTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerEvent);
                mTimer.Start();
            }
        }

        private void OnTimerEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                //this.DialogResult = DialogResult.Abort;
                mTimer.Stop();
                this.Hide();
            }));
        }

        //public void SetBackgroundColor(Color color)
        //{
        //    this.Background = color;
        //}

        //public void SetTextColor(Color color)
        //{
        //    this.textLabel.Foreground = color;
        //}

        private void exitBtn_Click(object sender, EventArgs e)
        {
            // 关闭屏幕分享功能
            //if (!IsSetScreenSuccess) return;
            mTRTCCloud.stopScreenCapture();
            this.Hide();

            // 移除混流中的屏幕分享画面
            //RemoveVideoMeta(mUserId, TRTCVideoStreamType.TRTCVideoStreamTypeSub);
            //UpdateMixTranCodeInfo();

            //mMainWindow.Show();
        }

        


    }
}
