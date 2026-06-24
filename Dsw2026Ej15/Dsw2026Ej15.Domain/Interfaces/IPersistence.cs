using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    Task<Speciality?> GetSpecialityById(Guid Id);
    Task<IEnumerable<Doctor>> GetAllDoctors();
    Task<Doctor?> GetDoctorById(Guid Id);
    Task SaveDoctor(Doctor doctor);
    Task DeleteDoctor(Doctor doctor);
}