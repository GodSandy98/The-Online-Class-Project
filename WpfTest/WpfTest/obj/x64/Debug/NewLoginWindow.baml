   M S B A M L   `   `   `  ����B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfTestWpfTest  5   	       WpfTest.NewLoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfTest     5   	     � Icon$  
MyIcon.ico��5       � 
ResizeMode$ CanMinimize=�6,    � Title$ NewLoginWindow��5   	   $��Center=�6    $	��300��6C   $	��344��6P    � WindowStyle$
 None=�6\    � WindowStartupLocation$ CenterScreen=�6o    � 	Resources 5	         BlueButtonStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius �� Triggers P� Property P� Value��%K  &	        �� 5
   
   " ��6(   .�5      ��  � ��$ 45��6&   �� 5       � ��$ 45��6'   �� 5       � ��$ 0q�60   �� 5       � ��
 �����6+   �� 5       � �� �#006EFF6,   �� 5       � �� �#006EFF6+   �� 5       � ��$ 51��6&   �� 5       � ��$
 Auto��6'   �� 5       � ��$ Center=�64   �� 5       � ��$ 23.8,9,23.6,37q�6'   �� 5       � �� 5      �� 5      "	 ��6&   .R�5      �� $

 Back��6!   $ 3��6/   $��1q�6@   #��z��5   1   #��z��5   1   ._�5      � .+�5   "   �� $��Center=�64   $��Center=�6Q   5      5       5      P� 5        �@J�IsMouseOver . . �5!   "   ��  � �� �#1A8CFF6?   �� 5"   "    � �� �#1A8CFF6@   5#      5%      5'      5(      5)      .��5*      � .+�5+   
   ]� .+�5,      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�62   $��Right=�6G   $	��Top=�6d   $��
0,0,-0.4,0q�6|   $	��328��6�   .+�5-      �� $��28��6   $��26��6$   $��
MyIcon.ico��6/   �� 5.      -   5         ��Name$ LonginWindowLabel��5.   %   $��登录��6   $	��240��6@   �� 52      -   5         ��Name$ MinBtn��52      $��32��6)   $	��Top=�64   
������ $��0q�6e   .��53      �� $��28��6   $��26��6(   $��Min.png��63   $��Center=�6D   $��Center=�6a   54      �� 58      -   5        $ ExitBtn��58      $��32��6*   $	��Top=�65   
������ $��0q�59      .��5:      �� $��28��6   $��26��6(   $��	Close.png��63   $��Center=�6F   $��Center=�6c   5;      5<      � 5=      .+�5>      �� -   5        $ accountLabel��5>   (   $��教育ID：��6   $
��Left=�6>   $��	60,40,0,0q�6Y   $	��Top=�6l   �� 5?      -   5        $ passwordLabel��5?   (   $��密  码：��6   $
��Left=�6?   $��	60,90,0,0q�6Z   $	��Top=�6m   �� 5@      -   5         ��Name$ accountTextBox��5@      $�� ��62   $
��Left=�6:   $��23��6U   $��
120,40,0,0q�5A       �� TextWrapping$
 Wrap=�6&   $��Center=�6:   $	��Top=�6\   $	��165��6t   G� 5B      -   5         G�Name$ passwordTextBox��5B       G� Password$  ��67   $
��Left=�6C    G� PasswordChar$ *��6^   $��23��6o   $��
120,90,0,0q�5C      $��Center=�6(   $	��Top=�6J   $	��165��6b   �� 5D      -   5        $ LoginBtn��5D      #��[  6+   $��登录��6l   $
��Left=�6y   $��
60,200,0,0q�5E      $	��Top=�6%   $	��224��6=   $��23��6I   �� 5F      -	   5        $ 
loginLabel��5F   %   $��注册��6   $��
60,170,0,0q�69   $
��Left=�6M   $	��Top=�6i    J� RenderTransformOrigin$ -0.458,1.328&�6�   �� 5G      -
   5         ��Name$ rememberCheckBox��5G   *   $��记住密码��6   $��
60,135,0,0q�6D   $
��Left=�6X   $	��Top=�6t   �� 5H      -   5        $ forgetLabel��5H   (   $��忘记密码？��6   $��215,170,0,0q�6=   $
��Left=�6R   $	��Top=�6n   5I      5J      5K      5L      