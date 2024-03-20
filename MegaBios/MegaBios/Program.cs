using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        // Load JSON file containing user data
        string jsonFilePath = @"C:\Users\mauri\source\repos\Informatica jaar 1\test login\test login\customers.json";  // Assuming the JSON file is named customers.json
        string jsonText = File.ReadAllText(jsonFilePath);
        JsonDocument jsonDocument = JsonDocument.Parse(jsonText);
        JsonElement root = jsonDocument.RootElement;

        Console.WriteLine("Login Form");
        Console.WriteLine("-----------");

        Console.Write("Enter email: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = MaskPasswordInput();

        bool isAuthenticated = false;

        foreach (JsonElement user in root.EnumerateArray())
        {
            string storedUsername = user.GetProperty("email").GetString();
            string storedPassword = user.GetProperty("wachtwoord").GetString();

            if (storedUsername == username && storedPassword == password)
            {
                isAuthenticated = true;
                break;
            }
        }

        if (isAuthenticated)
        {
            Console.WriteLine("Login successful!");
            // Add your code here to redirect to the main application or perform other actions
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again.");
            // Add your code here to handle unsuccessful login attempts, such as displaying an error message or allowing the user to retry
        }
    }

    static string MaskPasswordInput()
    {
        string password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            // Ignore any key that isn't a visible character
            if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            // Handle backspace
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b"); // Erase the last character from the console
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine(); // Move to the next line after password entry
        return password;
    }
}
