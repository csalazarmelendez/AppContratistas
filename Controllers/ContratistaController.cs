using Contratistas.Data;
using Contratistas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Contratistas.Controllers
{
    
    public class ContratistaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly string wwwrootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public ContratistaController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            var operadores = _context.Operador.ToList();
            List<(string, string)> listaoperadores = new List<(string, string)>();
            foreach (var o in operadores)
            {
                var value = o.Id.ToString() + ',' + o.Nombre + ',' + o.Numero;
                var item = (Data: value, Nombre: o.Nombre);
                listaoperadores.Add(item);
            }
            @ViewBag.Operadores = listaoperadores;
            
            return View("Views/Contratista/Registro.cshtml");
        }

        [HttpPost]
        public IActionResult Registro(SolicitudRegistro solicitudRegistro)
        {
            //Crea la solicitud de registro

            bool valid = false;
            solicitudRegistro.Transaccion = "Pendiente";
            if (ModelState.IsValid)
            {
                valid = true;
            }
            else
            {
                string operador = Request.Form["nuevooperador"].ToString();
                string numero = Request.Form["numeros"].ToString();
                solicitudRegistro.Operador = operador;
                solicitudRegistro.NumeroOperador = numero;
                valid = ModelState.IsValid ? true : false;
            }
            
            if (valid)
            {
                string contrasena = Request.Form["contrasena"].ToString();
                string conf_contrasena = Request.Form["conf_contrasena"].ToString();
                if (contrasena == conf_contrasena)
                {
                    _context.SolicitudRegistro.Add(solicitudRegistro);
                    _context.SaveChanges();
                    TempData["mensaje"] = "Solicitud de creación de usuario enviada";
                    return RedirectToAction("Index", "Ingreso");
                }
            }
            TempData["mensaje"] = "Algo ocurrió. No se envió la solicitud...";
            return RedirectToAction("Registro", "Contratista");
        }
        /*--------------------------------Ingreso datos de trabajador-------------------------------------*/
        public IActionResult IngresoTrabajador(int? contratistaid)
        {
            //Envía los datos para el formulario de ingresar trabajador
            var listaObras = _context.Obra.ToList();
            List<string> listaObrasNombre = new List<string>();
            foreach (var item in listaObras)
            {
                string obra = item.obrCodigo + " " + item.obrNombre;
                listaObrasNombre.Add(obra);
            }
            @ViewBag.Obras = listaObrasNombre;
            @ViewData["Contratista"] = contratistaid;
            var nombre = _context.Contratista.Find(contratistaid);
            @ViewBag.Nombre = nombre.Razon_social;
            return View();
        }

        public IActionResult ModificarDatosTrabajador(int contratistaid, int? trabajadorced, int? trabajadorid, int? actualizarid, ValidarDatosTrabajador datosActualizar)
        {
            var contratista = _context.Contratista.Find(contratistaid);
            @ViewBag.Nombre = contratista.Razon_social;
            @ViewData["Contratista"] = contratistaid;
            if (trabajadorced != null || trabajadorid != null)
            {
                string cedula = (trabajadorced != null) ? Request.Form["cedula"].ToString() : _context.Trabajador.Find(trabajadorid).Cedula.ToString();
                var getTrabajador = _context.Trabajador.Where(s => s.Cedula == cedula && s.IdContratista == contratistaid).ToList();
                if (getTrabajador.Count > 0)
                {
                    List<Obra> obras = new List<Obra>();
                    var obrasxtrabajador = _context.TrabajadorXObra.Where(s => s.IdTrabajador == getTrabajador[0].Id).ToList();
                    foreach (var o in obrasxtrabajador)
                    {
                        var obra = _context.Obra.Find(o.ObrId);
                        obras.Add(obra);                        
                    }
                    var subcontratista = (getTrabajador[0].IdSubcontratista != null) ? _context.Subcontratista.Where(s => s.Id == getTrabajador[0].IdSubcontratista).ToList()[0] : null;
                    @ViewBag.Obras = obras;
                    @ViewBag.Trabajador = getTrabajador[0];
                    @ViewBag.Subcontratista = subcontratista;
                }
                else
                {
                    @ViewBag.Trabajador = null;
                }
            }
            else if (actualizarid != null)
            {
                var trabajador = _context.Trabajador.Find(actualizarid);
                var documento = _context.Documento.Where(s => s.Trabajador == actualizarid).ToList()[0];
                trabajador.Nombre = datosActualizar.Nombre;
                trabajador.Apellido = datosActualizar.Apellido;
                trabajador.Email = datosActualizar.Email;
                trabajador.Telefono = datosActualizar.Telefono;
                trabajador.PersonaContacto = datosActualizar.PersonaContacto;
                trabajador.TelefonoContacto = datosActualizar.TelefonoContacto;

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Documentos"); //borrar linea filePath = filePath + @"\Documentos";
                filePath = filePath + @"\" + contratista.NIT;
                string ced = trabajador.Cedula;
                string rutaDefinitiva = "";
                string rutawwwroot = "~/Documentos/" + contratista.NIT + "/";
                if (datosActualizar.CedulaImg != null)
                {
                    //Guarda la cedula
                    string cedula = ced + "_cedula" + datosActualizar.CedulaImg.FileName.Substring(datosActualizar.CedulaImg.FileName.Length - 4);
                    rutaDefinitiva = Path.Combine(filePath, cedula);
                    datosActualizar.CedulaImg.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));
                    documento.Cedula = Path.Combine(rutawwwroot, cedula);
                }
                if (datosActualizar.EPS != null)
                {
                    //Guarda la EPS
                    string eps = ced + "_eps" + datosActualizar.EPS.FileName.Substring(datosActualizar.EPS.FileName.Length - 4);
                    rutaDefinitiva = Path.Combine(filePath, eps);
                    datosActualizar.EPS.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));
                    documento.EPS = Path.Combine(rutawwwroot, eps);
                }
                if (datosActualizar.ARL != null)
                {
                    //Guarda el ARL
                    string arl = ced + "_arl" + datosActualizar.ARL.FileName.Substring(datosActualizar.ARL.FileName.Length - 4);
                    rutaDefinitiva = Path.Combine(filePath, arl);
                    datosActualizar.ARL.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));
                    documento.ARL = Path.Combine(rutawwwroot, arl);
                }
                if (datosActualizar.Pension != null)
                {
                    //Guarda la Pension
                    string pension = ced + "_pension" + datosActualizar.Pension.FileName.Substring(datosActualizar.Pension.FileName.Length - 4);
                    rutaDefinitiva = Path.Combine(filePath, pension);
                    datosActualizar.Pension.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));
                    documento.Pension = Path.Combine(rutawwwroot, pension);
                }
                _context.Trabajador.Update(trabajador);
                _context.SaveChanges();
                _context.Documento.Update(documento);
                _context.SaveChanges();
                return RedirectToAction("ModificarDatosTrabajador", "Contratista", new { contratistaid = contratista.Id });
            }
            else
            {
                var trabajadores = _context.Trabajador.Where(s => s.IdContratista == contratistaid).ToList();
                @ViewBag.Trabajadores = trabajadores;
            }

            return View();
        }

        public IActionResult RegistroTrabajador(ValidarDatosTrabajador validarDatosTrabajador, int contratistaid)
        {
            //Valida los datos para guardarlos en la base de datos
            var nombre = _context.Contratista.Find(contratistaid);
            @ViewBag.Nombre = nombre.Razon_social;
            validarDatosTrabajador.EstadoIngreso = "No autorizado";
            var contratista = _context.Contratista.Find(contratistaid);
            string nitContratista = contratista.NIT;
            var trabajadorExiste = _context.Trabajador.Where(s => s.Cedula == validarDatosTrabajador.Cedula).ToList();
            if (ModelState.IsValid && trabajadorExiste.Count == 0)
            {
                //Guardar los documentos
                string guidImage = null;
                if (validarDatosTrabajador.CedulaImg != null && validarDatosTrabajador.EPS != null && validarDatosTrabajador.ARL != null && validarDatosTrabajador.Pension != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Documentos"); //borrar linea filePath = filePath + @"\Documentos";
                    /*string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    filePath = filePath + @"\Documentos";
                    //Crea carpeta documentos en el escritorio
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }*/
                    filePath = filePath + @"\" + nitContratista;
                    //Crea carpeta para los documentos de los trabajadores de un contratista
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    guidImage = Guid.NewGuid().ToString();
                    string ced = validarDatosTrabajador.Cedula;
                    //Guarda la cedula
                    string cedula = ced + "_cedula" + validarDatosTrabajador.CedulaImg.FileName.Substring(validarDatosTrabajador.CedulaImg.FileName.Length - 4);
                    string rutaDefinitiva = Path.Combine(filePath, cedula);
                    validarDatosTrabajador.CedulaImg.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));
                    //Guarda la EPS
                    string eps = ced + "_eps" + validarDatosTrabajador.EPS.FileName.Substring(validarDatosTrabajador.EPS.FileName.Length - 4);
                    rutaDefinitiva = Path.Combine(filePath, eps);
                    validarDatosTrabajador.EPS.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));
                    //Guarda el ARL
                    string arl = ced + "_arl" + validarDatosTrabajador.ARL.FileName.Substring(validarDatosTrabajador.ARL.FileName.Length - 4);
                    rutaDefinitiva = Path.Combine(filePath, arl);
                    validarDatosTrabajador.ARL.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));
                    //Guarda la Pension
                    string pension = ced + "_pension" + validarDatosTrabajador.Pension.FileName.Substring(validarDatosTrabajador.Pension.FileName.Length - 4);
                    rutaDefinitiva = Path.Combine(filePath, pension);
                    validarDatosTrabajador.Pension.CopyTo(new FileStream(rutaDefinitiva, FileMode.Create));

                    //Crear subcontratista
                    string nitSubcontratista = Request.Form["nitSubcontratista"].ToString();
                    string nombreSubcontratista = Request.Form["nombreSubcontratista"].ToString();
                    bool tieneSubcontratista = false;
                    if (nitSubcontratista != null && nitSubcontratista != "" && nombreSubcontratista != null && nombreSubcontratista != "")
                    {
                        tieneSubcontratista = true;
                        var subcontratistas = _context.Subcontratista.Where(s => s.NIT == nitSubcontratista).ToList();
                        if (subcontratistas.Count == 0)
                        {
                            Subcontratista subcontratista = new Subcontratista();
                            subcontratista.NIT = nitSubcontratista;
                            subcontratista.Razon_social = nombreSubcontratista;
                            _context.Subcontratista.Add(subcontratista);
                            _context.SaveChanges();
                        }
                    }

                    Trabajador trabajador = new Trabajador();
                    trabajador.Cedula = validarDatosTrabajador.Cedula;
                    trabajador.Nombre = validarDatosTrabajador.Nombre;
                    trabajador.Apellido = validarDatosTrabajador.Apellido;
                    trabajador.Email = validarDatosTrabajador.Email;
                    trabajador.Telefono = validarDatosTrabajador.Telefono;
                    trabajador.PersonaContacto = validarDatosTrabajador.PersonaContacto;
                    trabajador.TelefonoContacto = validarDatosTrabajador.TelefonoContacto;
                    trabajador.Ciudad = validarDatosTrabajador.Ciudad;
                    trabajador.CedulaValida = "No válida";
                    trabajador.EPSValida = "No válida";
                    trabajador.ARLValida = "No válida";
                    trabajador.PensionValida = "No válida";
                    trabajador.EstadoIngreso = validarDatosTrabajador.EstadoIngreso;
                    trabajador.IdContratista = contratistaid;
                    if (tieneSubcontratista)
                    {
                        var subcontratistas = _context.Subcontratista.Where(s => s.NIT == nitSubcontratista).ToList();
                        int idSubcontratista = subcontratistas[0].Id;
                        trabajador.IdSubcontratista = idSubcontratista;
                    }

                    _context.Trabajador.Add(trabajador);
                    _context.SaveChanges();
                    //Agregar Documento del trabajador
                    var getTrabajador = _context.Trabajador.Where(s => s.Cedula == trabajador.Cedula).ToList();
                    int idTrabajador = getTrabajador[0].Id;
                    Documento documento = new Documento();
                    string rutawwwroot = "~/Documentos/" + contratista.NIT + "/";
                    /*documento.Cedula = Path.Combine(filePath, cedula);
                    documento.EPS = Path.Combine(filePath, eps);
                    documento.ARL = Path.Combine(filePath, arl);
                    documento.Pension = Path.Combine(filePath, pension);*/
                    documento.Cedula = Path.Combine(rutawwwroot, cedula);
                    documento.EPS = Path.Combine(rutawwwroot, eps);
                    documento.ARL = Path.Combine(rutawwwroot, arl);
                    documento.Pension = Path.Combine(rutawwwroot, pension);
                    documento.Trabajador = idTrabajador;
                    _context.Documento.Add(documento);
                    _context.SaveChanges();
                    //Agregar Obra a la tabla TrabajadorXObra
                    string obra = Request.Form["cod"].ToString();
                    var getObra = _context.Obra.Where(s => s.obrCodigo == obra).ToList();
                    TrabajadorXObra obraTrabajador = new TrabajadorXObra();
                    if (getObra.Count > 0)
                    {
                        obraTrabajador.ObrId = getObra[0].Id;
                    }
                    else
                    {
                        TempData["mensaje"] = "Obra digitada no disponible. Ingrese unicamente los valores del desplegable";
                        return RedirectToAction("IngresoTrabajador", "Contratista", new { contratistaid = contratista.Id });
                    }
                    obraTrabajador.IdTrabajador = idTrabajador;
                    Console.WriteLine(obraTrabajador.ObrId);
                    Console.WriteLine(obraTrabajador.IdTrabajador);
                    _context.TrabajadorXObra.Add(obraTrabajador);
                    _context.SaveChanges();
                    TempData["mensaje1"] = "Se registró el trabajador con éxito";
                }
                else
                {
                    TempData["mensaje"] = "Hay uno o más documentos no soportados";
                }
            }
            else if (trabajadorExiste.Count > 0)
            {
                TempData["mensaje"] = "Ya existe un trabajador registrado con esta cédula";
            }
            else
            {
                TempData["mensaje"] = "Es necesario llenar los campos solicitados";
            }
            return RedirectToAction("IngresoTrabajador", "Contratista", new { contratistaid = contratista.Id });
        }

        public IActionResult SolicitudAgregarObra(int contratistaid)
        {
            @ViewData["Contratista"] = contratistaid;
            var nombre = _context.Contratista.Find(contratistaid);
            @ViewBag.Nombre = nombre.Razon_social;
            var trabajadores = _context.Trabajador.Where(s => s.IdContratista == contratistaid).ToList();
            List<string> nomTrabajadores = new List<string>();
            foreach (var t in trabajadores)
            {
                string nombreCompleto = t.Cedula + " " + t.Nombre + " " + t.Apellido;
                nomTrabajadores.Add(nombreCompleto);
            }
            @ViewBag.Trabajadores = nomTrabajadores;
            if (nomTrabajadores.Count == 0)
            {
                TempData["mensaje"] = "Aun no hay trabajadores registrados con este contratista.";
            }
            var listaObras = _context.Obra.ToList();
            List<string> listaObrasNombre = new List<string>();
            foreach (var item in listaObras)
            {
                string obra = item.obrCodigo + " " + item.obrNombre;
                listaObrasNombre.Add(obra);
            }
            @ViewBag.Obras = listaObrasNombre;
            return View();
        }

        public IActionResult EnviarSolicitud(int contratistaid)
        {
            @ViewData["Contratista"] = contratistaid;
            string cedula = Request.Form["ced"].ToString();
            string nombre = Request.Form["nombre"].ToString();
            string obra = Request.Form["cod"].ToString();
            SolicitudObra solicitudObra = new SolicitudObra();
            var idtrabajador = _context.Trabajador.Where(s => s.Cedula == cedula).ToList();
            var idobra = _context.Obra.Where(s => s.obrCodigo == obra).ToList();
            List<TrabajadorXObra> obrasTrabajador = new List<TrabajadorXObra>();
            if (idtrabajador.Count > 0 && idobra.Count > 0)
            {
                obrasTrabajador = _context.TrabajadorXObra.Where(s => s.IdTrabajador == idtrabajador[0].Id && s.ObrId == idobra[0].Id).ToList();
            }
            else 
            {
                TempData["mensaje"] = "Obra o trabajador digitado no disponible. Ingrese unicamente los valores del desplegable";
                return RedirectToAction("SolicitudAgregarObra", "Contratista", new { contratistaid = contratistaid });
            }
            
            if (obrasTrabajador.Count > 0)
            {
                TempData["mensaje"] = "El trabajador ya está asociado a esta obra";
                return RedirectToAction("SolicitudAgregarObra", "Contratista", new { contratistaid = contratistaid });
            }
            
            var solicitudesObra = _context.SolicitudObra.Where(s => s.IdTrabajador == idtrabajador[0].Id && s.ObrId == idobra[0].Id && s.Estado == "Pendiente").ToList();
            if (solicitudesObra.Count > 0)
            {
                TempData["mensaje"] = "Ya se envió una solicitud de este trabajador para esta obra";
                return RedirectToAction("SolicitudAgregarObra", "Contratista", new { contratistaid = contratistaid });
            }
            solicitudObra.IdTrabajador = idtrabajador[0].Id;
            solicitudObra.ObrId = idobra[0].Id;
            solicitudObra.Estado = "Pendiente";
            _context.SolicitudObra.Add(solicitudObra);
            _context.SaveChanges();
            TempData["mensajeE"] = "Solicitud enviada con éxito";
            return RedirectToAction("SolicitudAgregarObra", "Contratista", new { contratistaid = contratistaid });
        }

        /*-----------------------------------Cambio de contraseña----------------------------------------------*/
        public IActionResult CambioContrasena(int contratistaid, int? cambio)
        {
            @ViewData["Contratista"] = contratistaid;
            var nombre = _context.Contratista.Find(contratistaid);
            @ViewBag.Nombre = nombre.Razon_social;
            if (cambio == 1)
            {
                string actual = Request.Form["actual"].ToString();
                string nueva = Request.Form["nueva"].ToString();
                string confirmar = Request.Form["confirmar"].ToString();
                if (nueva == confirmar)
                {
                    var contratista = _context.Contratista.Find(contratistaid);
                    if (contratista.Contrasena == actual)
                    {
                        contratista.Contrasena = nueva;
                        _context.Contratista.Update(contratista);
                        _context.SaveChanges();
                        TempData["mensajeContrasenaActualizada"] = "Contraseña actualizada";
                        return RedirectToAction("Index", "Ingreso");
                    }
                    else
                    {
                        TempData["mensajeError"] = "La contraseña actual es errónea.";
                        return RedirectToAction("CambioContrasena", "Contratista", new { contratistaid = contratistaid });
                    }
                }
                else
                {
                    TempData["mensajeConfirmar"] = "Las contraseñas no coinciden";
                    return RedirectToAction("CambioContrasena", "Contratista", new { contratistaid = contratistaid });
                }
            }
            return View();
        }
    }
}
