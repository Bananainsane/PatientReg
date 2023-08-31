using System;
using System.Collections.Generic;
using System.Threading;

namespace PatientRegistrationApp
{
    class Program
    {
        private static List<Doctor> doctors;
        private static List<Patient> patients;

        static void Main(string[] args)
        {
            doctors = new List<Doctor>
            {
                new Doctor("Peter Hansen", "øjenlæge", "11111111"),
                new Doctor("Martin Jensen", "Radiologi", "22222222"),
                new Doctor("Thomas Olsen", "Kirurgi", "33333333"),
                new Doctor("Ole Nielsen", "Onkologi", "44444444")
            };

            patients = new List<Patient>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Patient Registration System");
                Console.WriteLine("===========================");
                Console.WriteLine("1. Register New Patient");
                Console.WriteLine("2. List Registered Patients");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Choose an option:");

                string menuChoice = Console.ReadKey().KeyChar.ToString();

                if (menuChoice == "1")
                {
                    RegisterNewPatient();
                }
                else if (menuChoice == "2")
                {
                    ListRegisteredPatients();
                }
                else if (menuChoice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Press any key to return to main menu...");
                    Console.ReadKey();
                }
            }
        }

        // ... (Other methods and class definition remain the same)

        private static void RegisterNewPatient()
        {
            Console.Clear();
            Console.WriteLine("=== Register New Patient ===");

            string firstName;
            while (true)
            {
                Console.Write("Enter patient's first name: ");
                firstName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(firstName) || firstName.Any(char.IsDigit))
                {
                    Console.WriteLine(
                        "Invalid first name. Names should contain only letters. Try again."
                    );
                }
                else
                {
                    break;
                }
            }

            // Validate last name
            string lastName;
            while (true)
            {
                Console.Write("Enter patient's last name: ");
                lastName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(lastName) || lastName.Any(char.IsDigit))
                {
                    Console.WriteLine(
                        "Invalid last name. Names should contain only letters. Try again."
                    );
                }
                else
                {
                    break;
                }
            }
            // Validate phone number
            string phoneNumber;
            while (true)
            {
                Console.Write("Enter patient's phone number (optional +45): ");
                phoneNumber = Console.ReadLine();
                if (
                    phoneNumber.StartsWith("+45")
                    && phoneNumber.Length == 11
                    && phoneNumber.Substring(3).All(char.IsDigit)
                )
                {
                    break;
                }
                else if (phoneNumber.Length == 8 && phoneNumber.All(char.IsDigit))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Invalid phone number. The phone number should either be 8 digits or start with +45 followed by 8 digits. Try again."
                    );
                }
            }

            Patient newPatient = new Patient(firstName, lastName, phoneNumber);

            List<Doctor> selectedDoctors = new List<Doctor>();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Assign Doctors to Patient ===");
                Console.WriteLine("Available Doctors:");
                for (int i = 0; i < doctors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {doctors[i].Name} - {doctors[i].Speciality}");
                }
                Console.WriteLine();
                Console.WriteLine("Selected Doctors:");
                if (selectedDoctors.Count == 0)
                {
                    Console.WriteLine("None");
                }
                else
                {
                    foreach (var doctor in selectedDoctors)
                    {
                        Console.WriteLine($"- {doctor.Name} ({doctor.Speciality})");
                    }
                }

                Console.WriteLine(
                    "\nSelect a doctor to add to the patient (or type 'done' to finish):"
                );
                string choice = Console.ReadLine();

                if (choice?.ToLower() == "done")
                {
                    break;
                }

                if (
                    !int.TryParse(choice, out int doctorIndex)
                    || doctorIndex < 1
                    || doctorIndex > doctors.Count
                )
                {
                    Console.WriteLine("Invalid choice. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                selectedDoctors.Add(doctors[doctorIndex - 1]);
            }

            try
            {
                newPatient.AssignDoctors(selectedDoctors);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
                return;
            }

            patients.Add(newPatient);
            Console.WriteLine(
                "\nPatient registered successfully. Press any key to return to main menu..."
            );
            Console.ReadKey();
        }

        private static void ListRegisteredPatients()
        {
            Console.Clear();
            Console.WriteLine("=== List of Registered Patients ===\n");

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients have been registered.");
            }
            else
            {
                // Print header
                Console.WriteLine(
                    "{0,-20} {1,-20} {2,-15} {3}",
                    "First Name",
                    "Last Name",
                    "Phone",
                    "Assigned Doctors"
                );
                Console.WriteLine(new string('=', 70));

                // Print each patient's details
                foreach (var patient in patients)
                {
                    Console.Write(
                        "{0,-20} {1,-20} {2,-15}",
                        patient.FirstName,
                        patient.LastName,
                        patient.PhoneNumber
                    );

                    // List assigned doctors
                    if (patient.AssignedDoctors.Count == 0)
                    {
                        Console.WriteLine("None");
                    }
                    else
                    {
                        var doctorNames = string.Join(
                            ", ",
                            patient.AssignedDoctors.Select(d => $"{d.Name} ({d.Speciality})")
                        );
                        Console.WriteLine(doctorNames);
                    }
                }
            }

            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
