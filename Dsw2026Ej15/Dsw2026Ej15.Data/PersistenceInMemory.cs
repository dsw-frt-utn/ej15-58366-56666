using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Dsw2026Ej15.Data.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory: IPersistence
{
    private readonly List<Speciality> _specialities = new List<Speciality>();
    private readonly List<Doctor> _doctors = new List<Doctor>();

    private static async Task<IEnumerable<Speciality>> LoadSpecialities()
    {
        var json = await File.ReadAllTextAsync("specialities.json");
        var specialities = JsonSerializer.Deserialize<List<Speciality>>(json);
        return specialities ?? new List<Speciality>();
    }
    public void InitializeDoctors()
    {
        throw new NotImplementedException();
    }
    public void InitializeSpecialities()
    {
        var specialities = LoadSpecialities().Result;
        if(specialities != null)
        {
            foreach (var speciality in specialities)
            {
                _specialities.Add(speciality);
            }
        }
    }
    public void InitializeData()
    {
        InitializeDoctors();
        InitializeSpecialities();
    }
    public List<Doctor> GetDoctors()
    {
        return _doctors;
    }
    public List<Speciality> GetSpecialities()
    {
        return _specialities;
    }

    public bool AddDoctor(Doctor doctor)
    {
        try
        {
            _doctors.Add(doctor);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool AddSpeciality(Speciality speciality)
    {
        try
        {
            _specialities.Add(speciality);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public Doctor GetDoctor(string licenseNumber)
    {
        return _doctors.FirstOrDefault(d => d.LicenseNumber == licenseNumber);
    }
    public Speciality GetSpeciality(string name)
    {
        return _specialities.FirstOrDefault(s => s.Name == name);
    }
}