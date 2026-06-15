using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Entities;
namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorsController : ControllerBase
{
    [Route("api/doctors")]
    [HttpPost("Insertar Doctor")]
    public void Post(Doctor doctor)
    {
        
    }
    [Route("api/doctors")]
    [HttpGet("Obtener Medicos")]
    public IEnumerable<Doctor> Get()
    {
        
    }
    [Route("api/doctors/{id}")]
    [HttpGet("Obtener Medico")]
    public Doctor Get(int id)
    {

    }
    [Route("api/doctors/{id}")]
    [HttpDelete("Inhabilitar Medico")]
    public void Delete(int id)
    {

    }
}