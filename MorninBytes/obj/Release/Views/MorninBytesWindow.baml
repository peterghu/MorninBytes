   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  F BMorninBytes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MorninBytes.ViewsMorninBytes.Views ?$clr-namespace:MorninBytes.ViewModelsMorninBytes.ViewModels )   #MorninBytes.Views.MorninBytesWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     1Viewsclr-namespace:MorninBytes.Views     ;
ViewModels$clr-namespace:MorninBytes.ViewModels     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  MorninBytes��$	��830��$	��565��$	��830�� � 
ResizeMode$ CanMinimize=� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName P� 
SourceName��%�  '��       �� " ��.���  � �� �� " ��.R�� .+��� $��Border��
Border�����#428fe2
�������$��0,2,0,2q�$��	-2,-1,0,0q� . ._��� $��	TextBlock��	TextBlock��$��14(�$��Arial)�$��Center=�$��Center=�
��������� $ ContentSite��ContentSite��$��Center=�$��Center=�$��Header��$��	12,0,12,0q�	 P� 
 �@��
IsSelected . . ��� $ Border�� � �� �	#FFFFC356�� $ 	TextBlock�� � ��
 ������� 	Setter_77��$ Border�� � ��
 �����P� $ Border��
 �@J�IsMouseOver . . ��� $ Border�� � �� �	#FF2569B1�� 	Setter_81��$ Border�� � �� �	#FF2569B1(   "MahApps.Metro.Controls.MetroWindow   RightWindowCommands +   %MahApps.Metro.Controls.WindowCommands  .@��� $
��Open���� $��About������ *AboutMenu_Click+ � DataContext /  )MorninBytes.ViewModels.WebsiteUIViewModel  .���� .+���  �� TabStripPlacement$
 Left=�$��0,0,0,0q�
������� ��%3   '�       �� " �.���  � �� �#428fe2.@��� -    ��Name$ OpenWebsites��$��Dest Manager��$��80��.��&   MorninBytes.Views.WebsiteManager  �� -   $ ImageDownloader��$��Image Downloader��$��80��.���� $��WrapWithOverflow=�$	��509��$��10,0,0,0q�USBatch image downloading features be implemented in later versions of MorninBytes :)