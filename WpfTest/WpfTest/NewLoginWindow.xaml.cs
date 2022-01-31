using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewLoginWindow : Window
    {
        private List<UserInfo> allUsers;

        public NewLoginWindow()
        {
            InitializeComponent();
            allUsers = DataManager.userInfos;

            this.accountTextBox.Text = DataManager.GetInstance().userId;
            this.passwordTextBox.Password = DataManager.GetInstance().password;
            if (!this.passwordTextBox.Password.Equals(""))
            {
                this.rememberCheckBox.IsChecked = true;
            }
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

        
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            UserInfo mUser = allUsers.Find(per => per.userId.Equals(this.accountTextBox.Text));
            if (mUser == null)
            {
                System.Windows.MessageBox.Show("没有此用户！");
                return;
            }
            if (!mUser.password.Equals(this.passwordTextBox.Password))
            {
                System.Windows.MessageBox.Show("密码错误！请重试");
                return;
            }
            else
            {
                this.Hide();
                if (this.rememberCheckBox.IsChecked == true)
                {
                    DataManager.GetInstance().userId = mUser.userId;
                    DataManager.GetInstance().password = this.passwordTextBox.Password;
                }
                else
                {
                    DataManager.GetInstance().userId = mUser.userId;
                    DataManager.GetInstance().password = "";
                }
                SelectWindow SelectForm = new SelectWindow(mUser);
                SelectForm.Show();
            }
        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
