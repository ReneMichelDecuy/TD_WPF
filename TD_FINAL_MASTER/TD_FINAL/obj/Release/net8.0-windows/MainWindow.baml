   M S B A M L   `   `   `  ����   TD_FINAL    PromeoCrypto.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  PromeoCrypto��$	��350��$	��525��
�������.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $(��"Saisissez la chaîne à chiffrer :��$��Center=��� -   $��	TextInput��$��1��$	��500��$��25��$��0,5,0,5q��� $��Méthode de chiffrement��$��2��$��0,10,0,0q�$	��500��.����  �� Orientation$ 
Horizontal=�$��Center=�.+�	� -   $��CaesarRadioButton��$��César�� d� 	IsChecked$
 TrueY�$��10q�	� -   $��SubstitutionRadioButton��$��Substitution��$��10q�	� -   $��PlayfairRadioButton��$��Playfair��$��10q��� -   $��Chiffrer��$��3��$	��100��$��30��$��	0,10,0,10q�$��Center=��� $��5��$ 
Horizontal=�$��Center=�$��0,5,0,0q�.+��� $��Chaîne chiffrée :��$��Center=��� -   $��
OutputText��$	��400��$��25�� �� 
IsReadOnly$
 True��
�������$��10,0,0,0q��� $��6��$ 
Horizontal=�$��Center=�$��0,5,0,0q�.+��� $��État de la connexion :��$��Center=��� -   $��ConnectionStatus��$	��200��$��25��
�������$��10,0,0,0q�$��Center=�