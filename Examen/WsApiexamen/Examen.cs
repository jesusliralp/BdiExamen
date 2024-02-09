using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WsApiexamen
{
    public class Examen
    {
        [Key]
        [Column("idExamen")]
        public int IdExamen { get; set; }
        [Column("Nombre")]
        public string? Nombre { get; set; }
        [Column("Descripcion")]
        public string? Descripcion { get; set; }
    }
}