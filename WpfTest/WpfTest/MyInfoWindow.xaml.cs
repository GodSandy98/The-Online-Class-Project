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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfTest
{
    
    public partial class MyInfoWindow : Window
    {
        ClassInfoStuWindow m_classInfoStuWindow;

        public MyInfoWindow(ClassInfoStuWindow classInfoStuWindow)
        {
            InitializeComponent();
            m_classInfoStuWindow = classInfoStuWindow;
            List<QuizSheet> quizInfos = DataManager.QuizInfos;
            this.topicInfoListBox.ItemsSource = quizInfos;
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
            m_classInfoStuWindow.Show();
        }
    }
}
