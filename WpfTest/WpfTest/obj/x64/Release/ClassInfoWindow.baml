   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest      WpfTest.ClassInfoWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfTest     -     � Icon$  
MyIcon.ico�� � Title$ ClassInfoWindow��$	��470��$	��300�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    ScrollBarThumb �� 
TargetType �� Property �� Value �� 
TargetType	 �� CornerRadius ! HorizontalScrollBarPageButton  VerticalScrollBarPageButton   VerticalScrollBarPageButton2  for_scrollbar
 �Name J� SnapsToDevicePixels � RowDefinitions �� PageUpCommand ]�Name J� IsMouseOver ]� DecreaseRepeatButton ]� IncreaseRepeatButton �� PageDownCommand ]� Thumb �� Triggers P� Property P� Value �� PageLeftCommand �� PageRightCommand  for_scrollviewer � Cursor ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize  �� VerticalOffset! � Value*" �� #ComputedVerticalScrollBarVisibility# �� ScrollableWidth$ �� HorizontalOffset% �� ViewportWidth,& �� %ComputedHorizontalScrollBarVisibility' �� Triggers( :� RoutedEvent) �� 
TargetName* �� TargetProperty	+ W� To, g� Duration- g� 	BeginTime. :� 
SourceName/ �� BasedOn  BlueButtonStyle0 ��Name  lessonItemTemplate1 � ColumnDefinitions2 ��Name3 ��Name
4 � Tag��%  &	        &	 �     &	 [    &	 )    &	     7  7  7  7   7  7  7  7   &	 &    7  7  '�� )    7  '�� 8    7  &	 G    &	 �    7  7  �� " n�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � �� �� " n�.R�� .+��� ���#646465$	 3���� " ��.���  �@�OverridesDefaultStyle . ��  � ��
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
MouseLeave��$. PART_HorizontalScrollBar��.[��� .���� .��W� $) PART_HorizontalScrollBar��$* Opacity�$	+ 0.2X�$, 0:0:0.7D��� " ��8/   �� " ��8/   �� " ��.���  � ��$ 0q���  � ��
 �������  � �� �#006EFF��  � �� �#006EFF��  � ��$ 60����  � ��$ 20����  � ��$ Center=���  � �� �� " ��.R��� $
0 Back��$	 3��$��1q�#��z��#��z��._�� .+��� $��Center=�$��Center=�' P�  �@J�IsMouseOver . . ���  � �� �#1A8CFF��  � �� �#1A8CFF�� .R�� $
��10,5q�1 �� $��3* ��� $��* �.+��� $��0��.+��� $
��Left=�$��20(�$	��190��$
��Bold"����� *
lessonId+�� $2 第1节课时间��$
��Left=����� *	startTime+�� $��1��.+��� $3 informationBtn��-   8��  $��统计信息��$y�Visible=�$��Center=�$��0,10,0,5q�4 �� *
lessonId+�� -   8�� $��删除��$y�Visible=�$��Center=�$��0,5,0,10q�$��60��.��]� .+�� $	��Top=�1 �� $��* ��� $��* �.+��� $��课堂信息��$��0���� 5 �� Orientation$5 
Horizontal=�$��1��$��Right=�$	��Top=�.+��� -   $3 
SettingBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Setting.png��$��Center=�$��Center=��� -   $3 MinBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Min.png��$��Center=�$��Center=��� -   $3 MaxBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Max.png��$��Center=�$��Center=��� -   $3 ExitBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��	Close.png��$��Center=�$��Center=��� -   6 ��Name$6 lessonInfoListBox��#��[ $	��425��$7 �� IsSynchronizedWithCurrentItem$
7 TrueY�