using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    [HttpPost("Crear Medico")]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
    {
        if(string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y Matricula son requeridos");
        }
        var speciality = _persistence.GetSpeciality(request.SpecialityId);
        if(speciality  == null)
        {
            return BadRequest();
        }
        return Created();
    }
    [HttpGet("Obtener Medicos")]
    public async Task<IActionResult> GetDoctors()
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Route("{id}")]
    [HttpGet("Obtener Medico")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Route("{id}")]
    [HttpDelete("Inhabilitar Medico")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}}