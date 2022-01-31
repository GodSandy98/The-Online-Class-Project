using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using ManageLiteAV;
using System.Runtime.InteropServices;

using WpfTest.Common;
using System.Windows.Interop;

//用于最小化
using WinForms = System.Windows.Forms;
using System.Windows.Forms;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;

using System.Drawing;
using System.Diagnostics;


namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window, ITRTCCloudCallback, ITRTCLogCallback, ITRTCAudioFrameCallback
    {
        private LocalVedioWhenShare mLocalVideoWhenShare;  //屏幕分享优化时使用
        private SelectWindow mSelectWindow;
        private QuizWindow mQuizWindow;
        private bool mIsStudentShare; //教师端时，标记是否有学生正在进行屏幕共享
        private bool isOwner;
        private string mTeacherName;
        private string mTeacherId;
        DispatcherTimer mCarouselTimer;
        DispatcherTimer mFaceTimer;

        private bool isAudio = false;
        private bool isVideo = false;
        private bool isScreenShare = false;
        private bool isTeacherView = true;

        private bool isClosedtoNotifyicon = false;  //用于区分最小化（任务栏）和关闭按钮（托盘）

        private ITRTCCloud mTRTCCloud;
        private ITXDeviceManager mDeviceManager;

        // 渲染模式：
        // 1 为真窗口渲染（通过窗口句柄传入 SDK），2 为自定义渲染（使用 TXLiteAVVideoView 渲染）
        // 默认为1（真窗口渲染）
        private int mRenderMode = 1;
        private IntPtr mCameraVideoHandle;

        private string mUserId;          // 本地用户 Id
        private uint mRoomId;            // 房间 Id

        private bool mIsSetScreenSuccess;   // 是否设置屏幕参数成功

        private List<UserVideoMeta> mMixStreamVideoMeta;   //混流信息
        private List<RemoteUserInfo> mRemoteUsers;    // 当前房间里的远端用户（除了本地用户）

        // 当前正在使用的摄像头、麦克风和扬声器设备
        private string mCurCameraDevice;
        private string mCurMicDevice;
        private string mCurSpeakerDevice;

        // 日志等级：0 为 不显示仪表盘， 1 为 精简仪表盘， 2 为 完整仪表盘
        private int mLogLevel = 0;

        // 窗口实例
        private UserInfo userInfo;
        private List<ClassInfo> ClassInfos;
        private SettingWindow mSettingWindow;
        private ToastWindow mToastWindow;

        // 检查是否第一次退出房间
        private bool mIsFirstExitRoom;

        //最小化到托盘
        private NotifyIcon notifyIcon = null;


        //构造函数
        public MainWindow(UserInfo userInfo, string classId, SelectWindow selectWindow, Boolean isOpenVideo, Boolean isOpenAudio)
        {
            InitializeComponent();

            mIsStudentShare = false; //当学生进行屏幕分享时，占用teacherpanel句柄，teacher的视频放在新的窗口

            //判断进房是否自动开启音频、视频
            isVideo = isOpenVideo;
            isAudio = isOpenAudio;


            //【数据库】本可以只传入userId，通过查user表获得user全部信息
            this.userInfo = userInfo;

            //【数据库】改为：通过数据库寻找满足传入classId的Class信息
            ClassInfos = DataManager.classInfos;
            ClassInfo mClass = ClassInfos.Find(cls => cls.classId.Equals(classId));

            //【数据库】改为：判断本用户的userId和本教室创建者的userId是否一致来获得isOwner属性
            this.isOwner = false;//是否为房间所有者（其他教师进入时，界面同学生端一致）
            this.mTeacherName = "xxx"; //【数据库】根据teacherId获取当前课堂的TeacherName
            this.mTeacherId = mClass.teacherId;
            if (userInfo.userId.Equals(mTeacherId))
            {
                isOwner = true;
            }

            //最小化到托盘
            InitialTray();

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

            this.Closed += new EventHandler(OnDisposed); //退出销毁资源

            // 初始化数据
            mTRTCCloud = DataManager.GetInstance().trtcCloud;
            mDeviceManager = mTRTCCloud.getDeviceManager();
            mMixStreamVideoMeta = new List<UserVideoMeta>();
            mRemoteUsers = new List<RemoteUserInfo>();

            // 初始化 SDK 配置并设置回调
            Log.I(String.Format(" SDKVersion : {0}", mTRTCCloud.getSDKVersion()));
            mTRTCCloud.addCallback(this);
            mTRTCCloud.setLogCallback(this);
            mTRTCCloud.setConsoleEnabled(true);
            mTRTCCloud.setLogLevel(TRTCLogLevel.TRTCLogLevelVerbose);

            //学生端和教师端区别
            this.teacherInfoForm.Visibility = Visibility.Hidden;
            this.infoForm2.Visibility = Visibility.Hidden;
            this.infoForm3.Visibility = Visibility.Hidden;
            this.infoForm4.Visibility = Visibility.Hidden;

            //初始化窗口
            mSelectWindow = selectWindow;
            mQuizWindow = new QuizWindow();
            mSettingWindow = new SettingWindow(this);
            mLocalVideoWhenShare = new LocalVedioWhenShare(mTeacherName);//屏幕分享时，左上角显示的教师摄像头画面
        }

        /// <summary>
        /// 初始化画面时教师端与学生端区别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isOwner)
            {
                mCameraVideoHandle = teacherVideoPanel.Handle;
                this.form2.Visibility = Visibility.Visible;
                this.form3.Visibility = Visibility.Visible;
                this.form4.Visibility = Visibility.Visible;
                this.chatTextBox.Visibility = Visibility.Hidden;
                this.handsUpForm.Visibility = Visibility.Hidden;
            }
            else
            {
                mCameraVideoHandle = remoteVideoPanel1.Handle;
                this.handsUpForm.Visibility = Visibility.Visible;
                this.form3.Visibility = Visibility.Hidden;
                this.form4.Visibility = Visibility.Hidden;
                this.classOverBtn.Visibility = Visibility.Hidden;

                mFaceTimer = new DispatcherTimer();   //人脸检测的定时器，该算法待优化，暂时关闭入口
                mFaceTimer.Interval = TimeSpan.FromSeconds(20);
                mFaceTimer.Tick += new EventHandler(faceDetection);
                mFaceTimer.Start();
            }
            TextBox messageTextBox = new TextBox();
            messageTextBox.Width = 160;
            messageTextBox.BorderStyle = BorderStyle.FixedSingle;
            this.messagePanel.Controls.Add(messageTextBox);
            PictureBox messagePicBox = new PictureBox();
            messagePicBox.Size = new System.Drawing.Size(25, 25);
            messagePicBox.Image = Properties.Resources.closeMessage;
            messagePicBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.messagePanel.Controls.Add(messagePicBox);
            messagePanel.BorderStyle = BorderStyle.FixedSingle;

            mCarouselTimer = new DispatcherTimer();
            mCarouselTimer.Interval = TimeSpan.FromSeconds(10);
            mCarouselTimer.Tick += new EventHandler(onTimedEvent);
        }

        /// <summary>
        /// 用于判断是否为点击下方任务栏退出
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x112 && ((ushort)wParam & 0xfff0) == 0xf060)
            {
                Console.WriteLine("Close reason: User closing from menu");
                System.Windows.Application.Current.Shutdown();
            }
            return IntPtr.Zero;
        }

        #region MinimizeToTray;

        private void InitialTray()
        {
            //设置托盘的各个属性
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "WpfTest";
            notifyIcon.Icon = new System.Drawing.Icon("../../../MyIcon.ico");
            notifyIcon.Visible = true;
            //notifyIcon.BalloonTipText = "BalloonTipText";
            //notifyIcon.ShowBalloonTip(1000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            //设置菜单项
            WinForms.MenuItem setting1 = new WinForms.MenuItem("setting1");
            WinForms.MenuItem setting2 = new WinForms.MenuItem("setting2");
            WinForms.MenuItem setting = new WinForms.MenuItem("setting", new WinForms.MenuItem[] { setting1, setting2 });

            //帮助选项
            WinForms.MenuItem help = new WinForms.MenuItem("help");

            //关于选项
            WinForms.MenuItem about = new WinForms.MenuItem("about");

            //退出菜单项
            WinForms.MenuItem exit = new WinForms.MenuItem("exit");
            exit.Click += new EventHandler(exit_Click);

            //关联托盘控件
            WinForms.MenuItem[] childen = new WinForms.MenuItem[] { setting, help, about, exit };
            notifyIcon.ContextMenu = new WinForms.ContextMenu(childen);

            //窗体状态改变时候触发
            this.StateChanged += new EventHandler(notifyIcon_StateChanged);
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //如果鼠标左键单击
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                    isClosedtoNotifyicon = false;

                }
            }
        }

        /// <summary>
        /// 窗体状态改变时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_StateChanged(object sender, EventArgs e)
        {

            if (this.WindowState == WindowState.Minimized && isClosedtoNotifyicon == true)
            {
                this.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 退出选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.MessageBox.Show("sure to exit?",
                                               "application",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                // 退出房间
                if (mIsFirstExitRoom) return;
                mIsFirstExitRoom = true;
                //if (mBeautyForm != null)
                //    mBeautyForm.Close();
                //if (mAudioEffectOldForm != null)
                //    mAudioEffectOldForm.Close();
                //if (mAudioEffectForm != null)
                //    mAudioEffectForm.Close();
                //if (mSettingForm != null)
                //    mSettingForm.Close();
                //if (mVedioSettingForm != null)
                //    mVedioSettingForm.Close();
                //if (mAudioSettingForm != null)
                //    mAudioSettingForm.Close();
                //if (mOtherSettingForm != null)
                //    mOtherSettingForm.Close();
                //if (mConnectionForm != null)
                //    mConnectionForm.Close();
                Uninit();
                // 如果进房成功，需要正常退房再关闭窗口，防止资源未清理和释放完毕
                if (DataManager.GetInstance().enterRoom)
                    mTRTCCloud.exitRoom();
                else
                    onExitRoom(0);

                this.Close();
                mSelectWindow.Show();
                //System.Windows.Application.Current.Shutdown();  //无法杀线程
                //Environment.Exit(0);                
            }
        }

        #endregion

        #region titleBar;
        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mSettingWindow != null)
            {

            }
            mSettingWindow = new SettingWindow(this);
            mSettingWindow.ShowDialog();
        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void maxBtn_Click(object sender, RoutedEventArgs e)
        {

            //点击最大化，再次点击后切换图片并回到窗口模式

            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.maxImg.Source = new BitmapImage(new Uri("Normal.png", UriKind.Relative));
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.maxImg.Source = new BitmapImage(new Uri("Max.png", UriKind.Relative));
                this.WindowState = System.Windows.WindowState.Normal;
            }

        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataManager.GetInstance().closeMethod)
            {
                this.Hide();
                this.isClosedtoNotifyicon = true;
            }
            else
            {
                exit_Click(sender, e);
            }

            //this.Close();
            //mSelectWindow.Show();


        }
        #endregion

        /// <summary>
        /// 退出销毁资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            // 清理资源（TRTC原有）
            mTRTCCloud.removeCallback(this);
            mTRTCCloud.setLogCallback(null);
            mTRTCCloud = null;
            mDeviceManager = null;
            //清理资源（新增）
            if(isOwner == false)
                mFaceTimer.Stop();
            notifyIcon.Dispose(); //释放notifyIcon的所有资源，以保证托盘图标在程序关闭时立即消失
        }


        public void EnterRoom()
        {
            // 设置进房所需的相关参数
            TRTCParams trtcParams = new TRTCParams();
            trtcParams.sdkAppId = GenerateTestUserSig.SDKAPPID;
            trtcParams.roomId = DataManager.GetInstance().roomId;
            trtcParams.userId = DataManager.GetInstance().userId;
            trtcParams.strRoomId = DataManager.GetInstance().strRoomId;
            trtcParams.userSig = GenerateTestUserSig.GetInstance().GenTestUserSig(DataManager.GetInstance().userId);
            // 如果您有进房权限保护的需求，则参考文档{https://cloud.tencent.com/document/product/647/32240}完成该功能。
            // 在有权限进房的用户中的下面字段中添加在服务器获取到的privateMapKey。
            trtcParams.privateMapKey = "";
            trtcParams.businessInfo = "";
            trtcParams.role = DataManager.GetInstance().roleType;
            // 若您的项目有纯音频的旁路直播需求，请配置参数。
            // 配置该参数后，音频达到服务器，即开始自动旁路；
            // 否则无此参数，旁路在收到第一个视频帧之前，会将收到的音频包丢弃。
            if (DataManager.GetInstance().pureAudioStyle)
                trtcParams.businessInfo = "{\"Str_uc_params\":{\"pure_audio_push_mod\": 1}}";
            else
                trtcParams.businessInfo = "";

            // 用户进房
            mTRTCCloud.enterRoom(ref trtcParams, DataManager.GetInstance().appScene);

            //如果当前是视频通话模式，默认打开弱网降分辨率
            if (DataManager.GetInstance().appScene == TRTCAppScene.TRTCAppSceneVideoCall)
            {
                DataManager.GetInstance().videoEncParams.enableAdjustRes = true;
            }

            // 设置默认参数配置
            TRTCVideoEncParam encParams = DataManager.GetInstance().videoEncParams;   // 视频编码参数设置
            TRTCNetworkQosParam qosParams = DataManager.GetInstance().qosParams;      // 网络流控相关参数设置
            mTRTCCloud.setVideoEncoderParam(ref encParams);
            mTRTCCloud.setNetworkQosParam(ref qosParams);
            TRTCRenderParams renderParams = DataManager.GetInstance().GetRenderParams();
            mTRTCCloud.setLocalRenderParams(ref renderParams);
            mTRTCCloud.setVideoEncoderMirror(DataManager.GetInstance().isRemoteVideoMirror);

            // 设置美颜
            if (DataManager.GetInstance().isOpenBeauty)
                mTRTCCloud.setBeautyStyle(DataManager.GetInstance().beautyStyle, DataManager.GetInstance().beauty,
                    DataManager.GetInstance().white, DataManager.GetInstance().ruddiness);

            // 设置大小流
            if (DataManager.GetInstance().pushSmallVideo)
            {
                TRTCVideoEncParam param = new TRTCVideoEncParam
                {
                    videoFps = 15,
                    videoBitrate = 100,
                    videoResolution = TRTCVideoResolution.TRTCVideoResolution_320_240
                };
                mTRTCCloud.enableSmallVideoStream(true, ref param);
            }

            // 房间信息
            mUserId = trtcParams.userId;
            mRoomId = trtcParams.roomId;
            this.roomIDLabel.Content = mRoomId;
            this.teacherNameLabel.Content = mTeacherName;
            if (isOwner == false)
            {
                this.remoteUserIdLabel1.Content = mUserId;
                this.remoteNameLabel1.Content = userInfo.userName;
            }
            if (isVideo)
            {
                StartLocalVideo(mCameraVideoHandle);
            }
            else
            {
                //如果进入房间时没开video，则隐藏右侧学生端黑色背景
                this.form2.Visibility = Visibility.Hidden;
                this.infoForm2.Visibility = Visibility.Visible;
                setLocalPreviewPicBoxState(false);
            }

            if (isAudio)
            {
                if (trtcParams.role != TRTCRoleType.TRTCRoleAudience)
                    mTRTCCloud.startLocalAudio(DataManager.GetInstance().AudioQuality);
            }
            else
            {
                setLocalAudioPicBoxState(false);
            }

            InitLocalDevice();
            InitLocalConfig();
        }

        private void StartLocalVideo(IntPtr intPtr)
        {
            mTRTCCloud.startLocalPreview(intPtr);
            //如果有想添加关于renderMode==2的情况，请参考TRTCDemo如何书写
        }

        public void onError(TXLiteAVError errCode, string errMsg, IntPtr arg)
        {
            Log.E(String.Format("errCode : {0}, errMsg : {1}, arg = {2}", errCode, errMsg, arg));
            if (errCode == TXLiteAVError.ERR_SERVER_CENTER_ANOTHER_USER_PUSH_SUB_VIDEO ||
                errCode == TXLiteAVError.ERR_SERVER_CENTER_NO_PRIVILEDGE_PUSH_SUB_VIDEO ||
                errCode == TXLiteAVError.ERR_SERVER_CENTER_INVALID_PARAMETER_SUB_VIDEO)
            {
                ShowMessage("Error: 屏幕分享发起失败，是否当前已经有人发起了共享！");
            }
            else if (errCode == TXLiteAVError.ERR_MIC_START_FAIL || errCode == TXLiteAVError.ERR_CAMERA_START_FAIL ||
                errCode == TXLiteAVError.ERR_SPEAKER_START_FAIL)
            {
                ShowMessage("Error: 请检查本地电脑设备。");
            }
            else
            {
                ShowMessage(String.Format("SDK出错[err:{0},msg:{1}]", errCode, errMsg));
            }
        }

        /// <summary>
        /// 显示 Message 的 Dialog
        /// </summary>
        private void ShowMessage(string text, int delay = 0)
        {
            //判断是否此时该窗口句柄已创建，防止出现问题
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                Dispatcher.Invoke(new Action(() =>
                {
                    MessageWindow msgBox = new MessageWindow();
                    msgBox.setText(text, delay);
                    msgBox.setCancelBtn(false);
                    msgBox.ShowDialog();
                }));
            //解决双人同时屏幕分享，后来者分享失败后ToastWindow显示问题
            if (text.Contains("屏幕分享发起失败"))
            {
                mToastWindow.Hide();
                isScreenShare = false;
                this.screenShareFlagLabel1.Content = "";
                this.screenShareFlagLabel2.Content = "";
                this.screenShareFlagLabel3.Content = "";
            }
        }

        /// <summary>
        /// 初始化本地设备使用
        /// </summary>
        private void InitLocalDevice()
        {
            ITRTCDeviceCollection cameraList = mDeviceManager.getDevicesList(TRTCDeviceType.TXMediaDeviceTypeCamera);
            mCurCameraDevice = "";
            if (cameraList.getCount() > 0)
            {
                ITRTCDeviceInfo camera = mDeviceManager.getCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeCamera);
                mCurCameraDevice = camera.getDevicePID();
            }
            cameraList.release();
            ITRTCDeviceCollection micList = mDeviceManager.getDevicesList(TRTCDeviceType.TXMediaDeviceTypeMic);
            mCurMicDevice = "";
            if (micList.getCount() > 0)
            {
                ITRTCDeviceInfo mic = mDeviceManager.getCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeMic);
                mCurMicDevice = mic.getDevicePID();
            }
            micList.release();
            ITRTCDeviceCollection speakerList = mDeviceManager.getDevicesList(TRTCDeviceType.TXMediaDeviceTypeSpeaker);
            mCurSpeakerDevice = "";
            if (speakerList.getCount() > 0)
            {
                ITRTCDeviceInfo speaker = mDeviceManager.getCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeSpeaker);
                mCurSpeakerDevice = speaker.getDevicePID();
            }
            speakerList.release();
        }

        /// <summary>
        /// 设置本地配置信息到 SDK（包括美颜、镜像、远端混流等功能）如需参考TRTCDemo
        /// </summary>
        private void InitLocalConfig() {}

        public void onWarning(TXLiteAVWarning warningCode, string warningMsg, IntPtr arg)
        {
            Log.I(String.Format("warningCode : {0}, warningMsg : {1}", warningCode, warningMsg));

            if (warningCode == TXLiteAVWarning.WARNING_MICROPHONE_DEVICE_EMPTY)
            {
                ShowMessage("Warning: 未检出到麦克风，请检查本地电脑设备。");
            }
            else if (warningCode == TXLiteAVWarning.WARNING_CAMERA_DEVICE_EMPTY)
            {
                ShowMessage("Warning: 未检出到摄像头，请检查本地电脑设备。");
            }
            else if (warningCode == TXLiteAVWarning.WARNING_SPEAKER_DEVICE_EMPTY)
            {
                ShowMessage("Warning: 未检出到扬声器，请检查本地电脑设备。");
            }
        }

        public void onEnterRoom(int result)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                Dispatcher.Invoke(new Action(() =>
                {
                    if (result >= 0)
                    {
                        // 进房成功
                        DataManager.GetInstance().enterRoom = true;
                        // 确保 SDK 内部的音频和视频采集是开启的。
                        mTRTCCloud.muteLocalVideo(false);
                        mTRTCCloud.muteLocalAudio(false);
                        // 更新混流信息
                        UpdateMixTranCodeInfo();
                    }
                    else
                    {
                        // 进房失败
                        DataManager.GetInstance().enterRoom = false;
                        ShowMessage("进房失败，请重试");
                    }
                }));
        }

        /// <summary>
        /// 更新云端混流界面信息（本地用户进房或远程用户进房或开启本地屏幕共享画面则需要更新）【混流相关】
        /// </summary>
        private void UpdateMixTranCodeInfo()
        {
            // 没有打开云端混流功能则退出
            //if (!this.mixTransCodingCheckBox.Checked)
            //    return;

            // 云端混流的没有辅流界面，则退出（无需混流）
            if (mMixStreamVideoMeta.Count == 0)
            {
                mTRTCCloud.setMixTranscodingConfig(null);
                return;
            }

            // 如果使用的是纯音频进房，则需要混流设置每一路为纯音频，云端会只混流音频数据
            if (DataManager.GetInstance().pureAudioStyle)
            {
                foreach (UserVideoMeta meta in mMixStreamVideoMeta)
                    meta.pureAudio = true;
            }

            // 没有主流，直接停止混流。
            //if (this.muteVideoCheckBox.Checked && this.muteAudioCheckBox.Checked)
            //{
            //    mTRTCCloud.setMixTranscodingConfig(null);
            //    return;
            //}

            // 配置本地主流的混流信息
            UserVideoMeta localMainVideo = new UserVideoMeta()
            {
                userId = mUserId
            };

            // 这里的显示混流的方式只提供参考，如需使用其他方式显示请参考以下方式
            int canvasWidth = 960, canvasHeight = 720;
            //int appId = 1400474658;
            int appId = 0;
            int bizId = 0;

            if (appId == 0 || bizId == 0)
            {
                //ShowMessage("混流功能不可使用，请在TRTCGetUserIDAndUserSig.h->TXCloudAccountInfo填写混流的账号信息\n");
                return;
            }
            TRTCTranscodingConfig config = new TRTCTranscodingConfig();
            config.mode = TRTCTranscodingConfigMode.TRTCTranscodingConfigMode_Manual;
            config.appId = (uint)appId;
            config.bizId = (uint)bizId;
            config.videoWidth = (uint)canvasWidth;
            config.videoHeight = (uint)canvasHeight;
            config.videoBitrate = 800;
            config.videoFramerate = 15;
            config.videoGOP = 1;
            config.audioSampleRate = 48000;
            config.audioBitrate = 64;
            config.audioChannels = 1;
            config.mixUsersArraySize = (uint)(1 + mMixStreamVideoMeta.Count);
            // 设置每一路子画面的位置信息（仅供参考）
            TRTCMixUser[] mixUsersArray = new TRTCMixUser[config.mixUsersArraySize];
            for (int i = 0; i < config.mixUsersArraySize; i++)
                mixUsersArray[i] = new TRTCMixUser();

            int zOrder = 1, index = 0;
            mixUsersArray[index].roomId = null;
            mixUsersArray[index].userId = localMainVideo.userId;
            mixUsersArray[index].pureAudio = localMainVideo.pureAudio;
            RECT rect = new RECT()
            {
                left = 0,
                top = 0,
                right = canvasWidth,
                bottom = canvasHeight
            };
            mixUsersArray[index].rect = rect;
            mixUsersArray[index].streamType = localMainVideo.streamType;
            mixUsersArray[index].zOrder = zOrder++;
            index++;
            foreach (UserVideoMeta info in mMixStreamVideoMeta)
            {
                int left = 20, top = 40;

                if (zOrder == 2)
                {
                    left = 240 / 4 * 3 + 240 * 2;
                    top = 240 / 3 * 1;
                }
                if (zOrder == 3)
                {
                    left = 240 / 4 * 3 + 240 * 2;
                    top = 240 / 3 * 2 + 240 * 1;
                }
                if (zOrder == 4)
                {
                    left = 240 / 4 * 2 + 240 * 1;
                    top = 240 / 3 * 1;
                }
                if (zOrder == 5)
                {
                    left = 240 / 4 * 2 + 240 * 1;
                    top = 240 / 3 * 2 + 240 * 1;
                }
                if (zOrder == 6)
                {
                    left = 240 / 4 * 1;
                    top = 240 / 3 * 1;
                }
                if (zOrder == 7)
                {
                    left = 240 / 4 * 1;
                    top = 240 / 3 * 2 + 240 * 1;
                }

                int right = 240 + left, bottom = 240 + top;
                if (info.roomId <= 0)
                    mixUsersArray[index].roomId = null;
                else
                    mixUsersArray[index].roomId = info.roomId.ToString();
                mixUsersArray[index].userId = info.userId;
                mixUsersArray[index].pureAudio = info.pureAudio;
                RECT rt = new RECT()
                {
                    left = left,
                    top = top,
                    right = right,
                    bottom = bottom
                };
                mixUsersArray[index].rect = rt;
                mixUsersArray[index].streamType = info.streamType;
                mixUsersArray[index].zOrder = zOrder;
                zOrder++;
                index++;
            }
            config.mixUsersArray = mixUsersArray;
            // 设置云端混流配置信息
            mTRTCCloud.setMixTranscodingConfig(config);
        }

        private void Uninit()
        {
            // 如果开启了自定义采集和渲染，则关闭功能，清理资源
            if (DataManager.GetInstance().isLocalVideoMirror && DataManager.GetInstance().isRemoteVideoMirror)
            {
                mTRTCCloud.enableCustomVideoCapture(false);
                mTRTCCloud.enableCustomAudioCapture(false);
            }
            mTRTCCloud.stopAllRemoteView();
            if (mRenderMode == 1)
            {
                mTRTCCloud.stopLocalPreview();
            }

            mTRTCCloud.stopLocalAudio();
            mTRTCCloud.muteLocalAudio(true);
            mTRTCCloud.muteLocalVideo(true);

            // 清理混流信息和用户信息
            mMixStreamVideoMeta.Clear();
            mRemoteUsers.Clear();

            if (this.isScreenShare == true)
            {
                mTRTCCloud.stopScreenCapture();
                if (mToastWindow != null)
                    mToastWindow.Close();
                this.isScreenShare = false;
            }
            //【混流相关】
            //if (this.mixTransCodingCheckBox.Checked)
            //    mTRTCCloud.setMixTranscodingConfig(null);
            
            //当课程结束时，轮播计时器停止
            mCarouselTimer.Stop();
        }

        private void StopLocalVideo()
        {
            mTRTCCloud.stopLocalPreview();
            //如果有想添加关于renderMode==2的情况，请参考TRTCDemo如何书写
        }

        public void onExitRoom(int reason)
        {
            DataManager.GetInstance().enterRoom = false;
            this.Close();
            mFaceTimer.Stop();
            System.Windows.Application.Current.Shutdown();
        }

        public void onSwitchRole(TXLiteAVError errCode, string errMsg)
        {
            Log.I(String.Format("onSwitchRole : errCode = {0}, errMsg = {1}", errCode, errMsg));
        }

        public void onConnectOtherRoom(string userId, TXLiteAVError errCode, string errMsg)
        {
        }

        public void onDisconnectOtherRoom(TXLiteAVError errCode, string errMsg)
        {
        }

        public void onSwitchRoom(TXLiteAVError errCode, string errMsg)
        {
            Log.I(String.Format("onSwitchRoom : errCode = {0}, errMsg = {1}", errCode, errMsg));
        }

        public void onRemoteUserLeaveRoom(string userId, int reason)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    // 清理远端用户退房信息，回收窗口
                    int pos = FindOccupyRemoteVideoPosition(userId, true);
                    if (pos != -1)
                    {
                        SetVisableInfoView(pos, Visibility.Hidden);
                    }
                    if (pos == 111)
                    {
                        // 退出房间
                        if (mIsFirstExitRoom) return;
                        mIsFirstExitRoom = true;
                        Uninit();
                        // 如果进房成功，需要正常退房再关闭窗口，防止资源未清理和释放完毕
                        if (DataManager.GetInstance().enterRoom)
                            mTRTCCloud.exitRoom();
                        else
                            onExitRoom(0);
                        this.Close();
                        mSelectWindow.Show();
                    }
                    foreach (RemoteUserInfo user in mRemoteUsers)
                    {
                        if (user.userId.Equals(userId))
                        {
                            mRemoteUsers.Remove(user);
                            break;
                        }
                    }
                }));
        }

        /// <summary>
        /// 用户是否开启摄像头视频(zx:主流)
        /// </summary>
        public void onUserVideoAvailable(string userId, bool available)
        {
            if (isOwner == false && userId.Equals(mTeacherId) == false) //学生端 且 新进入的人不是教师
            {
                return;
            }
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    // 判断用户是否已经退出房间
                    bool isExit = mRemoteUsers.Exists((user) =>
                    {
                        if (user.userId.Equals(userId)) return true;
                        else return false;
                    });
                    if (!isExit) return;
                    if (available)
                    {
                        if (mRenderMode == 1)
                        {
                            if (isOwner)  //教师端
                            {
                                if (mRemoteUsers.Count > 3)  //zx:只有在教师端 并且 当前学生数量大于3个时 会进行轮播
                                {
                                    carousel();
                                    mCarouselTimer.Start();
                                }
                                else  //当前学生数量小于或等于3个
                                {
                                    int pos = GetIdleRemoteVideoPosition(userId);
                                    if (pos != -1)
                                    {
                                        SetVisableInfoView(pos, Visibility.Hidden);
                                        TRTCVideoStreamType streamType = DataManager.GetInstance().playSmallVideo ?
                                                TRTCVideoStreamType.TRTCVideoStreamTypeSmall : TRTCVideoStreamType.TRTCVideoStreamTypeBig;
                                        IntPtr ptr = GetHandleAndSetUserId(pos, userId);
                                        TRTCRenderParams renderParams = DataManager.GetInstance().GetRenderParams();
                                        mTRTCCloud.setRemoteRenderParams(userId, TRTCVideoStreamType.TRTCVideoStreamTypeBig, ref renderParams);
                                        mTRTCCloud.startRemoteView(userId, streamType, ptr);
                                    }
                                    foreach (RemoteUserInfo info in mRemoteUsers)
                                    {
                                        if (info.userId.Equals(userId))
                                        {
                                            info.position = pos;
                                            break;
                                        }
                                    }
                                }

                            }
                            else  //学生端
                            {
                                TRTCVideoStreamType streamType = DataManager.GetInstance().playSmallVideo ?
                                        TRTCVideoStreamType.TRTCVideoStreamTypeSmall : TRTCVideoStreamType.TRTCVideoStreamTypeBig;
                                SetVisableInfoView(111, Visibility.Hidden);  //111为教师视频panel
                                IntPtr ptr = teacherVideoPanel.Handle;
                                TRTCRenderParams renderParams = DataManager.GetInstance().GetRenderParams();
                                mTRTCCloud.setRemoteRenderParams(userId, TRTCVideoStreamType.TRTCVideoStreamTypeBig, ref renderParams);
                                mTRTCCloud.startRemoteView(userId, streamType, ptr);

                            }
                        }
                    }   //开始远端用户主流画面

                    else
                    {
                        // 移除远端用户主流画面
                        int pos = GetRemoteVideoPosition(userId);
                        if (pos != -1)
                        {
                            SetVisableInfoView(pos, Visibility.Visible);
                            mTRTCCloud.stopRemoteView(userId, TRTCVideoStreamType.TRTCVideoStreamTypeBig);
                            if (isOwner)
                            {
                                if (mRemoteUsers.Count <= 3)
                                    mCarouselTimer.Stop();
                            }
                        }
                    }
                }));
        }

        /// <summary>
        /// zx:dispatcherTimer的tick事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onTimedEvent(object sender, EventArgs e)
        {
            //标记该函数运行
            Console.WriteLine("test_" + DateTime.Now.ToString() + "_" + Thread.CurrentThread.ManagedThreadId.ToString());

            //轮播
            carousel();
        }
        /// <summary>
        /// zx:轮播
        /// </summary>
        private void carousel()
        {
            //轮播
            foreach (RemoteUserInfo info in mRemoteUsers)  //将所有学生的位置信息初始化 移除现有的学生主流画面
            {
                if (info.position != -1)  //轮播时第一次显示
                {
                    int pos = GetRemoteVideoPosition(info.userId);
                    if (pos != -1)
                    {
                        SetVisableInfoView(pos, Visibility.Visible);
                        mTRTCCloud.stopRemoteView(info.userId, TRTCVideoStreamType.TRTCVideoStreamTypeBig);
                        info.position = -1;
                    }
                }
            }

            string[] nextUserid = get3Random();  //即将显示在3个学生界面的userId
            for (int i = 0; i < nextUserid.Length; i++)
            {
                int pos = i + 1;
                SetVisableInfoView(pos, Visibility.Hidden);
                TRTCVideoStreamType streamType = DataManager.GetInstance().playSmallVideo ?
                        TRTCVideoStreamType.TRTCVideoStreamTypeSmall : TRTCVideoStreamType.TRTCVideoStreamTypeBig;

                IntPtr ptr = GetHandleAndSetUserId(pos, nextUserid[i]);
                TRTCRenderParams renderParams = DataManager.GetInstance().GetRenderParams();
                mTRTCCloud.setRemoteRenderParams(nextUserid[i], TRTCVideoStreamType.TRTCVideoStreamTypeBig, ref renderParams);
                mTRTCCloud.startRemoteView(nextUserid[i], streamType, ptr);

                foreach (RemoteUserInfo info in mRemoteUsers)
                {
                    if (info.userId.Equals(nextUserid[i]))
                    {
                        info.position = pos;
                    }
                }//foreach
            }//for
        }

        private string[] get3Random()
        {
            int rm1 = new Random().Next(mRemoteUsers.Count);
            int rm2 = new Random().Next(mRemoteUsers.Count);
            while (mRemoteUsers[rm2].userId.Equals(mRemoteUsers[rm1].userId))
            {
                rm2 = new Random().Next(mRemoteUsers.Count);
            }
            int rm3 = new Random().Next(mRemoteUsers.Count);
            while (mRemoteUsers[rm3].userId.Equals(mRemoteUsers[rm1].userId) || mRemoteUsers[rm3].userId.Equals(mRemoteUsers[rm2].userId))
            {
                rm3 = new Random().Next(mRemoteUsers.Count);
            }
            return new string[3] { mRemoteUsers[rm1].userId, mRemoteUsers[rm2].userId, mRemoteUsers[rm3].userId };
        }

        /// <summary>
        /// 用户是否开启辅流
        /// </summary>
        public void onUserSubStreamAvailable(string userId, bool available)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (available)
                    {
                        // 显示远端辅流界面
                        int pos = GetIdleRemoteVideoPosition(userId);
                        if (pos != -1)
                        {
                            SetVisableInfoView(pos, Visibility.Hidden);
                            if (mRenderMode == 1)
                            {
                                if (isOwner)
                                {
                                    mIsStudentShare = true;
                                    StartVedioinNewPanel();
                                    IntPtr ptr = GetHandleAndSetUserId_ScreenShare(pos, userId);
                                    TRTCRenderParams renderParams = DataManager.GetInstance().GetRenderParams();
                                    mTRTCCloud.setRemoteRenderParams(userId, TRTCVideoStreamType.TRTCVideoStreamTypeSub, ref renderParams);
                                    mTRTCCloud.startRemoteView(userId, TRTCVideoStreamType.TRTCVideoStreamTypeSub, ptr);
                                }
                                else
                                {
                                    teacherViewImg.Visibility = Visibility.Visible;
                                    StartVedioinNewPanel();
                                    IntPtr ptr = this.teacherVideoPanel.Handle;
                                    TRTCRenderParams renderParams = DataManager.GetInstance().GetRenderParams();
                                    mTRTCCloud.setRemoteRenderParams(userId, TRTCVideoStreamType.TRTCVideoStreamTypeSub, ref renderParams);
                                    mTRTCCloud.startRemoteView(userId, TRTCVideoStreamType.TRTCVideoStreamTypeSub, ptr);
                                }
                            }
                        }
                    }
                    else
                    {
                        // 移除远端辅流界面
                        int pos = FindScreenSharePosition(userId, true);
                        if (pos != -1)
                        {
                            if (isOwner)
                            {
                                mIsStudentShare = false;
                            }
                            else
                            {
                                teacherViewImg.Visibility = Visibility.Hidden;
                            }
                            mTRTCCloud.stopRemoteView(userId, TRTCVideoStreamType.TRTCVideoStreamTypeSub);
                            StopVedioinNewPanel();
                        }
                    }
                }));
        }
        /// <summary>
        /// 在新的panel中显示视频 zx
        /// </summary>
        private void StartVedioinNewPanel()
        {
            mLocalVideoWhenShare.Show();
            mLocalVideoWhenShare.Owner = this;
            if (isOwner)
            {
                StopLocalVideo();
                mTRTCCloud.startLocalPreview(mLocalVideoWhenShare.getHandle());
            }
            else
            {
                mTRTCCloud.stopRemoteView(mTeacherId, TRTCVideoStreamType.TRTCVideoStreamTypeBig);  //关闭teacherPanel的主流
                mTRTCCloud.startRemoteView(mTeacherId, TRTCVideoStreamType.TRTCVideoStreamTypeBig, mLocalVideoWhenShare.getHandle());
            }
        }

        /// <summary>
        /// 停止在新的panel中显示视频 zx
        /// </summary>
        private void StopVedioinNewPanel()
        {
            mLocalVideoWhenShare.Hide();
            if (isOwner)
            {
                StopLocalVideo();
                mTRTCCloud.startLocalPreview(teacherVideoPanel.Handle);
            }
            else
            {
                mTRTCCloud.stopRemoteView(mTeacherId, TRTCVideoStreamType.TRTCVideoStreamTypeBig);  //关闭newPanel的主流
                mTRTCCloud.startRemoteView(mTeacherId, TRTCVideoStreamType.TRTCVideoStreamTypeBig, teacherVideoPanel.Handle);  //在teacherPanel重新打开主流
            }
        }

        /// <summary>
        /// 获取主窗口句柄 修改屏幕分享标识 zx
        /// </summary>
        private IntPtr GetHandleAndSetUserId_ScreenShare(int pos, string userId)
        {
            if (isOwner)
            {
                switch (pos)
                {
                    case 1:
                        this.screenShareFlagLabel1.Content = "(屏幕共享)";
                        return this.teacherVideoPanel.Handle;
                    case 2:
                        this.screenShareFlagLabel2.Content = "(屏幕共享)";
                        return this.teacherVideoPanel.Handle;
                    case 3:
                        this.screenShareFlagLabel3.Content = "(屏幕共享)";
                        return this.teacherVideoPanel.Handle;
                    default:
                        return IntPtr.Zero;
                }
            }
            else
            {
                this.screenShareFlagLabel1.Content = "(屏幕共享)";
                return this.teacherVideoPanel.Handle;
            }
        }

        /// <summary>
        /// 用户是否开启音频上行
        /// </summary>
        public void onUserAudioAvailable(string userId, bool available)
        {
            Log.I(String.Format("onUserAudioAvailable : userId = {0}, available = {1}", userId, available));
            mTRTCCloud.setAudioFrameCallback(this);
        }

        /// <summary>
        /// 开始渲染本地或远程用户的首帧画面
        /// </summary>
        public void onFirstVideoFrame(string userId, TRTCVideoStreamType streamType, int width, int height)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                this.teacherVideoPanel.BeginInvoke(new Action(() =>
                {
                    if (isScreenShare == false)
                    {
                        if (string.IsNullOrEmpty(userId) && streamType == TRTCVideoStreamType.TRTCVideoStreamTypeSub)
                            return;
                    }
                    if (!string.IsNullOrEmpty(userId))
                    {
                        // 暂时只支持最多6个人同时视频
                        if (streamType == TRTCVideoStreamType.TRTCVideoStreamTypeBig && FindOccupyRemoteVideoPosition(userId, false) == -1)
                            return;
                        if (streamType == TRTCVideoStreamType.TRTCVideoStreamTypeSub && FindOccupyRemoteVideoPosition(userId + "(屏幕分享)", false) == -1)
                            return;
                    }

                    //这里主要用于添加用户的混流信息（包括本地和远端用户），并实时更新混流信息
                    if (string.IsNullOrEmpty(userId)) userId = mUserId;
                    bool find = false;
                    foreach (UserVideoMeta info in mMixStreamVideoMeta)
                    {
                        if (info.userId.Equals(userId) && info.streamType == streamType)
                        {
                            info.width = width;
                            info.height = height;
                            find = true;
                            break;
                        }
                        else if (info.userId.Equals(userId) && streamType != TRTCVideoStreamType.TRTCVideoStreamTypeSub)
                        {
                            info.streamType = streamType;
                            find = true;
                            break;
                        }
                    }
                    if (!find && !(streamType == TRTCVideoStreamType.TRTCVideoStreamTypeBig && userId == mUserId))
                    {
                        UserVideoMeta info = new UserVideoMeta();
                        info.streamType = streamType;
                        info.userId = userId;
                        info.width = width;
                        info.height = height;
                        mMixStreamVideoMeta.Add(info);
                        UpdateMixTranCodeInfo();
                    }
                    else
                    {
                        if (userId != mUserId)
                            UpdateMixTranCodeInfo();
                    }
                }));
        }

        public void onFirstAudioFrame(string userId)
        {
            Log.I(String.Format("onFirstAudioFrame : userId = {0}", userId));
        }

        public void onSendFirstLocalVideoFrame(TRTCVideoStreamType streamType)
        {
            Log.I(String.Format("onSendFirstLocalVideoFrame : streamType = {0}", streamType));

        }

        public void onSendFirstLocalAudioFrame()
        {
            Log.I(String.Format("onSendFirstLocalAudioFrame"));

        }

        public void onNetworkQuality(TRTCQualityInfo localQuality, TRTCQualityInfo[] remoteQuality, uint remoteQualityCount)
        {

        }

        public void onStatistics(TRTCStatistics statis)
        {
            if (statis.localStatisticsArray != null && statis.localStatisticsArraySize > 0)
            {
                // 从这里记录本地的屏幕分享信息，实时更新混流
                TRTCLocalStatistics[] localStatisticsArray = statis.localStatisticsArray;
                for (int i = 0; i < statis.localStatisticsArraySize; i++)
                {
                    if (localStatisticsArray[i].streamType == TRTCVideoStreamType.TRTCVideoStreamTypeSub)
                    {
                        int width = (int)localStatisticsArray[i].width;
                        int height = (int)localStatisticsArray[i].height;
                        TRTCVideoStreamType streamType = localStatisticsArray[i].streamType;
                        onFirstVideoFrame(null, TRTCVideoStreamType.TRTCVideoStreamTypeSub, width, height);
                    }
                }
            }
        }

        public void onConnectionLost()
        {
            Log.I(String.Format("onConnectionLost"));
            ShowMessage("网络异常，请重试");
        }

        public void onTryToReconnect()
        {
            Log.I(String.Format("onTryToReconnect"));
            ShowMessage("尝试重进房...");
        }

        public void onConnectionRecovery()
        {
            Log.I(String.Format("onConnectionRecovery"));
            ShowMessage("网络恢复，重进房成功");
        }

        

        public void onSpeedTest(TRTCSpeedTestResult currentResult, uint finishedCount, uint totalCount)
        {
            Log.I(String.Format(@"onSpeedTest : currentResult.ip = {0}, currentResult.quality = {1}, 
                currentResult.upLostRate = {2}, currentResult.downLostRate = {3}, currentResult.rtt = {4}, 
                finishedCount = {5}, totalCount = {6}", currentResult.ip, currentResult.quality, currentResult.upLostRate,
                currentResult.downLostRate, currentResult.rtt, finishedCount, totalCount));
        }

        public void onCameraDidReady()
        {
            Log.I(String.Format("onCameraDidReady"));
            // 实时获取当前使用的摄像头设备信息
            if (mTRTCCloud != null)
                mCurCameraDevice = mDeviceManager.getCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeCamera).getDevicePID();

        }

        public void onMicDidReady()
        {
            Log.I(String.Format("onMicDidReady"));
            // 实时获取当前使用的麦克风设备信息
            if (mTRTCCloud != null)
                mCurMicDevice = mDeviceManager.getCurrentDevice(TRTCDeviceType.TXMediaDeviceTypeMic).getDevicePID();

        }

        /// <summary>
        /// 用于提示音量大小的回调,包括每个 userId 的音量和远端总音量
        /// </summary>
        public void onUserVoiceVolume(TRTCVolumeInfo[] userVolumes, uint userVolumesCount, uint totalVolume)
        {
        }

        public void onDeviceChange(string deviceId, TRTCDeviceType type, TRTCDeviceState state)
        {
            //throw new NotImplementedException();
            // 实时监控本地设备的拔插
            // SDK 内部已支持摄像头、麦克风以及扬声器拔插时判断是否需要自动打开或切换，外部无需做处理
            Log.I(String.Format("onDeviceChange : deviceId = {0}, type = {1}, state = {2}", deviceId, type, state));
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                Dispatcher.Invoke(new Action(() =>
                {
                    if (type == TRTCDeviceType.TXMediaDeviceTypeCamera)
                    {
                        if (mSettingWindow != null)
                            mSettingWindow.OnDeviceChange(deviceId, type, state);
                    }
                    else if (type == TRTCDeviceType.TXMediaDeviceTypeMic || type == TRTCDeviceType.TXMediaDeviceTypeSpeaker)
                    {
                        if (mSettingWindow != null)
                            mSettingWindow.OnDeviceChange(deviceId, type, state);
                    }
                }));
        }

        /// <summary>
        /// 测试麦克风设备的音量回调
        /// </summary>
        public void onTestMicVolume(uint volume)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                Dispatcher.Invoke(new Action(() =>
                {
                    if (mSettingWindow != null)
                        mSettingWindow.OnTestMicVolume(volume);
                }));
        }

        /// <summary>
        /// 测试扬声器设备的音量回调
        /// </summary>
        public void onTestSpeakerVolume(uint volume)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                Dispatcher.Invoke(new Action(() =>
                {
                    if (mSettingWindow != null)
                        mSettingWindow.OnTestSpeakerVolume(volume);
                }));
        }

        public void onAudioDeviceCaptureVolumeChanged(uint volume, bool muted)
        {
            Log.I(String.Format("onAudioDeviceCaptureVolumeChanged : volume = {0}, muted = {1}", volume, muted));
        }

        public void onAudioDevicePlayoutVolumeChanged(uint volume, bool muted)
        {
            Log.I(String.Format("onAudioDevicePlayoutVolumeChanged : volume = {0}, muted = {1}", volume, muted));
        }

        public void onRecvCustomCmdMsg(string userId, int cmdId, uint seq, byte[] msg, uint msgSize)
        {
            Log.I(String.Format("onRecvCustomCmdMsg : userId = {0}, cmdId = {1}, seq = {2}, msg = {3}, msgSize = {4}", userId, cmdId, seq, msg, msgSize));
        }

        public void onMissCustomCmdMsg(string userId, int cmdId, int errCode, int missed)
        {
            Log.I(String.Format("onMissCustomCmdMsg : userId = {0}, cmdId = {1}, errCode = {2}, missed = {3}", userId, cmdId, errCode, missed));
        }

        public void onRecvSEIMsg(string userId, byte[] message, uint msgSize)
        {
            Log.I(String.Format("onRecvSEIMsg : userId = {0}, message = {1}, msgSize = {2}", userId, message, msgSize));
        }

        public void onStartPublishing(int errCode, string errMsg)
        {
            throw new NotImplementedException();
        }

        public void onStopPublishing(int errCode, string errMsg)
        {
            throw new NotImplementedException();
        }

        public void onStartPublishCDNStream(int errCode, string errMsg)
        {
            throw new NotImplementedException();
        }

        public void onStopPublishCDNStream(int errCode, string errMsg)
        {
            throw new NotImplementedException();
        }

        public void onSetMixTranscodingConfig(int errCode, string errMsg)
        {

        }

        public void onScreenCaptureCovered()
        {
            Log.I(String.Format("onScreenCaptureCovered"));
        }

        public void onScreenCaptureStarted()
        {
            Log.I(String.Format("onScreenCaptureStarted"));
        }

        public void onScreenCapturePaused(int reason)
        {
            Log.I(String.Format("onScreenCapturePaused : reason = {0}", reason));
        }

        public void onScreenCaptureResumed(int reason)
        {
            Log.I(String.Format("onScreenCaptureResumed : reason = {0}", reason));
        }

        public void onScreenCaptureStoped(int reason)
        {
            Log.I(String.Format("onScreenCaptureStoped : reason = {0}", reason));
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    //if (mToastWindow != null && mToastWindow.IsVisible) { }
                    mToastWindow.Hide();
                    this.screenShareFlagLabel1.Content = "";
                    this.screenShareFlagLabel2.Content = "";
                    this.screenShareFlagLabel3.Content = "";
                    this.isScreenShare = false;
                }));
        }
        /// <summary>
        /// 注意：该接口已被废弃，不推荐使用（保持空实现），请使用 onRemoteUserEnterRoom 替代。
        /// </summary>
        public void onUserEnter(string userId) { }

        /// <summary>
        /// 注意：该接口已被废弃，不推荐使用（保持空实现），请使用 onRemoteUserLeaveRoom 替代。
        /// </summary>
        public void onUserExit(string userId, int reason) { }

        public void onRemoteUserEnterRoom(string userId)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            if (hwnd.ToInt64() != 0)
                Dispatcher.Invoke(new Action(() =>
                {
                    // 添加远端进房用户信息
                    mRemoteUsers.Add(new RemoteUserInfo() { userId = userId, position = -1 });
                    OnPKUserEnter(userId);
                }));
        }

        /// <summary>
        /// 远端连麦用户进房通知
        /// </summary>
        private void OnPKUserEnter(string userId){}

        public void onAudioEffectFinished(int effectId, int code)
        {
            throw new NotImplementedException();
        }

        public void onPlayBGMBegin(TXLiteAVError errCode)
        {
            Log.I(String.Format("onPlayBGMBegin : errCode = {0}", errCode));
        }

        public void onPlayBGMProgress(uint progressMS, uint durationMS)
        {
            Log.I(String.Format("onPlayBGMProgress : progressMs = {0}, durationMS = {1}", progressMS, durationMS));
        }

        public void onPlayBGMComplete(TXLiteAVError errCode)
        {
        }

        public void onLog(string log, TRTCLogLevel level, string module)
        {
            // SDK 内部日志显示
            Log.I(String.Format("onLog : log = {0}, level = {1}, module = {2}", log, level, module));
        }

        public void onCapturedAudioFrame(TRTCAudioFrame frame)
        {
        }

        public void onPlayAudioFrame(TRTCAudioFrame frame, string userId)
        {
            Log.I(String.Format("onPlayAudioFrame : userId = {0}", userId));
        }

        public void onMixedPlayAudioFrame(TRTCAudioFrame frame)
        {
        }

        public void OnSetScreenParamsCallback(bool success)
        {
            mIsSetScreenSuccess = success;
            if (success)
            {
                if (mTRTCCloud != null)
                {
                    mTRTCCloud.startScreenCapture(IntPtr.Zero, TRTCVideoStreamType.TRTCVideoStreamTypeSub, null);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        if (mToastWindow == null)
                            mToastWindow = new ToastWindow(mTRTCCloud, this);
                        mToastWindow.SetText(userInfo.userName + " 正在屏幕共享");
                        mToastWindow.Show();
                        this.isScreenShare = true;
                    }));
                }
            }
            else
            {
                this.isScreenShare = false;
            }
        }

        /// <summary>
        /// zx : 返回学生的第pos个窗口句柄，并显示对应的id和name
        /// </summary>
        private IntPtr GetHandleAndSetUserId(int pos, string userId)
        {
            switch (pos)
            {
                case 1:
                    this.remoteUserIdLabel1.Content = userId;
                    this.remoteNameLabel1.Content = "【待】数据库获得";   //【数据库】通过userID获得username
                    return this.remoteVideoPanel1.Handle;
                case 2:
                    this.remoteUserIdLabel2.Content = userId;
                    this.remoteNameLabel2.Content = "【待】数据库获得";
                    return this.remoteVideoPanel2.Handle;
                case 3:
                    this.remoteUserIdLabel3.Content = userId;
                    this.remoteNameLabel3.Content = "【待】数据库获得";
                    return this.remoteVideoPanel3.Handle;
                default:
                    return IntPtr.Zero;
            }
        }


        /// <summary>
        /// 获取远端用户的的窗口位置
        /// </summary>
        private int GetRemoteVideoPosition(string userId)
        {
            if (this.mTeacherId == userId)
            {
                return 111;
            }
            else if (this.remoteUserIdLabel1.Content.Equals(userId))
                return 1;
            else if (this.remoteUserIdLabel2.Content.Equals(userId))
                return 2;
            else if (this.remoteUserIdLabel3.Content.Equals(userId))
                return 3;
            return -1;
        }

        /// <summary>
        /// 获取空闲窗口的位置
        /// </summary>
        private int GetIdleRemoteVideoPosition(String userId)
        {
            if (string.IsNullOrEmpty(this.remoteUserIdLabel1.Content.ToString()) || this.remoteUserIdLabel1.Content.Equals(userId))
                return 1;
            else if (string.IsNullOrEmpty(this.remoteUserIdLabel2.Content.ToString()) || this.remoteUserIdLabel2.Content.Equals(userId))
                return 2;
            else if (string.IsNullOrEmpty(this.remoteUserIdLabel3.Content.ToString()) || this.remoteUserIdLabel3.Content.Equals(userId))
                return 3;
            return -1;
        }

        /// <summary>
        /// 是否显示提示远端用户是否打开视频的画面
        /// </summary>
        private void SetVisableInfoView(int pos, Visibility visable)
        {
            switch (pos)
            {
                case 111:
                    this.teacherInfoForm.Visibility = visable;
                    break;
                case 1:
                    this.infoForm2.Visibility = visable;
                    break;
                case 2:
                    this.infoForm3.Visibility = visable;
                    break;
                case 3:
                    this.infoForm4.Visibility = visable;
                    break;
            }
        }



        /// <summary>
        /// 根据用户是否退房找到用户画面当前窗口的位置
        /// </summary>
        private int FindOccupyRemoteVideoPosition(string userId, bool isExitRoom)
        {
            int pos = -1;
            if (mTeacherId.Equals(userId))
            {
                pos = 111;
                if (isExitRoom)
                {
                    SetVisableInfoView(pos, Visibility.Hidden);
                }

            }
            if (this.remoteUserIdLabel1.Content.Equals(userId))
            {
                pos = 1;
                if (isExitRoom)
                {
                    this.remoteUserIdLabel1.Content = "";
                    this.remoteNameLabel1.Content = "";
                }

            }
            if (this.remoteUserIdLabel2.Content.Equals(userId))
            {
                pos = 2;
                if (isExitRoom)
                {
                    this.remoteUserIdLabel2.Content = "";
                    this.remoteNameLabel2.Content = "";
                }
            }
            if (this.remoteUserIdLabel3.Content.Equals(userId))
            {
                pos = 3;
                if (isExitRoom)
                {
                    this.remoteUserIdLabel3.Content = "";
                    this.remoteNameLabel3.Content = "";
                }
            }
            if (isExitRoom)
                SetVisableInfoView(pos, Visibility.Hidden);
            return pos;
        }

        /// <summary>
        /// 返回正在进行屏幕分享的用户的位置 zx
        /// </summary>
        private int FindScreenSharePosition(string userId, bool isExitRoom)
        {
            int pos = -1;
            if (isOwner)
            {
                if (this.remoteUserIdLabel1.Content.Equals(userId))
                {
                    pos = 1;
                    if (isExitRoom)
                        this.screenShareFlagLabel1.Content = "";
                }
                if (this.remoteUserIdLabel2.Content.Equals(userId))
                {
                    pos = 2;
                    if (isExitRoom)
                        this.screenShareFlagLabel2.Content = "";
                }
                if (this.remoteUserIdLabel3.Content.Equals(userId))
                {
                    pos = 3;
                    if (isExitRoom)
                        this.screenShareFlagLabel3.Content = "";
                }
                if (isExitRoom)
                    SetVisableInfoView(pos, Visibility.Hidden);
                return pos;
            }
            else
            {
                pos = 111;   //111代表教师
                return pos;
            }
        }

        /// <summary>
        /// 打开仪表盘信息（自定义渲染下由于不是使用真窗口渲染，所以暂时无法显示仪表盘信息）
        /// </summary>
        private void OnLogCheckBoxClick(object sender, RoutedEventArgs e)
        {
            mLogLevel++;
            int style = mLogLevel % 3;
            if (style > 0)
            {
                this.logCheckBox.IsChecked = true;
            }
            else
            {
                this.logCheckBox.IsChecked = false;
            }
            if (mTRTCCloud != null)
            {
                mTRTCCloud.showDebugView(style);
            }
        }

        #region 功能图标

        private void startLocalAudioImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!DataManager.GetInstance().enterRoom)
            {
                ShowMessage("进房失败，请重试");
                //如果需要设置默认进房音频模式 在此进行设置
                isAudio = false;
                return;
            }
            if (isAudio)
            {
                setLocalAudioPicBoxState(false);
                mTRTCCloud.stopLocalAudio();
                isAudio = false;
            }
            else
            {
                setLocalAudioPicBoxState(true);
                mTRTCCloud.startLocalAudio(DataManager.GetInstance().AudioQuality);
                isAudio = true;
            }
        }
        /// <summary>
        /// 更改图片_audio 
        /// </summary>
        /// <param name="state"></param>
        private void setLocalAudioPicBoxState(Boolean state)
        {
            if (state == true)
            {
                isAudio = true;
                startLocalAudioImg.Source = new ImageSourceConverter().ConvertFromString("../../../Images/audioOpen.png") as ImageSource;
            }
            else
            {
                isAudio = false;
                startLocalAudioImg.Source = new ImageSourceConverter().ConvertFromString("../../../Images/audioClose.png") as ImageSource;
            }
        }

        private void startLocalPreviewImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!DataManager.GetInstance().enterRoom)
            {
                ShowMessage("进房失败，请重试");
                this.isVideo = false;
                return;
            }
            //if (DataManager.GetInstance().pureAudioStyle)
            //{
            //    ShowMessage("Error: 纯音频场景，无法打开视频，请退房重新选择模式");
            //    this.isVideo = false;
            //    return;
            //}

            if (isVideo == false)
            {
                setLocalPreviewPicBoxState(true);
                if (isOwner)
                {
                    if (mIsStudentShare)
                    {
                        StartLocalVideo(this.mLocalVideoWhenShare.getHandle());   //在小窗口开始视频
                        mLocalVideoWhenShare.Show();
                    }
                    else
                    {
                        StartLocalVideo(mCameraVideoHandle);
                        this.form1.Visibility = Visibility.Visible;
                        this.teacherInfoForm.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    StartLocalVideo(mCameraVideoHandle);
                    this.form2.Visibility = Visibility.Visible;
                    this.infoForm2.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                StopLocalVideo();
                setLocalPreviewPicBoxState(false);
                if (isOwner)
                {
                    if (mIsStudentShare)
                    {
                        mLocalVideoWhenShare.Hide(); //关闭小窗口
                    }
                    else
                    {
                        this.form1.Visibility = Visibility.Hidden;
                        this.teacherInfoForm.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    this.form2.Visibility = Visibility.Hidden;
                    this.infoForm2.Visibility = Visibility.Visible;
                }
            }


        }
        /// <summary>
        /// 更改图片_video 
        /// </summary>
        /// <param name="state"></param>
        private void setLocalPreviewPicBoxState(Boolean state)
        {
            if (state == true)
            {
                isVideo = true;
                startLocalPreviewImg.Source = new BitmapImage(new Uri("pack://application:,,,/Images/videoOpen.png"));
            }
            else
            {
                isVideo = false;
                startLocalPreviewImg.Source = new BitmapImage(new Uri("pack://application:,,,/Images/videoClose.png"));
            }
        }

        private void screenShareImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!DataManager.GetInstance().enterRoom)
            {
                ShowMessage("进房失败，请重试");
                //this.screenShareCheckBox.IsChecked = false;
                return;
            }
            if (isScreenShare == false)
            {
                // 开启屏幕分享功能
                ScreenWindow screenForm = new ScreenWindow(this);
                screenForm.ShowDialog();
                if (mIsSetScreenSuccess)
                {
                    isScreenShare = true;

                    if (isOwner)
                    {
                        //将对话框和举手提示置于最前

                    }
                    else
                    {
                        this.screenShareFlagLabel1.Content = "(屏幕分享)";
                    }
                }
            }
            else
            {
                // 关闭屏幕分享功能
                if (!mIsSetScreenSuccess)
                {
                    isScreenShare = false;
                    return;
                }
                mTRTCCloud.stopScreenCapture();
                if (mToastWindow != null)
                    mToastWindow.Hide();
                isScreenShare = false;
                if (isOwner)
                {
                    //将对话框和举手提示恢复
                }
                else
                {
                    this.screenShareFlagLabel1.Content = "";
                }
                //    // 移除混流中的屏幕分享画面
                //    //RemoveVideoMeta(mUserId, TRTCVideoStreamType.TRTCVideoStreamTypeSub);
                //    UpdateMixTranCodeInfo();
            }
        }

        private void teacherViewImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isTeacherView == false)
            {
                isTeacherView = true;
                setTeacherViewPicBoxState(true);
                mLocalVideoWhenShare.Show();
            }
            else
            {
                isTeacherView = false;
                setTeacherViewPicBoxState(false);
                mLocalVideoWhenShare.Hide();
            }
        }

        /// <summary>
        /// 更改图片_teacherView
        /// </summary>
        /// <param name="state"></param>
        private void setTeacherViewPicBoxState(Boolean state)
        {
            if (state == true)
            {
                isTeacherView = true;
                teacherViewImg.Source = new ImageSourceConverter().ConvertFromString("../../../Images/teacherView.png") as ImageSource;
            }
            else
            {
                isTeacherView = false;
                teacherViewImg.Source = new ImageSourceConverter().ConvertFromString("../../../Images/noTeacherView.png") as ImageSource;
            }
        }

        private void quizImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mQuizWindow != null)
            {

            }
            mQuizWindow = new QuizWindow();
            mQuizWindow.ShowDialog();
        }

        #endregion

        #region 音视频设置相关
        public void OnCameraDeviceChange(string deviceName)
        {
            mCurCameraDevice = deviceName;
        }

        public void OnMicDeviceChange(string deviceName)
        {
            mCurMicDevice = deviceName;
        }

        public void OnSpeakerDeviceChange(string deviceName)
        {
            mCurSpeakerDevice = deviceName;
        }
        #endregion

        private void classOverBtn_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("是否确定下课?",
                                                  "下课",
                                                   MessageBoxButton.YesNo,
                                                   MessageBoxImage.Question,
                                                   MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                // 退出房间
                if (mIsFirstExitRoom) return;
                mIsFirstExitRoom = true;
                Uninit();
                // 如果进房成功，需要正常退房再关闭窗口，防止资源未清理和释放完毕
                if (DataManager.GetInstance().enterRoom)
                    mTRTCCloud.exitRoom();
                else
                    onExitRoom(0);
                this.Close();
                mSelectWindow.Show();
            }

        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void faceDetection(object sender, EventArgs e)
        {
            string folderPath = "..\\..\\..\\screenshot\\";
            new Action(() =>
            {
                //耗时非UI操作放这　　
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //操作UI（异步）
                    screenshot(folderPath);
                }));
                //耗时非UI操作放这
                test(folderPath);
            }).BeginInvoke(null, null);

        }

        private void screenshot(string folderPath)
        {
            Bitmap localVideoScreenShot = new Bitmap(this.remoteVideoPanel1.Width, this.remoteVideoPanel1.Height);
            Graphics graph = Graphics.FromImage(localVideoScreenShot);
            graph.CopyFromScreen(this.remoteVideoPanel1.PointToScreen(System.Drawing.Point.Empty), System.Drawing.Point.Empty, localVideoScreenShot.Size);
            localVideoScreenShot.Save(folderPath + "localCapture.jpg");
        }

        private void test(string folderPath)
        {
            Process p = new Process();
            p.StartInfo.FileName = folderPath + "FaceDetection.exe";//需要执行的文件路径
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();//关键，等待外部程序退出后才能往下执行
            System.Windows.MessageBox.Show(output);
            p.Close();
        }
    }
}
