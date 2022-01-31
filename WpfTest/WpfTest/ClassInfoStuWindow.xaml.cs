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
    /// ClassInfoStuWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClassInfoStuWindow : Window
    {
        String classId;
        private List<LessonInfo> allLessons;
        private List<LessonInfo> mLessons;

        public ClassInfoStuWindow(String classId)
        {
            InitializeComponent();

            //任务栏右键退出功能
            this.Loaded += delegate
            {
                HwndSource source = (HwndSource)PresentationSource.FromDependencyObject(this);
                source.AddHook(WindowProc);
            };

            allLessons = DataManager.lessonInfos;
            this.classId = classId;
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

        private childItem FindVisualChild<childItem>(DependencyObject obj)
    where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            mLessons = allLessons.FindAll(les => les.classId.Equals(this.classId));
            this.lessonInfoListBox.ItemsSource = mLessons;
        }

        private void LessonInfoListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem myListBoxItem = (ListBoxItem)(lessonInfoListBox.ItemContainerGenerator.ContainerFromItem(lessonInfoListBox.Items.CurrentItem));

            // Getting the ContentPresenter of myListBoxItem
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

            // Finding textBlock from the DataTemplate that is set on that ContentPresenter
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            Button myButton = (Button)myDataTemplate.FindName("moreInfoBtn", myContentPresenter);

            //// Do something to the DataTemplate-generated TextBlock
            string lessonId = myButton.Tag.ToString();
            MyInfoWindow myInfoWindow = new MyInfoWindow(this);
            myInfoWindow.Show();
        }

        private void deleteLesson_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("确定删除本次课程？",
                                               "有风险的操作",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                MessageBox.Show("已删除该门课程");
                //【数据库】删除该次课程
                //刷新
                this.Close();
                ClassInfoWindow classInfoWindow = new ClassInfoWindow(classId);
                classInfoWindow.Show();
            }

        }

        private void lesoonInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            string lessonId = (sender as Button).Tag.ToString();

            LessonInfoWindow lessonInfoWindow = new LessonInfoWindow(classId, lessonId,this);
            lessonInfoWindow.Show();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void moreInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MyInfoWindow myInfoWindow = new MyInfoWindow(this);
            myInfoWindow.Show();
        }

        private void leaveClassBtn_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("确定退出本次课程？",
                                               "有风险的操作",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                MessageBox.Show("已删除该门课程");
                //【数据库】删除该学生与本课程的连接
                //刷新SelectWindow
                this.Close();
            }
        }
    }
}
