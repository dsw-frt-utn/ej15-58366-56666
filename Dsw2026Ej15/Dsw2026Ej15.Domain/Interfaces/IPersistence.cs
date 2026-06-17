using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    Task<List<Speciality>> GetSpecialitiesAsync();
    Task<List<Doctor>> GetDoctorsAsync();
    Task CreateDoctorAsync(Doctor doctor);
    Task<Doctor?> GetDoctorAsync(Guid Id);
    Task<Speciality?> GetSpecialityAsync(Guid Id);
    Task DeleteDoctorAsync(Doctor doctor);
}