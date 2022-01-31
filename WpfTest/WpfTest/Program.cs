using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfTest.Common;

namespace WpfTest
{
    class Program
    {
        
        // 外部函数声明
        [DllImport("User32.dll")]
        private static extern Int32 SetProcessDPIAware();

        public static EventWaitHandle ProgramStarted;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ManageLiteAV.CrashDump dump = new ManageLiteAV.CrashDump();
            dump.open();

            // 尝试创建一个命名事件
            bool createNew;
            ProgramStarted = new EventWaitHandle(false, EventResetMode.AutoReset, "TRTCStartEvent", out createNew);

            // 如果该命名事件已经存在(存在有前一个运行实例)，则发事件通知并退出
            if (!createNew)
            {
                ProgramStarted.Set();
                return;
            }

            SetProcessDPIAware();   // 默认关闭高DPI，避免SDK录制出错

            Log.Open();
            WpfTest.App app = new WpfTest.App();
            // 初始化SDK的 Local 配置信息
            DataManager.GetInstance().InitConfig();

            app.InitializeComponent();
            NewLoginWindow windows = new NewLoginWindow();
            app.MainWindow = windows;
            app.Run();

            // 退出程序前写入最新的 Local 配置信息。
            DataManager.GetInstance().Uninit();
            DataManager.GetInstance().Dispose();

            Log.Close();

            dump.close();

            //Environment.Exit(0);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }



    }
}
