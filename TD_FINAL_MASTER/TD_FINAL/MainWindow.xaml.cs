using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace PromeoCrypto
{
    public partial class MainWindow : Window
    {
        private const int Port = 6666; // Port d'écoute du serveur
        private const string ServerAddress = "127.0.0.1"; // Adresse du serveur (localhost pour l'exemple)

        public MainWindow()
        {
            InitializeComponent();
            UpdateConnectionStatus(); // Mise à jour de l'état de connexion au démarrage
        }

        // Événement déclenché lorsque l'utilisateur clique sur le bouton "Chiffrer"
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = TextInput.Text.ToUpper().Replace(" ", ""); // Transformation de la saisie en majuscules et suppression des espaces
            if (Regex.IsMatch(inputText, "^[A-Z]+$")) // Vérification que la chaîne ne contient que des lettres
            {
                // Détermination de la méthode de chiffrement sélectionnée
                char encryptionMethod = CaesarRadioButton.IsChecked == true ? 'C' :
                                        SubstitutionRadioButton.IsChecked == true ? 'S' :
                                        'P';

                string messageToSend = $"{encryptionMethod}|{inputText}"; // Formatage du message à envoyer au serveur
                string encryptedMessage = SendMessageToServer(messageToSend); // Envoi du message au serveur et réception de la réponse

                OutputText.Text = encryptedMessage; // Affichage de la chaîne chiffrée
            }
            else
            {
                // Affichage d'un message d'erreur si la saisie est invalide
                MessageBox.Show("Veuillez saisir uniquement des lettres de l'alphabet.", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Envoie le message au serveur et retourne la réponse chiffrée
        private string SendMessageToServer(string message)
        {
            try
            {
                using (TcpClient client = new TcpClient(ServerAddress, Port)) // Connexion au serveur
                {
                    NetworkStream stream = client.GetStream(); // Obtention du flux de données
                    byte[] data = Encoding.ASCII.GetBytes(message); // Conversion du message en octets
                    stream.Write(data, 0, data.Length); // Envoi du message

                    data = new byte[256]; // Tampon pour les données reçues
                    StringBuilder response = new StringBuilder(); // StringBuilder pour construire la réponse
                    int bytes = stream.Read(data, 0, data.Length); // Lecture de la réponse
                    response.Append(Encoding.ASCII.GetString(data, 0, bytes)); // Conversion des octets en chaîne de caractères

                    UpdateConnectionStatus(true); // Mise à jour de l'état de connexion à "connecté"
                    return response.ToString(); // Retour de la réponse chiffrée
                }
            }
            catch (Exception ex)
            {
                // Affichage d'un message d'erreur en cas de problème de connexion
                MessageBox.Show($"Erreur lors de la connexion au serveur : {ex.Message}", "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
                UpdateConnectionStatus(false); // Mise à jour de l'état de connexion à "déconnecté"
                return string.Empty;
            }
        }

        // Met à jour l'état de connexion avec le serveur
        private void UpdateConnectionStatus(bool isConnected = false)
        {
            if (isConnected)
            {
                ConnectionStatus.Text = "Connecté";
                ConnectionStatus.Background = Brushes.LightGreen;
            }
            else
            {
                ConnectionStatus.Text = "Déconnecté";
                ConnectionStatus.Background = Brushes.LightCoral;
            }
        }
    }
}
