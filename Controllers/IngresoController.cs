using Contratistas.Data;
using Contratistas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contratistas.Controllers
{
    public class IngresoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public IngresoController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.signInManager = signInManager;
        }

        public IActionResult Index(string returnUrl)
        {
            @ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public IActionResult SolicitudRegistro()
        {
            return View("Views/Contratista/Registro.cshtml");
        }

        //Http Post Ingresar
        [HttpPost]
        public IActionResult Ingresar(IFormCollection formCollection, string returnUrl)
        {
            string tipo_usuario = formCollection["tipo_usuario"].ToString();
            string usuario = formCollection["usuario"].ToString();
            string contrasena = formCollection["contrasena"].ToString();
            /*
            var claims = new List<Claim>();
            claims.Add(new Claim("username", usuario));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario));
            var claimsIdentity = new ClaimsIdentity(claims)
            */
            //var identityResult = await signInManager.PasswordSignInAsync(usuario, contrasena, false, false);
            //Console.WriteLine(identityResult.Succeeded);
            //if (identityResult.Succeeded)
            //{
                if (tipo_usuario == "contratista")
                {
                    var usr = _context.Contratista.Where(s => s.Usuario == usuario && s.Contrasena == contrasena).ToList();
                    if (usr.Count() != 0)
                    {
                        if (usr[0].ingresoPortal == true)
                        {
                            Contratista contratista = usr[0]; //Usuario que ingresó
                            return RedirectToAction("IngresoTrabajador", "Contratista", new { contratistaid = contratista.Id });
                        }
                        else
                        {
                            TempData["mensaje"] = "Su cuenta está pendiente a aprobar o fue dada de baja";
                        }
                    }
                    else
                    {
                        TempData["mensaje"] = "Usuario o contraseña incorrectos";
                    }
                }
                else if (tipo_usuario == "administrador")
                {
                    var usr = _context.Administrador.Where(s => s.Usuario == usuario && s.Contrasena == contrasena).ToList();
                    if (usr.Count() != 0)
                    {
                        Administrador admin = usr[0];
                        IEnumerable<SolicitudRegistro> listaSolicitudes = _context.SolicitudRegistro;
                        @ViewData["Administrador"] = admin.Id;
                        //return View("~/Views/Admin/SolicitudContratistas.cshtml", listaSolicitudes);
                        //return View(Administrador);
                        return RedirectToAction("SolicitudContratistas", "Admin", new { adminid = admin.Id });
                    }
                    else
                    {
                        TempData["mensaje"] = "Usuario o contraseña incorrectos";
                    }
                }
                else
                {
                    return View("Views/Ingreso/Index.cshtml");
                }
            //}
            //else
            //{
            //    TempData["mensaje"] = "Usuario o contraseña incorrectos";   
            //}
            return View("Views/Ingreso/Index.cshtml");
        }

        public IActionResult RecuperarContrasena()
        {
            return View();
        }

        public IActionResult ContrasenaNueva()
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&";
            string contrasena_generada = "";
            for (int i = 0; i < 10; i++)
            {
                var random = new Random();
                int indice = random.Next(caracteres.Length);
                contrasena_generada = contrasena_generada + caracteres[indice];
            }
            string usrmail = Request.Form["UsrMail"].ToString();
            var getContratista = _context.Contratista.Where(s => s.Usuario == usrmail || s.Email == usrmail).ToList();
            if (getContratista.Count > 0)
            {
                Contratista contratista = getContratista[0];
                string newcontra = contrasena_generada;
                string mail = contratista.Email;
                string emailOrigen = "vmtoro@constructoramelendez.com";
                string emailDestino = "victortoromanuel@gmail.com";
                //string emailDestino = "mail";
                string contrasena = "vmtc19999";
                MailMessage mailMessage = new MailMessage(emailOrigen, emailDestino, "Recuperar contraseña software contratistas", "<p>Tu contraseña nueva es: " + newcontra + "<p>");
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, contrasena);
                smtpClient.Send(mailMessage);
                smtpClient.Dispose();
                contratista.Contrasena = newcontra;
                _context.Update(contratista);
                _context.SaveChanges();
                TempData["mensajesuc"] = "La contraseña se envió al correo " + mail;
            }
            else
            {
                TempData["mensajedan"] = "El usuario o correo no se encuentra.";
            }
            
            return RedirectToAction("RecuperarContrasena", "Ingreso");
        }
    }
}
