   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest      WpfTest.NewLoginWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfTest       � Icon$  
MyIcon.ico�� � Title$ NewLoginWindow��$��Center=�$	��300��$	��344�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources    BlueButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius �� Triggers P� Property P� Value��%�  &	        �� " ��.���  � ��$ 45����  � ��$ 45����  � ��$ 0q���  � ��
 �������  � �� �#006EFF��  � �� �#006EFF��  � ��$ 51����  � ��$
 Auto����  � ��$ Center=���  � ��$ 23.8,9,23.6,37q���  � �� �� " ��.R��� $
	 Back��$
 3��$��1q�#��z��#��z��._�� .+��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � �� �#1A8CFF��  � �� �#1A8CFF.��� .+�]� .+���  �� Orientation$ 
Horizontal=�$	��Top=�$��Right=�$	��Top=�.+��� -    ��Name$ LonginWindowLabel��$��登录��$	��200���� -    ��Name$ 
SettingBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Setting.png��$��Center=�$��Center=��� -   $ MinBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Min.png��$��Center=�$��Center=��� -   $ MaxBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��Max.png��$��Center=�$��Center=��� -   $ ExitBtn��$��32��$	��Top=�
������ $��0q�.���� $��28��$��26��$��	Close.png��$��Center=�$��Center=�� .+��� -   $ accountLabel��$��教育ID：��$
��Left=�$��	60,40,0,0q�$	��Top=��� -   $ passwordLabel��$��密  码：��$
��Left=�$��	60,90,0,0q�$	��Top=��� -    ��Name$ accountTextBox��$�� ��$
��Left=�$��23��$��
120,40,0,0q� �� TextWrapping$
 Wrap=�$��Center=�$	��Top=�$	��165��G� -	    G�Name$ passwordTextBox�� G� Password$  ��$
��Left=� G� PasswordChar$ *��$��23��$��
120,90,0,0q�$��Center=�$	��Top=�$	��165���� -
   $ LoginBtn��#��[  $��登录��$
��Left=�$��
60,200,0,0q�$	��Top=�$	��224��$��23���� -   $ 
loginLabel��$��注册��$��
60,170,0,0q�$
��Left=�$	��Top=� J� RenderTransformOrigin$ -0.458,1.328&��� -    ��Name$ rememberCheckBox��$��记住密码��$��
60,135,0,0q�$
��Left=�$	��Top=��� -   $ forgetLabel��$��忘记密码？��$��215,170,0,0q�$
��Left=�$	��Top=�