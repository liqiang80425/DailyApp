   M S B A M L   `   `   `  ���� G  CDailyApp.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:DailyApp.WPF.ViewsDailyApp.WPF.Views       DailyApp.WPF.Views.MainWin   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:DailyApp.WPF.Views     [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  _	 [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors 	 !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True � WindowStartupLocation$ CenterScreen=�   MaterialDesignBody#���     MaterialDesignPaper#���  $��微软雅黑)� � Title$ MainWin��$	��700��$
��1200��'  !System.Windows.Shell.WindowChrome   WindowChrome      GlassFrameThickness$ -1q�.��)  #MaterialDesignThemes.Wpf.DialogHost     DialogTheme$ Inherit=�   
Identifier$ 
RootDialog��.��)  #MaterialDesignThemes.Wpf.DrawerHost  -     Name$ 
drawerHost��   IsLeftDrawerOpen �� 	 �� ElementName$	 MenuToggleButton��
 �� Path$
 	IsChecked�   LeftDrawerContent ]� $	��220��.+��� $
��0,20q�$	��Top=�.+��� $��/Images/user.jpg��$��50��$��50�� J� Clip @�  @� Center$ 24,24&� @� RadiusX$ 24Q� @� RadiusY$ 24Q��� -     LeftMenuStyle#��[ ���� *LeftMenuList+* 	 $Microsoft.Xaml.Behaviors.Interaction   Triggers + 	 %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged��* 	 $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2 	 ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *NavigateCmm+   CommandParameter �� *SelectedItem+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType ListBox���� .R���  �� Orientation$ 
Horizontal=�
������ .+�'	  !MaterialDesignThemes.Wpf.PackIcon	  $
��15,0q� 	  Kind �� *Icon+�� ���� *
MenuName+.��]� .+�(
  "MaterialDesignThemes.Wpf.ColorZone
  -   $��16q�.  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp4=�$	��Top=� 
  Mode$ 
PrimaryMid=�.��]� $
��True��.+��� $��Right=�$ 
Horizontal=�.+��� $��/Images/user.jpg��$��25��$��25�� @� $ 12,12&�$ 12Q�$ 12Q��� -    ��Name$ btnMin��$	��—�� ! MaterialDesignFlatMidBgButton#��[ �� -   $ btnMax��$	��☐��#��[ �� -   $ btnClose��$	��✕��#��[ �� $ 
Horizontal=�.+�d� -    d�Name$ MenuToggleButton��4  .System.Windows.Automation.AutomationProperties   Name$ HamburgerToggleButton�� d� 	IsChecked$ FalseY� ' #MaterialDesignHamburgerToggleButton#��[ �� $��24,0,0,0q�  MaterialDesignToolButton#��[   � ToolTip$  Previous Item��+  %MaterialDesignThemes.Wpf.RippleAssist!   Feedback! ��  �� *Self+$
 
Foreground�  BrushRoundConverter" �� 	Converter#" [ ���� *	GoBackCmm+��0  *MaterialDesignThemes.Wpf.PackIconExtension  #   Kind$# 	ArrowLeft=�$   Size$ 24����  �� " �$
 (TextElement.Foreground)��� $��16,0,0,0q�#��[ $  	Next Item��! ��  �� *Self+$
 
Foreground�#" [ ���� *GoForwardCmm+��  $# 
ArrowRight=�$ 24����  �� " �$
 (TextElement.Foreground)��� $
��15,0q�$��Center=�$��Center=�$) #Mat15.0erial Design In XAML Toolkit��$��22(�$��我的日常���� !  Prism.Regions.RegionManager%   
RegionName$% MainViewRegion��