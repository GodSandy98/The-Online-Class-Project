   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest      WpfTest.CreateRoomWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfTest       � Icon$  
MyIcon.ico�� � Title$ CreateRoomWindow��$	��450��$	��300�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    BlueButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers P� Property P� Value��%�  &	        �� " ��.���  � ��$ 45����  � ��$ 45����  � ��$ 0q���  � ��
 �������  � �� �#006EFF��  � �� �#006EFF��  � ��$ 51����  � ��$
 Auto����  � ��$ Center=���  � ��$ 23.8,9,23.6,37q���  � �� �� " ��.R��� $
	 Back��$
 3��$��1q�#��z��#��z��._�� .+��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � �� �#1A8CFF��  � �� �#1A8CFF.��� .+�]� .+���  �� Orientation$ 
Horizontal=�$	��Top=�$��Right=�$	��Top=�.+��� -    ��Name$ exitBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��	Close.png��$��Center=�$��Center=��� .+��� $��	25 0 25 0q�.+��� $��教室名称：��$��2q�$��Stretch=�$��Stretch=��� -    ��Name$ classTitleTextBox��$	��8,2q�$��20��$��Center=�$��Stretch=��� $��人数上限：��$��2 8 2 2q��� -   $ maxPeopleTextBox��$	��8,2q�$��Center=�$��20��$��Stretch=�.��   HintText j� TileMode �� Opacity j� Stretch j� 
AlignmentX !� Visual��%k   &	       !� $
 None=�$	 0.5Q�$
 None=�$
 Left=� �� $��Italic$�$��默认人数上限50��.������ " �� �� Triggers P�  � �� Z� . ���  � ��# [ P�  � ��$  ��. ���  � ��# [ .���� $��教室密码��$��2 8 2 2q��� -    ��Name$ hasPasswordCheckBox��$��开启教室密码��$��8 2 8 4q�G� -    G�Name$ passwordPasswordBox��$	��8,2q�$��20��$��Center=�{���  �� ElementName$ hasPasswordCheckBox�� �� Path$ 	IsChecked��� -   $ createRoomBtn��
 � Tag$  ��$��创建教室��#��[  $��	8 160 8 8q�$��28��$	��326��