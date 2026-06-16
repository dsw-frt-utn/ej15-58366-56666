using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    void InitializeSpecialities();
    void InitializeDoctors();
    List<Speciality> GetSpecialities();
    List<Doctor> GetDoctors();
    void InitializeData();
    bool CreateDoctor(Doctor doctor);
    bool AddSpeciality(Speciality speciality);
    Doctor? GetDoctor(Guid Id);
    Speciality? GetSpeciality(Guid Id);
}