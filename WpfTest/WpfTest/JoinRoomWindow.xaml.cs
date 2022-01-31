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
    /// Interaction logic for JoinRoomWindow.xaml
    /// </summary>
    public partial class JoinRoomWindow : Window
    {
        //private LoginWindow mLoginWindow;
        UserInfo user;
        List<ClassInfo> allClassInfo;
        ClassInfo mClassInfo;
        SelectWindow selectWindow;

        public JoinRoomWindow(UserInfo userInfo, SelectWindow selectWindow)
        {
            InitializeComponent();
            this.user = userInfo;
            this.selectWindow = selectWindow;

            //任务栏右键退出功能
            this.Loaded += delegate
            {
                HwndSource source = (HwndSource)PresentationSource.FromDependencyObject(this);
                source.AddHook(WindowProc);
            };

            //实现拖拽功能
            this.MouseDown += (x, y) =>
            {
                if (y.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
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

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            selectWindow.Show();
        }

        private void joinRoomButton_Click(object sender, RoutedEventArgs e)
        {
            allClassInfo = DataManager.classInfos;
            mClassInfo = allClassInfo.Find(cla => cla.classId.Equals(this.roomIDTextBox.Text));//【数据库】根据classId获取password

            if (mClassInfo == null)
            {
                MessageBox.Show("没有该课程！请向任课教师验证");
                return;
            }
            if (this.roomPasswordBox.Password.Equals(mClassInfo.password))
            {
                JoinRoom(this.roomIDTextBox.Text);
                //【数据库】添加该stuid和clsid到cs表
            }
            else
            {
                MessageBox.Show("密码错误！请向任课教师验证");
            }
        }
        
        public void JoinRoom(String roomID)
        {
            if (GenerateTestUserSig.SDKAPPID == 0)
            {
                MessageBox.Show("Error: 请先在 GenerateTestUserSig 填写 sdkappid 信息");
                return;
            }

            string roomId = roomID;
            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("房间号或用户号不能为空！");
                return;
            }

            uint room = 0;
            if (!uint.TryParse(roomId, out room))
            {
                MessageBox.Show(String.Format("目前支持的最大房间号为{0}", uint.MaxValue));
                return;
            }


            DataManager.GetInstance().roomId = room;

            // 从本地计算获取 userId 对应的 userSig
            // 注意！本地计算是适合在本地环境下调试使用，正确的做法是将 UserSig 的计算代码和加密密钥放在您的业务服务器上，
            // 然后由 App 按需向您的服务器获取实时算出的 UserSig。
            // 由于破解服务器的成本要高于破解客户端 App，所以服务器计算的方案能够更好地保护您的加密密钥。
            string userSig = GenerateTestUserSig.GetInstance().GenTestUserSig(user.userName);
            if (string.IsNullOrEmpty(userSig))
            {
                MessageBox.Show("userSig 获取失败，请检查是否填写账号信息！");
                return;
            }
            //if (this.isOpenVideo.IsChecked == false)
            //{
            //    DataManager.GetInstance().pureAudioStyle = true;
            //}
            //else
            //{
            //    DataManager.GetInstance().pureAudioStyle = false;
            //}
            this.Hide();
            MainWindow mainWindow = new MainWindow(user,roomID, this.selectWindow, (Boolean)this.isOpenVideo.IsChecked, (Boolean)this.isOpenAudio.IsChecked);//把user传到datamaneger
            mainWindow.Show();
            mainWindow.EnterRoom();
        }
    }
}
