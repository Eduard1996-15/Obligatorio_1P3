using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Negocio.EntidadesNegocios;
using Dominio.EntidadesNegocios;
using Dominio.InterfacesRepositorios;

namespace Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        private IRepositorioUsuario _repoCli;
        public UsuarioController(IRepositorioUsuario repoClientes)
        {
            _repoCli = repoClientes;//punto debil
        }

        public IActionResult Login()
        {
            return View();
        }

        //logueo post 
        [HttpPost]
        public IActionResult Login(string e, string p)
        {
            try
            {
                Usuario buscado = new Usuario { Email = e, Password = p };
                if (buscado.Validar())
                {
                    buscado = _repoCli.Loguin(e, p);
                    if (buscado != null)
                    {
                        HttpContext.Session.SetString("UsuarioEmail", buscado.Email);//guardo el email
                        HttpContext.Session.SetInt32("UsuarioId", buscado.Id);//guardo el id
                        TempData["mje"] = $" bien venido {buscado.Email}";
                        return RedirectToAction("Index", "Home");//redireccionar al indice
                    }
                    else
                    {
                        ViewBag.mje = "Error de login ";//no encontro en la bd
                       return View();
                    }
                }
                else
                {
                    ViewBag.mje = "Error- email o contrasenia ingresados incorrectamente ";//no paso por la validacion
                    return View();
                }
            }
            catch
            {
                ViewBag.mje = "Error- de Login ";
                return View();
            }
        }


        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
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

        public IActionResult Logout()
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
        public IActionResult Logout(string n)
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UsuarioId");
            return RedirectToAction("Login", "Usuario");
        }
    }
}
