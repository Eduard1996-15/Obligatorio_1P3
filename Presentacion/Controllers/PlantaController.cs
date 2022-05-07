using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;
using Negocio.InterfacesRepositorios;

namespace Presentacion.Controllers
{
    public class PlantaController : Controller
    {
        private IRepositorioTipoPlanta repoTipo;
        private IWebHostEnvironment _environment;
        private IRepositorioPlanta _repoPlanta;
        private IRepositorioParametro repoPara;
        public PlantaController(IRepositorioPlanta repoPlantas, IWebHostEnvironment environment, IRepositorioTipoPlanta repoTipoPlantas, IRepositorioParametro repPa )
        { 
            repoTipo = repoTipoPlantas;
            _environment = environment;
            _repoPlanta = repoPlantas;
            repoPara = repPa;
        }
        public ActionResult Visualizar(Planta p)
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                return View(p);
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }

        public ActionResult VerFicha(string f, string t, int ta)
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                //Planta pl = _repoPlanta.FindById(id);
                Ficha fi = new Ficha { FrecuenciaRiego = f, TipoIluminacion = t, Temperatura = ta };
                return View(fi);
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                List<Planta> p = _repoPlanta.FindAll();

            if (p != null)
            {
                foreach (Planta item in p)
                {
                    item.Tipo = repoTipo.FindById(item.Tipo.Id);
                }
                ViewBag.mje = "Lista de plantas: ";
                return View(p);
            }
            ViewBag.mje = "No hay plantas para mostrar";
            return View(p);
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
                 try
            {
                List<TipoPlanta> tp = repoTipo.FindAll();
            if(tp != null)
            {
                ViewBag.mje = "Create :";
                ViewBag.tipos = tp;
                return View();
            }
            ViewBag.mje = "No hay tipos de planta no se pueden crear plantas sin tipo";
            return View();

            }
            catch (Exception ex)
            {
                ViewBag.mje = "No hay tipos de planta no se pueden crear plantas sin tipo " + ex.Message;
                return View();
            }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
           
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormFile imagen, Planta p, int tipo)
        {
            try
            {
                TipoPlanta tp = repoTipo.FindById(tipo);
                p.TopeDescripcion = repoPara.BuscarRetornarValor("DESCRIPCIONP");
                p.Tipo = tp;
                if (imagen == null || p == null)
                    return View();
                ////ruta física donde está ubicada wwroot en el servidor
                //comprobar si los datos estan correctos
                if (GuardarImagen(imagen, p) && p.ValidarTope())
                {
                    if (_repoPlanta.Add(p))
                    {
                        ViewBag.mje = "se creo planta :";
                        return RedirectToAction("Visualizar", p);
                    }
                }
                List<TipoPlanta> tip = repoTipo.FindAll();
                
                    ViewBag.mje = "ERROR No se creo planta  ";
                    ViewBag.tipos = tip;
                    return View();
                

            }
            catch(Exception ex)
            {
                ViewBag.mje = "ERROR No se creo planta "+ex.Message;
                return View();
            }
        }
        private bool GuardarImagen(IFormFile imagen, Planta p)
        {
            if (imagen == null || p == null)
                return false;
            // SUBIR LA IMAGEN
            string rutaFisicaWwwRoot = _environment.WebRootPath;
            //ruta donde se guardan las fotos de las personas
            string nombreImagen = p.NombreCientifico;
            nombreImagen = p.Generarnombre(nombreImagen);
            string aux = imagen.FileName;
            nombreImagen += aux.Substring(aux.Length - 4);
            string rutaFisicaFoto = Path.Combine(rutaFisicaWwwRoot, "Imagenes", "Fotos", nombreImagen);
            //FileStream permite manejar archivos
            try
            {
                //el método using libera los recursos del objeto FileStream al finalizar
                using (FileStream f = new FileStream(rutaFisicaFoto, FileMode.Create))
                {
                    //si fueran archivos grandes o si fueran varios, deberíamos usar la versión
                    //asincrónica de CopyTo, aquí no es necesario.
                    //sería: await imagen.CopyToAsync (f);
                    imagen.CopyTo(f);
                }
                //GUARDAR EL NOMBRE DE LA IMAGEN SUBIDA EN EL OBJETO
                p.Foto = nombreImagen;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult ListarporNombre()
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
        public ActionResult ListarporNombre(string n)
        {
            try
            {
                IEnumerable<Planta> p = _repoPlanta.ListarPorNombre(n);
            if (p != null)
            {
                ViewBag.Msj = "Plantas por nombre";
                return View(p);
            }
            ViewBag.Msj = "No hay plantas por ese nombre";
            return View();

            }
            catch(Exception ex)
            {
                ViewBag.Msj = "No hay plantas por ese nombre "+ex.Message;
                return View();
            }
           
        }
        [HttpGet]
        public ActionResult ListarporTipo()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
                try
            { 
                List<TipoPlanta> tp = repoTipo.FindAll();
            if (tp != null)
            {
                ViewBag.tp = tp;
                ViewBag.mje = "tipos:";
                return View();
            }
            ViewBag.mje = " no hay tipos para mostrar ";
            return View();

            }catch(Exception x)
            {
                ViewBag.mje = " no hay tipos para mostrar "+x.Message;
                return View();
            }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        [HttpPost]
        public ActionResult ListarporTipo(string n)
        {
            try
            {
                IEnumerable<Planta> p = _repoPlanta.ListarPorTipo(n);
            List<TipoPlanta> tp = repoTipo.FindAll();
            if (p != null && tp != null)
            {
                ViewBag.tp = tp;
                ViewBag.Msj = "Plantas por tipo";
                return View(p);
            }
            ViewBag.Msj = "No hay plantas por ese tipo";
            return View();

            }
            catch (Exception ex)
            {
                ViewBag.Msj = "No hay plantas por ese tipo " + ex.Message;
                return View();
            }
           
        }
        [HttpGet]
        public ActionResult ListarporAmbiente()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
            {
            try
            {
                List<Planta> pa = _repoPlanta.FindAll();
            List<string> amb = guardarAm(pa);
            if (amb != null)
            {
                ViewBag.pa = amb;
                ViewBag.Msj = "Plantas por ambiente ";
                return View();
            }
            ViewBag.Msj = "No hay plantas por ese ambiente";
            return View();

            }
            catch (Exception ex)
            {
                ViewBag.Msj = "No hay plantas por ese ambiente "+ ex.Message;
                return View();
            }
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }
        [HttpPost]
        public ActionResult ListarporAmbiente(string a)
        {
            try
            {
                IEnumerable<Planta> p = _repoPlanta.ListarPorAmbiente(a);
            List<Planta> pa = _repoPlanta.FindAll();
            List<string> amb = guardarAm(pa);
            if (p != null && amb != null)
            {
                ViewBag.pa = amb;
                ViewBag.Msj = "Plantas por ambiente";
                return View(p);
            }
            ViewBag.Msj = "No hay plantas por ese ambiente";
            return View();

            }
            catch (Exception ex)
            {
                ViewBag.Msj = "No hay plantas por ese ambiente " + ex.Message;
                return View();
            }
            
        }
        public List<string> guardarAm(List<Planta> p)
        {
            List<string> aux = new List<string>();
            foreach (Planta item in p)
            {
                //string a = ;
                if (!aux.Contains(item.Ambiente))//eliminar repetidos
                aux.Add(item.Ambiente);
            }
            return aux;
        }
        [HttpGet]
        public ActionResult ListaPorAlturaMayor()
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
        public ActionResult ListaPorAlturaMayor(int altmay)
        {
            List<Planta> p = (List<Planta>)_repoPlanta.ListarPorAlturaMayor(altmay);
            if (p != null)
            {
                ViewBag.Msj = "Plantas por Altura mayor";
                return View(p);
            }
            ViewBag.Msj = "No hay plantas mayores a esa altura";
            return View();
        }
        public ActionResult ListaPorAlturaMenor()
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
        public ActionResult ListaPorAlturaMenor(int altmen)
        {
            List<Planta> p = (List<Planta>)_repoPlanta.ListarPorAlturaMenor(altmen);
            if (p != null)
            {

                ViewBag.Msj = "Plantas por Altura menor";
                return View(p);
            }
            ViewBag.Msj = "No hay plantas menores a esa altura";
            return View();
        }
    }
}
