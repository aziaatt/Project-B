using System;

namespace MegaBios
{
    public static class ReadAccount
    {
        public static void DisplayUserInfo(TestAccount loggedInUser)
        {
            Console.WriteLine("User Information:");
            Console.WriteLine("-----------------");
            Console.WriteLine($"Name: {loggedInUser.Voornaam} {loggedInUser.Tussenvoegsel} {loggedInUser.Achternaam}");
            Console.WriteLine($"Date of Birth: {loggedInUser.Geboorte_datum}");
            Console.WriteLine("Address:");
            Console.WriteLine($"  {loggedInUser.Adres.Straat} {loggedInUser.Adres.Huisnummer}");
            Console.WriteLine($"  {loggedInUser.Adres.Postcode} {loggedInUser.Adres.Woonplaats}");
            Console.WriteLine($"Email: {loggedInUser.Email}");
            Console.WriteLine($"Phone Number: {loggedInUser.Telefoonnummer}");
            Console.WriteLine($"Preferred Payment Method: {loggedInUser.Voorkeur_betaalwijze}");
            Console.WriteLine($"Student: {(loggedInUser.Is_student ? "Yes" : "No")}");
        }
    }
}