   M S B A M L   `   `   `  ���� G  CDailyApp.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DailyApp.WPF.ViewsDailyApp.WPF.Views  A%clr-namespace:DailyApp.WPF.ExtensionsDailyApp.WPF.Extensions       DailyApp.WPF.Views.LoginUC   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:DailyApp.WPF.Views     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 3pwdEx%clr-namespace:DailyApp.WPF.Extensions  	 $	��350��$	��600��# 	 Prism.Services.Dialogs.Dialog    WindowStyle  ��  �� 
TargetType" �.���  �� Property � �� �� Value$	 600����  � ��$	 350����  �@�SizeToContent$ WidthAndHeight=���  �@�
ResizeMode$ NoResize=���  �@ WindowStartupLocation$ CenterScreen=�.��� 
������� � ColumnDefinitions �� $
��1.5* ��� .+�'  !MaterialDesignThemes.Wpf.Snackbar  -     Name$ RegLoginBar��$��2��   MessageQueue 4  .MaterialDesignThemes.Wpf.MessageQueueExtension  �� $��/Images/login.png��7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  $��1�� �� SelectedIndex �� *SelectedIndex+ �� FallbackValue$ 0��.@�<  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  .��]� $��15q�$��Center=�.+��� $��欢迎使用��$
��Bold"�$	��Top=�$��22(�$
��0,10q��� $
��0,10q�)  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 请输入账号��$	��Top=����� *	Account+G� $
��0,10q�$	 请输入密码��$	��Top=�/   )DailyApp.WPF.Extensions.PasswordBoxExtend

   Pwd
 �� *Pwd+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 1	   +DailyApp.WPF.Extensions.PasswordBoxBehavior	  �� $��登录系统��$	��Top=����� *
LoginCmm+]� $	��0,5q�$��False��.+��� $��注册账号��   Triggers +
  %Microsoft.Xaml.Behaviors.EventTrigger
   
  	EventName$ MouseLeftButtonDown��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *ShowRegInfoCmm+�� $��忘记密码?��$��Right=�  .��]� $��15q�$��Center=�.+��� $��注册账号��$
��Bold"�$	��Top=�$��22(�$
��0,10q��� $
��0,10q�$	 请输入姓名��$	��Top=����� *AccountInfoDTO.Name+�� $
��0,10q�$	 请输入账号��$	��Top=����� *AccountInfoDTO.Account+G� $
��0,10q�$	 请输入密码��$	��Top=�
 �� *AccountInfoDTO.Pwd+$ TwoWay=�$ PropertyChanged=� 	  G� $
��0,10q�$	 请再次输入密码��$	��Top=�
 �� *AccountInfoDTO.ConfirmPwd+$ TwoWay=�$ PropertyChanged=� 	  �� $��注册账号��$	��Top=����� *RegCmm+�� $��返回登录��$
��0,10q�$	��Top=�    MaterialDesignOutlinedButton#��[  ���� *ShowLoginInfoCmm+