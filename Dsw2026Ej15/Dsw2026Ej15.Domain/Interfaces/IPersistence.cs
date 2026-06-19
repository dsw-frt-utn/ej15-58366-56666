using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    List<Speciality> GetSpecialities();
    List<Doctor> GetDoctors();
    void SaveDoctor(Doctor doctor);
    Doctor? GetDoctor(Guid Id);
    Speciality? GetSpeciality(Guid Id);
    void DeleteDoctor (Doctor doctor);
}