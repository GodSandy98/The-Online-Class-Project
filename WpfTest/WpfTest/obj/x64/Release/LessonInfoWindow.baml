   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest      WpfTest.LessonInfoWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfTest     -     � Title$  LessonInfoWindow��$	��450��$	��520�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    ScrollBarThumb �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ! HorizontalScrollBarPageButton  VerticalScrollBarPageButton   VerticalScrollBarPageButton2  for_scrollbar	 �Name
 J� SnapsToDevicePixels � RowDefinitions �� PageUpCommand ]�Name J� IsMouseOver ]� DecreaseRepeatButton ]� IncreaseRepeatButton �� PageDownCommand ]� Thumb �� Triggers P� Property P� Value �� PageLeftCommand �� PageRightCommand  for_scrollviewer � Cursor ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize �� VerticalOffset  � Value*! �� #ComputedVerticalScrollBarVisibility" �� ScrollableWidth# �� HorizontalOffset$ �� ViewportWidth,% �� %ComputedHorizontalScrollBarVisibility& �� Triggers' :� RoutedEvent( �� 
TargetName) �� TargetProperty	* W� To+ g� Duration, g� 	BeginTime- :� 
SourceName. �� BasedOn  BlueButtonStyle/ ��Name  GrayButtonStyle��%�  &	        &	 �     &	 [    &	 )    &	     7  7  7  7   7  7  7  7   &	 &    7  7  '�� )    7  '�� 8    7  &	 G    &	 �    �� " n�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � �� �� " n�.R�� .+��� ���#646465$ 3���� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$	 0.2Q���  � �� �� " ��.R��� #��z��#��z��#��z���� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$	 0.2Q���  � �� �� " ��.R��� #��z��#��z��#��z���� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$ 0Q���  � �� �� " ��.R��� ���	#90000000$��0��$��0���� " ��.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��
 ���� ��  � ��$ 0,1,1,6q���  � ��$ 10����  � ��$ 5����  �@J�Opacity$	 0.2Q���  � �� �� " ��.R�� $	 Bg��
 .  �� $
��auto ��� $��* ��� $
��auto �.+��� $��0��8��  #��Z ]� $ 
PART_Track��$��1��#{�z ��.  �� 8�� #��Z  �� 8�� #��Z  n� 8�� �� $��2��8�� #��Z  P�  �@��Orientation$ 
Horizontal=�. ���  � ��
 ���� ��  � ��$ 1,0,6,1q���  � ��$ 5����  � ��$ 5����  � ��$
 Auto����  �@J�Opacity$	 0.2Q���  � �� �� " ��.R�� $	 Bg��
 . .+�]� $ 
PART_Track��#{�z  �� 8�� #��Z  �� 8�� #��Z  n� 8�� �� " ��.���  � ��
 �������  � ��$ 0q���  � �� �� " ��.R��� #��z��#��z��
 . ._�� #��z��.+��� # z #��z��#��z���� $ PART_VerticalScrollBar��$��Right=�# z $ Vertical=�8��  # z #  z #y�z! �� $ PART_HorizontalScrollBar��# z" $ 
Horizontal=�8�� $��Bottom=�#  z# # z$ #y�z% & :� $' ScrollChanged��.[��� .���� .��W� $( PART_VerticalScrollBar��$) Opacity�$	* 0.8X�$+ 0:0:1D�W� $( PART_VerticalScrollBar��$) Opacity�$	* 0.2X�$+ 0:0:1D�$, 0:0:1X�W� $( PART_HorizontalScrollBar��$) Opacity�$	* 0.8X�$+ 0:0:1D�W� $( PART_HorizontalScrollBar��$) Opacity�$	* 0.2X�$+ 0:0:1D�$, 0:0:1X�:� $' 
MouseEnter��$- PART_VerticalScrollBar��.[��� .���� .��W� $( PART_VerticalScrollBar��$) Opacity�$	* 0.8X�$+ 0:0:0.7D�:� $' 
MouseLeave��$- PART_VerticalScrollBar��.[��� .���� .��W� $( PART_VerticalScrollBar��$) Opacity�$	* 0.2X�$+ 0:0:0.7D�:� $' 
MouseEnter��$- PART_HorizontalScrollBar��.[��� .���� .��W� $( PART_HorizontalScrollBar��$) Opacity�$	* 0.8X�$+ 0:0:0.7D�:� $' 
MouseLeave��$- PART_HorizontalScrollBar��.[��� .���� .��W� $( PART_HorizontalScrollBar��$) Opacity�$	* 0.2X�$+ 0:0:0.7D��� " ��8.   �� " ��8.   �� " ��.���  � ��
 �������  � �� �#006EFF��  � �� �#006EFF��  � �� �� " ��.R��� $
/ Back��$ 3 3 3 3��$��0q�#��z��#��z��$��	0,0,7,2.4q�._�� .+��� $��Center=�$��Center=�& P�  �@J�IsMouseOver . . ���  � �� �#1A8CFF��  � �� �#1A8CFF�� " ��.���  � ��
 �   ���  � ��
 �������  � ��
 �������  � ��$ 60����  � ��$ 30����  � �� �� " ��.R��� $
/ Back��$ 3 3 3 3��$��0q�#��z��#��z��$��	0,0,7,2.4q�._�� .+��� $��Center=�$��Center=�& P�  �@J�IsMouseOver . . ���  � ��
 ��ح���  � ��
 ��ح�.��]� .+�� $	��Top=�0 � ColumnDefinitions0 �� $��* ��� $��* �.+��� -   1 ��Name$1 LonginWindowLabel��$��Teacher's Window��$��0���� 2 �� Orientation$2 
Horizontal=�$��1��$��Right=�$	��Top=�.+��� -   3 ��Name$3 
SettingBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Setting.png��$��Center=�$��Center=��� -   $3 MinBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Min.png��$��Center=�$��Center=��� -   $3 MaxBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Max.png��$��Center=�$��Center=��� -   $3 ExitBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��	Close.png��$��Center=�$��Center=��� .+�� 0 �� $��* ��� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $1 teacherLabel��$��教师：xxx��$��0��$��0���� -   $1 classIDLabel��$��教室ID：��$��1��$��0���� -	   $1 
classLabel��$��课程名：��$��2��$��0���� -
   $1 classNumLabel��$��	课次：��$��3��$��0���� -   $1 statisticLabel��$��总体统计信息��$��0��$��1���� -   $1 dueStudentNumLabel��$��应到 xx 人��$��1��$��1���� -   $1 actualStudentLabel��$��实到 xx 人��$��2��$��1���� -   $1 attendanceRateLabel��$��出勤率：xx%��$��3��$��1���� -   $1 speakerNumLabel��$��发言人数：xx 人次��$��0��$��2���� -   $1 quizNumLabel��$��题目数量：xx��$��1��$��2���� -   $1 correctRateLabel��$��正确率：xx%��$��2��$��2��]� .+�� $	��Top=�.+�� $	��120��$��40��$
��Left=�0 �� �� .+��� -   $3 studentStatisticBtn��#��[ $��学生信息��$��0��$��5,5,0,0q�$��60��$��30���� -   $3 quizStatisticBtn��#��[ $��答题信息��$��1��$��0,5,0,0q�$��60��$��30��� .+�� -   $	 studentInfoPanel��$��5��$y�Visible=�$	��490��$
��Left=�$��5 0 0 0q�.+�&   System.Windows.Controls.DataGrid  -   4  Name$4 studentInfoDataGrid��$��1��$��5��5   AutoGenerateColumns5 .  6   CanUserAddRows6 .  7   CanUserDeleteRows7 .  8   
IsReadOnly8 . $��Center=�$��1q�$	��242��9   	RowHeight$9 22Q�:   Columns: 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn;   Header$; 姓  名��<   Width5  /System.Windows.Controls.DataGridLengthConverter$< 70 1  +System.Windows.Controls.DataGridBoundColumn=   Binding= �� *StudentName+  $; 入会时间��$	< 140 = �� *
JoinTime+> �� StringFormat$> {0:yyyy-MM-dd  HH:mm:ss}��  $; 离会时间��$	< 140 = �� *	LeaveTime+$> {0:yyyy-MM-dd  HH:mm:ss}��  $; 	总时间��$< 50 = �� *	TotalTime+4  .System.Windows.Controls.DataGridTemplateColumn  $; 详细信息��$< * ?   CellTemplate? �� .R��� $2 
Horizontal=�$��Center=�$��Center=�.+��� -   $��详细信息��� -   $	 quizInfoPanel��$��5��$y�Hidden=�$	��490��$
��Left=�$��5 0 0 0q�.+�  -   $4 quizInfoDataGrid��$��1��$��5��5 .  @   CanUserReorderColumns@ .  6 .  7 .  8 . $��1q�$	��242��$9 22Q�:   $; 编号��$< 50 = �� *QuizId+  $; 题目内容��$	< 360 = �� *Quiz+  $; 详细信息��$< * ? �� .R��� $2 
Horizontal=�$��Center=�$��Center=�.+��� -   $��详细信息��