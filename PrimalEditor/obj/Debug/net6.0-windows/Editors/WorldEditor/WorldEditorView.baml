   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PrimalEditor.EditorsPrimalEditor.Editors  5      ?$clr-namespace:PrimalEditor.UtilitiesPrimalEditor.Utilities  5      *    $PrimalEditor.Editors.WorldEditorView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:PrimalEditor.Editors     5      4utl$clr-namespace:PrimalEditor.Utilities     5      $
|�True��5        J� InputBindings  5      �� 5   
   
 �� Key$ Z��65    �� 	Modifiers$
 Ctrlf�6=    �� Command 6   �� *UndoCommand+6P   �� 5   
   $ Y��65   $
 Ctrlf�6=    6   �� *RedoCommand+6P   �� 5   
   $ S��65   $
 Ctrlf�6=    6   �� *SaveCommand+6P   .��5      �  � RowDefinitions 5   
   �� 5      $��32 �6   6*   �� 5      6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $
��Undo��6   ��6%   �� *UndoCommand+6G   �� 5      $
��Redo��6   $
��10,0q�6E   ��6%   �� *RedoCommand+6U   5      � 5   
   $��1��6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��0��6    5      �� 5       �� 5!      .+�5#      �� $��1��6   $��5��6(   $��Stretch=�63   $	��Top=�6Q   )   #PrimalEditor.Utilities.UndoRedoView  5$      $��1��6A    � DataContext 6    �� *
undoRedo+6A   5%      �� 5&   
   $��1��6   $��5��6'   $
��Left=�61   $��Stretch=�6L   � 5'   
   $��1��6   $��5,0,0,0q�6    5(      �� 5)      �� 5*      .+�5,      �� $��1��6   $��5��6(   $��Stretch=�63   $	��Top=�6Q   ,   &PrimalEditor.Editors.ProjectLayoutView  5-      6(   5.      50      51      52      