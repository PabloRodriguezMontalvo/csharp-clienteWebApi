using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcConsumoWebApi.Models
{
    public class CursoViewModel
    {
        public int idCurso { get; set; }
        public string nombre { get; set; }
        public Nullable<int> profesor { get; set; }
        public System.DateTime inicio { get; set; }
        public int duracion { get; set; }
        public String NombreProfesor { get; set; }
    }
}