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
    /// Interaction logic for CreateRoomWindow.xaml
    /// </summary>
    public partial class CreateRoomWindow : Window
    {
        UserInfo user;
        SelectWindow selectWindow;
        Boolean textboxHasText = false;//判断输入框是否有文本
        List<ClassInfo> allClassInfo;
        ClassInfo mClassInfo;

        public CreateRoomWindow(UserInfo userInfo, SelectWindow selectWindow)
        {
            InitializeComponent();
            this.selectWindow = selectWindow;
            this.user = userInfo;

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

        /// <summary>
        /// 更新房间信息时调用
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="selectWindow"></param>
        /// <param name="classId"></param>
        public CreateRoomWindow(UserInfo userInfo, SelectWindow selectWindow, string classId)
        {
            InitializeComponent();
            this.selectWindow = selectWindow;
            this.user = userInfo;

            allClassInfo = DataManager.classInfos;
            mClassInfo = allClassInfo.Find(cla => cla.classId.Equals(classId));

            //修改控件内容
            this.classTitleTextBox.Text = mClassInfo.classTitle;
            this.maxPeopleTextBox.Text = mClassInfo.maxPeople.ToString();
            if(mClassInfo.hasPassword == true)
            {
                this.hasPasswordCheckBox.IsChecked = true;
                this.passwordPasswordBox.Password = mClassInfo.password;
            }
            this.createRoomBtn.Content = "确认修改";
            this.titleLabel.Content = "修改课堂信息";
            this.createRoomBtn.Tag = "update";

            //实现拖拽功能
            this.MouseDown += (x, y) =>
            {
                if (y.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
        }


        private void createRoomBtn_Click(object sender, RoutedEventArgs e)
        {
            selectWindow.Close();
            SelectWindow sWindow = new SelectWindow(user);
            int maxPeopleNum;
            String newClassId;
            if (!this.createRoomBtn.Tag.Equals("update"))//非更新
            {
                this.Close();
                newClassId = createRandomClassID().ToString();
                Console.WriteLine(newClassId);
                sWindow.allClasses.Add(new ClassInfo()//【数据库】改为添加一个class
                {
                    classId = newClassId,
                    teacherId = user.userId,
                    classTitle = classTitleTextBox.Text,
                    maxPeople = int.TryParse(maxPeopleTextBox.Text, out maxPeopleNum)? maxPeopleNum : 50,
                    hasPassword = (bool)hasPasswordCheckBox.IsChecked,
                    password = passwordPasswordBox.Password
                });
                
            }
            else//更新
            {
                this.Close();
                ClassInfo newClassInfo;
                newClassInfo = sWindow.allClasses.Find(cla => cla.classId.Equals(mClassInfo.classId));
                newClassInfo.classTitle = this.classTitleTextBox.Text;
                newClassInfo.maxPeople = int.TryParse(maxPeopleTextBox.Text, out maxPeopleNum) ? maxPeopleNum : 50;
                newClassInfo.hasPassword = (bool)hasPasswordCheckBox.IsChecked;
                newClassInfo.password = passwordPasswordBox.Password;
            }
            sWindow.Show();
        }

        private int createRandomClassID()
        {
            while (true)
            {
                Random rd = new Random();
                int randomClassId = rd.Next(1, 10000);
                if (selectWindow.allClasses.Where(cla => cla.classId.Equals(randomClassId)).ToList().Count == 0)
                {
                    return randomClassId;
                }
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            selectWindow.Show();
        }

        private void hasPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.passwordPasswordBox.Password = "";
        }
    }
}
