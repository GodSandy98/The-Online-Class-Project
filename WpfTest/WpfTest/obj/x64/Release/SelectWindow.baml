   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest      WpfTest.SelectWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfTest     -     � Icon$  
MyIcon.ico�� � Title$ SelectWindow��$	��520��$	��450�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    ScrollBarThumb �� 
TargetType �� Property �� Value �� 
TargetType	 �� CornerRadius ! HorizontalScrollBarPageButton  VerticalScrollBarPageButton   VerticalScrollBarPageButton2  for_scrollbar
 �Name J� SnapsToDevicePixels � RowDefinitions �� PageUpCommand ]�Name J� IsMouseOver ]� DecreaseRepeatButton ]� IncreaseRepeatButton �� PageDownCommand ]� Thumb �� Triggers P� Property P� Value �� PageLeftCommand �� PageRightCommand  for_scrollviewer � Cursor ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize  �� VerticalOffset! � Value*" �� #ComputedVerticalScrollBarVisibility# �� ScrollableWidth$ �� HorizontalOffset% �� ViewportWidth,& �� %ComputedHorizontalScrollBarVisibility' �� Triggers( :� RoutedEvent) �� 
TargetName* �� TargetProperty	+ W� To, g� Duration- g� 	BeginTime. :� 
SourceName/ �� BasedOn  BlueButtonStyle0 ��Name  BlueComboBoxStyle1 �� 
TargetName2 �� 	ClickMode3 d� 	IsChecked4 �� Path5 �� Mode6 �� RelativeSource7 �Name8 �� IsDropDownOpen9 � AllowsTransparency: �� MaxDropDownHeight; � ActualWidth< J� Effect3  -System.Windows.Media.Effects.DropShadowEffect=   Color>   
BlurRadius?   ShadowDepth@   OpacityA K� IsItemsHost-  'System.Windows.Input.KeyboardNavigationB   DirectionalNavigation  classItemTemplateC � ColumnDefinitionsD �� OrientationE ��Name
F � TagG ��Name 	 classStuItemTemplate��%�  &	        &	 �     &	 [    &	 )    &	     7  7  7  7   7  7  7  7   &	 &    7  7  '�� )    7  '�� 8    7  &	 G    &	 �    &	 Z    7  7  7  7  &		 q    7  7  7  �� " n�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � �� �� " n�.R�� .+��� ���#646465$	 3���� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$	 0.2Q���  � �� �� " ��.R��� #��z��#��z��#��z���� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$	 0.2Q���  � �� �� " ��.R��� #��z��#��z��#��z���� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$ 0Q���  � �� �� " ��.R��� ���	#90000000$��0��$��0���� " ��.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��
 ���� ��  � ��$ 0,1,1,6q���  � ��$ 10����  � ��$ 5����  �@J�Opacity$	 0.2Q���  � �� �� " ��.R�� $
 Bg�� .  �� $
��auto ��� $��* ��� $
��auto �.+��� $��0��8��  #��Z ]� $ 
PART_Track��$��1��#{�z ��.  �� 8�� #��Z  �� 8�� #��Z  n� 8�� �� $��2��8�� #��Z  P�  �@��Orientation$ 
Horizontal=�. ���  � ��
 ���� ��  � ��$ 1,0,6,1q���  � ��$ 5����  � ��$ 5����  � ��$
 Auto����  �@J�Opacity$	 0.2Q���  � �� �� " ��.R�� $
 Bg�� . .+�]� $ 
PART_Track��#{�z  �� 8�� #��Z  �� 8�� #��Z  n� 8�� �� " ��.���  � ��
 �������  � ��$ 0q���  � �� �� " ��.R��� #��z��#��z�� . ._�� #��z��.+��� # z #��z��#��z���� $ PART_VerticalScrollBar��$��Right=�# z $ Vertical=�8��  # z #! z  #y�z" �� $ PART_HorizontalScrollBar��# z# $ 
Horizontal=�8�� $��Bottom=�#! z$ # z% #y�z& ' :� $( ScrollChanged��.[��� .���� .��W� $) PART_VerticalScrollBar��$* Opacity�$	+ 0.8X�$, 0:0:1D�W� $) PART_VerticalScrollBar��$* Opacity�$	+ 0.2X�$, 0:0:1D�$- 0:0:1X�W� $) PART_HorizontalScrollBar��$* Opacity�$	+ 0.8X�$, 0:0:1D�W� $) PART_HorizontalScrollBar��$* Opacity�$	+ 0.2X�$, 0:0:1D�$- 0:0:1X�:� $( 
MouseEnter��$. PART_VerticalScrollBar��.[��� .���� .��W� $) PART_VerticalScrollBar��$* Opacity�$	+ 0.8X�$, 0:0:0.7D�:� $( 
MouseLeave��$. PART_VerticalScrollBar��.[��� .���� .��W� $) PART_VerticalScrollBar��$* Opacity�$	+ 0.2X�$, 0:0:0.7D�:� $( 
MouseEnter��$. PART_HorizontalScrollBar��.[��� .���� .��W� $) PART_HorizontalScrollBar��$* Opacity�$	+ 0.8X�$, 0:0:0.7D�:� $( 
MouseLeave��$. PART_HorizontalScrollBar��.[��� .���� .��W� $) PART_HorizontalScrollBar��$* Opacity�$	+ 0.2X�$, 0:0:0.7D��� " ��8/   �� " ��8/   �� " ��.���  � ��$ 40����  � ��$ 60����  � ��
 �������  � ��$ 0q���  � ��
 �������  � �� �#006EFF��  � �� �#006EFF��  � �� �� " ��.R��� $
0 Back��$	 3,0,0,3��$��1q�#��z��#��z��._�� .+��� $��Center=�$��Center=�' P�  �@J�IsMouseOver . . ���  � �� �#1A8CFF��  � �� �#1A8CFF�� " ��.���  � ��$ 20����  � ��$ 41����  � ��$ 0,1,0,0q���  � �� �#006EFF��  � �� �#006EFF��  � �� �� " ��.���  � ��$ Center=���  � �� �� " ��.R��� $
��Back��
������ $��0,0,0,0q����#81D779._��� $��Center=�$
��Left=�$��10,0,0,0q�' P�  �@J�IsMouseOver . . ��� $
1 Back�� � ��
 �����P�  �@��IsHighlighted . . ��� $
1 Back�� � ��
 �����P�  �@J�IsMouseOver . . ���  � �� �#1A8CFF��  � �� �#1A8CFF��  � �� �� " ��.R�� $��	0,0,7,0.4q�.+��� $��1��$��1,1,3,1q����#006EFF$	 0,3,3,0��$��0.4,0,0,0.4q�._�d� $��1q����#006EFF���#006EFF$2 Press=�3 �� $4 IsDropDownOpen�$5 TwoWay=�6 �� *TemplatedParent+� $7 Popup��#��z8 $��Bottom=�|�.  9 . $��Slide=�$��0,0,0.4,0.4q�.���� $0 DropDown��$	 3��
�������$��2q�#��z: #��z;  . <   $= 	LightGray��$> 5Q�$? 0Q�$@ 1Q�._��� $��0,0,0,0q� . $
��Auto=�$
��Auto=����#006EFF$��1q�.���� A . $B 	Contained=�
�������$��Center=��� .R�� C �� $��3* ��� $��1* �.+��� $D Vertical=�$��0��$��10q�$	��280��$��70��.+��� $��25(����� *
classTitle+� .+��� $��任课教师：��$��Stretch=�$��Stretch=��� $
��Left=�$��	-12 0 3 0q����� *	teacherId+�� $��房间号：��$��18 0 0 0q�$��Stretch=�$��Stretch=��� $
��Left=�$��	-12 0 0 0q����� *	classId+� $��1��$��0,20,0,0q�$	��100��.+��� $E startClassBtn��-   8��  $��开始上课��$��40��$��60��$��1q�$��0q�F �� *	classId+�� $G ExtendComboBox��-   8�� $��Center=�.@��� .���� -   $��复制房间信息��8�� $��20��$��80��$��	-10,0,0,0q�F �� *	classId+�� .���� -   $��修改课堂信息��8�� $��20��$��80��$��	-10,0,0,0q�F �� *	classId+�� .R�� C �� $��3* ��� $��1* �.+��� $D Vertical=�$��0��$��10q�$	��280��$��70��.+��� $��25(����� *
classTitle+� .+��� $��任课教师：��$��Stretch=�$��Stretch=��� $
��Left=�$��	-12 0 3 0q����� *	teacherId+�� $��房间号：��$��18 0 0 0q�$��Stretch=�$��Stretch=��� $
��Left=�$��	-12 0 0 0q����� *	classId+� $��1��$��0,20,0,0q�$	��100��.+��� $E startClassBtn��-   8��  $��开始上课��$��40��$��60��$��1q�$��0q�F �� *	classId+�� $G ExtendComboBox��-   8�� $��Center=�.@��� .���� -   $��复制房间信息��8�� $��20��$��80��$��	-10,0,0,0q�F �� *	classId+.��]� .+�� $	��Top=�C �� $��* ��� $��* �.+��� -	   H ��Name$H LonginWindowLabel��$�� ��$��0���� $D 
Horizontal=�$��1��$��Right=�$	��Top=�.+��� -
   $E 
SettingBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Setting.png��$��Center=�$��Center=��� -   $E MinBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Min.png��$��Center=�$��Center=��� -   $E MaxBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Max.png��$��Center=�$��Center=��� -   $E ExitBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��	Close.png��$��Center=�$��Center=��� .+��  �� $��0* ��� $
��Auto �.+��� 
�������$��0,0,0,1q�._��� $��0��$
��auto��$
��auto��$	��Top=�$��0,0,0,20q�.+�� $��70��C �� $��* ��� $��* �.+��� -   $E joinRoomBtn��
�������
�������$��Center=�$��0,10,0,0q�$��Center=�$	��160��$��56��$��0��.��� $��56��$	��160��.+��� $��55��$��61��$0��*pack://application:,,,/Images/joinRoom.png��$��Stretch=�$��Stretch=��� $
��Wrap=�$�� 加入课堂��$��97��$��20(�$��Center=�$��28���� -   $E createRoomBtn��
�������
�������$��0,10,0,0q�$��Center=�$��Center=�$	��160��$��56��$��1��.��� $��56��$	��160��.+��� $��52��$��58��$2��,pack://application:,,,/Images/createRoom.png��$��Stretch=�$��Stretch=��� $
��Wrap=�$�� 创建课堂��$��97��$��20(�$��Center=�$��28���� $��1��.+��  �� $��* �.+��� -   I ��Name$I classInfoListBox��$y�Visible=�$��0��$��0q�#��[ $	��380���� -   $I classStuInfoListBox��$y�Visible=�$��0��$��0q�#��[	 $	��380��