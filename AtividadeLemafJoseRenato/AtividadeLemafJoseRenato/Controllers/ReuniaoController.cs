using AtividadeLemafJoseRenato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtividadeLemafJoseRenato.Controllers
{
    public class ReuniaoController : Controller
    {
        // GET: Reuniao
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult AgendarReuniao(DateTime dataInicio, DateTime dataFim, int quantidadePessoas,
            bool precisaInternet, bool precisaWebcam)
        {

            return Json(new { sucesso = true }, JsonRequestBehavior.AllowGet);
        }
    }
}