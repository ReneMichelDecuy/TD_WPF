   M S B A M L   `   `   `  ����  TD_FINAL    PromeoCrypto.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  PromeoCrypto��5   	   $	��350��6   $	��525��6+   
�������.��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $(��"Saisissez la chaîne à chiffrer :��6   $��Center=�6;   �� 5   
   -   5        $��	TextInput��5      $��1��6#   $	��500��60   $��25��6<   $��0,5,0,5q�6H   �� 5   
   $��Méthode de chiffrement��6   $��2��63   $��0,10,0,0q�6@   $	��500��6R   .��5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      	� -   5        $��CaesarRadioButton��5      $��César��67    d� 	IsChecked$
 TrueY�6G   $��10q�6X   	� 5      -   5        $��SubstitutionRadioButton��5      $��Substitution��6=   $��10q�6T   	� 5      -   5        $��PlayfairRadioButton��5      $��Playfair��69   $��10q�6L   5      5      �� 5   
   -   5        $��Chiffrer��5      $��3��6$   $	��100��61   $��30��6=   $��	0,10,0,10q�6I   $��Center=�6\   6y   �� 5!   
   $��5��6   $ 
Horizontal=�6"   $��Center=�6;   $��0,5,0,0q�6X   .+�5"      �� $��Chaîne chiffrée :��6   $��Center=�60   �� 5#      -   5        $��
OutputText��5#      $	��400��6(   $��25��64    �� 
IsReadOnly$
 True��6@   
�������$��10,0,0,0q�6e   5$      �� 5'   
   $��6��6   $ 
Horizontal=�6"   $��Center=�6;   $��0,5,0,0q�6X   .+�5(      �� $��État de la connexion :��6   $��Center=�65   �� 5)      -   5        $��ConnectionStatus��5)      $	��200��60   $��25��6<   
�������$��10,0,0,0q�6[   $��Center=�6m   5*      5+      5,      