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

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }

    private void LoadSpecialities()
    {
        try {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources" ,"specialities.json");
            var json = File.ReadAllText(jsonPath);
            var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? [];
            _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al cargar las especialidades", ex);
        }
    }
    public Doctor? GetDoctor(Guid Id) => _doctors.FirstOrDefault(d => d.Id == Id);

    public Task<Speciality?> GetSpecialityById(Guid Id) => Task.FromResult(_specialities.SingleOrDefault(s => s.Id == Id));
    public Task<IEnumerable<Doctor>> GetAllDoctors() => Task.FromResult<IEnumerable<Doctor>>(_doctors);
    public Task<Doctor?> GetDoctorById(Guid Id) => Task.FromResult(_doctors.SingleOrDefault(d => d.Id == Id));

    public Task SaveDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
        return Task.CompletedTask;
    }

    public Task DeleteDoctor(Doctor doctor)
    {
        doctor.Deactivate();
        return Task.CompletedTask;
    }
}