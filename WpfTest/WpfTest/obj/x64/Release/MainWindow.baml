   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest  [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms ^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration     WpfTest.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfTest     Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     _wfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration     -     � Icon$  
MyIcon.ico�� � Title$ 
MainWindow��$	��561��$��1123.1�� � WindowStyle$
 None=�.��� .+�]� .+�� $	��Top=� � ColumnDefinitions �� $��3* ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�$
��Left=�$	��Top=�.+��� $��28��$��26��$��
MyIcon.ico���� $��教室主题:���� -    ��Name$ 
themeLabel��$�� ���� $��	教室ID:���� -   $ roomIDLabel��$�� ���� $��教师姓名:���� -   $ teacherNameLabel��$�� ���� $��1��$ 
Horizontal=�$��Right=�$	��Top=�.+��� -    ��Name$ 
settingBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Setting.png��$��Center=�$��Center=��� -   $ sinBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Min.png��$��Center=�$��Center=��� -   $ maxBtn��$��32��$	��Top=�
������ $��0q�.���� -    ��Name$ maxImg��$��28��$��26��$��Max.png��$��Center=�$��Center=��� -	   $ exitBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��	Close.png��$��Center=�$��Center=��  �� $��4* ��� $��* �.+�]� -
    ]�Name$ localDockPanel��$��0��.+��� $ 
Horizontal=�$��Bottom=�$��5q�.+��� -   	 ��Name$	 logCheckBox��$��	仪表盘��$��10q��� -   $ startLocalAudioImg��$��40��$��50��$1��+pack://application:,,,/Images/audioOpen.png���� -   $ startLocalPreviewImg��$��40��$��50��$1��+pack://application:,,,/Images/videoOpen.png���� -   $ screenShareImg��$��40��$��50��$3��-pack://application:,,,/Images/screenShare.png���� $��40��$��50��$,��&pack://application:,,,/Images/chat.png���� $��40��$��50��$.��(pack://application:,,,/Images/member.png���� $��40��$��50��$/��)pack://application:,,,/Images/handsUp.png���� -   $ teacherViewImg��$��40��$��50��$3��-pack://application:,,,/Images/teacherView.png��$y�Hidden=�� .+�7  1System.Windows.Forms.Integration.WindowsFormsHost  -   
  Name$
 form1��$��
10,5,10,10q�   Child.    System.Windows.Forms.Panel  -   teacherVideoPanel��  -   $
 handsUpForm��$��40��$��35��$��Right=�$��10q�   
Background
 �   �. %  System.Windows.Forms.PictureBox  -   handsUpPicBox��   ImageLocation$( "../../../Images/handsUpinVedio.png��   SizeMode$ StretchImage=�  $��28��$	��170��$
��Left=�$��Bottom=�$��	10,0,0,30q�. *  $System.Windows.Forms.FlowLayoutPanel  -   messagePanel��  -   $
 teacherInfoForm��$
��Left=�$��	10,10,0,0q�$	��Top=�.    System.Windows.Forms.Label  -   teacherInfoLabel��   Text$* $教师未开启视频或屏幕共享��� -    �Name$ teacherRightGrid��$��1�� � RowDefinitions �� $��* ��� $��* ��� $��* ��� $
��0.5* �.+�]� $��0��.+��� $��Bottom=�$ 
Horizontal=�$��Bottom=�.+��� -   $��remoteUserIdLabel1��$�� ��$��5q��� -   $��remoteNameLabel1��$�� ��$��5q��� -   $��screenShareFlagLabel1��$�� ��$��5q�� .+�  -   $
 form2��$��5q�$��Stretch=�$��Stretch=�
 �   �.   -   remoteVideoPanel1��  -   $
 	infoForm2��$��5q�$��Stretch=�$��Stretch=�.   "  System.Windows.Forms.Control   Name$ remoteInfoLabel1��$ 学生未开启视频��   Height$ 51��   Width$	 136��]� $��1��.+��� $��Bottom=�$ 
Horizontal=�$��Bottom=�.+��� -   $��remoteUserIdLabel2��$�� ��$��5q��� -   $��remoteNameLabel2��$�� ��$��5q��� -    $��screenShareFlagLabel2��$�� ��$��5q�� .+�  -!   $
 form3��$��5q�$��Stretch=�$��Stretch=�
 �   �.   -"   remoteVideoPanel2��  -#   $
 	infoForm3��$��5q�$��Stretch=�$��Stretch=�.   $ remoteInfoLabel2��$ 学生未开启视频��$ 51��$	 136��]� $��2��.+��� $��Bottom=�$ 
Horizontal=�$��Bottom=�.+��� -$   $��remoteUserIdLabel3��$�� ��$��5q��� -%   $��remoteNameLabel3��$�� ��$��5q��� -&   $��screenShareFlagLabel3��$�� ��$��5q�� .+�  -'   $
 form4��$��5q�$��Stretch=�$��Stretch=�
 �   �.   -(   remoteVideoPanel3��  -)   $
 	infoForm4��$��5q�$��Stretch=�$��Stretch=�.   $ remoteInfoLabel3��$ 学生未开启视频��$ 51��$	 136���� -*   $ classOverBtn��$��下课��$��3��$��Bottom=�$��10q�$��70��$��30��� -+   $ studentRightGrid��$��1�� �� $��* ��� $
��2.5* �.+��� -,    ��Name$ chatTextBox��$��1��$��学生聊天界面��