using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly Dsw2026Ej15DbContext _context;
    public PersistenceEf(Dsw2026Ej15DbContext context)
    {
        _context = context;
    }
    public async Task DeleteDoctor(Doctor doctor)
    {
        doctor.Deactivate();
        await _context.SaveChangesAsync();
    }
    public async Task<Doctor?> GetDoctorById(Guid Id) => await _context.Doctors.Include(d => d.Speciality).FirstOrDefaultAsync(d => d.Id == Id && d.IsActive);
    public async Task<IEnumerable<Doctor>> GetAllDoctors() => await _context.Doctors.Include(d => d.Speciality).Where(d => d.IsActive).ToListAsync();
    public async Task<List<Speciality>> GetSpecialities() => await _context.Specialities.ToListAsync();
    public async Task<Speciality?> GetSpecialityById(Guid Id) => await _context.Specialities.FirstOrDefaultAsync(s => s.Id == Id);
    public async Task SaveDoctor(Doctor doctor)
    {
        await _context.AddAsync(doctor);
        await _context.SaveChangesAsync();
    }
}