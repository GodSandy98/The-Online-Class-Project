using ManageLiteAV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shapes;
using WpfTest.Common;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for ScreenWindow.xaml
    /// </summary>
    public partial class ScreenWindow : Window
    {
        private MainWindow mMainWindow;
        private ITRTCCloud mTRTCCloud;
        private ITRTCScreenCaptureSourceList mScreenList;

        private ImageList mImageList;

        public ScreenWindow(MainWindow window)
        {
            InitializeComponent();

            this.Closed += new EventHandler(OnClosed);

            mTRTCCloud = DataManager.GetInstance().trtcCloud;
            mMainWindow = window;

            mImageList = new ImageList();
            mImageList.ImageSize = new System.Drawing.Size(120, 70);
            //任务栏右键退出功能
            this.Loaded += delegate
            {
                HwndSource source = (HwndSource)PresentationSource.FromDependencyObject(this);
                source.AddHook(WindowProc);
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

        private void OnClosed(object sender, EventArgs e)
        {
            //清理资源
            mScreenList.release();
            mScreenList = null;
            mTRTCCloud = null;
            mMainWindow = null;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            SIZE thumbSize = new SIZE() { cx = 120, cy = 70 };
            SIZE iconSize = new SIZE() { cx = 20, cy = 20 };
            mScreenList = mTRTCCloud.getScreenCaptureSources(ref thumbSize, ref iconSize);
            for (uint i = 0; i < mScreenList.getCount(); i++)
            {
                TRTCScreenCaptureSourceInfo sourse = mScreenList.getSourceInfo(i);
                Log.I(String.Format("ScreenCaoture{0} : type = {1}, sourseId = {2}, sourseName = {3}, thumbBuffer = {4}, iconBuffer = {5}",
                    i + 1, sourse.type, sourse.sourceId, sourse.sourceName, sourse.thumbBGRA.buffer + " {" + sourse.thumbBGRA.width + ", " + sourse.thumbBGRA.height + "}, length = " + sourse.thumbBGRA.length,
                    sourse.iconBGRA.buffer + " {" + sourse.iconBGRA.width + ", " + sourse.iconBGRA.height + "}, length = " + sourse.iconBGRA.length));
                string name;
                if (sourse.sourceName.Equals("Screen1"))
                    name = "显示器-1";
                else if (sourse.sourceName.Equals("Screen2"))
                    name = "显示器-2";
                else if (sourse.sourceName.Equals("Screen3"))
                    name = "显示器-3";
                else if (sourse.sourceName.Equals("Screen4"))
                    name = "显示器-4";
                else if (sourse.sourceName.Equals("Screen5"))
                    name = "显示器-5";
                else
                    name = sourse.sourceName;

                // 设置屏幕缩略图

                int width = 120;
                int height = 70;
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                if (sourse.thumbBGRA.length <= 0)
                {
                    // 未找到缩略图，不显示
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                    }
                    mImageList.Images.Add(name, bmp);
                    continue;
                }

                BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

                int stride = bmpData.Stride;
                IntPtr iptr = bmpData.Scan0;
                int scanBytes = stride * height;
                int posScan = 0, posReal = 0;
                byte[] pixelValues = new byte[scanBytes];

                for (int j = 0; j < sourse.thumbBGRA.buffer.Length; j++)
                    pixelValues[posScan++] = sourse.thumbBGRA.buffer[posReal++];

                Marshal.Copy(pixelValues, 0, iptr, scanBytes);
                bmp.UnlockBits(bmpData);

                mImageList.Images.Add(name, bmp);
            }
            this.screenListView.LargeImageList = mImageList;
            this.screenListView.BeginUpdate();
            for (int i = 0; i < mImageList.Images.Count; i++)
            {
                System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem();
                item.ImageIndex = i;
                item.Text = mImageList.Images.Keys[i];
                this.screenListView.Items.Add(item);
            }
            this.screenListView.EndUpdate();
            this.screenListView.HideSelection = true;
            if (this.screenListView.Items.Count > 0)
            {
                this.screenListView.Items[0].Selected = true;
                this.screenListView.Select();
            }
        }

        private void OnSaveBtnClick(object sender, EventArgs e)
        {
            if (this.screenListView.SelectedItems.Count == 0)
            {
                //MessageForm msg = new MessageForm();
                //msg.setCancelBtn(false);
                //msg.setText("请选择一个需要共享的屏幕！");
                //msg.ShowDialog();
                Console.WriteLine("请选择一个需要共享的屏幕！");
                return;
            }
            TRTCScreenCaptureSourceInfo sourceinfo = mScreenList.getSourceInfo(0);
            for (uint i = 0; i < mScreenList.getCount(); i++)
            {
                TRTCScreenCaptureSourceInfo info = mScreenList.getSourceInfo(i);

                string name = this.screenListView.SelectedItems[0].Text;

                if (this.screenListView.SelectedItems[0].Text.Equals("显示器-1"))
                    name = "Screen1";
                else if (this.screenListView.SelectedItems[0].Text.Equals("显示器-2"))
                    name = "Screen2";
                else if (this.screenListView.SelectedItems[0].Text.Equals("显示器-3"))
                    name = "Screen3";
                else if (this.screenListView.SelectedItems[0].Text.Equals("显示器-4"))
                    name = "Screen4";
                else if (this.screenListView.SelectedItems[0].Text.Equals("显示器-5"))
                    name = "Screen5";

                if (name.Equals(mScreenList.getSourceInfo(i).sourceName))
                {
                    sourceinfo = info;
                    break;
                }
            }
            RECT rect = new RECT()
            {
                top = 0,
                left = 0,
                right = 0,
                bottom = 0
            };
            TRTCScreenCaptureProperty property = new TRTCScreenCaptureProperty();
            mTRTCCloud.selectScreenCaptureTarget(ref sourceinfo, ref rect, ref property);
            mMainWindow.OnSetScreenParamsCallback(true);
            this.Close();
        }

        private void OnCancelBtnClick(object sender, EventArgs e)
        {
            mMainWindow.OnSetScreenParamsCallback(false);
            this.Close();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            mMainWindow.OnSetScreenParamsCallback(false);
            this.Close();
        }
    }
}
