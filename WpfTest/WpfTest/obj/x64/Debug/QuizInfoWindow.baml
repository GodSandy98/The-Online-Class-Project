   M S B A M L   `   `   `  ����B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest  5   	       WpfTest.QuizInfoWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfTest     5   	     � 
ResizeMode$  CanMinimize=�5       � Title$ QuizInfoWindow��5   	   $	��450��6    $	��520��6-    � WindowStyle$
 None=�69    � WindowStartupLocation$ CenterScreen=�6L    � 	Resources 5	         BlueButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers P� Property P� Value��%�  &	        �� 5
   
   " ��6(   .�5      ��  � ��$ 0q�60   �� 5       � ��$ 20��6'   �� 5       � ��$ 40��6&   �� 5       � ��
 �����6+   �� 5       � �� �#006EFF6,   �� 5       � �� �#006EFF6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $
	 Back��6!   $
 3��6/   $��0q�6@   #��z��5   1   #��z��5   1   ._�5      � .+�5   "   �� $��Center=�64   $��Center=�6Q   5      5       5      P� 5       �@J�IsMouseOver . . �5   "   ��  � �� �#1A8CFF6?   �� 5   "    � �� �#1A8CFF6@   5      5!      5#      5$      5%      .��5&      � .+�5'   
   ]� .+�5(      � $	��Top=�6    � ColumnDefinitions 5)      �� 5*      $��* �6'   �� 5+      $��* �6'   .+�5-      �� $��0��6    �� Orientation$ 
Horizontal=�6-   $
��Left=�6F   $	��Top=�6a   .+�5.      �� $��28��6   $��26��6(   $��
MyIcon.ico��63   �� 5/      $��题目信息��6   50      �� 51      $ 
Horizontal=�6   $��1��66   $��Right=�6F   $	��Top=�6c   .+�52      �� -   5         ��Name$ MinBtn��52      $��32��6-   $	��Top=�68   
������ $��0q�6i   .��53      �� $��28��6    $��26��6,   $��Min.png��67   $��Center=�6H   $��Center=�6e   54      �� 58      -   5        $ ExitBtn��58      $��32��6.   $	��Top=�69   
������ $��0q�59      .��5:      �� $��28��6    $��26��6,   $��	Close.png��67   $��Center=�6J   $��Center=�6g   5;      5<      5=      �� 5?      $
��15 0q�6   .+�5A      � $��0q�6   $��30��6'   .+�5B      �� $��	题目：��6   �� 5C      -   5         ��Name$ topicTextBlock��5C   <   $�� ��6    $
��Wrap=�6(   6<   5D      � 5E      $��0q�6   $��30��6'   .+�5F      �� $
��A：��6    �� 5G      -   5        $ optionATextBlock��5G   @   $�� ��6$   $
��Wrap=�6,   6@   5H      � 5I      $��0q�6   $��30��6'   .+�5J      �� $
��B：��6    �� 5K      -   5        $ optionBTextBlock��5K   @   $�� ��6$   $
��Wrap=�6,   6@   5L      � 5M      $��0q�6   $��30��6'   .+�5N      �� $
��C：��6    �� 5O      -   5        $ optionCTextBlock��5O   @   $�� ��6$   $
��Wrap=�6,   6@   5P      � 5Q      $��0q�6   $��30��6'   .+�5R      �� $
��D：��6    �� 5S      -   5        $ optionDTextBlock��5S   @   $�� ��6$   $
��Wrap=�6,   6@   5T      � 5U      $��0q�6   $��30��6'   .+�5V      �� $��	答案：��6   �� 5W      -   5        $ answerTextBlock��5W   <   $�� ��6    $
��Wrap=�6(   6<   5X      � 5Y      $��0q�6   $��30��6'   .+�5Z      �� $��正确人数：��6   �� 5[      -	   5        $ correctNumTextBlock��5[   <   $�� ��6    $
��Wrap=�6(   $	��350��6Z   �� 5\      -
   5        $ correctNumBtn��5\   *   $��名单��6   #��[  6A   5]      � 5^      $��0q�6   $��30��6'   .+�5_      �� $��未作答人数：��6   �� 5`      -   5        $ noAnswerNumTextBlock��5`   <   $�� ��6    $
��Wrap=�6(   $	��340��6Z   �� 5a      -   5        $ noAnswerNumBtn��5a   *   $��名单��6   #��[  6B   5b      � 5c      $��0q�6   $��30��6'   .+�5d      �� $��错误人数：��6   �� 5e      -   5        $ wrongNumTextBlock��5e   <   $�� ��6    $
��Wrap=�6(   $	��350��6W   �� 5f      -   5        $ wrongNumBtn��5f   *   $��名单��6   #��[  6?   5g      � 5h      $��0q�6   $��30��6'   .+�5i      �� -   5        $ statisticsBtn��5i   .   $��图表情况统计��6   #��[  6E   $	��100��6n   $��25��6{   $��Center=�6�   $��Center=�6�   5j      5o      5r      5s      5t      