   M S B A M L   `   `   `  ����B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest  5   	        WpfTest.ClassInfoStuWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfTest     5   	   -   5          � Icon$  
MyIcon.ico��5       � 
ResizeMode$ CanMinimize=�6,    � Title$ ClassInfoStuWindow��5   	   $	��470��6$   $	��300��61    � WindowStyle$
 None=�6=    � WindowStartupLocation$ CenterScreen=�6P    � 	Resources 5	         ScrollBarThumb �� 
TargetType �� Property �� Value	 �� 
TargetType
 �� CornerRadius ! HorizontalScrollBarPageButton  VerticalScrollBarPageButton   VerticalScrollBarPageButton2  for_scrollbar �Name J� SnapsToDevicePixels � RowDefinitions �� PageUpCommand ]�Name J� IsMouseOver ]� DecreaseRepeatButton ]� IncreaseRepeatButton �� PageDownCommand ]� Thumb �� Triggers P� Property P� Value �� PageLeftCommand �� PageRightCommand  for_scrollviewer � Cursor ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight  �� ViewportSize! �� VerticalOffset" � Value*# �� #ComputedVerticalScrollBarVisibility$ �� ScrollableWidth% �� HorizontalOffset& �� ViewportWidth,' �� %ComputedHorizontalScrollBarVisibility( �� Triggers) :� RoutedEvent* �� 
TargetName+ �� TargetProperty	, W� To- g� Duration. g� 	BeginTime/ :� 
SourceName0 �� BasedOn  BlueButtonStyle1 ��Name  lessonItemTemplate2 � ColumnDefinitions3 ��Name4 �Name  RoundedGelButton5 ��Name
6 � Tag��%
  &	        &	     &	 �    &	 �    &	 v    7  7  7  7   7  7  7  7   &	 I    7  7  '�� V    7  '�� x    7  &	 �    &	 �    7  7  �� 5   
   " n�6'   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � �� .  6*   �� 5       � �� 5      �� 5      "	 n�6&   .R�5      � .+�5      �� ���#646465$
 3��6:   5      5      5      5      �� 5   
   " ��66   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � ��
 ���� 6+   �� 5       � |� .  6*   �� 5       � �� .  6*   �� 5       �@J�Opacity$	 0.2Q�6(   �� 5       � �� 5       �� 5!      "	 ��6&   .R�5"      �� #��z��6$   #��z��6H   #��z��6h   5#      5%      5&      �� 5'   
   " ��64   .�5(      ��  �@�OverridesDefaultStyle . 66   �� 5)       � ��
 ���� 6+   �� 5*       � |� .  6*   �� 5+       � �� .  6*   �� 5,       �@J�Opacity$	 0.2Q�6(   �� 5-       � �� 5.      �� 5/      "	 ��6&   .R�50      �� #��z��6$   #��z��6H   #��z��6h   51      53      54      �� 57   
   " ��65   .�58      ��  �@�OverridesDefaultStyle . 67   �� 59       � ��
 ���� 6+   �� 5:       � |� .  6*   �� 5;       � �� .  6*   �� 5<       �@J�Opacity$ 0Q�6)   �� 5=       � �� 5>      �� 5?      "	 ��6&   .R�5@      �� ���	#90000000$��0��65   $��0��6?   5A      5C      5D      �� 5E   
   " ��6&   .�5F      ��  �@��IsPressAndHoldEnabled .  6=   �� 5G       �@��IsFlicksEnabled .  67   �� 5H       � ��
 ���� 6+   �� 5I       � ��$ 0,1,1,6q�6'   �� 5J       � ��$ 10��6'   �� 5K       � ��$ 5��6*   �� 5L       �@J�Opacity$	 0.2Q�6*   �� 5M       � �� 5N      �� 5O      "	 ��6&   .R�5P      � $ Bg��6    .  5Q      �� 5R   "   $
��auto �60   6@   �� 5S   "   $��* �60   6=   �� 5T   "   $
��auto �60   6@   .+�5V      �� $��0��6,   8��  #��Z 6o   ]� 5W      $ 
PART_Track��6$   $��1��6:   #{�z 6G   ��.  5X   "   �� 5Y   &   8�� #��Z 6h    5[   "   �� 5\   &   8�� #��Z 6h    5^   "   n� 5_   &   8�� 6,   5a      �� 5b      $��2��6+   8�� #��Z 6n   5c      5d      5f       5g      P� 5h       �@��Orientation$ 
Horizontal=�61   . �5i      ��  � ��
 ���� 64   �� 5j       � ��$ 1,0,6,1q�6/   �� 5k       � ��$ 5��61   �� 5l       � ��$ 5��63   �� 5m       � ��$
 Auto��61   �� 5n       �@J�Opacity$	 0.2Q�60   �� 5o       � �� 5p      �� 5q      "	 ��6.   .R�5r   "   � $ Bg��6'    . .+�5s   &   ]� $ 
PART_Track��6,   #{�z 6@    5t   *   �� 5u   .   8�� #��Z 6s    5w   *   �� 5x   .   8�� #��Z 6s    5z   *   n� 5{   .   8�� 64   5}   '   5~   #   5      5�      5�      5�      �� 5�   
   " ��5�      .�5�      ��  � ��
 �����6,   �� 5�       � ��$ 0q�60   �� 5�       � �� 5�      �� 5�      "	 ��6&   .R�5�      �� #��z��6!   #��z��6M    . ._�5�      � #��z��6#   .+�5�   "   �� # z 6:   #��z��6\   #��z��6   �� 5�   "   $ PART_VerticalScrollBar��6,   $��Right=�5�   (   # z 5�   (   $ Vertical=�5�   (   8��  #  z 5�   (   #" z! 5�   (   #y�z# 5�   (   �� 5�   "   $ PART_HorizontalScrollBar��6,   # z$ 5�   (   $ 
Horizontal=�5�   (   8�� $��Bottom=�5�   (   #" z% 5�   (   #  z& 5�   (   #y�z' 5�   (   5�      5�      ( 5�      :� 5�      $) ScrollChanged��6+   .[�5�   "   �� .��5�   &   �� .��5�   *   W� $* PART_VerticalScrollBar��5�   )   $+ Opacity�5�   )   $	, 0.8X�5�   )   $- 0:0:1D�5�   )   W� 5�   *   $* PART_VerticalScrollBar��5�   )   $+ Opacity�5�   )   $	, 0.2X�5�   )   $- 0:0:1D�5�   )   $. 0:0:1X�5�   )   W� 5�   *   $* PART_HorizontalScrollBar��5�   )   $+ Opacity�5�   )   $	, 0.8X�5�   )   $- 0:0:1D�5�   )   W� 5�   *   $* PART_HorizontalScrollBar��5�   )   $+ Opacity�5�   )   $	, 0.2X�5�   )   $- 0:0:1D�5�   )   $. 0:0:1X�5�   )   5�   '   5�   #   5�      :� 5�      $) 
MouseEnter��6+   $/ PART_VerticalScrollBar��5�   '   .[�5�   "   �� .��5�   &   �� .��5�   *   W� $* PART_VerticalScrollBar��5�   )   $+ Opacity�5�   )   $	, 0.8X�5�   )   $- 0:0:0.7D�5�   )   5�   '   5�   #   5�      :� 5�      $) 
MouseLeave��6+   $/ PART_VerticalScrollBar��5�   '   .[�5�   "   �� .��5�   &   �� .��5�   *   W� $* PART_VerticalScrollBar��5�   )   $+ Opacity�5�   )   $	, 0.2X�5�   )   $- 0:0:0.7D�5�   )   5�   '   5�   #   5�      :� 5�      $) 
MouseEnter��6+   $/ PART_HorizontalScrollBar��5�   '   .[�5�   "   �� .��5�   &   �� .��5�   *   W� $* PART_HorizontalScrollBar��5�   )   $+ Opacity�5�   )   $	, 0.8X�5�   )   $- 0:0:0.7D�5�   )   5�   '   5�   #   5�      :� 5�      $) 
MouseLeave��6+   $/ PART_HorizontalScrollBar��5�   '   .[�5�   "   �� .��5�   &   �� .��5�   *   W� $* PART_HorizontalScrollBar��5�   )   $+ Opacity�5�   )   $	, 0.2X�5�   )   $- 0:0:0.7D�5�   )   5�   '   5�   #   5�      5�      5�      5�      �� 5�   
   " ��6   80   6'   �� 5�   
   " ��6   80   6*   �� 5�   
   " ��6(   .�5�      ��  � ��$ 0q�60   �� 5�       � ��
 �����6+   �� 5�       � �� �#006EFF6,   �� 5�       � �� �#006EFF6+   �� 5�       � �� 5      �� 5     "	 ��6&   .R�5     �� $
1 Back��6!   $
 3��6/   $��1q�6@   #��z��5  1   #��z��5  1   ._�5     � .+�5  "   �� $��Center=�64   $��Center=�6Q   5     5     ( 5	     P� 5
      �@J�IsMouseOver . . �5  "   ��  � �� �#1A8CFF6?   �� 5  "    � �� �#1A8CFF6@   5     5     5     5     �� 5  
   .R�5     � $
��10,5q�6   2 5     �� 5     $��3* �6'   �� 5     $��* �6'   .+�5     �� $��0��6   .+�5     �� $
��Left=�69   $��20(�6[   $	��160��6i   $
��Bold"�6u   ��6   �� *
lessonId+6u   �� 5     $3 第1节课时间��6:   $
��Left=�6K   ��6   �� *	startTime+6K   5     � 5     $4 stuPanel��6   $��1��6/   $��0,15,0,0q�6?   .+�5     �� 8��  $	��▶��6G   $��5,0,5,0q�6S   6g   �� 5     $5 moreInfoBtn��6   -   5        8�� $	��➕��5  \   $��0,0,10,0q�6h   6 6�   �� *
lessonId+6�   5      5!     5"     5#     .��5$     ]� .+�5%  
   � $	��Top=�6   2 5&     �� 5'     $��* �6#   �� 5(     $��* �6#   .+�5*     �� $��0��6   7 �� Orientation$7 
Horizontal=�6)   $
��Left=�6B   $	��Top=�6]   .+�5+     �� $��28��6   $��26��6$   $��
MyIcon.ico��6/   �� 5,     $��课程信息��6   5-     �� 5.     $7 
Horizontal=�6   $��1��62   $��Right=�6B   $	��Top=�6_   .+�53     �� -   5        $5 MinBtn��53     $��32��6)   $	��Top=�64   
������ $��0q�6e   .��54     �� $��28��6   $��26��6(   $��Min.png��63   $��Center=�6D   $��Center=�6a   55     �� 56     -   5        $5 ExitBtn��56     $��32��6*   $	��Top=�65   
������ $��0q�57     .��58     �� $��28��6   $��26��6(   $��	Close.png��63   $��Center=�6F   $��Center=�6c   59     5:     5;     � 5<  
   $��Center=�6   .+�5=     �� -   5        8 ��Name$8 lessonInfoListBox��5=     #��[ 61   $	��378��6�   $	��280��6�   $	��Top=�6�   $9 �� IsSynchronizedWithCurrentItem$
9 TrueY�6�   �� 5>     -   5        $5 leaveClassBtn��5>     #��[ 6,   $��退出课程��6U   $��30��6d   $	��140��6p   $��	70,8,0,20q�6}   $��18(�6�   6�   5?     5A     5B     