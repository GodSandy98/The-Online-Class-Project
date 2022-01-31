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
    /// LessonInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LessonInfoWindow : Window
    {
        private List<TimeSheet> allTimeSheetInfo = new List<TimeSheet>();
        private List<TimeSheet> timeSheetInfo = new List<TimeSheet>();
        private List<QuizSheet> allQuizInfo = new List<QuizSheet>();
        private List<QuizSheet> quizInfo = new List<QuizSheet>();
        string m_classId;
        string m_lessonId;
        ClassInfoStuWindow m_classInfoStuWindow;
        ClassInfoWindow m_classInfoWindow;
        Boolean isStuWindow;

        public LessonInfoWindow(string classId, string lessonId, ClassInfoWindow classInfoWindow)
        {
            InitializeComponent();
            allTimeSheetInfo = DataManager.timeSheetInfos;
            allQuizInfo = DataManager.QuizInfos;
            m_classId = classId;
            m_lessonId = lessonId;
            m_classInfoWindow = classInfoWindow;
            isStuWindow = false;

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

        public LessonInfoWindow(string classId, string lessonId, ClassInfoStuWindow classInfoStuWindow)
        {
            InitializeComponent();
            allTimeSheetInfo = DataManager.timeSheetInfos;
            allQuizInfo = DataManager.QuizInfos;
            m_classId = classId;
            m_lessonId = lessonId;
            m_classInfoStuWindow = classInfoStuWindow;
            isStuWindow = true;
            //实现拖拽功能
            this.MouseDown += (x, y) =>
            {
                if (y.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
        }



        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (isStuWindow)
            {
                m_classInfoStuWindow.Show();
            }
            else
            {
                m_classInfoWindow.Show();
            }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            timeSheetInfo = allTimeSheetInfo.FindAll(tsi => tsi.ClassId.Equals("30") && tsi.LessonId.Equals("1"));
            quizInfo = allQuizInfo.FindAll(qi => qi.ClassId.Equals("30") && qi.LessonId.Equals("1"));
            this.quizInfoDataGrid.ItemsSource = quizInfo;
            //this.studentInfoListBox.ItemsSource = timeSheetInfo;
            this.studentInfoDataGrid.ItemsSource = timeSheetInfo;
        }

        private void studentStatisticBtn_Click(object sender, RoutedEventArgs e)
        {
            this.studentStatisticBtn.Style = (Style)this.FindResource("BlueButtonStyle");
            this.quizStatisticBtn.Style = (Style)this.FindResource("GrayButtonStyle");
            this.quizInfoPanel.Visibility = Visibility.Hidden;
            this.studentInfoPanel.Visibility = Visibility.Visible;
        }

        private void quizStatisticBtn_Click(object sender, RoutedEventArgs e)
        {
            this.studentStatisticBtn.Style = (Style)this.FindResource("GrayButtonStyle");
            this.quizStatisticBtn.Style = (Style)this.FindResource("BlueButtonStyle");
            this.studentInfoPanel.Visibility = Visibility.Hidden;
            this.quizInfoPanel.Visibility = Visibility.Visible;
        }

        private void studentDetailedInfoTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StuInfoWindow stuInfoWindow = new StuInfoWindow();
            stuInfoWindow.Show();
        }
        private void quizDetailedInfoTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            QuizInfoWindow quizInfoWindow = new QuizInfoWindow();
            quizInfoWindow.Show();
        }

        private void infoTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.Gray);
        }
        private void infoTextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.Black);
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }


}
