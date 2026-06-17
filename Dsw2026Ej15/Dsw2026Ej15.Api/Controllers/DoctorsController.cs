using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
    {
        if(string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y Matricula son requeridos");
        }
        var speciality = await _persistence.GetSpecialityAsync(request.SpecialityId);
        if(speciality  == null)
        {
            return BadRequest("Especialidad no encontrada");
        }
        await _persistence.CreateDoctorAsync(new Doctor(request.Name, request.LicenseNumber, speciality));
        return Created();
    }
    [HttpGet]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = (await _persistence.GetDoctorsAsync()).Where(d => d.IsActive == true);
        return Ok(doctors);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var doctor = await _persistence.GetDoctorAsync(id);
        if(doctor == null || doctor.IsActive == false)
        {
            return NotFound("Médico no encontrado");
        }
        return Ok(new {doctor.Name, doctor.LicenseNumber, SpecialityName = doctor.Speciality.Name});
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var doctor = await _persistence.GetDoctorAsync(id);
        if(doctor == null || doctor.IsActive == false)
        {
            return NotFound("Médico no encontrado");
        }
        await _persistence.DeleteDoctorAsync(doctor);
        return NoContent();
    }
}