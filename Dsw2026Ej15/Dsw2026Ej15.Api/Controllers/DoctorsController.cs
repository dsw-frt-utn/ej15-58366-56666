using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Exceptions;
namespace Dsw2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Name es requerido.");

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("LicenseNumber es requerido.");

        var speciality = _persistence.GetSpeciality(request.SpecialityId);
        if (speciality == null)
            throw new ValidationException("SpecialityId no corresponde a ninguna especialidad existente.");

        _persistence.SaveDoctor(new Doctor(request.Name, request.LicenseNumber, speciality));
        return Created();
    }
    [HttpGet]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = _persistence.GetDoctors().Where(d => d.IsActive == true);
        return Ok(doctors);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById([FromRoute] Guid id)
    {
        var doctor = _persistence.GetDoctor(id);
        if(doctor == null || doctor.IsActive == false)
        {
            return NotFound("Médico no encontrado");
        }
        return Ok(new {doctor.Name, doctor.LicenseNumber, SpecialityName = doctor.Speciality.Name});
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorById([FromRoute] Guid id)
    {
        var doctor = _persistence.GetDoctor(id);
        if(doctor == null || doctor.IsActive == false)
        {
            return NotFound("Médico no encontrado");
        }
        _persistence.DeleteDoctor(doctor);
        return NoContent();
    }
}