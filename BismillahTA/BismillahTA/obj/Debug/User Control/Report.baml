   M S B A M L   `   `   `  ����l  hSystem.Windows.Controls.DataVisualization.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5      �rclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit)System.Windows.Controls.DataVisualization  5       BismillahTA%   BismillahTA.User_Control.Report   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      �g{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5      �hrclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit     5      .��5
      � 
�������.+�5   
   �� $
��Left=�6   $	��537��60   $��	10,10,0,0q�6=   $	��Top=�6P   $	��902��6h   .@�5      �� $��Riwayat��6   $��Verdana)�6'   $��15(�6<   $��	-2,0,0,-6q�6J   $
��Left=�6]   $	��123��6x   ���	#FF2980B9
���   �.��5      � ���	#FFBDDDE6$��0,5,0,0q�6.   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��
reportGrid��5      $
��Left=�61   $	��449��6L   $��	41,22,0,0q�6Y   $	��Top=�6l   $	��432��6�   $��verdana)�6�   $��15(�6�   6�   �� 5      $��Lihat berdasarkan :��6   $
��Left=�6:   $��28��6U   $��
659,22,0,0q�6a   $	��Top=�6u   $	��154��6�   ���#2980b96�   �� 5      $��
Pelatihan ��6   $
��Left=�61   $��28��6L   $��
659,55,0,0q�6X   $	��Top=�6l   $��83��6�   ���#2980b96�   �� 5      $��Level ��6   $
��Left=�6-   $��28��6H   $��
659,97,0,0q�6T   $	��Top=�6h   $��59��6�   ���#2980b96�   �� 5      -   5        $��taskComboHistory��5      $
��Left=�67   $��28��6R   $��
761,55,0,0q�6^   $	��Top=�6r   $��81��6�   �� 5      -   5        $��levelComboHistory��5      $
��Left=�68   $��28��6S   $��
761,97,0,0q�6_   $	��Top=�6s   $��81��6�   ���	#FF2980B96�   �� 5      -   5        $��chooseButtonHistory��5      $��Pilih��68   $
��Left=�6H   $��34��6c   $��761,150,0,0q�6o   $	��Top=�6�   $��81��6�   $��18(�6�   ���	#FFF9F3F3���	#FF2980B9��6�   Z�   J� Effect  5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ 	#FF2980B9��6/   5      5      5      �� 5      $��Grafik��6   $��Verdana)�6&   $��15(�6;   $��-5,0,-41,-4q�6I   
���   ����	#FF5CF954.��5      � ���	#FFA9F3B6 � ColumnDefinitions 5      �� 5      $
��211* �6+   �� 5      $
��685* �6+   .+�5!      >   8System.Windows.Controls.DataVisualization.Charting.Chart  -   5        $��MyChart1��5!      
�������$��37,29,200,59q�6@   $��10(�6�   
���   �$��2��6�   ��6n   Z�    Series. 5"      C   =System.Windows.Controls.DataVisualization.Charting.LineSeries  -   5        $��myLine��5"   '   ?   9System.Windows.Controls.DataVisualization.Charting.Series   Title$ Perkembangan pasien��5#   `   $��12(�6|   
���   �$��0,0,6,0q�6�   H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   IndependentValueBinding 5"   5   �� *Key+   DependentValueBinding 5#   8   �� *Value+5$      5%      �� 5&      $��Lihat berdasarkan :��6   $
��Left=�6:   $��28��6U   $��490.119,32,0,0q�6a   $	��Top=�6y   $	��154��6�   ���#2980b9$��1��6�   �� 5'      $��
Pelatihan ��6   $
��Left=�61   $��28��6L   $��
490,65,0,0q�6X   $	��Top=�6l   $��83��6�   ���#2980b9$��1��6�   �� 5(      $��Level ��6   $
��Left=�6-   $��28��6H   $��490.119,107,0,0q�6T   $	��Top=�6m   $��59��6�   ���#2980b9$��1��6�   �� 5)      -   5        $��taskComboBox��5)      $
��Left=�63   $��28��6N   $��
594,65,0,0q�6Z   $	��Top=�6n   $��81��6�   $��1��6�   �� 5*      -   5        $��levelComboBox��5*      $
��Left=�64   $��28��6O   $��594,107,0,0q�6[   $	��Top=�6p   $��81��6�   ���	#FF2980B9$��1��6�   �� 5+      -	   5        $��chooseButton��5+      $��Pilih��61   $
��Left=�6A   $��34��6\   $��594,158,0,0q�6h   $	��Top=�6}   $��81��6�   $��18(�6�   ���	#FFF9F3F3���	#FF2980B9$��1��6  ��6�   Z�   5,        5-      $ 	#FF2980B9��6/   5/      �� 50      $��Tanggal Pelatihan��6   $
��Left=�68   $��28��6S   $��
22,448,0,0q�6_   $	��Top=�6s   $	��118��6�   
���   �$��12(�6�   $��1��6�   �� 51      $��Nilai��6   $
��Left=�6,   $��28��6G   $��	2,221,0,0q�6S   $	��Top=�6f   $��44��6~   
���   �$��12(�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�52      V� 53      .~�54   "   �� �� 55   "   �� 56   "    �� Angle$ -90.0Q�62   T� 57   "   58      5:      5;      5<      �� 5=      $��Catatan��6   $��Verdana)�6'   $��15(�6<   $��42,0,-88,-4q�6J   ���	#FFF98714
���   �.��5>      � ���	#FFF9EBBB 5?      �� 5@      $
��153* �6+   �� 5A      $
��295* �6+   .+�5C        -
   5        $��noteGrid��5C      $
��Left=�6/   $	��428��6J   $��	38,31,0,0q�6W   $	��Top=�6j   $	��613��6�   $��verdana)�6�   $��15(�6�   $��2��6�   �� 5D      $��Cari berdasarkan tanggal��6   $
��Left=�6?   $��28��6Z   $��
370,31,0,0q�6f   $	��Top=�6z   $	��210��6�   ���#2980b9$��1��6�   �� 5E      -   5        	 ��Name$	 searchButton��5E      $
��Cari��63   $
��Left=�6B   $��34��6]   $��477,247,0,0q�6i   $	��Top=�6~   $��89��6�   $��18(�6�   ���	#FFF9F3F3���	#FF2980B9$��1��6  ��6�   Z�   5F        5G      $ 	#FF2980B9��6/   5I      �� 5J      -   5        $��comboDay��5J      $��1��6/   $
��Left=�6?   $��37��6Z   $��
477,64,0,0q�6f   $	��Top=�6z   $��89��6�   �� 5K      -   5        $��
comboMonth��5K      $��1��61   $
��Left=�6A   $��37��6\   $��477,119,0,0q�6h   $	��Top=�6}   $��89��6�   �� 5L      -   5        $��	comboYear��5L      $��1��60   $
��Left=�6@   $��37��6[   $��477,176,0,0q�6g   $	��Top=�6|   $��89��6�   �� 5M      $��Tanggal��6   $
��Left=�6.   $��28��6I   $��
370,64,0,0q�6U   $	��Top=�6i   $��88��6�   ���#2980b9$��1��6�   �� 5N      $��Bulan��6   $
��Left=�6,   $��28��6G   $��370,119,0,0q�6S   $	��Top=�6h   $��88��6�   ���#2980b9$��1��6�   �� 5O      $��Tahun��6   $
��Left=�6,   $��28��6G   $��370,176,0,0q�6S   $	��Top=�6h   $��88��6�   ���#2980b9$��1��6�   5P      5Q      5R      5T      5U      