   M S B A M L   `   `   `  ����W  SMicrosoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35M8clr-namespace:Microsoft.Kinect;assembly=Microsoft.KinectMicrosoft.Kinect  5      ^ ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers 5       BismillahTA$   BismillahTA.User_Control.Task2   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Fk8clr-namespace:Microsoft.Kinect;assembly=Microsoft.Kinect     5      u
WpfViewers^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5      -   5        .��5
      � 
�������.+�5   
   �� -   5          ��Name$  
mainCanvas��5      $	��720��6%   $��
0,0,189,17q�62   $��Right=�6F   $	��980��6b   
�������.+�5      ;  5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   5        $��kinectColorViewer��5   +   $	��720��6D   $	��980��6Q   5  /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 6]   �� *Kinect+ �� ElementName$ kinectSensorChooser��=  7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5      -   5        $��kinectSensorChooser��5   -    �� Left$ 10��6H   
 �� Top$	 166��6Y   A� 5      -   5        $
��head��5      $
��Left=�6"   $��40��6=   $	��Top=�6J   $��40��6b   
���  ��$ 87��6x   $	 184��6�    J� Opacity$	 0.1Q�6�   A� 5      -   5        $��leftHand��5      
����� �$
��Left=�62   $��40��6M   $	��Top=�6Y   $��40��6q   $	 132��6|   $	 214��6�   $	 0.1Q�6�   A� 5      -   5        $��	rightHand��5      
�������$
��Left=�65   $��40��6P   $	��Top=�6\   $��40��6t   $ 29��6   $	 244��6�   $	 0.1Q�6�   A� 5      -   5        $��leftFoot��5      
���� ��$
��Left=�65   $��40��6P   $	��Top=�6\   $��40��6t    J� RenderTransformOrigin$ -1.688,-6.495&�6   $ 29��6�   $	 184��6�   $	 0.1Q�6�   A� 5      -	   5        $��	rightFoot��5      
���  ��$
��Left=�65   $��40��6P   $	��Top=�6\   $��40��6t   $ 6.255,-8.584&�6   $ 87��6�   $	 244��6�   $	 0.1Q�6�   �� 5      -
   5        $��timer2��5      $	��307��6&   $	 397��63   $
��Wrap=�6E   $	 107��6Y   $	��237��6j   $	��283(�6v   $��Center=�6�    �� TextAlignment$ Center=�6�   $��Center=�6�   $��Verdana)�6�   
���  ��6�   �� 5      -   5        $��startButton��5      $��70��6'   $	 193��63   $ 10��6E   $��70��6U   $0��*/BismillahTA;component/Resources/start.png��6`   6�   �� 5      -   5        $��
helpButton��5      $��70��6&   $	 891��62   $	 634��6D   $��70��6U   $/��)/BismillahTA;component/Resources/help.png��6`   6�   �� 5      -   5        $��
noteButton��5      $��70��6&   $	 724��62   $ 10��6D   $��70��6T   $8��2/BismillahTA;component/Resources/notesUnactive.png��6_   6�   �� 5      $��71��6   $	 275��6    $	��202��62   $:��4/BismillahTA;component/Resources/backgroundScore.png��6>   $ 10��6|   �� 5      $��71��6   $	 484��6    $ 10��62   $	��207��6B   $9��3/BismillahTA;component/Resources/backgroundTime.png��6N   �� 5      -   5        $��finishButton��5      $��70��6(   $	 891��64   $ 10��6F   $��70��6V   $1��+/BismillahTA;component/Resources/finish.png��6a   6�   �� 5      -   5        $��score��5      $��33��6%   $	 363��61   $
��Wrap=�6C   $ 28��6W   $��82��6g   $ 	0.5,0.424&�6r   $��Verdana)�6�   $��30(�6�   
�������$��Center=�6�   $��Center=�6�   $ Center=�6  �� 5      -   5        $��timer��5      $��33��6%   $	 552��61   $
��Wrap=�6C   $ 29��6W   $	��127��6g   $ 	0.5,0.424&�6s   $��Verdana)�6�   $��30(�6�   
�������$��Center=�6�   $��Center=�6�   $ Center=�6  �� 5      -   5        $��
saveButton��5      $��70��6&   $	 811��62   $ 10��6D   $��70��6T   $7��1/BismillahTA;component/Resources/saveUnactive.png��6_   6�   �� 5      -   5        $��
homeButton��5      $��70��6&   $	 109��62   $ 10��6D   $��70��6T   $/��)/BismillahTA;component/Resources/home.png��6_   6�   �� 5      -   5        $��	homeLabel��5      $
��Home��6%   $	 116��64   $ 72��6F   $��63��6V   $��verdana)�6a   $��15(�6v   ���	#FF158DB96�   �� 5      -   5        $��
startLabel��5      $��Mulai��6&   $	 208��66   $ 72��6H   $��62��6X   $��verdana)�6c   $��15(�6x   ���	#FF158DB96�   �� 5       -   5        $��	noteLabel��5       $��Catatan��6%   $	 730��67   $ 72��6I   $��80��6Y   $��verdana)�6d   $��15(�6y   ���	#FF158DB96�   �� 5!      -   5        $��	saveLabel��5!      $��Simpan��6%   $	 815��66   $ 72��6H   $��80��6X   $��verdana)�6c   $��15(�6x   ���	#FF158DB96�   �� 5"      -   5        $��finishLabel��5"      $��Selesai��6'   $	 895��69   $ 72��6K   $��80��6[   $��verdana)�6f   $��15(�6{   ���	#FF158DB96�   �� 5#      -   5        $��	helpLabel��5#      $��Bantuan��6%   $	 890��67   $	 606��6I   $��80��6Z   $��verdana)�6e   $��15(�6z   ���	#FF158DB96�   �� 5$      -   5        $��instructionLabel��5$      $*��$Berdiri pada jarak +- 3m dari kinect��6,   
�������$ 48��6n   $	 262��6   $	��932��6�   $��63��6�   $��verdana)�6�   $��40(�6�   
���  ��$��Center=�6�   $	 0.8Q�6   �� 5%      -   5        $��rectSuccess��5%      ���	#FF7EEC6C$	��160��6<   $	 333��6I   $	 214��6[   $	��342��6l   �� 5&      -   5        $��textSuccess��5&      $��76��6+   $	 371��67   $
��Wrap=�6I   $��SELAMAT��6]   $	 252��6l   $	��263��6}   $��Verdana)�6�   $��55(�6�   ���	#FF1212E26�   A� 5'      -   5        $��head_��5'      $
��Left=�6#   $��20��6>   $	��Top=�6K   $��20��6c   
���  ��$ 87��6y   $	 184��6�   A� 5(      -   5        $��	leftHand_��5(      
����� �$
��Left=�63   $��20��6N   $	��Top=�6Z   $��20��6r   $	 132��6}   $	 214��6�   A� 5)      -   5        $��
rightHand_��5)      
�������$
��Left=�66   $��20��6Q   $	��Top=�6]   $��20��6u   $ 29��6�   $	 244��6�   A� 5*      -   5        $��	leftFoot_��5*      
���� ��$
��Left=�66   $��20��6Q   $	��Top=�6]   $��20��6u   $ -1.688,-6.495&�6�   $ 29��6�   $	 184��6�   A� 5+      -    5        $��
rightFoot_��5+      
���  ��$
��Left=�66   $��20��6Q   $	��Top=�6]   $��20��6u   $ 6.255,-8.584&�6�   $ 87��6�   $	 244��6�   5-      �� 5.   
   -!   5        $��
backButton��5.      $	��209��6"   $��90��6/   $
 -149��6:   $	 256��6M   $��48,297,1220,231q�6^   $9��3/BismillahTA;component/Resources/backButton3-v3.png��6w   6�   �� 5/   
   -"   5        $��	backLabel��5/      $��Kembali ke halaman level��6!   $
��Left=�6D   $��
15,232,0,0q�6_   $	��Top=�6s   $	��174��6�   50      51      