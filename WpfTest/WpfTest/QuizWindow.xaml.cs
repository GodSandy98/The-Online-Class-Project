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
    /// <summary>
    /// QuizWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QuizWindow : Window
    {
        public QuizWindow(/*string classId, string lessonId*/)
        {
            InitializeComponent();
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
        }

        private void publishBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("发布一次Quiz");//【数据库】添加一组问题表信息
            this.Close();
        }

    }
}
