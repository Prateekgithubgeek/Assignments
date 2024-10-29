using System.ComponentModel.DataAnnotations;

namespace Assignment_Day4_part2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Assignment 12
            /*create a Customer class with the below properties
            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public DateTime DateOfBirth { get; set; }
            create a separate class to validate PhoneNumber, Email, DOB using some function which should return bool (true if valid ,False if invalid)
            */

            Customer customer = new Customer()
            {
                Name = "Prateek Sohani",
                Email = "ps@gmail.com",
                PhoneNumber = "78795",
                DateOfBirth = new DateTime(2024, 1, 1)
            };

            // Calling the validate methods 
            bool isPhoneValid = CustomerValidator.ValidatePhoneNumber(customer.PhoneNumber);
            bool isEmailValid = CustomerValidator.ValidateEmail(customer.Email);
            bool isDobValid = CustomerValidator.ValidateDateOfBirth(customer.DateOfBirth);

            Console.WriteLine($"Phone number valid: {isPhoneValid}");
            Console.WriteLine($"Email valid: {isEmailValid}");
            Console.WriteLine($"Date of birth valid: {isDobValid}");

            Console.ReadKey();

        }
    }
}
