using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    public static List<TestAccount> jsonData = new();
    static void Main(string[] args)
    {
        
        // Load JSON file containing user data
        string jsonFilePath = "../../../customers.json";  // Assuming the JSON file is named customers.json
        string jsonText = File.ReadAllText(jsonFilePath);
        JsonDocument jsonDocument = JsonDocument.Parse(jsonText);
        JsonElement root = jsonDocument.RootElement;
        jsonData = ConvertJsonToList(root);

        Console.WriteLine("Login Form");
        Console.WriteLine("-----------");

        Console.Write("Enter email: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = MaskPasswordInput();

        bool isAuthenticated = false;

        foreach (JsonElement user in root.EnumerateArray())
        {
            string storedUsername = user.GetProperty("email").GetString()!;
            string storedPassword = user.GetProperty("wachtwoord").GetString()!;

            if (storedUsername == username && storedPassword == password)
            {
                isAuthenticated = true;
                TestAccount account = ConvertJsonToAccount(user);
                System.Console.WriteLine(account.Wachtwoord);
                System.Console.WriteLine("Do you want to delete your account?");
            
                if (Console.ReadLine() == "yes") {
                    RemoveAccount(account);
                }
            
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

    static TestAccount ConvertJsonToAccount(JsonElement user) {
        string voornaam = user.GetProperty("voornaam").GetString()!;
        string tussenvoegsel = user.GetProperty("tussenvoegsel").GetString()!;
        string achternaam = user.GetProperty("achternaam").GetString()!;
        string geboorteDatum = user.GetProperty("geboorte_datum").GetString()!;
        // Get data from "adres field and put into dict
        Dictionary<string, string> adres = new();
        JsonElement adresElement = user.GetProperty("adres")!;
        foreach(var property in adresElement.EnumerateObject()) {
            adres.Add(property.Name, property.Value.GetString()!);
        }
        string email = user.GetProperty("email").GetString()!;
        string wachtwoord = user.GetProperty("wachtwoord").GetString()!;
        string telefoonNr = user.GetProperty("telefoonnummer").GetString()!;
        string betaalwijze = user.GetProperty("voorkeur_betaalwijze").GetString()!;
        bool is_student = user.GetProperty("is_student").GetBoolean()!;
        return new TestAccount(voornaam, tussenvoegsel, achternaam, geboorteDatum, adres, email, wachtwoord, telefoonNr, betaalwijze, is_student);
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

    static List<TestAccount> ConvertJsonToList(JsonElement root) {
        foreach (JsonElement user in root.EnumerateArray()) {
            jsonData.Add(ConvertJsonToAccount(user));
        }
        return jsonData;
    }

    static void WriteToJson(string filePath, List<TestAccount> data) {
        string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions{WriteIndented = true});
        File.WriteAllText(filePath, jsonString);
    }

    static void RemoveAccount(TestAccount account) {
        for (int i = 0; i < jsonData.Count; i++) {
            if (jsonData[i].Email == account.Email && jsonData[i].Wachtwoord == account.Wachtwoord) {
                jsonData.RemoveAt(i);
                break;                
            }
        }
        jsonData.Remove(account);
        WriteToJson("../../../customers.json", jsonData);
    }

}