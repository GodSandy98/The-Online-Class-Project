   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest      WpfTest.QuizInfoWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfTest       � Title$  QuizInfoWindow��$	��450��$	��520�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    BlueButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� CornerRadius
 �� Triggers P� Property P� Value��%}  &	        �� " ��.���  � ��$ 0q���  � ��$ 20����  � ��$ 40����  � ��
 �������  � �� �#006EFF��  � �� �#006EFF��  � �� �� " ��.R��� $
 Back��$	 3��$��0q�#��z��#��z��._�� .+��� $��Center=�$��Center=�
 P�  �@J�IsMouseOver . . ���  � �� �#1A8CFF��  � �� �#1A8CFF.��� .+�]� .+�� $	��Top=� � ColumnDefinitions �� $��* ��� $��* �.+��� -    ��Name$ LonginWindowLabel��$��QuizInfoWindow��$��0����  �� Orientation$ 
Horizontal=�$��1��$��Right=�$	��Top=�.+��� -    ��Name$ 
SettingBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Setting.png��$��Center=�$��Center=��� -   $ MinBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Min.png��$��Center=�$��Center=��� -   $ MaxBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Max.png��$��Center=�$��Center=��� -   $ ExitBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��	Close.png��$��Center=�$��Center=��� $
��15 0q�.+�� $��0q�$��30��.+��� $��	题目：���� -    ��Name$ topicTextBlock��$�� ��$
��Wrap=�� $��0q�$��30��.+��� $
��A：���� -   $ optionATextBlock��$�� ��$
��Wrap=�� $��0q�$��30��.+��� $
��B：���� -   $ optionBTextBlock��$�� ��$
��Wrap=�� $��0q�$��30��.+��� $
��C：���� -	   $ optionCTextBlock��$�� ��$
��Wrap=�� $��0q�$��30��.+��� $
��D：���� -
   $ optionDTextBlock��$�� ��$
��Wrap=�� $��0q�$��30��.+��� $��	答案：���� -   $ answerTextBlock��$�� ��$
��Wrap=�� $��0q�$��30��.+��� $��正确人数：���� -   $ correctNumTextBlock��$�� ��$
��Wrap=�$	��350���� -   $ correctNumBtn��$��名单��#��[  � $��0q�$��30��.+��� $��未作答人数：���� -   $ noAnswerNumTextBlock��$�� ��$
��Wrap=�$	��340���� -   $ noAnswerNumBtn��$��名单��#��[  � $��0q�$��30��.+��� $��错误人数：���� -   $ wrongNumTextBlock��$�� ��$
��Wrap=�$	��350���� -   $ wrongNumBtn��$��名单��#��[  � $��0q�$��30��.+��� -   $ statisticsBtn��$��图表情况统计��#��[  $	��100��$��25��$��Center=�$��Center=�