   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:PrimalEditor.GameProject1PrimalEditor.GameProject1  5      /    )PrimalEditor.GameProject1.OpenProjectView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:PrimalEditor.GameProject1     5      $	��800��5        � DataContext  5	      +  %PrimalEditor.GameProject1.OpenProject  5
   
   .��5      �� 
�������.+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5      �� -   5         ��Name$ projectsListBox��5   P   $	��195��6   $	��250��6"   ��6/   �� *
Projects+��5      �� 5       �� 
TargetType" ��6   .�5      -   5        5      ��5      �� 5      .R�5      �� $ 
Horizontal=�6%   .+�5      �� $��0,0,5,0q�6<   $��50��6N   ��6$   �� *Icon+6\   �� 5      ��6(   �� *ProjectName+6G   5      5      5      �� 5!      $	��400��6   $	��250��6!   $��5,0,0,0q�6.   ��5"      �� 5#       �� ImageSource 6!   ��  �� Path$ SelectedItem.Screenshot� �� ElementName$ projectsListBox��5&      5'      �� 5(   
   $ 
Horizontal=�6   $��Center=�6.   $��	30,30,0,0q�6K   .+�5)      �� -   5        $
��Open��5)      $��96��6$    �� 	IsDefault$
 True��6/   6W   �� 5*      $
��Exit��6   $��96��6$   $��30,0,0,0q�6/    �� IsCancel$
 True��6A   6S   5+      5,      5-      