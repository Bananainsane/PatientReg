using System;

public class Doctor
{
    public string Name { get; set; }
    public string Speciality { get; set; }
    public string PhoneNumber { get; set; }
    public int PatientCount { get; private set; }

    public Doctor(string name, string speciality, string phoneNumber)
    {
        Name = name;
        Speciality = speciality;
        PhoneNumber = phoneNumber;
        PatientCount = 0;
    }

    public void IncrementPatientCount()
    {
        if (PatientCount >= 3)
        {
            throw new Exception($"Doctor {Name} has been assigned to 3 patients already.");
        }
        PatientCount++;
    }
}
