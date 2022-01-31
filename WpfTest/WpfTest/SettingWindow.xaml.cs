using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using ManageLiteAV;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        //视频设置
        private TRTCVideoEncParam mEncParam;
        private ITRTCDeviceCollection mCameraDeviceList;
        private ITRTCCloud mTRTCCloud;
        private ITXDeviceManager mDeviceManager;
        private ITRTCDeviceInfo mCameraDevice;
        private MainWindow mMainWindow;
        //音频设置
        private ITRTCDeviceInfo mMicDevice;
        private ITRTCDeviceInfo mSpeakerDevice;
        private ITRTCDeviceCollection mMicDeviceList;
        private ITRTCDeviceCollection mSpeakerDeviceList;

        private int mMicVolume;
        private int mSpeakerVolume;
        private string mTestPath = System.Environment.CurrentDirectory + "\\Resources\\trtcres\\testspeak.mp3";

        public SettingWindow(MainWindow mainWindow)
        {
            InitializeComponent();
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

            closeMethedPanel.Visibility = Visibility.Visible;
            videoSettingPanel.Visibility = Visibility.Hidden;
            audioSettingPanel.Visibility = Visibility.Hidden;

            //视频设置
            this.Closed += new EventHandler(OnDisposed);

            this.mTRTCCloud = DataManager.GetInstance().trtcCloud;
            this.mDeviceManager = mTRTCCloud.getDeviceManager();

            this.resolutionComboBox.Items.Add("120 x 120");
            this.resolutionComboBox.Items.Add("160 x 160");
            this.resolutionComboBox.Items.Add("270 x 270");
            this.resolutionComboBox.Items.Add("480 x 480");
            this.resolutionComboBox.Items.Add("160 x 120");
            this.resolutionComboBox.Items.Add("240 x 180");
            this.resolutionComboBox.Items.Add("280 x 210");
            this.resolutionComboBox.Items.Add("320 x 240");
            this.resolutionComboBox.Items.Add("400 x 300");
            this.resolutionComboBox.Items.Add("480 x 360");
            this.resolutionComboBox.Items.Add("640 x 480");
            this.resolutionComboBox.Items.Add("960 x 720");
            this.resolutionComboBox.Items.Add("160 x 90");
            this.resolutionComboBox.Items.Add("256 x 144");
            this.resolutionComboBox.Items.Add("320 x 180");
            this.resolutionComboBox.Items.Add("480 x 270");
            this.resolutionComboBox.Items.Add("640 x 360");
            this.resolutionComboBox.Items.Add("960 x 540");
            this.resolutionComboBox.Items.Add("1280 x 720");

            this.fpsComboBox.Items.Add("15 fps");
            this.fpsComboBox.Items.Add("20 fps");
            this.fpsComboBox.Items.Add("24 fps");

            this.resolutionModeComboBox.Items.Add("横屏模式");
            this.resolutionModeComboBox.Items.Add("竖屏模式");

            //音频设置
            mTRTCCloud = DataManager.GetInstance().trtcCloud;
            mDeviceManager = mTRTCCloud.getDeviceManager();

            mMainWindow = mainWindow;
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
            if (this.micTestBtn.Content.Equals("停止"))
            {
                mDeviceManager.stopMicDeviceTest();
            }
            if (this.speakerTestBtn.Content.Equals("停止"))
            {
                mDeviceManager.stopSpeakerDeviceTest();
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataManager.GetInstance().closeMethod)
            {
                this.trayCloseCheckBox.IsChecked = true;
                this.normalCloseCheckBox.IsChecked = false;
            }
            else
            {
                this.trayCloseCheckBox.IsChecked = false;
                this.normalCloseCheckBox.IsChecked = true;
            }
            //视频设置
            RefreshCameraDeviceList();
            InitData();
            int selectedIndex = -1;
            if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_120_120)
                selectedIndex = 0;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_160_160)
                selectedIndex = 1;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_270_270)
                selectedIndex = 2;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_480_480)
                selectedIndex = 3;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_160_120)
                selectedIndex = 4;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_240_180)
                selectedIndex = 5;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_280_210)
                selectedIndex = 6;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_320_240)
                selectedIndex = 7;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_400_300)
                selectedIndex = 8;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_480_360)
                selectedIndex = 9;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_640_480)
                selectedIndex = 10;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_960_720)
                selectedIndex = 11;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_160_90)
                selectedIndex = 12;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_256_144)
                selectedIndex = 13;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_320_180)
                selectedIndex = 14;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_480_270)
                selectedIndex = 15;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_640_360)
                selectedIndex = 16;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_960_540)
                selectedIndex = 17;
            else if (mEncParam.videoResolution == TRTCVideoResolution.TRTCVideoResolution_1280_720)
                selectedIndex = 18;
            this.resolutionComboBox.SelectedIndex = selectedIndex;

            if (mEncParam.videoFps == 15)
                this.fpsComboBox.SelectedIndex = 0;
            else if (mEncParam.videoFps == 20)
                this.fpsComboBox.SelectedIndex = 1;
            else if (mEncParam.videoFps == 24)
                this.fpsComboBox.SelectedIndex = 2;

            if (mEncParam.resMode == TRTCVideoResolutionMode.TRTCVideoResolutionModeLandscape)
                this.resolutionModeComboBox.SelectedIndex = 0;
            else
                this.resolutionModeComboBox.SelectedIndex = 1;

            int bitrate = (int)mEncParam.videoBitrate;
            this.bitrateSlider.Value = bitrate;
            this.bitrateNumLabel.Content = bitrate + " kbps";

            //this.saveBtn.IsEnabled = false;
            //音频设置
            this.audioQualityComboBox.Items.Clear();
            this.audioQualityComboBox.Items.Add("语音（16K 单声道）");
            this.audioQualityComboBox.Items.Add("默认（48K 单声道）");
            this.audioQualityComboBox.Items.Add("音乐（128K 双声道）");

            switch (DataManager.GetInstance().AudioQuality)
            {
                case TRTCAudioQuality.TRTCAudioQualitySpeech:
                    this.audioQualityComboBox.SelectedIndex = 0;
                    break;
                case TRTCAudioQuality.TRTCAudioQualityDefault:
                    this.audioQualityComboBox.SelectedIndex = 1;
                    break;
                case TRTCAudioQuality.TRTCAudioQualityMusic:
                    this.audioQualityComboBox.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
            RefreshMicDeviceList();
            RefreshSpeakerList();

            mMicVolume = (int)mDeviceManager.getCurrentDeviceVolume(TRTCDeviceType.TXMediaDeviceTypeMic);
            this.micVolumeSlider.Value = mMicVolume;
            this.micVolumeNumLabel.Content = mMicVolume + "%";

            mSpeakerVolume = (int)mDeviceManager.getCurrentDeviceVolume(TRTCDeviceType.TXMediaDeviceTypeSpeaker);
            this.speakerVolumeSlider.Value = mSpeakerVolume;
            this.speakerVolumeNumLabel.Content = mSpeakerVolume + "%";
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            //视频清理资源
            if (mTRTCCloud == null) return;

            if (mCameraDeviceList != null)
                mCameraDeviceList.release();
            mCameraDeviceList = null;

            mTRTCCloud = null;
            mDeviceManager = null;
            //音频清理资源
            if (mTRTCCloud == null || mDeviceManager == null) return;
            if (this.micTestBtn.Content.Equals("停止"))
            {
                mDeviceManager.stopMicDeviceTest();
            }
            if (this.speakerTestBtn.Content.Equals("停止"))
            {
                mDeviceManager.stopSpeakerDeviceTest();
            }

            if (this.systemAudioCheckBox.IsChecked == true)
                mTRTCCloud.stopSystemAudioLoopback();

            if (mMicDevice != null)
                mMicDevice.release();
            if (mSpeakerDevice != null)
                mSpeakerDevice.release();

            if (mMicDeviceList != null)
                mMicDeviceList.release();
            if (mSpeakerDeviceList != null)
                mSpeakerDeviceList.release();

            mMicDevice = null;
            mSpeakerDevice = null;
            mMicDeviceList = null;
            mSpeakerDeviceList = null;
            mTRTCCloud = null;
            mDeviceManager = null;
        }

        #region 关闭方式
        private void closeMethod_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            closeMethedPanel.Visibility = Visibility.Visible;
            videoSettingPanel.Visibility = Visibility.Hidden;
            audioSettingPanel.Visibility = Visibility.Hidden;
        }

        private void normalCloseCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DataManager.GetInstance().closeMethod = false;
            this.trayCloseCheckBox.IsChecked = false;
        }

        private void trayCloseCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DataManager.GetInstance().closeMethod = true;
            this.normalCloseCheckBox.IsChecked = false;
        }
        #endregion

        #region 视频设置
        private void videoSetting_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            closeMethedPanel.Visibility = Visibility.Hidden;
            videoSettingPanel.Visibility = Visibility.Visible;
            audioSettingPanel.Visibility = Visibility.Hidden;
        }

        public void OnDeviceChange(string deviceId, TRTCDeviceType type, TRTCDeviceState state)
        {
            if (type == TRTCDeviceType.TXMediaDeviceTypeCamera)
            {
                RefreshCameraDeviceList();
            }
            else if (type == TRTCDeviceType.TXMediaDeviceTypeMic)
            {
                RefreshMicDeviceList();
            }
            else if (type == TRTCDeviceType.TXMediaDeviceTypeSpeaker)
            {
                RefreshSpeakerList();
            }
        }

        private void InitData()
        {
            mEncParam = new TRTCVideoEncParam()
            {
                videoBitrate = DataManager.GetInstance().videoEncParams.videoBitrate,
                videoFps = DataManager.GetInstance().videoEncParams.videoFps,
                videoResolution = DataManager.GetInstance().videoEncParams.videoResolution,
                resMode = DataManager.GetInstance().videoEncParams.resMode
            };
        }

        private void RefreshCameraDeviceList()
        {
            if (mDeviceManager == null) return;
            this.cameraDeviceComboBox.Items.Clear();
            mCameraDeviceList = mDeviceManager.getDevicesList(TRTCDeviceType.TXMediaDeviceTypeCamera);
            if (mCameraDeviceList.getCount() <= 0)
            {
                this.cameraDeviceComboBox.Items.Add("");
                this.cameraDeviceComboBox.SelectedItem = this.cameraDeviceComboBox.Text.Length;
                return;
            }
            mCameraDevice = mDeviceManager.getCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeCamera);
            for (uint i = 0; i < mCameraDeviceList.getCount(); i++)
            {
                this.cameraDeviceComboBox.Items.Add(mCameraDeviceList.getDeviceName(i));
                if (mCameraDevice.getDeviceName().Equals(mCameraDeviceList.getDeviceName(i)))
                    this.cameraDeviceComboBox.SelectedIndex = (int)i;
            }
            if (string.IsNullOrEmpty(mCameraDevice.getDeviceName()) && mCameraDeviceList.getCount() > 0)
                this.cameraDeviceComboBox.SelectedIndex = 0;
        }
        private void resolutionComboBox_SelectionChanged(object sender, EventArgs e)
        {
            int index = this.resolutionComboBox.SelectedIndex;
            if (index == 0)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_120_120;
            else if (index == 1)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_160_160;
            else if (index == 2)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_270_270;
            else if (index == 3)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_480_480;
            else if (index == 4)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_160_120;
            else if (index == 5)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_240_180;
            else if (index == 6)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_280_210;
            else if (index == 7)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_320_240;
            else if (index == 8)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_400_300;
            else if (index == 9)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_480_360;
            else if (index == 10)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_640_480;
            else if (index == 11)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_960_720;
            else if (index == 12)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_160_90;
            else if (index == 13)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_256_144;
            else if (index == 14)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_320_180;
            else if (index == 15)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_480_270;
            else if (index == 16)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_640_360;
            else if (index == 17)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_960_540;
            else if (index == 18)
                mEncParam.videoResolution = TRTCVideoResolution.TRTCVideoResolution_1280_720;
            //this.saveBtn.IsEnabled = this.IsChanged();
        }

        private bool IsChanged()
        {
            if (DataManager.GetInstance().videoEncParams.videoResolution != mEncParam.videoResolution)
                return true;
            if (DataManager.GetInstance().videoEncParams.videoFps != mEncParam.videoFps)
                return true;
            if (DataManager.GetInstance().videoEncParams.resMode != mEncParam.resMode)
                return true;
            if (DataManager.GetInstance().videoEncParams.videoBitrate != mEncParam.videoBitrate)
                return true;


            return false;
        }

        private void cameraDeviceComboBox_SelectionChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.cameraDeviceComboBox.Text)) return;
            for (uint i = 0; i < mCameraDeviceList.getCount(); i++)
            {
                if (mCameraDeviceList.getDeviceName(i).Equals(this.cameraDeviceComboBox.Text))
                {
                    mDeviceManager.setCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeCamera, mCameraDeviceList.getDevicePID(i));
                    mMainWindow.OnCameraDeviceChange(mCameraDeviceList.getDevicePID(i));
                }
            }
        }

        private void fpsComboBox_SelectionChanged(object sender, EventArgs e)
        {
            int index = this.fpsComboBox.SelectedIndex;
            switch (index)
            {
                case 0:
                    mEncParam.videoFps = 15;
                    break;
                case 1:
                    mEncParam.videoFps = 20;
                    break;
                case 2:
                    mEncParam.videoFps = 24;
                    break;
            }
            //this.saveBtn.IsEnabled = this.IsChanged();
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            TRTCVideoEncParam encParams = DataManager.GetInstance().videoEncParams;
            TRTCNetworkQosParam qosParams = DataManager.GetInstance().qosParams;
            TRTCAppScene appScene = DataManager.GetInstance().appScene;
            if (encParams.videoResolution != mEncParam.videoResolution || encParams.videoFps != mEncParam.videoFps
                || encParams.videoBitrate != mEncParam.videoBitrate || encParams.resMode != mEncParam.resMode)
            {
                mTRTCCloud.setVideoEncoderParam(ref mEncParam);
            }

            DataManager.GetInstance().videoEncParams = mEncParam;

            this.Close();
        }

        private void resolutionModeComboBox_SelectionChanged(object sender, EventArgs e)
        {
            int index = this.resolutionModeComboBox.SelectedIndex;
            switch (index)
            {
                case 0:
                    mEncParam.resMode = TRTCVideoResolutionMode.TRTCVideoResolutionModeLandscape;
                    break;
                case 1:
                    mEncParam.resMode = TRTCVideoResolutionMode.TRTCVideoResolutionModePortrait;
                    break;
            }
            //this.saveBtn.IsEnabled = this.IsChanged();
        }

        private void bitrateSlider_DragCompleted(object sender, EventArgs e)
        {
            int bitrate = (int)this.bitrateSlider.Value;
            this.bitrateNumLabel.Content = bitrate + " kbps";
            mEncParam.videoBitrate = (uint)bitrate;
            //this.saveBtn.IsEnabled = this.IsChanged();
        }
        #endregion

        #region 音频设置
        private void audioSetting_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            closeMethedPanel.Visibility = Visibility.Hidden;
            videoSettingPanel.Visibility = Visibility.Hidden;
            audioSettingPanel.Visibility = Visibility.Visible;
        }

        private void RefreshMicDeviceList()
        {
            if (mDeviceManager == null) return;
            this.micDeviceComboBox.Items.Clear();
            mMicDeviceList = mDeviceManager.getDevicesList(TRTCDeviceType.TXMediaDeviceTypeMic);
            if (mMicDeviceList.getCount() <= 0)
            {
                this.micDeviceComboBox.Items.Add("");
                this.micDeviceComboBox.SelectedItem = this.micDeviceComboBox.Text.Length;
                return;
            }
            mMicDevice = mDeviceManager.getCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeMic);
            for (uint i = 0; i < mMicDeviceList.getCount(); i++)
            {
                this.micDeviceComboBox.Items.Add(mMicDeviceList.getDeviceName(i));
                if (mMicDevice.getDeviceName().Equals(mMicDeviceList.getDeviceName(i)))
                    this.micDeviceComboBox.SelectedIndex = (int)i;
            }
        }

        private void RefreshSpeakerList()
        {
            if (mDeviceManager == null) return;
            this.speakerDeviceComboBox.Items.Clear();
            mSpeakerDeviceList = mDeviceManager.getDevicesList(TRTCDeviceType.TXMediaDeviceTypeSpeaker);
            if (mSpeakerDeviceList.getCount() <= 0)
            {
                this.speakerDeviceComboBox.Items.Add("");
                this.speakerDeviceComboBox.SelectedItem = this.speakerDeviceComboBox.Text.Length;
                return;
            }
            mSpeakerDevice = mDeviceManager.getCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeSpeaker);
            for (uint i = 0; i < mSpeakerDeviceList.getCount(); i++)
            {
                this.speakerDeviceComboBox.Items.Add(mSpeakerDeviceList.getDeviceName(i));
                if (mSpeakerDevice.getDeviceName().Equals(mSpeakerDeviceList.getDeviceName(i)))
                    this.speakerDeviceComboBox.SelectedIndex = (int)i;
            }
        }

        private void micDeviceComboBox_SelectionChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.micDeviceComboBox.Text)) return;
            for (uint i = 0; i < mMicDeviceList.getCount(); i++)
            {
                if (mMicDeviceList.getDeviceName(i).Equals(this.micDeviceComboBox.Text))
                {
                    mDeviceManager.setCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeMic, mMicDeviceList.getDevicePID(i));
                    mMainWindow.OnMicDeviceChange(mMicDeviceList.getDevicePID(i));
                }
            }
        }

        private void micVolumeSlider_DragCompleted(object sender, EventArgs e)
        {
            mMicVolume = (int)this.micVolumeSlider.Value;
            this.micVolumeNumLabel.Content = mMicVolume + "%";
            mDeviceManager.setCurrentDeviceVolume(TRTCDeviceType.TXMediaDeviceTypeMic, (uint)(mMicVolume * 100 / 100));
        }

        private void speakerDeviceComboBox_SelectionChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.speakerDeviceComboBox.Text)) return;
            for (uint i = 0; i < mSpeakerDeviceList.getCount(); i++)
            {
                if (mSpeakerDeviceList.getDeviceName(i).Equals(this.speakerDeviceComboBox.Text))
                {
                    mDeviceManager.setCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeSpeaker, mSpeakerDeviceList.getDevicePID(i));
                    mMainWindow.OnSpeakerDeviceChange(mSpeakerDeviceList.getDevicePID(i));
                }
            }
        }

        private void speakerVolumeSlider_DragCompleted(object sender, EventArgs e)
        {
            mSpeakerVolume = (int)this.speakerVolumeSlider.Value;
            this.speakerVolumeNumLabel.Content = mSpeakerVolume + "%";
            mDeviceManager.setCurrentDeviceVolume(TRTCDeviceType.TXMediaDeviceTypeSpeaker, (uint)(mSpeakerVolume * 100 / 100));
        }

        private void micTestBtn_Click(object sender, EventArgs e)
        {
            if (this.micTestBtn.Content.Equals("麦克风测试"))
            {
                // 开始麦克风测试
                this.micTestBtn.Content = "停止";

                if (mTRTCCloud != null)
                    mDeviceManager.startMicDeviceTest(200);
            }
            else
            {
                // 停止麦克风测试
                this.micTestBtn.Content = "麦克风测试";
                this.micProgressBar.Value = 0;
                if (mTRTCCloud != null)
                    mDeviceManager.stopMicDeviceTest();
            }
        }

        private void speakerTestBtn_Click(object sender, EventArgs e)
        {
            if (this.speakerTestBtn.Content.Equals("扬声器测试"))
            {
                // 开始扬声器测试
                this.speakerTestBtn.Content = "停止";
                if (mTRTCCloud != null)
                    mDeviceManager.startSpeakerDeviceTest(mTestPath);
            }
            else
            {
                // 停止扬声器测试
                this.speakerTestBtn.Content = "扬声器测试";
                this.speakerProgressBar.Value = 0;
                if (mTRTCCloud != null)
                    mDeviceManager.stopSpeakerDeviceTest();
            }
        }

        public void OnTestMicVolume(uint volume)
        {
            if (this.micTestBtn.Content.Equals("停止"))
            {
                this.micProgressBar.Value = (int)volume;
            }
            else
            {
                this.micProgressBar.Value = 0;
            }
        }

        public void OnTestSpeakerVolume(uint volume)
        {
            if (this.speakerTestBtn.Content.Equals("停止"))
            {
                this.speakerProgressBar.Value = (int)volume;
            }
            else
            {
                this.speakerProgressBar.Value = 0;
            }
        }

        private void OnSystemAudioCheckBoxClick(object sender, EventArgs e)
        {
            if (this.systemAudioCheckBox.IsChecked == true)
            {
                // 这里直接采集操作系统的播放声音，如需采集个别软件的声音请填写对应 exe 的路径。
                mTRTCCloud.startSystemAudioLoopback(null);
            }
            else
            {
                mTRTCCloud.stopSystemAudioLoopback();
            }
        }

        private void aecCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.aecCheckBox.IsChecked == true)
            {
                string api = "{\"api\":\"enableAudioAEC\",\"params\":{\"enable\":1}}";
                mTRTCCloud.callExperimentalAPI(api);
            }
            else
            {
                string api = "{\"api\":\"enableAudioAEC\",\"params\":{\"enable\":0}}";
                mTRTCCloud.callExperimentalAPI(api);
            }
        }

        private void ansCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ansCheckBox.IsChecked == true)
            {
                string api = "{\"api\":\"enableAudioANS\",\"params\":{\"enable\":1}}";
                mTRTCCloud.callExperimentalAPI(api);
            }
            else
            {
                string api = "{\"api\":\"enableAudioANS\",\"params\":{\"enable\":0}}";
                mTRTCCloud.callExperimentalAPI(api);
            }
        }

        private void agcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.agcCheckBox.IsChecked == true)
            {
                string api = "{\"api\":\"enableAudioAGC\",\"params\":{\"enable\":1}}";
                mTRTCCloud.callExperimentalAPI(api);
            }
            else
            {
                string api = "{\"api\":\"enableAudioAGC\",\"params\":{\"enable\":0}}";
                mTRTCCloud.callExperimentalAPI(api);
            }
        }

        private void audioQualityComboBox_SelectionChanged(object sender, EventArgs e)
        {
            switch (this.audioQualityComboBox.SelectedIndex)
            {
                case 0:
                    DataManager.GetInstance().AudioQuality = TRTCAudioQuality.TRTCAudioQualitySpeech;
                    break;
                case 1:
                    DataManager.GetInstance().AudioQuality = TRTCAudioQuality.TRTCAudioQualityDefault;
                    break;
                case 2:
                    DataManager.GetInstance().AudioQuality = TRTCAudioQuality.TRTCAudioQualityMusic;
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
