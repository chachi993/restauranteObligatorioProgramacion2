﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObligatorioP2;
using ObligatorioP2MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP2MVC.Controllers
{
    public class HomeController : Controller
    {
        Sistema s = Sistema.GetInstancia(); //nos permite hacer uso del singleton

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() //muestra la vista inicial de la pagina
        {
            int? logueadoId = HttpContext.Session.GetInt32("LogueadoId");
            
            if(logueadoId != null) //si se inicio sesion como un usuario con rol...
            {
                string rol = HttpContext.Session.GetString("LogueadoRol"); 
                ViewBag.msg = $"Bienvenido/a, usted inició sesión como {rol}"; //se muestra el mensaje de bienvenido y su rol
            }
            else {
                ViewBag.msg = "Inicie sesión"; //de lo contrario se pide que inicie sesion
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login() //nos permite retornar a la vista cuando se hace login
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password) 
        {
            Persona login = s.ValidarDatosLogin(username, password); //la persona  es traida de la validacion de datos de login
            string rol; 
            if(login != null)
            {
                if(login is Cliente) //si el la persona que se loguea es cliente, entonces su rol es cliente
                {
                    rol = "Cliente";
                }
                else if(login is Repartidor)//si el la persona que se loguea es repartidor, entonces su rol es repartidor
                {
                    rol = "Repartidor";
                }
                else //igual razonamiento con mozo
                {
                    rol = "Mozo";
                }
                HttpContext.Session.SetInt32("LogueadoId", login.Id);
                HttpContext.Session.SetString("LogueadoRol", rol);

                ViewBag.msg = "Bienvenido";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Datos no válidos";
                return View();
            }
        }
         public IActionResult Logout() //si se desea cerrar sesion, se hace clear del rol y se redirige a index
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
