using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcConsumoWebApi.Models;
using MvcConsumoWebApi.Utils;

namespace MvcConsumoWebApi.Controllers
{
    public class CursoController : Controller
    {
        // GET: Curso
        public ActionResult Index()
        {
            var url = "http://localhost:53302/v1/curso";
            List<CursoViewModel> lista;
            var cl = WebRequest.Create(url);
            cl.Method = "GET";
            var res = cl.GetResponse();
            using (var stream = res.GetResponseStream())
            {
                using (var reader=new StreamReader(stream))
                {
                    var resultado = reader.ReadToEnd();
                    lista = Serializacion<List<CursoViewModel>>.
                        Deserializar(resultado);

                }    
            }
            
            return View(lista);
        }
        public ActionResult AddCurso()
        {
            return View(new CursoViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> AddCurso(CursoViewModel model)
        {
            model.inicio = DateTime.Now;
          Alta(model);
            return View();
        }

        private async Task<CursoViewModel> Alta(CursoViewModel model)
        {
            var serializado = Serializacion<CursoViewModel>.Serializar(model);
            var url = "http://localhost:53302/v1/curso";

            using (var handler=new HttpClientHandler())
            {
                using (var client=new HttpClient(handler))
                {
                    var contenido = new StringContent(serializado);
                    contenido.Headers.ContentType=
                        new MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync(new Uri(url), contenido);

                    
                    var resultado = await response.Content.ReadAsStringAsync();

                    return model;

                }
            }



        }

    }
}