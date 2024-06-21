using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
//amel.sghaier@u-picardie.fr
class Server
{
    private const int Port = 6666; // Port d'écoute du serveur

    public static void Main()
    {
        TcpListener listener = null;
        try
        {
            listener = new TcpListener(IPAddress.Any, Port); // Initialisation du serveur pour écouter sur toutes les adresses IP disponibles
            listener.Start(); // Démarrage du serveur
            Console.WriteLine("Serveur en attente de connexion...");

            while (true) // Boucle infinie pour accepter les connexions des clients
            {
                TcpClient client = listener.AcceptTcpClient(); // Accepte une connexion entrante
                Console.WriteLine($"Client connecté : {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

                // Crée un nouveau thread pour gérer la communication avec le client
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur : {e.Message}");
        }
        finally
        {
            listener?.Stop(); // Arrête le serveur en cas d'exception
        }
    }

    // Gère la communication avec un client
    private static void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream(); // Obtient le flux de données du client
        byte[] buffer = new byte[256]; // Tampon pour les données entrantes
        int bytesRead;

        try
        {
            // Lit les données envoyées par le client
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string data = Encoding.ASCII.GetString(buffer, 0, bytesRead); // Convertit les octets en chaîne de caractères
                Console.WriteLine($"Reçu : {data}");
                string encryptedData = EncryptData(data); // Chiffre les données
                byte[] response = Encoding.ASCII.GetBytes(encryptedData); // Convertit la chaîne de caractères chiffrée en octets
                stream.Write(response, 0, response.Length); // Envoie les données chiffrées au client
                Console.WriteLine($"Envoyé : {encryptedData}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur de communication avec le client : {e.Message}");
        }
        finally
        {
            client.Close(); // Ferme la connexion avec le client
        }
    }

    // Chiffre les données en fonction de la méthode spécifiée
    private static string EncryptData(string data)
    {
        string[] parts = data.Split('|'); // Sépare la méthode de chiffrement du texte à chiffrer
        if (parts.Length != 2) return "Erreur de format";

        char method = parts[0][0];
        string text = parts[1];
        return method switch
        {
            'C' => CaesarCipher(text), // Chiffrement de César
            'S' => SubstitutionCipher(text), // Chiffrement par substitution
            'P' => PlayfairCipher(text), // Chiffrement de Playfair
            _ => "Méthode de chiffrement inconnue"
        };
    }

    // Implémente le chiffrement de César
    private static string CaesarCipher(string text)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in text)
        {
            char shifted = (char)((c + 3 - 'A') % 26 + 'A'); // Décale chaque caractère de 3 positions dans l'alphabet
            result.Append(shifted);
        }
        return result.ToString();
    }

    // Implémente le chiffrement par substitution
    private static string SubstitutionCipher(string text)
    {
        string key = "HIJKLMNVWXYZBCADEFGOPQRSTU"; // Clé de substitution
        StringBuilder result = new StringBuilder();
        foreach (char c in text)
        {
            result.Append(key[c - 'A']); // Remplace chaque caractère par le caractère correspondant dans la clé
        }
        return result.ToString();
    }

    // Implémente le chiffrement de Playfair (non implémenté ici)
    private static string PlayfairCipher(string text)
    {
        // Convertit le texte en majuscules, remplace les 'W' par des 'X' et supprime les espaces
        text = text.ToUpper().Replace("W", "X").Replace(" ", "");

        // Si le nombre de lettres est impair, ajoute un 'X' à la fin
        if (text.Length % 2 != 0) text += "X";

        // Définition de la matrice de Playfair utilisée pour le chiffrement
        char[,] matrix = {
        { 'B', 'Y', 'D', 'G', 'Z' },
        { 'J', 'S', 'F', 'U', 'P' },
        { 'L', 'A', 'R', 'K', 'X' },
        { 'C', 'O', 'I', 'V', 'E' },
        { 'Q', 'N', 'M', 'H', 'T' }
    };

        // Initialise un StringBuilder pour accumuler le texte chiffré
        StringBuilder encryptedText = new StringBuilder();

        // Parcourt le texte par paires de lettres
        for (int i = 0; i < text.Length; i += 2)
        {
            char a = text[i];
            char b = text[i + 1];
            EncryptPlayfairPair(matrix, a, b, out char encryptedA, out char encryptedB);
            // Ajoute les lettres chiffrées au résultat
            encryptedText.Append(encryptedA).Append(encryptedB);
        }

        // Retourne le texte chiffré
        return encryptedText.ToString();
    }

    // Chiffre une paire de lettres en utilisant la matrice de Playfair
    private static void EncryptPlayfairPair(char[,] matrix, char a, char b, out char encryptedA, out char encryptedB)
    {
        // Trouve la position des lettres dans la matrice
        GetPlayfairPosition(matrix, a, out int rowA, out int colA);
        GetPlayfairPosition(matrix, b, out int rowB, out int colB);

        // Cas 1 : Les lettres sont sur la même ligne
        if (rowA == rowB)
        {
            // Chaque lettre est remplacée par la lettre à sa droite (en boucle)
            encryptedA = matrix[rowA, (colA + 1) % 5];
            encryptedB = matrix[rowB, (colB + 1) % 5];
        }
        // Cas 2 : Les lettres sont dans la même colonne
        else if (colA == colB)
        {
            // Chaque lettre est remplacée par la lettre en dessous (en boucle)
            encryptedA = matrix[(rowA + 1) % 5, colA];
            encryptedB = matrix[(rowB + 1) % 5, colB];
        }
        // Cas 3 : Les lettres forment un rectangle
        else
        {
            // Les lettres sont remplacées par les lettres sur la même ligne dans les colonnes opposées
            encryptedA = matrix[rowB, colA];
            encryptedB = matrix[rowA, colB];
        }
    }

    // Trouve la position d'une lettre dans la matrice
    private static void GetPlayfairPosition(char[,] matrix, char c, out int row, out int col)
    {
        for (row = 0; row < 5; row++)
        {
            for (col = 0; col < 5; col++)
            {
                // Si la lettre est trouvée, retourne ses coordonnées
                if (matrix[row, col] == c) return;
            }
        }
        // Si la lettre n'est pas trouvée, lance une exception
        throw new ArgumentException("Caractère non trouvé dans la matrice");
    }
}