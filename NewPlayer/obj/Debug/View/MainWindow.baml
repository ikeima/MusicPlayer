   M S B A M L   `   `   `  ����D  @NewPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:NewPlayer	NewPlayer  5   	       NewPlayer.MainWindow   5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:NewPlayer     5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��700��6)    � WindowStyle$
 None=�5   	   
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � ��$ Comic sans ms)�6+   �� 5       �@��TextAlignment$ Center=�6.   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �� -   5        
���   �$��2��5   %   	 J� Opacity$		 0.4Q�69   
 �� CornerRadius$
 20��6G   $	��450��6Y   $	��700��6f   6r   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6%   $��24��6>   $��70��6J   $	��Top=�6U   $��Right=�6m   $��0,0,40,0q�6�   .+�5      �� 
���   �
���   �$��5(�6@   $��20��6M   $��20��6Y   $��2q�6d   .��5       '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Minus=�6*   $��-14,-3q�67   $		 0.8Q�6G   5!      �� 5"      
���   �
���   �$��20��6?   $��20��6K   $��2q�6V   .��5#        $ SquareOutline=�6*   $��-14,-3q�6?   $		 0.8Q�6O   5$      �� 5%      
���   �
���   �$��20��6?   $��20��6K   $��2q�6V   .��5&        $ Close=�6*   $��-14,-3q�67   $		 0.8Q�6G   5'      5(      #  MaterialDesignThemes.Wpf.Card  5)   
   
�������$	��400��60      UniformCornerRadius$ 20Q�6=   $	��300��6V   $��Center=�6b   .��5*      �� $��10q�6   .+�5+      �� 
������ $	��330��63   $	��Top=�6@   $��Hidden=�5.      $��Hidden=�5/      
�������$
��Left=�50   -   $	��280��6H   $��Comic sans ms)�51      $��15(�65   ��5,      �� *MusicCollection+ �� SelectedItem 5-      �� *SelectedSong+52      �� 54      $��50��6   $��50��6$   
���   �
���   ���55      �� *	LoadMusic+.��58        $ 
FolderOpen=�6.   $��40��6@   $��40��6M   $	��-11q�6X   59      5:      5;        5=   
   $��1��6   $	��400��6.   $ 20Q�6;   $	��300��6T   
�������.��5>      �� .+�5?      �� $	��250��6=   $	��300��6J   $	��Top=�6V   $
��Fill=�6n   ��6   �� *SelectedSongImage+6   �� 5@      $��50��6   $��20(�6(   ��66   �� *SelectedSongName+6Z   �� 5A      $��35��6   $��20(�6(   ��66   �� *SelectedSongArtist+6\   �� 5B      $��Center=�6   $ 
Horizontal=�6:   $��0,10,0,0q�6S   .+�5C      �� 
���   �
���   �$��50��6E   $��40��6P   ��6\   �� *
Previous+.��5D        $ SkipPreviousCircleOutline=�62   $	��-10q�6S   $��30��6`   $��30��6l   5E      �� 5F      
���   �
���   �$��50��6F   $��40��6Q   ��6]   �� *Pause+.��5G        $ PauseCircleOutline=�62   $	��-10q�6L   $��30��6Y   $��30��6e   5H      �� 5I      
���   �
���   �$��50��6F   $��40��6Q   ��6]   �� *Play+.��5J        $ 	PlayArrow=�62   $	��-10q�6C   $��30��6P   $��30��6\   5K      �� 5L      
���   �
���   �$��50��6G   $��40��6R   ��6^   �� *Resume+.��5M        $ PlayCircleOutline=�62   $	��-10q�6K   $��30��6X   $��30��6d   5N      �� 5O      
���   �
���   �$��50��6F   $��40��6Q   ��6]   �� *Next+.��5P        $ SkipNextCircleOutline=�62   $	��-10q�6O   $��30��6\   $��30��6h   5Q      5R      5S      5T      5V      5W      