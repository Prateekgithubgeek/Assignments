using Microsoft.VisualBasic;

using System.Text.Json;
using System.Xml.Serialization;

namespace Assignment_Day_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Assignment 4
            // 11) Create a Book and Author class with suitable properties and Hardcoded with Minimum 5 data for both the classes and covert into Json and XML Format  and store that data in Local Disk using File concept .
            // Read the Json and XML data and display the same in console App

            var authors = new List<Author>
            {
                new Author { BookId = 1, Title = "1984", AuthorId = 1, PublishedYear = 1949, Genre = "Dystopian" },
                new Author { BookId = 2, Title = "The Naval Ravikant", AuthorId = 2, PublishedYear = 1960, Genre = "Fiction" },
                new Author { BookId = 3, Title = "The Alchemist", AuthorId = 3, PublishedYear = 1813, Genre = "Romance" },
                new Author { BookId = 4, Title = "The Ultimate Gift", AuthorId = 4, PublishedYear = 1925, Genre = "Tragedy" },
                new Author { BookId = 5, Title = "Champak", AuthorId = 5, PublishedYear = 1851, Genre = "Adventure" }
            };

            var books = new List<Book>
            {
                new Book { AuthorId = 1, Name = "George Orwell", Country = "United Kingdom" },
                new Book { AuthorId = 2, Name = "Naval Ravikant", Country = "India" },
                new Book { AuthorId = 3, Name = "Jane Austen", Country = "United Kingdom" },
                new Book { AuthorId = 4, Name = "Jim Stovall", Country = "India" },
                new Book { AuthorId = 5, Name = "Herman Melville", Country = "India" }
            };

            // Json Serialize
            string jsonFilePath = "C:\\Users\\Admin\\Desktop\\Hexa Training\\Domain Specific\\Git\\Day 4\\Test.txt";
            File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(new { Author = authors, Books = books }));
            Console.WriteLine($"JSON data saved to {jsonFilePath}");

            // REad JSON
            var jsonData = File.ReadAllText(jsonFilePath);
            Console.WriteLine("\n Read JSON dataL \n" + jsonData);

            // XML Serialization 
            string xmlFilePath = "C:\\Users\\Admin\\Desktop\\Hexa Training\\Domain Specific\\Git\\Day 4\\Test.txt";
            using (var xmlFile = new FileStream(xmlFilePath, FileMode.Create))
            {
                new XmlSerializer(typeof(List<Author>)).Serialize(xmlFile, authors);
            }
            Console.WriteLine($"\nXML data saved to {xmlFilePath}");

            // Read XML
            using (var xmlFile = new FileStream(xmlFilePath, FileMode.Open))
            {
                var deserializedAuthors = (List<Author>)new XmlSerializer(typeof(List<Author>)).Deserialize(xmlFile);
                Console.WriteLine("\nRead XML data:");
                deserializedAuthors.ForEach(author => Console.WriteLine($"{author.Title} by AuthorId: {author.AuthorId}"));
            }
        }
    }
}
