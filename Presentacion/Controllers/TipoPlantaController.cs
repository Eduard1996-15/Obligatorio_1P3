using Dominio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.EntidadesNegocios;

namespace Presentacion.Controllers
{
    public class TipoPlantaController : Controller
    {

        private IRepositorioTipoPlanta _repoTipo;
        private IRepositorioParametro repoPar;
        public TipoPlantaController(IRepositorioTipoPlanta repoPlantas, IRepositorioParametro rp )
        {
            _repoTipo = repoPlantas;//punto debil
            repoPar = rp;
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                IEnumerable<TipoPlanta> tps = null;
                    tps = _repoTipo.FindAll();
            if (tps.Count() > 0)
            {
                ViewBag.mje = "Lista de Tipos de Plantas:";
                return View(tps);
            }
            ViewBag.mje = "No hay Tipos de Plantas para mostrar:";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
           
        }
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Nombre, string Descripcion)
        {
            try
            {
                TipoPlanta tp = new TipoPlanta(Nombre,Descripcion);
                tp.TopeDescripcion = repoPar.BuscarRetornarValor("DESCRIPCIONT");
                if(tp!= null && tp.ValidarTope())
                {
                   bool creo = _repoTipo.Add(tp);
                    if (creo)
                    {
                     ViewBag.mje = "Se creo Tipo Planta correctamente";
                        //cargar lista automaticamente 
                    return RedirectToAction("Index"); 
                    }
                    else {
                        ViewBag.mje = "ERROR- no se creo Tipo Planta ";
                            return View();
                    }
                }
                else
                {
                    ViewBag.mje = "ERROR- Datos ingresados Incorrectamente";
                return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.mje = ex.Message;
                 return View();
            }
        }
        public ActionResult Edit()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,string descripcion)
        {
                try
                {
                    TipoPlanta tip = _repoTipo.FindById(id);
                    tip.Descripcion = descripcion;
                    tip.TopeDescripcion = repoPar.BuscarRetornarValor("DESCRIPCIONT");
                if (tip.ValidarTope()&& _repoTipo.Update(tip))
                    {
                        ViewBag.mje = " se modifico tipo";
                        return View();
                    }
                    ViewBag.mje = " No  se modifico tipo";
                    return View();
                 }
                    catch(Exception ex)
                    {
                        ViewBag.mje = " No  se modifico tipo"+ex.Message;
                        return View();
                    }
        }
        public ActionResult Delete()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                bool tp = _repoTipo.Remove(id);
                if (tp)
                {
                    ViewBag.mje = " se elimino tipo";
                    return View();
                }
                ViewBag.mje = " No  se elimino tipo";
                return View();
                
            }
            catch(Exception ex)
            {
                ViewBag.mje = " No  se elimino tipo"+ex.Message;
                return View();
            }
        }

        public ActionResult Buscar()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buscar(string nombretipo)
        {
            try
            {
                TipoPlanta tp = _repoTipo.BuscarPorNombre(nombretipo);
                if (tp != null)
                {
                    ViewBag.tp = tp;
                    ViewBag.mje = "tipo encontrado : ";
                    return View();
                }
                ViewBag.mje = "No se encontro con ese nombre ";
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.mje = "No se encontro con ese nombre "+ex.Message;
                return View();
            }
        }

    }
}
