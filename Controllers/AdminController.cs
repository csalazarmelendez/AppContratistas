using Contratistas.Data;
using Contratistas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Contratistas.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AdminController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AdminController> logger)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //-----------------------------------------------Solicitudes de Registro------------------------------------------
        
        public IActionResult SolicitudContratistas(int adminid, int? idcontratista, int? filtro)
        {
            //Vista de solicitudes de registro de contratistas
            IEnumerable<SolicitudRegistro> listaSolicitudes = _context.SolicitudRegistro;
            @ViewData["Administrador"] = adminid;
            var listaContratistas = _context.Contratista.ToList();
            if (idcontratista != null)
            {
                Console.WriteLine(idcontratista);
                listaSolicitudes = _context.SolicitudRegistro.Where(s => s.Id == idcontratista).ToList();

            }
            else if (filtro != null)
            {
                if (filtro == 0)
                {
                    listaSolicitudes = _context.SolicitudRegistro.Where(s => s.Transaccion == "Aprobada");
                }
                else if (filtro == 1)
                {
                    listaSolicitudes = _context.SolicitudRegistro.Where(s => s.Transaccion == "Rechazada");
                }
                else
                {
                    listaSolicitudes = _context.SolicitudRegistro.Where(s => s.Transaccion == "Pendiente");
                }
            }
            List<SolicitudRegistro> contratistas = _context.SolicitudRegistro.ToList();
            contratistas.Reverse();
            @ViewBag.Contratistas = contratistas;
            return View(listaSolicitudes);
            //return View();
        }

        public IActionResult FiltroTransaccion(int? adminid, int filtro)
        {
            //Filtro solicitudes aprobadas de registro de contratistas
            IEnumerable<SolicitudRegistro> listaSolicitudes = _context.SolicitudRegistro.Where(s => s.Transaccion.Contains("Aprobada"));
            @ViewData["Administrador"] = adminid;
            return RedirectToAction("SolicitudContratistas", "Admin", new { adminid = adminid, filtro = filtro });
        }

        public async Task<IActionResult> Aprobar(int adminid, int? id, string filtro)
        {
            //Aprueba la solicitud de registro de contratista al portal
            @ViewData["Administrador"] = adminid;
            var solicitud = _context.SolicitudRegistro.Find(id);
            var getOperador = _context.Operador.Where(s => s.Numero == solicitud.NumeroOperador).ToList();
            int oprId = 0;
            string nombreOperador = " ";
            if (getOperador.Count == 0)
            {
                //Agregar operador
                Operador operador = new Operador();
                operador.Nombre = solicitud.Operador;
                operador.Numero = solicitud.NumeroOperador;
                _context.Operador.Add(operador);
                _context.SaveChanges();
                var getNewOperador = _context.Operador.Where(s => s.Numero == operador.Numero).ToList();
                oprId = getNewOperador[0].Id;
                nombreOperador = getNewOperador[0].Nombre;
            }
            else
            {
                oprId = getOperador[0].Id;
                nombreOperador = getOperador[0].Nombre;
            }
            var contratistas = _context.Contratista.Where(s => s.IdSolicitudRegistro == solicitud.Id).ToList();
            var contratistasnit = _context.Contratista.Where(s => s.NIT == solicitud.NIT).ToList();
            if (contratistas.Count == 0 && contratistasnit.Count == 0)
            {
                //Agregar contratista
                Contratista contratista = new Contratista();
                contratista.NIT = solicitud.NIT;
                contratista.Razon_social = solicitud.Razon_social;
                contratista.Email = solicitud.Email;
                contratista.Usuario = solicitud.Usuario;
                contratista.Contrasena = solicitud.Contrasena;
                contratista.ins_mod_elim = 'i';
                contratista.Responsable = adminid;
                contratista.IdOperador = oprId;
                contratista.IdSolicitudRegistro = solicitud.Id;
                contratista.ingresoPortal = true;
               
                var user = new IdentityUser()
                {
                    UserName = contratista.Usuario,
                    Email = contratista.Email
                };
                
                var result = await userManager.CreateAsync(user, contratista.Contrasena);

                if (result.Succeeded)
                {   
                    await signInManager.SignInAsync(user, false);
                    _context.Contratista.Add(contratista);
                    _context.SaveChanges();
                }
            }
            else if (contratistasnit.Count != 0)
            {
                TempData["mensaje"] = "Ya hay un contratista registrado con este NIT";
                return RedirectToAction("SolicitudContratistas", "Admin", new { adminid = adminid });
            }
            else
            {
                Contratista contratista = _context.Contratista.Find(contratistas[0].Id);
                contratista.ingresoPortal = true;
                contratista.ins_mod_elim = 'm';
                _context.Contratista.Update(contratista);
                _context.SaveChanges();
            }
            solicitud.Transaccion = "Aprobada";
            solicitud.Operador = nombreOperador;
            _context.SolicitudRegistro.Update(solicitud);
            _context.SaveChanges();
            return RedirectToAction("SolicitudContratistas", "Admin", new { adminid = adminid });
        }

        public IActionResult Rechazar(int adminid, int? id, string filtro)
        {
            //Rechaza una solicitud de registro de contratista
            @ViewData["Administrador"] = adminid;
            Console.WriteLine(filtro);
            var solicitud = _context.SolicitudRegistro.Find(id);
            var contratistas = _context.Contratista.Where(s => s.IdSolicitudRegistro == solicitud.Id).ToList();
            Contratista contratista = _context.Contratista.Find(contratistas[0].Id);
            contratista.ingresoPortal = false;
            _context.Contratista.Update(contratista);
            _context.SaveChanges();
            solicitud.Transaccion = "Rechazada";
            _context.SolicitudRegistro.Update(solicitud);
            _context.SaveChanges();
            return RedirectToAction("SolicitudContratistas", "Admin", new { adminid = adminid });
        }

        //-----------------------------------------Validación de datos de trabajadores-----------------------------------------------
        
        public IActionResult ValidarDatos(int adminid, int? idtrabajador, int? idcontratista, int? filtro)
        {
            //Vista de los datos del trabajador buscado en BuscarTrabajador
            @ViewData["Administrador"] = adminid;
            if (idtrabajador != null)
            {
                //Datos del trabajador
                var trabajador = _context.Trabajador.Find(idtrabajador);
                Console.WriteLine(trabajador.Nombre);
                Console.WriteLine(trabajador.SeguridadSocialValida);
                var contratista = _context.Contratista.Find(trabajador.IdContratista);
                var documentos = _context.Documento.Where(s => s.Trabajador == idtrabajador).ToList();
                var gettrabajadorxobras = _context.TrabajadorXObra.Where(s => s.IdTrabajador == idtrabajador).ToList();
                List<string> obras = new List<string>();
                foreach (var i in gettrabajadorxobras)
                {
                    var obra = _context.Obra.Find(i.ObrId);
                    obras.Add(obra.obrNombre);
                }
                var documento = documentos[0];
                @ViewBag.Id = idtrabajador;
                @ViewBag.Cedula = trabajador.Cedula;
                @ViewBag.Nombre = trabajador.Nombre;
                @ViewBag.Apellido = trabajador.Apellido;
                @ViewBag.Email = trabajador.Email;
                @ViewBag.Telefono = trabajador.Telefono;
                @ViewBag.PersonaContacto = trabajador.PersonaContacto;
                @ViewBag.TelefonoContacto = trabajador.TelefonoContacto;
                @ViewBag.Ciudad = trabajador.Ciudad;
                @ViewBag.CedulaValida = trabajador.CedulaValida;
                @ViewBag.EPSValida = trabajador.EPSValida;
                @ViewBag.ARLValida = trabajador.ARLValida;
                @ViewBag.PensionValida = trabajador.PensionValida;
                @ViewBag.SeguridadValida = trabajador.SeguridadSocialValida;
                @ViewBag.PlanillaValida = trabajador.PlanillaValida;
                @ViewBag.CMLValido = trabajador.CertificadoMedicoLaboralValido;
                @ViewBag.Contratista = contratista.Razon_social;
                @ViewBag.NitContratista = contratista.NIT;
                @ViewBag.CedulaImg = documento.Cedula;
                @ViewBag.EPS = documento.EPS;
                @ViewBag.ARL = documento.ARL;
                @ViewBag.Pension = documento.Pension;
                @ViewBag.Seguridad = documento.SeguridadSocial;
                @ViewBag.Planilla = documento.Planilla;
                @ViewBag.CML = documento.CertificadoMedicoLaboral;

                @ViewBag.EstadoIngreso = trabajador.EstadoIngreso;
                @ViewBag.FechaIngreso = trabajador.FechaIngresoObra;
                @ViewBag.FechaFin = trabajador.FechaFinObra;
                @ViewBag.Observacion = trabajador.Observacion;
                @ViewBag.Obras = obras;
                if (trabajador.EstadoIngreso == "No autorizado")
                {
                    @ViewBag.NotEstadoIngreso = "Autorizado";
                }
                else
                {
                    @ViewBag.NotEstadoIngreso = "No autorizado";
                }
                if (trabajador.IdSubcontratista != null)
                {
                    var subcontratista = _context.Contratista.Find(trabajador.IdSubcontratista);
                    @ViewBag.Subcontratista = subcontratista.Razon_social;
                    @ViewBag.NitSubcontratista = subcontratista.NIT;
                }
                else
                {
                    @ViewBag.Subcontratista = "Sin subcontratista";
                    @ViewBag.NitSubcontratista = "Sin subcontratista";
                }
            }
            else
            {
                List<Trabajador> listaTrabajadores;
                listaTrabajadores = (idcontratista != null) ? _context.Trabajador.Where(s => s.IdContratista == idcontratista).ToList() : _context.Trabajador.ToList();
                if (filtro != null)
                {
                    listaTrabajadores = (filtro == 0) ? _context.Trabajador.Where(s => s.EstadoIngreso == "No Autorizado").ToList() : _context.Trabajador.Where(s => s.EstadoIngreso == "Autorizado").ToList();
                }
                List<List<string>> trabajadores = new List<List<string>>();
                
                foreach (var t in listaTrabajadores)
                {
                    List<string> trabajador = new List<string>();
                    var contratista = _context.Contratista.Find(t.IdContratista);
                    trabajador.Add(t.Cedula);
                    trabajador.Add(t.Nombre);
                    trabajador.Add(t.Apellido);
                    trabajador.Add(contratista.Razon_social);
                    if (t.IdSubcontratista != null)
                    {
                        var subcontratista = _context.Subcontratista.Find(t.IdSubcontratista);
                        trabajador.Add(subcontratista.Razon_social);
                    }
                    else
                    {
                        trabajador.Add("No tiene");
                    }
                    trabajador.Add(t.EstadoIngreso);
                    trabajador.Add(t.Id.ToString());
                    trabajadores.Add(trabajador);
                }
                @ViewBag.Trabajadores = trabajadores;
            }
            List<Contratista> contratistas = _context.Contratista.ToList();
            contratistas.Reverse();
            @ViewBag.Contratistas = contratistas;
            List<Trabajador> trabajadores_ = _context.Trabajador.ToList();
            List<string> nomTrabajadores = new List<string>();
            foreach (var t in trabajadores_)
            {
                string nombreCompleto = t.Cedula + " " + t.Nombre + " " + t.Apellido;
                nomTrabajadores.Add(nombreCompleto);
            }
            nomTrabajadores.Reverse();
            @ViewBag.Trabajadores_ = nomTrabajadores;
            return View();
        }

        public async  Task<IActionResult> DownloadFile(int adminid, int idtrabajador, string filePath)
        {
            /*filePath = filePath.Remove(0, 1);
            string path = Directory.GetCurrentDirectory() + @"\wwwroot";
            foreach (var c in filePath)
            {
                if (c == '/')
                {
                    path = path + @"\";
                }
                else
                {
                    path = path + c;
                }
            }*/
            string path = filePath;
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var contentType = "application/octet-stream";
            var fileName = Path.GetFileName(path);
            return File(memory, contentType, fileName);
        }

        public IActionResult FiltroEstadoIngreso(int adminid, int filtro)
        {
            @ViewData["Administrador"] = adminid;
            return RedirectToAction("ValidarDatos", "Admin", new { adminid = adminid , filtro = filtro});
        }

        [HttpPost]
        public IActionResult BuscarTrabajador(int? adminid)
        {
            //Busca el trabajador y envía el id del trabajador en caso de encontrar coincidencias
            @ViewData["Administrador"] = adminid;
            string cedula = Request.Form["ced"].ToString();
            string nombre = Request.Form["nombre"].ToString();
            if (!String.IsNullOrEmpty(nombre) && !String.IsNullOrEmpty(cedula))
            {
                var listaTrabajadores = _context.Trabajador.Where(s => s.Nombre.Contains(nombre) || s.Apellido.Contains(nombre) || s.Cedula.Contains(cedula)).ToList();
                if (listaTrabajadores.Count > 0)
                {
                    int idtrabajador = listaTrabajadores[0].Id;
                    return RedirectToAction("ValidarDatos", "Admin", new { adminid = adminid , idtrabajador = idtrabajador});
                }
                else
                {
                    TempData["mensaje"] = "No se encontraron coincidencias";
                    return RedirectToAction("ValidarDatos", "Admin", new { adminid = adminid });
                }
            }
            return RedirectToAction("ValidarDatos", "Admin", new { adminid = adminid });
        }


        public IActionResult GuardarDatos(int? adminid, int idtrabajador)
        {
            //Captura los datos cambiados y los guarda en la base de datos
            @ViewData["Administrador"] = adminid;
            var listaTrabajadores = _context.Trabajador.ToList();
            string cedula = Request.Form["cedula"].ToString();
            string eps = Request.Form["eps"].ToString();
            string arl = Request.Form["arl"].ToString();
            string pension = Request.Form["pension"].ToString();
            string seguridad = Request.Form["seguridad"].ToString();
            string planilla = Request.Form["planilla"].ToString();
            string cml = Request.Form["cml"].ToString();
            string ingreso = Request.Form["ingreso"].ToString();
            string fecha = Request.Form["fecha"].ToString();
            string fechaFin = Request.Form["fechaFin"].ToString();
            string observacion = Request.Form["observacion"].ToString();

            var trabajador = _context.Trabajador.Find(idtrabajador);
            string validacion;
            if (cedula == "valida1" || cedula == "valida2" || cedula == "novalida1" || cedula == "novalida2")
            {
                validacion = ((cedula == "valida1" || cedula == "valida2") && (cedula != "novalida1" || cedula != "novalida2")) ? "Valida" : "No valida";
                trabajador.CedulaValida = validacion;
            }
            if (eps == "valida1" || eps == "valida2" || eps == "novalida1" || eps == "novalida2")
            {
                validacion = ((eps == "valida1" || eps == "valida2") && (eps != "novalida1" || eps != "novalida2")) ? "Valida" : "No valida";
                trabajador.EPSValida = validacion;
            }
            if (arl == "valida1" || arl == "valida2" || arl == "novalida1" || arl == "novalida2")
            {
                validacion = ((arl == "valida1" || arl == "valida2") && (arl != "novalida1" || arl != "novalida2")) ? "Valida" : "No valida";
                trabajador.ARLValida = validacion;
            }
            if (pension == "valida1" || pension == "valida2" || pension == "novalida1" || pension == "novalida2")
            {
                validacion = ((pension == "valida1" || pension == "valida2") && (pension != "novalida1" || pension != "novalida2")) ? "Valida" : "No valida";
                trabajador.PensionValida = validacion;
            }
            if (seguridad == "valida1" || seguridad == "valida2" || seguridad == "novalida1" || seguridad == "novalida2")
            {
                validacion = ((seguridad == "valida1" || seguridad == "valida2") && (seguridad != "novalida1" || seguridad != "novalida2")) ? "Valida" : "No valida";
                trabajador.SeguridadSocialValida = validacion;
            }
            if (planilla == "valida1" || planilla == "valida2" || planilla == "novalida1" || planilla == "novalida2")
            {
                validacion = ((planilla == "valida1" || planilla == "valida2") && (planilla != "novalida1" || planilla != "novalida2")) ? "Valida" : "No valida";
                trabajador.PlanillaValida = validacion;
            }
            if (cml == "valida1" || cml == "valida2" || cml == "novalida1" || cml == "novalida2")
            {
                validacion = ((cml == "valida1" || cml == "valida2") && (cml != "novalida1" || cml != "novalida2")) ? "Valida" : "No valida";
                trabajador.CertificadoMedicoLaboralValido = validacion;
            }

            trabajador.EstadoIngreso = ingreso;
            trabajador.FechaIngresoObra = fecha;
            trabajador.Observacion = observacion;
            if (fechaFin != null)
            {
                trabajador.FechaFinObra = fechaFin;
                HistorialFechasPlanilla registro = new HistorialFechasPlanilla();
                registro.Fecha = fechaFin;
                registro.IdTrabajador = trabajador.Id;
                _context.HistorialFechasPlanilla.Add(registro);
            }
            _context.Trabajador.Update(trabajador);
            _context.SaveChanges();
            return RedirectToAction("ValidarDatos", "Admin", new { adminid = adminid, idtrabajador = idtrabajador });
        }

        /*---------------------------------Solicitud agregar obra---------------------------------------------*/

        public IActionResult SolicitudObraTrabajador(int adminid, int? idtrabajador, int? idcontratista, int? filtro)
        {
            //Vista de solicitudes para agregar obra en Trabajador

            List<SolicitudObra> solicitudesObra = new List<SolicitudObra>();
            if (idcontratista != null)
            {
                //Este bloque de código verifica agrega los registros que corresponden al contratista buscado
                var trab = _context.Trabajador.Where(s => s.IdContratista == idcontratista).ToList();
                foreach (var t in trab)
                {
                    var tmp = _context.SolicitudObra.Where(s => s.IdTrabajador == t.Id).ToList();
                    if (tmp.Count > 0)
                    {
                        foreach (var s in tmp)
                        {
                            solicitudesObra.Add(s);
                        }
                    }
                }
            }
            else if (idtrabajador != null)
            {
                solicitudesObra = _context.SolicitudObra.Where(s => s.IdTrabajador == idtrabajador).ToList();
            }
            else if (filtro != null)
            {
                //Filtros de los registros segun su estado
                if (filtro == 0)
                {
                    solicitudesObra = _context.SolicitudObra.Where(s => s.Estado == "Aprobada").ToList();
                }
                else if(filtro == 1)
                {
                    solicitudesObra = _context.SolicitudObra.Where(s => s.Estado == "Rechazada").ToList();
                }
                else
                {
                    solicitudesObra = _context.SolicitudObra.Where(s => s.Estado == "Pendiente").ToList();
                }
            }
            else
            {
                solicitudesObra = _context.SolicitudObra.ToList();
            }
            
            List<List<string>> listaSolicitudes = new List<List<string>>();

            foreach (var s in solicitudesObra)
            {
                //Se crea la lista de registros de solicitudes para mostrarlas
                List<string> solicitud = new List<string>();
                Trabajador trabajador = _context.Trabajador.Find(s.IdTrabajador);
                Obra obraNueva = _context.Obra.Find(s.ObrId);
                var trabajadorXObra = _context.TrabajadorXObra.Where(s => s.IdTrabajador == trabajador.Id).ToList();
                string obrasActual = "";
                for (int i = 0; i < trabajadorXObra.Count; i++)
                {
                    //Se obtienen todas las obras ya asociadas a cada trabajador
                    Obra obra = _context.Obra.Find(trabajadorXObra[i].ObrId);
                    obrasActual = (i+1 == trabajadorXObra.Count) ? obrasActual + obra.obrNombre : obrasActual + obra.obrNombre + ", ";
                }
                Contratista contratista = _context.Contratista.Find(trabajador.IdContratista);
                Subcontratista subcontratista = (trabajador.IdSubcontratista != null) ? _context.Subcontratista.Find(trabajador.IdSubcontratista) : new Subcontratista();
                if (subcontratista.Razon_social == null)
                {
                    subcontratista.Razon_social = "No tiene";
                }
                solicitud.Add(s.Id.ToString());
                solicitud.Add(trabajador.Cedula);
                solicitud.Add(trabajador.Nombre + " " + trabajador.Apellido);
                solicitud.Add(obrasActual);
                solicitud.Add(obraNueva.obrNombre);
                solicitud.Add(contratista.Razon_social);
                solicitud.Add(subcontratista.Razon_social);
                solicitud.Add(trabajador.EstadoIngreso);
                solicitud.Add(s.Estado);
                listaSolicitudes.Add(solicitud);

            }
            listaSolicitudes.Reverse();
            @ViewBag.listaSolicitudes = listaSolicitudes;
            List<Trabajador> listaTrabajadores = _context.Trabajador.ToList();
            List<Contratista> listaContratistas = _context.Contratista.ToList();
            List<string> nomTrabajadores = new List<string>();
            foreach (var t in listaTrabajadores)
            {
                string nombreCompleto = t.Cedula + " " + t.Nombre + " " + t.Apellido;
                nomTrabajadores.Add(nombreCompleto);
            }
            nomTrabajadores.Reverse();
            listaContratistas.Reverse();
            @ViewBag.Trabajadores = nomTrabajadores;
            @ViewBag.Contratistas = listaContratistas;
            @ViewData["Administrador"] = adminid;
            return View();
        }

        public IActionResult AprobarObra(int adminid, int idsolicitud)
        {
            SolicitudObra solicitudObra = _context.SolicitudObra.Find(idsolicitud);
            //
            var TXO = _context.TrabajadorXObra.Where(s => s.IdSolicitudObra == solicitudObra.Id).ToList();
            if (TXO.Count == 0)
            {
                TrabajadorXObra trabajadorXObra = new TrabajadorXObra();
                trabajadorXObra.IdSolicitudObra = solicitudObra.Id;
                trabajadorXObra.ObrId = solicitudObra.ObrId;
                trabajadorXObra.IdTrabajador = solicitudObra.IdTrabajador;
                solicitudObra.Estado = "Aprobada";
                _context.TrabajadorXObra.Add(trabajadorXObra);
            }
            _context.SolicitudObra.Update(solicitudObra);
            _context.SaveChanges();
            TempData["mensaje"] = "Se aprobó la solicitud con éxito";
            return RedirectToAction("SolicitudObraTrabajador", "Admin", new { adminid = adminid });
        }

        public IActionResult RechazarObra(int adminid, int idsolicitud)
        {
            SolicitudObra solicitudObra = _context.SolicitudObra.Find(idsolicitud);
            solicitudObra.Estado = "Rechazada";
            var trabajadorXObra = _context.TrabajadorXObra.Where(s => s.IdSolicitudObra == solicitudObra.Id).ToList();
            if (trabajadorXObra.Count != 0)
            {
                _context.TrabajadorXObra.Remove(trabajadorXObra[0]);
            }
            _context.SolicitudObra.Update(solicitudObra);
            _context.SaveChanges();
            TempData["mensaje"] = "Se rechazó la solicitud con éxito";
            return RedirectToAction("SolicitudObraTrabajador", "Admin", new { adminid = adminid });
        }

        public IActionResult BuscarTrabajadorObra(int adminid)
        {
            string cedula = Request.Form["ced"].ToString();
            string nombre = Request.Form["nombre"].ToString();
            var trabajadorn = _context.Trabajador.Where(s => s.Cedula == cedula).ToList();
            Trabajador trabajador = new Trabajador();
            if (trabajadorn.Count > 0)
            {
                trabajador = trabajadorn[0];
            }
            else
            {
                var listaTrabajadores = _context.Trabajador.Where(s => s.Nombre.Contains(nombre) || s.Apellido.Contains(nombre) || s.Cedula.Contains(nombre)).ToList();
                if (listaTrabajadores.Count > 1)
                {
                    trabajador = listaTrabajadores[0];
                }
                else if (listaTrabajadores.Count == 0)
                {
                    TempData["mensaje"] = "No se encontraron coincidencias";
                    return RedirectToAction("SolicitudObraTrabajador", "Admin", new { adminid = adminid });
                }
            }
            
            
            return RedirectToAction("SolicitudObraTrabajador", "Admin", new { adminid = adminid , idtrabajador = trabajador.Id });
        }

        public IActionResult FiltrarEstadoSolicitud(int adminid, int filtro)
        {
            @ViewData["Administrador"] = adminid;
            return RedirectToAction("SolicitudObraTrabajador", "Admin", new { adminid = adminid, filtro = filtro });
        }


        public IActionResult BuscarContratista(int adminid, int modulo)
        {
            //Busca los registros por contratista (Esta función se utiliza en SolicitudContratistas, SolicitudObraTrabajador y ValidarDatos)
            @ViewData["Administrador"] = adminid;
            List<SolicitudRegistro> solcontratista;
            List<Contratista> contratista;
            string redir = "";
            if (modulo == 0)
            {
                redir = "SolicitudContratistas";
            }
            else if (modulo == 1)
            {
                redir = "ValidarDatos";
            }
            else if (modulo == 2)
            {
                redir = "SolicitudObraTrabajador";
            }

            string buscar = Request.Form["contratista"].ToString();
            if (!String.IsNullOrEmpty(buscar))
            {
                if (modulo == 0)
                {
                    var existe = _context.SolicitudRegistro.Where(s => s.Razon_social.Contains(buscar)).ToList();
                    if (existe.Count > 0)
                    {
                        solcontratista = _context.SolicitudRegistro.Where(s => s.Razon_social.Contains(buscar)).ToList();
                        return RedirectToAction(redir, "Admin", new { adminid = adminid, idcontratista = solcontratista[0].Id });
                    }
                    else
                    {
                        TempData["mensaje"] = "No se encontraron coincidencias";
                    }
                }
                else
                {
                    var existe = _context.Contratista.Where(s => s.Razon_social.Contains(buscar)).ToList();
                    if (existe.Count > 0)
                    {
                        contratista = _context.Contratista.Where(s => s.Razon_social.Contains(buscar)).ToList();
                        return RedirectToAction(redir, "Admin", new { adminid = adminid, idcontratista = contratista[0].Id });
                    }
                    else
                    {
                        TempData["mensaje"] = "No se encontraron coincidencias";
                    }
                }
            }

            return RedirectToAction(redir, "Admin", new { adminid = adminid });
        }
    }
}
