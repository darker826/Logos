   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   	   P LCoding4Fun.Kinect.Wpf, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullnKclr-namespace:Coding4Fun.Kinect.Wpf.Controls;assembly=Coding4Fun.Kinect.WpfCoding4Fun.Kinect.Wpf.Controls 5   	   C ?PreHands, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PreHandsPreHands 5   	      PreHands.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   	   `ControlsKclr-namespace:Coding4Fun.Kinect.Wpf.Controls;assembly=Coding4Fun.Kinect.Wpf     5   	   (localclr-namespace:PreHands     5   	   -   5          � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)    � WindowState$ 	Maximized=�5	   	    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$# local:MainWindow.ShowSettings��5      6A     5   
   -   5        $# local:MainWindow.HideSettings��5      6A   .��5      �� -   5        $��
MainCanvas��5      .+�5   
   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   5        $��kinectColorViewer1��5   o   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Stretch$ UniformToFill=�6    �� Left$ 0��67   
 �� Top$ 0��6G   $	��800��6V   $	��600��6b      Kinect 5      ��  �� ElementName$ kinectSensorChooser1��	 �� Path$	 Kinect�A� 5   
   -   5        $��leftEllipse��5   =   $ 0��6   $ 0��6"   $��50��61   $��50��6Q   ���	#FF4D298D
 J� Opacity$
 1Q�6m   
�������6y   A� 5   
   -   5        $��rightEllipse��5   P   $	 100��6   $ 0��6$   ���	#FF2CACE3$��50��6D   $��50��6d   $
 1Q�6o   
�������6{   =   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5   
   -   5        $��kinectSensorChooser1��5   D   $	 250��6!   $	 380��63   $	��328��6`   �� 5   
   -	   5        $��	headImage��5   =   $ 66��6   $ 90��6!   $��87��61   $
��Fill=�6N   $��84��6]   $��/Images/Face.png��6h   �� 5   
   -
   5        $��SettingsButton��5      $��	750 0 0 0q�6'   $��Stretch=�6:   $	��566��5      $��45��6    $
��Open��6+   .��5      �� .+�5      ��  � LayoutTransform 5      V� 5      .~�5       ��  �� Angle$	 270Q�6.   5!      
Settings5$      �� 5%      $��Images\Settings-icon.png��6   $��33��6:   $��33��6E   $��0,10,0,0q�6Q   $��Center=�6c   5&      5'        PreHands.SettingsControl  5(   
   -   5          Name$ SettingsControl��5(       $��Right=�69   $��Stretch=�6U      Settings 6q   �� $	 Settings�0  *Coding4Fun.Kinect.Wpf.Controls.HoverButton  5,   
   -   5        $��hoverButtonRight��5,      $	 620��67   $ 20��6I   $��80��6Y   $	��150��6e      	ImageSize$ 78Q�6q   
�������
���� ��$
 1Q�5-       5.      �� 50   
    �� TextAlignment$ Right=�6   $��630 42 10 10q�6*   $��23(�6@   
�������$��Center=�6a   $��Center=�6~   �� 51      -   5         �Name$ PPT_Link��51   =   
������� � Cursor$
 Hand��6/   .�6M   Presentation53      54        56   
   -   5        $��hoverButtonRight2��56      $	 620��68   $	 120��6J   $��80��6[   $	��150��6g   $ 78Q�6s   
�������
���� ��$��8(�57      $
 1Q�6,   .��58      �� $��22(�6   
�������$��Center=�69   $��Center=�6V   Mouse Cursor6   59        5;   
   -   5        $��hoverButtonRight3��5;      $	 620��68   $	 220��6J   $��80��6[   $	��150��6g   $ 78Q�6s   
�������
���� ��$��8(�5<      $
 1Q�6,   5=      �� 5?   
   $ Right=�6   $��640 240 10 10q�6*   $��23(�6A   
�������$��Center=�6b   $��Center=�6   �� 5@      -   5        $ Record_Link��5@   e   $��/Record/KinectRecorder.cs?�6   
�������$
 Hand��6W   .�6x   	Recording5B      5C      5E      5F      