using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Interfaces;

public interface IPersistence
{
    void InitializeSpecialities();
    void InitializeDoctors();
    List<Speciality> GetSpecialities();
    List<Doctor> GetDoctors();
    void InitializeData();
    bool AddDoctor(Doctor doctor);
    bool AddSpeciality(Speciality speciality);
    Doctor GetDoctor(string licenseNumber);
    Speciality GetSpeciality(string name);
}