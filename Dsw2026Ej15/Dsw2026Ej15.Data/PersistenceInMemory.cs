using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dtos;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory: IPersistence
{
    private List<Speciality> _specialities = [];
    private List<Doctor> _doctors = [];

    private void LoadSpecialities()
    {
        try {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources" ,"specialities.json");
            var json = File.ReadAllText(jsonPath);
            var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? [];
            _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
        }
        catch (Exception)
        {

        }
    }
    public void InitializeDoctors()
    {
        throw new NotImplementedException();
    }
    public void InitializeData()
    {
        InitializeDoctors();
        LoadSpecialities();
    }
    public List<Doctor> GetDoctors()
    {
        return _doctors;
    }
    public List<Speciality> GetSpecialities()
    {
        return _specialities;
    }

    public bool CreateDoctor(Doctor doctor)
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
    public Doctor? GetDoctor(Guid Id)
    {
        return _doctors.FirstOrDefault(d => d.Id == Id);
    }
    public Speciality? GetSpeciality(Guid Id)
    {
        return _specialities.SingleOrDefault(s => s.Id == Id);
    }
}