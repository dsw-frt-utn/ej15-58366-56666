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

    private async Task LoadSpecialitiesAsync()
    {
        try {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources" ,"specialities.json");
            var json = await File.ReadAllTextAsync(jsonPath);
            var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? [];
            _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al cargar las especialidades", ex);
        }
    }
    public Task<List<Doctor>> GetDoctorsAsync() => Task.FromResult(_doctors);
    public Task<List<Speciality>> GetSpecialitiesAsync() => Task.FromResult(_specialities);

    public Task CreateDoctorAsync(Doctor doctor)
    {
        _doctors.Add(doctor);
        return Task.CompletedTask;
    }
    public Task<Doctor?> GetDoctorAsync(Guid Id) => Task
        .FromResult(_doctors.FirstOrDefault(d => d.Id == Id));
    public Task<Speciality?> GetSpecialityAsync(Guid Id) => Task.FromResult(_specialities.SingleOrDefault(s => s.Id == Id));

    public Task DeleteDoctorAsync(Doctor doctor)
    {
        doctor.IsActive = false;
        return Task.CompletedTask;
    }
}