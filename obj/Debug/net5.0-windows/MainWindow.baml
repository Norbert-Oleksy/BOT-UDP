   M S B A M L   `   `   `  ����B  >BOT UDP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:BOT_UDPBOT_UDP  5   	       BOT_UDP.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:BOT_UDP     5   	   -   5          � 
ResizeMode$  NoResize=�5   	    � Title$ Bot UDP��5	   	   $	��700��6   $	��800��6&   $��24(�62   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $	��370 �6   �� 5      $
��auto �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   �� $��1��6   $��1��6!   $��CONFIGURATION��61   $��20,0,0,0q�6F   �� 5    
   $��1��6   $��3��6!   $��UDP-BOT v.1.0.0��61   �� 5"   
   $��2��6   $��1��6!   $	��IP:��61   $��0,25,0,0q�6<   �� 5#   
   -   5         ��Name$ RndMsgBlock��5#      $��2��6)   $��3��66   $��Random��6F   �� 5%   
   -   5         ��Name$ 
IP_TextBox��5%      $��3��6(   $��1��65   �� 5'   
   $��4��6   $��1��6!   $��	UDP PORT:��61   $��0,15,0,0q�6B   �� 5)   
   -   5        $ UDP_PORT_TextBox��5)      $��5��6,   $��1��69   $��50000��6I   �� 5+   
   -   5         ��Name$ START_STOP_Button��5+      $��6��6,   $��1��69   $��START��6I   $��	10,25,0,0q�6Y   6l   �� 5-   
   $��7��6   $��1��6!   $��INCOMING DATA��61   $��	25,35,0,0q�6F   &   System.Windows.Controls.DataGrid  50   
   -   5          Name$ DataTableGrid��50      $��8��6*   $��1��67   $��3��6G   $
��True��6[   $
��Auto=�6�      Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn	   Header$	 	timestamp��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 68   �� *	timestamp+  53      $	 sourceIP��6%   
 67   �� *
sourceIP+  54      $	 message��6%   
 66   �� *	message+56      57      58      