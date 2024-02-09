using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WsApiexamen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamenController : ControllerBase
    {
        private readonly ILogger<ExamenController> _logger;
        private readonly ApplicationContext _context;

        public ExamenController(
            ILogger<ExamenController> logger,
            ApplicationContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        [HttpPut("AgregarExamen")]
        public IActionResult Put(Examen examen)
        {
            try
            {
                _context.Database.BeginTransactionAsync();

                _context.Examenes.Add(examen);
                _context.SaveChanges();

                _context.Database.CommitTransactionAsync();

                string? url = Url.Action("Get", new { examen });
                return Created(url ?? "", new { examen });
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new { message = "Ha ocurrido un error al guardar el examen" });
            }
        }

        [HttpPut("ActualizarExamen")]
        public IActionResult Update(Examen examenDto)
        {
            try
            {
                _context.Database.BeginTransactionAsync();

                Examen? examen = _context.Examenes.AsNoTracking().FirstOrDefault(e => e.IdExamen == examenDto.IdExamen);

                if (examen is null)
                {
                    return NotFound();
                }

                examen.Nombre = examenDto.Nombre;
                examen.Descripcion = examenDto.Descripcion;

                _context.Examenes.Update(examen);
                _context.SaveChanges();

                _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new { message = "Ha ocurrido un error al guardar el examen" });
            }
        }

        [HttpDelete("EliminarExamen")]
        public IActionResult Delete(int id)
        {
            try
            {
                _context.Database.BeginTransactionAsync();

                Examen? examen = _context.Examenes.AsNoTracking().FirstOrDefault(e => e.IdExamen == id);

                if (examen is null)
                {
                    return NotFound();
                }

                _context.Examenes.Remove(examen);
                _context.SaveChanges();

                _context.Database.CommitTransactionAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(new { message = "Ha ocurrido un error al eliminar el examen" });
            }
        }

        [HttpGet("ConsultarExamen")]
        public IActionResult Get(int? Id, string? Nombre, string? Descripcion)
        {
            IEnumerable<Examen> examenes = _context.Examenes.Where(e => (e.IdExamen == Id || Id == null) || e.Nombre != null && e.Nombre.Contains(Nombre) || e.Descripcion != null && e.Descripcion.Contains(Descripcion));

            if (examenes is null)
            {
                return NotFound();
            }

            return Ok(examenes);
        }
    }
}