using System;
using System.Collections.Generic;

public class Patient
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public List<Doctor> AssignedDoctors { get; set; } = new List<Doctor>();

    public Patient(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    public void AssignDoctors(List<Doctor> doctorsToAssign)
    {
        foreach (var doctor in doctorsToAssign)
        {
            if (AssignedDoctors.Exists(d => d.Speciality == "Kirurgi") && doctor.Speciality == "Onkologi" ||
                AssignedDoctors.Exists(d => d.Speciality == "Onkologi") && doctor.Speciality == "Kirurgi")
            {
                throw new Exception("A patient cannot be assigned both Kirurgi and Onkologi specialists.");
            }

            doctor.IncrementPatientCount();
            AssignedDoctors.Add(doctor);
        }
    }
}
