using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using ManageLiteAV;

using WpfTest.Common;

namespace WpfTest
{
    /// <summary>
    /// SDK 的 Local 数据信息，使用 ini 本地存储
    /// </summary>
    class DataManager : IDisposable
    {
        public const string INI_ROOT_KEY = "TRTCDemo";
        //ZX:设置
        public const string INI_KEY_CLOSE_METHOD = "INI_KEY_CLOSE_METHOD";
        public const string INI_KEY_PASSWORD = "INI_KEY_PASSWORD";
        // 用户
        public const string INI_KEY_USER_ID = "INI_KEY_USER_ID";
        public const string INI_KEY_ROOM_ID = "INI_KEY_ROOM_ID";
        public const string INI_KEY_ROLE_TYPE = "INI_KEY_ROLE_TYPE";
        // 设备
        public const string INI_KEY_CHOOSE_CAMERA = "INI_KEY_CHOOSE_CAMERA";
        public const string INI_KEY_CHOOSE_SPEAK = "INI_KEY_CHOOSE_SPEAK";
        public const string INI_KEY_CHOOSE_MIC = "INI_KEY_CHOOSE_MIC";
        // 视频
        public const string INI_KEY_VIDEO_BITRATE = "INI_KEY_VIDEO_BITRATE";
        public const string INI_KEY_VIDEO_RESOLUTION = "INI_KEY_VIDEO_RESOLUTION";
        public const string INI_KEY_VIDEO_RES_MODE = "INI_KEY_VIDEO_RES_MODE";
        public const string INI_KEY_VIDEO_FPS = "INI_KEY_VIDEO_FPS";
        public const string INI_KEY_VIDEO_QUALITY = "INI_KEY_VIDEO_QUALITY";
        public const string INI_KEY_VIDEO_QUALITY_CONTROL = "INI_KEY_VIDEO_QUALITY_CONTROL";
        public const string INI_KEY_VIDEO_APP_SCENE = "INI_KEY_VIDEO_APP_SCENE";
        public const string INI_KEY_VIDEO_FILL_MODE = "INI_KEY_VIDEO_FILL_MODE";
        public const string INI_KEY_VIDEO_ROTATION = "INI_KEY_VIDEO_ROTATION";
        // 音频
        public const string INI_KEY_AUDIO_MIC_VOLUME = "INI_KEY_AUDIO_MIC_VOLUME";
        public const string INI_KEY_AUDIO_SPEAKER_VOLUME = "INI_KEY_AUDIO_SPEAKER_VOLUME";
        public const string INI_KEY_AUDIO_SAMPLERATE = "INI_KEY_AUDIO_SAMPLERATE";
        public const string INI_KEY_AUDIO_CHANNEL = "INI_KEY_AUDIO_CHANNEL";
        public const string INI_KEY_AUDIO_QUALITY = "INI_KEY_AUDIO_QUALITY";
        // 美颜
        public const string INI_KEY_BEAUTY_OPEN = "INI_KEY_BEAUTY_OPEN";
        public const string INI_KEY_BEAUTY_STYLE = "INI_KEY_BEAUTY_STYLE";
        public const string INI_KEY_BEAUTY_VALUE = "INI_KEY_BEAUTY_VALUE";
        public const string INI_KEY_WHITE_VALUE = "INI_KEY_WHITE_VALUE";
        public const string INI_KEY_RUDDINESS_VALUE = "INI_KEY_RUDDINESS_VALUE";
        // 大小流
        public const string INI_KEY_SET_PUSH_SMALLVIDEO = "INI_KEY_SET_PUSH_SMALLVIDEO";
        public const string INI_KEY_SET_PLAY_SMALLVIDEO = "INI_KEY_SET_PLAY_SMALLVIDEO";
        // 测试
        public const string INI_KEY_SET_NETENV_STYLE = "INI_KEY_SET_NETENV_STYLE";
        public const string INI_KEY_ROOMCALL_STYLE = "INI_KEY_ROOMCALL_STYLE";
        // 镜像
        public const string INI_KEY_LOCAL_VIDEO_MIRROR = "INI_KEY_LOCAL_VIDEO_MIRROR";
        public const string INI_KEY_REMOTE_VIDEO_MIRROR = "INI_KEY_REMOTE_VIDEO_MIRROR";
        // 音量提示
        public const string INI_KEY_SHOW_AUDIO_VOLUME = "INI_KEY_SHOW_AUDIO_VOLUME";
        // 混流
        public const string INI_KEY_CLOUD_MIX_TRANSCODING = "INI_KEY_CLOUD_MIX_TRANSCODING";

        static public List<ClassInfo> classInfos = new List<ClassInfo>();
        static public List<LessonInfo> lessonInfos = new List<LessonInfo>();
        static public List<UserInfo> userInfos = new List<UserInfo>();
        static public List<CSInfo> csInfos = new List<CSInfo>();
        static public List<QuizSheet> QuizInfos = new List<QuizSheet>();
        static public List<TimeSheet> timeSheetInfos = new List<TimeSheet>();


        private IniStorage storage;

        public ITRTCCloud trtcCloud;

        private DataManager()
        {
            trtcCloud = ITRTCCloud.getTRTCShareInstance();

            storage = new IniStorage(".\\TRTCDemo.ini");

            videoEncParams = new TRTCVideoEncParam();
            qosParams = new TRTCNetworkQosParam();
        }

        #region Disposed
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (trtcCloud != null)
            {
                ITRTCCloud.destroyTRTCShareInstance();
                trtcCloud.Dispose();
                trtcCloud = null;
            }
            disposed = true;
        }

        ~DataManager()
        {
            Dispose(false);
        }
        #endregion

        public void InitConfig()
        {
            //ZX：设置
            //关闭方式：false：不隐藏到托盘，true：隐藏到托盘，默认false
            string closeMethod = storage.GetValue(INI_ROOT_KEY, INI_KEY_CLOSE_METHOD);
            if (string.IsNullOrEmpty(closeMethod))
                this.closeMethod = false;
            else
                this.closeMethod = Convert.ToBoolean(closeMethod);

            string password = storage.GetValue(INI_ROOT_KEY, INI_KEY_PASSWORD);
            if (string.IsNullOrEmpty(password))
                this.password = "";
            else
                this.password = password;

            // 用户信息配置
            string userId = storage.GetValue(INI_ROOT_KEY, INI_KEY_USER_ID);
            if (string.IsNullOrEmpty(userId))
                this.userId = "";
            else
                this.userId = userId;
            string roomId = storage.GetValue(INI_ROOT_KEY, INI_KEY_ROOM_ID);
            if (string.IsNullOrEmpty(roomId))
                this.roomId = uint.Parse(Util.GetRandomString(3));
            else
                this.roomId = uint.Parse(roomId);
            string role = storage.GetValue(INI_ROOT_KEY, INI_KEY_ROLE_TYPE);
            if (string.IsNullOrEmpty(role))
                this.roleType = TRTCRoleType.TRTCRoleAnchor;
            else
                this.roleType = (TRTCRoleType)(int.Parse(role));

            // 视频参数配置
            string param;
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_BITRATE);
            if (string.IsNullOrEmpty(param))
                this.videoEncParams.videoBitrate = 550;
            else
                this.videoEncParams.videoBitrate = uint.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_FPS);
            if (string.IsNullOrEmpty(param))
                this.videoEncParams.videoFps = 15;
            else
                this.videoEncParams.videoFps = uint.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_RESOLUTION);
            if (string.IsNullOrEmpty(param))
                this.videoEncParams.videoResolution = TRTCVideoResolution.TRTCVideoResolution_640_360;
            else
                this.videoEncParams.videoResolution = (TRTCVideoResolution)int.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_RES_MODE);
            if (string.IsNullOrEmpty(param))
                this.videoEncParams.resMode = TRTCVideoResolutionMode.TRTCVideoResolutionModeLandscape;
            else
                this.videoEncParams.resMode = (TRTCVideoResolutionMode)int.Parse(param);

            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_QUALITY);
            if (string.IsNullOrEmpty(param))
                this.qosParams.preference = TRTCVideoQosPreference.TRTCVideoQosPreferenceClear;
            else
                this.qosParams.preference = (TRTCVideoQosPreference)int.Parse(param);

            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_QUALITY_CONTROL);
            if (string.IsNullOrEmpty(param))
                this.qosParams.controlMode = TRTCQosControlMode.TRTCQosControlModeServer;
            else
                this.qosParams.controlMode = (TRTCQosControlMode)int.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_APP_SCENE);
            if (string.IsNullOrEmpty(param))
                this.appScene = TRTCAppScene.TRTCAppSceneVideoCall;
            else
                this.appScene = (TRTCAppScene)int.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_SET_PUSH_SMALLVIDEO);
            if (string.IsNullOrEmpty(param))
                this.pushSmallVideo = false;
            else
                this.pushSmallVideo = bool.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_SET_PLAY_SMALLVIDEO);
            if (string.IsNullOrEmpty(param))
                this.playSmallVideo = false;
            else
                this.playSmallVideo = bool.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_BEAUTY_OPEN);
            if (string.IsNullOrEmpty(param))
                this.isOpenBeauty = false;
            else
                this.isOpenBeauty = bool.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_BEAUTY_STYLE);
            if (string.IsNullOrEmpty(param))
                this.beautyStyle = TRTCBeautyStyle.TRTCBeautyStyleSmooth;
            else
                this.beautyStyle = (TRTCBeautyStyle)int.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_BEAUTY_VALUE);
            if (string.IsNullOrEmpty(param))
                this.beauty = 0;
            else
                this.beauty = uint.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_WHITE_VALUE);
            if (string.IsNullOrEmpty(param))
                this.white = 0;
            else
                this.white = uint.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_RUDDINESS_VALUE);
            if (string.IsNullOrEmpty(param))
                this.ruddiness = 0;
            else
                this.ruddiness = uint.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_LOCAL_VIDEO_MIRROR);
            if (string.IsNullOrEmpty(param))
                this.isLocalVideoMirror = false;
            else
                this.isLocalVideoMirror = bool.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_REMOTE_VIDEO_MIRROR);
            if (string.IsNullOrEmpty(param))
                this.isRemoteVideoMirror = false;
            else
                this.isRemoteVideoMirror = bool.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_FILL_MODE);
            if (string.IsNullOrEmpty(param))
                this.videoFillMode = TRTCVideoFillMode.TRTCVideoFillMode_Fit;
            else
                this.videoFillMode = (TRTCVideoFillMode)int.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_VIDEO_ROTATION);
            if (string.IsNullOrEmpty(param))
                this.videoRotation = TRTCVideoRotation.TRTCVideoRotation0;
            else
                this.videoRotation = (TRTCVideoRotation)int.Parse(param);

            // 音频参数配置
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_AUDIO_MIC_VOLUME);
            if (string.IsNullOrEmpty(param))
                this.micVolume = 25;
            else
                this.micVolume = uint.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_AUDIO_SPEAKER_VOLUME);
            if (string.IsNullOrEmpty(param))
                this.speakerVolume = 25;
            else
                this.speakerVolume = uint.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_SHOW_AUDIO_VOLUME);
            if (string.IsNullOrEmpty(param))
                this.isShowVolume = false;
            else
                this.isShowVolume = bool.Parse(param);


            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_AUDIO_QUALITY);
            if (string.IsNullOrEmpty(param))
                this.AudioQuality = TRTCAudioQuality.TRTCAudioQualityDefault;
            else
                this.AudioQuality = (TRTCAudioQuality)int.Parse(param);


            // 测试参数配置
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_SET_NETENV_STYLE);
            if (string.IsNullOrEmpty(param))
                this.testEnv = 0;
            else
                this.testEnv = int.Parse(param);
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_ROOMCALL_STYLE);
            if (string.IsNullOrEmpty(param))
                this.pureAudioStyle = false;
            else
                this.pureAudioStyle = bool.Parse(param);

            // 混流配置
            param = storage.GetValue(INI_ROOT_KEY, INI_KEY_CLOUD_MIX_TRANSCODING);
            if (string.IsNullOrEmpty(param))
                this.isMixTranscoding = false;
            else
                this.isMixTranscoding = bool.Parse(param);

            

        }

        static public void InitClassInfo()
        {
            classInfos.Add(new ClassInfo() { classId = "30", teacherId = "120171080702", 
                classTitle = "高三1班物理", maxPeople = 30, hasPassword = true, password = "123456" });
            classInfos.Add(new ClassInfo() { classId = "31", teacherId = "120171080701", 
                classTitle = "高二1班物理", maxPeople = 30, hasPassword = false, password = "" });
            classInfos.Add(new ClassInfo() { classId = "32", teacherId = "120171080701", 
                classTitle = "高二1班化学", maxPeople = 30, hasPassword = false, password = "" });
            classInfos.Add(new ClassInfo() { classId = "33", teacherId = "120171080702", 
                classTitle = "高三1班语文", maxPeople = 30, hasPassword = false, password = "" });
        }

        static public void InitUserInfo()
        {
            userInfos.Add(new UserInfo() { userId = "120171080701", userName = "姜莉杰", userSex = "女", isTeacher = true, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080702", userName = "于博淞", userSex = "男", isTeacher = true, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080703", userName = "赵鑫", userSex = "女", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080704", userName = "穆泽睿", userSex = "男", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080705", userName = "李仪", userSex = "女", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080706", userName = "张三", userSex = "男", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080707", userName = "李四", userSex = "男", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080708", userName = "王五", userSex = "男", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080709", userName = "赵六", userSex = "男", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080710", userName = "牛顿", userSex = "男", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080711", userName = "爱因斯坦", userSex = "男", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080712", userName = "普朗克", userSex = "男", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080713", userName = "玛丽居里", userSex = "女", isTeacher = false, password = "111111" });
            userInfos.Add(new UserInfo() { userId = "120171080714", userName = "杨振宁", userSex = "男", isTeacher = false, password = "111111" });
        }

        static public void InitLessonInfo()
        {
            lessonInfos.Add(new LessonInfo() { classId = "30", lessonId = "1", startTime = "2021-1-11 10:30", endTime = "2021-1-11 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "30", lessonId = "2", startTime = "2021-1-12 10:30", endTime = "2021-1-12 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "30", lessonId = "3", startTime = "2021-1-13 10:30", endTime = "2021-1-13 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "30", lessonId = "4", startTime = "2021-1-14 10:30", endTime = "2021-1-14 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "30", lessonId = "5", startTime = "2021-1-15 10:30", endTime = "2021-1-15 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "30", lessonId = "6", startTime = "2021-1-21 10:30", endTime = "2021-1-21 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "31", lessonId = "1", startTime = "2021-2-21 10:30", endTime = "2021-2-21 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "31", lessonId = "2", startTime = "2021-2-21 10:30", endTime = "2021-2-21 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "33", lessonId = "1", startTime = "2021-2-21 10:30", endTime = "2021-2-21 11:30", totleTime = "60" });
            lessonInfos.Add(new LessonInfo() { classId = "33", lessonId = "2", startTime = "2021-2-21 10:30", endTime = "2021-2-21 11:30", totleTime = "60" });
        }

        static public void InitCSInfo()
        {
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080703" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080704" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080705" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080706" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080707" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080708" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080709" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080710" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080711" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080712" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080713" });
            csInfos.Add(new CSInfo() { classId = "30", studentId = "120171080714" });
            csInfos.Add(new CSInfo() { classId = "31", studentId = "120171080704" });
            csInfos.Add(new CSInfo() { classId = "31", studentId = "120171080705" });
            csInfos.Add(new CSInfo() { classId = "33", studentId = "120171080705" });
        }

        static public void InitTimeSheetInfo()
        {
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080703",
                StudentName = "赵鑫",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7200,
                Concentration = 0.8F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080704",
                StudentName = "穆泽睿",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 9, 59, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 0),
                TotalTime = 7260,
                Concentration = 0.9F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "1",
                StudentId = "120171080705",
                StudentName = "李仪",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 25, 10, 0, 0),
                LeaveTime = new DateTime(2021, 1, 25, 12, 0, 5),
                TotalTime = 7205,
                Concentration = 0.95F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "30",
                LessonId = "2",
                StudentId = "120171080704",
                StudentName = "穆泽睿",
                TeacherId = "120171080702",
                JoinTime = new DateTime(2021, 1, 26, 9, 50, 0),
                LeaveTime = new DateTime(2021, 1, 26, 12, 1, 0),
                TotalTime = 7860,
                Concentration = 0.82F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "31",
                LessonId = "1",
                StudentId = "120171080704",
                StudentName = "穆泽睿",
                TeacherId = "120171080701",
                JoinTime = new DateTime(2021, 1, 25, 13, 55, 0),
                LeaveTime = new DateTime(2021, 1, 25, 16, 0, 0),
                TotalTime = 7500,
                Concentration = 0.85F
            });
            timeSheetInfos.Add(new TimeSheet()
            {
                ClassId = "31",
                LessonId = "2",
                StudentId = "120171080704",
                StudentName = "穆泽睿",
                TeacherId = "120171080701",
                JoinTime = new DateTime(2021, 1, 27, 13, 55, 0),
                LeaveTime = new DateTime(2021, 1, 27, 15, 55, 0),
                TotalTime = 7200,
                Concentration = 0.7F
            });
        }

        static public void InitQuizInfo()
        {
            QuizInfos.Add(new QuizSheet() { ClassId = "30", LessonId = "1", QuizId = "1", Quiz = "SQL是_____的缩写?" });
            QuizInfos.Add(new QuizSheet() { ClassId = "30", LessonId = "1", QuizId = "2", Quiz = "以下有关视图查询的叙述中正确的是?" });
            QuizInfos.Add(new QuizSheet() { ClassId = "30", LessonId = "1", QuizId = "3", Quiz = "部分匹配查询中有关通配符“％”的叙述中正确的是?" });
            QuizInfos.Add(new QuizSheet() { ClassId = "30", LessonId = "1", QuizId = "4", Quiz = "索引的作用之一是?" });
            QuizInfos.Add(new QuizSheet() { ClassId = "30", LessonId = "1", QuizId = "5", Quiz = "SQL语言的操作对象?" });
            QuizInfos.Add(new QuizSheet() { ClassId = "30", LessonId = "1", QuizId = "6", Quiz = "以下有关UNIQUE约束的叙述中不正确的是?" });
            QuizInfos.Add(new QuizSheet() { ClassId = "30", LessonId = "1", QuizId = "7", Quiz = "以下有关空值的叙述中不正确的是?" });
            QuizInfos.Add(new QuizSheet() { ClassId = "30", LessonId = "2", QuizId = "1", Quiz = "在分组检索中，要去掉不满足条件的分组，应当?" });
        } 

        public void Uninit()
        {
            WriteConfig();
        }

        private void WriteConfig()
        {
            //zx:设置
            storage.SetValue(INI_ROOT_KEY, INI_KEY_CLOSE_METHOD, closeMethod.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_PASSWORD, password);

            storage.SetValue(INI_ROOT_KEY, INI_KEY_USER_ID, userId);
            storage.SetValue(INI_ROOT_KEY, INI_KEY_ROOM_ID, roomId.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_ROLE_TYPE, ((int)roleType).ToString());

            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_BITRATE, videoEncParams.videoBitrate.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_FPS, videoEncParams.videoFps.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_RESOLUTION, ((int)videoEncParams.videoResolution).ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_RES_MODE, ((int)videoEncParams.resMode).ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_QUALITY, ((int)qosParams.preference).ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_QUALITY_CONTROL, ((int)qosParams.controlMode).ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_APP_SCENE, ((int)appScene).ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_SET_PUSH_SMALLVIDEO, pushSmallVideo.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_SET_PLAY_SMALLVIDEO, playSmallVideo.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_LOCAL_VIDEO_MIRROR, isLocalVideoMirror.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_REMOTE_VIDEO_MIRROR, isRemoteVideoMirror.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_BEAUTY_OPEN, isOpenBeauty.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_BEAUTY_STYLE, ((int)beautyStyle).ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_BEAUTY_VALUE, beauty.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_WHITE_VALUE, white.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_RUDDINESS_VALUE, ruddiness.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_FILL_MODE, ((int)videoFillMode).ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_VIDEO_ROTATION, ((int)videoRotation).ToString());

            storage.SetValue(INI_ROOT_KEY, INI_KEY_AUDIO_MIC_VOLUME, micVolume.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_AUDIO_SPEAKER_VOLUME, speakerVolume.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_SHOW_AUDIO_VOLUME, isShowVolume.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_AUDIO_QUALITY, ((int)AudioQuality).ToString());

            storage.SetValue(INI_ROOT_KEY, INI_KEY_SET_NETENV_STYLE, testEnv.ToString());
            storage.SetValue(INI_ROOT_KEY, INI_KEY_ROOMCALL_STYLE, pureAudioStyle.ToString());

            storage.SetValue(INI_ROOT_KEY, INI_KEY_CLOUD_MIX_TRANSCODING, isMixTranscoding.ToString());
        }

        private static DataManager mInstance;
        private static readonly object padlock = new object();

        public static DataManager GetInstance()
        {
            if (mInstance == null)
            {
                lock (padlock)
                {
                    if (mInstance == null)
                    {
                        mInstance = new DataManager();
                        InitClassInfo();
                        InitLessonInfo();
                        InitUserInfo();
                        InitCSInfo();
                        InitTimeSheetInfo();
                        InitQuizInfo();
                    }
                }
            }
            return mInstance;
        }

        # region 设置
        public bool closeMethod { get; set; }
        public string password { get; set; }
        #endregion

        #region 用户相关
        public string userId { get; set; }

        public uint roomId { get; set; }

        public List<UserInfo> userInfo { get; set; }

        public List<ClassInfo> classInfo { get; set; }

        public List<LessonInfo> lessonInfo { get; set; }

        public string strRoomId { get; set; }

        // 该字段只作用于直播模式
        public TRTCRoleType roleType { get; set; }

        public bool enterRoom { get; set; }
        #endregion

        #region 视频相关
        public TRTCVideoEncParam videoEncParams { get; set; }

        public TRTCNetworkQosParam qosParams { get; set; }

        public TRTCAppScene appScene { get; set; }

        public TRTCVideoFillMode videoFillMode { get; set; }
        public TRTCVideoRotation videoRotation { get; set; }
        public bool isLocalVideoMirror { get; set; }

        public bool pushSmallVideo { get; set; }
        public bool playSmallVideo { get; set; }


        public bool isRemoteVideoMirror { get; set; }

        public bool isMixTranscoding { get; set; }

        public TRTCRenderParams GetRenderParams()
        {
            TRTCRenderParams renderParams = new TRTCRenderParams
            {
                fillMode = videoFillMode,
                mirrorType = isLocalVideoMirror ?
                    TRTCVideoMirrorType.TRTCVideoMirrorType_Enable : TRTCVideoMirrorType.TRTCVideoMirrorType_Disable,
                rotation = videoRotation
            };
            return renderParams;
        }
        #endregion

        #region 音频相关
        public uint micVolume { get; set; }

        public uint speakerVolume { get; set; }

        public bool isShowVolume { get; set; }
        public TRTCAudioQuality AudioQuality { get; set; }
        #endregion

        #region 美颜相关
        public bool isOpenBeauty { get; set; }

        public TRTCBeautyStyle beautyStyle { get; set; }

        public uint beauty { get; set; }

        public uint white { get; set; }

        public uint ruddiness { get; set; }
        #endregion

        #region 测试
        public int testEnv { get; set; }

        public bool pureAudioStyle { get; set; }
        #endregion

    }

    class AVFrameBufferInfo
    {
        public byte[] data { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public bool newFrame { get; set; }

        public TRTCVideoRotation rotation { get; set; }

        public AVFrameBufferInfo()
        {
            rotation = TRTCVideoRotation.TRTCVideoRotation0;
            newFrame = false;
            width = 0;
            height = 0;
            data = null;
        }
    }

    class RemoteUserInfo
    {
        public string userId { get; set; }

        public int position { get; set; }
    }

    class PKUserInfo
    {
        public string userId { get; set; }

        public uint roomId { get; set; }

        public bool isEnterRoom { get; set; }
    }

    class UserVideoMeta
    {
        public string userId { get; set; }
        public uint roomId { get; set; }
        public TRTCVideoStreamType streamType { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public uint fps { get; set; }
        public bool pureAudio { get; set; }
        public bool mainStream { get; set; }

        public UserVideoMeta()
        {
            userId = "";
            streamType = TRTCVideoStreamType.TRTCVideoStreamTypeBig;
            width = 0;
            height = 0;
            fps = 0;
            pureAudio = false;
            mainStream = false;
        }
    }

    class IniStorage
    {
        // 声明INI文件的写操作函数 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        private string sPath = null;

        public IniStorage(string path)
        {
            this.sPath = path;
        }

        public void SetValue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径
            WritePrivateProfileString(section, key, value, sPath);
        }

        public string GetValue(string section, string key)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, sPath);
            return temp.ToString();
        }
    }

    public class UserInfo
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userSex { get; set; }
        public Boolean isTeacher { get; set; }
        public string password { get; set; }
    }

    public class ClassInfo
    {
        public string classId { get; set; }
        public string teacherId { get; set; }
        public string classTitle { get; set; }
        public int maxPeople { get; set; }
        public Boolean hasPassword { get; set; }
        public string password { get; set; }
    }

    public class LessonInfo
    {
        public string classId { get; set; }
        public string lessonId { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string totleTime { get; set; }
    }

    public class CSInfo
    {
        public string classId { get; set; }
        public string studentId { get; set; }
    }

    public class TimeSheet
    {
        private string classId;
        private string lessonId;
        private string studentId;
        private string studentName;
        private string teacherId;
        private DateTime joinTime;
        private DateTime leaveTime;
        private int totalTime; // 单位：s
        private float concentration;

        public string ClassId { get => classId; set => classId = value; }
        public string LessonId { get => lessonId; set => lessonId = value; }
        public string StudentId { get => studentId; set => studentId = value; }
        public string StudentName { get => studentName; set => studentName = value; }
        public string TeacherId { get => teacherId; set => teacherId = value; }
        public DateTime JoinTime { get => joinTime; set => joinTime = value; }
        public DateTime LeaveTime { get => leaveTime; set => leaveTime = value; }
        public int TotalTime { get => totalTime; set => totalTime = value; }
        public float Concentration { get => concentration; set => concentration = value; }

        public string getClassTime()
        {
            TimeSpan ts = new TimeSpan(TotalTime);
            Console.WriteLine(ts.ToString());
            return ts.ToString();
        }
    }

    public class QuizSheet
    {
        private string classId;
        private string lessonId;
        private string quizId;
        private string quiz;

        public string ClassId { get => classId; set => classId = value; }
        public string LessonId { get => lessonId; set => lessonId = value; }
        public string QuizId { get => quizId; set => quizId = value; }
        public string Quiz { get => quiz; set => quiz = value; }
    }

}
