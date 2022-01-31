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
    /// Interaction logic for SelectWindow.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        private NewLoginWindow mLoginWindow;
        private UserInfo user;
        public List<ClassInfo> allClasses;
        private List<ClassInfo> mClasses;
        public List<CSInfo> allClassesStu;
        private List<CSInfo> mClassesStu;
        private bool isClosedtoNotifyicon = false;  //用于区分最小化（任务栏）和关闭按钮（托盘）

        public SelectWindow(UserInfo userInfo)
        {
            InitializeComponent();
            user = userInfo;
            
            if (user.isTeacher == true)
            {
                allClasses = DataManager.classInfos;
                this.classStuInfoListBox.Visibility = Visibility.Hidden;
            }
            else
            {
                this.createRoomBtn.Visibility = Visibility.Hidden;
                this.classInfoListBox.Visibility = Visibility.Hidden;
                allClasses = DataManager.classInfos;
                allClassesStu = DataManager.csInfos;
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
            this.Close();
            System.Windows.Application.Current.Shutdown();
        }

        private void JoinRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            JoinRoomWindow joinRoomWindow = new JoinRoomWindow(user, this);
            joinRoomWindow.Show();
        }

        private void CreateRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreateRoomWindow createRoomWindow = new CreateRoomWindow(user, this);
            createRoomWindow.Show();
        }

        
        private void onLoad(object sender, RoutedEventArgs e)
        {
            if (user.isTeacher == true)
            {
                mClasses = allClasses.FindAll(cla => cla.teacherId.Equals(user.userId));//【数据库】在class表中根据教师ID查全部信息
                this.classInfoListBox.ItemsSource = mClasses;
            }
            else
            {
                mClassesStu = allClassesStu.FindAll(cla => cla.studentId.Equals(user.userId));//【数据库】在cs表中根据学生ID查课程ID
                List<string> mClassId = new List<string>();
                foreach (var item in mClassesStu)
                {
                    mClassId.Add(item.classId);
                }
                mClasses = allClasses.FindAll(cla => mClassId.Contains(cla.classId));//【数据库】在class表中根据课程ID查全部信息
                this.classInfoListBox.ItemsSource = mClasses;
                this.classStuInfoListBox.ItemsSource = mClasses;
            }
        }

        private void ClassInfoListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (user.isTeacher == true)
            {
                int i = (sender as ListBox).SelectedIndex;
                if (i != -1)
                {
                    ClassInfoWindow classInfoWindow = new ClassInfoWindow(mClasses.ElementAt(i).classId);
                    classInfoWindow.Show();
                }
            }
            else
            {
                int i = (sender as ListBox).SelectedIndex;
                if (i != -1)
                {
                    ClassInfoStuWindow classInfoStuWindow = new ClassInfoStuWindow(mClassesStu.ElementAt(i).classId);
                    classInfoStuWindow.Show();
                }
            }

        }


        private void StartClassBtn_Click(object sender, RoutedEventArgs e)
        {
            string classId = (sender as Button).Tag.ToString();
            MessageBox.Show("添加一次课次");//【数据库】添加一次课次
            this.Hide();
            JoinRoomWindow joinRoomWindow = new JoinRoomWindow(user, this);
            joinRoomWindow.isOpenAudio.IsChecked = true;
            joinRoomWindow.isOpenVideo.IsChecked = true;
            joinRoomWindow.JoinRoom(classId);
        }

        private void ExtendComboBox_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as ComboBox).Background = new SolidColorBrush(Color.FromRgb(26, 140, 255));
        }

        private void ExtendComboBox_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as ComboBox).Background = new SolidColorBrush(Color.FromRgb(0, 110, 255));
        }

        private void UpdateClassInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            string classId = (sender as Button).Tag.ToString();
            this.Hide();
            CreateRoomWindow createRoomWindow = new CreateRoomWindow(user, this, classId);
            createRoomWindow.Show();
        }

        private void CopyRoomInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            string classId = (sender as Button).Tag.ToString();
            string teacherName = "xxx";//【数据库】根据classId查询教师名
            string roomInfoText = "教室号：" +classId+"\n任课教师："+teacherName;
            Clipboard.SetDataObject(roomInfoText);
        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

    }
}
