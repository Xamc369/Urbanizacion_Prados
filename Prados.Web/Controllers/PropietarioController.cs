﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prados.Web.Data;
using Prados.Web.Data.Entities;
using Prados.Web.Helpers;
using Prados.Web.Models;
using Vereyon.Web;

namespace Prados.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PropietarioController : Controller
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;

        public PropietarioController(DataContext context,
             IUserHelper userHelper,
             ICombosHelper combosHelper,
             IConverterHelper converterHelper,
             IImageHelper imageHelper,
             IFlashMessage flashMessage,
             IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _flashMessage = flashMessage;
            _mailHelper = mailHelper;
        }


        // GET: Propietarios
        public IActionResult Index()
        {
            return View(_context.Propietariostbls
                .Include(o => o.User)
                .ThenInclude(o => o.TipIde)
                .Include(o => o.User)
                .ThenInclude(o => o.TipPer)
                .Include(o => o.User)
                .ThenInclude(o => o.TipViv)
                .Include(o => o.Vehiculos)
                .Include(o => o.Pagos)
                .Include(o => o.Negocio));
        }

        // GET: Propietarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietariostbls
                .Include(o => o.User)
                 .Include(o => o.Vehiculos)
                 .Include(o => o.Negocio)
                 .Include(o => o.Pagos)
                 .ThenInclude(p => p.Anio)
                 .Include(o => o.Pagos)
                 .ThenInclude(p => p.Mes)
                 .Include(o => o.Pagos)
                 .ThenInclude(p => p.Val)
                 .Include(o => o.Pagos)
                 .ThenInclude(p => p.PuntodePago)
                 .Include(o => o.Pagos)
                 .ThenInclude(p => p.Tipos)
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (propietario == null)
            {
                return NotFound();
            }

            return View(propietario);
        }

        #region USUARIOS

        // GET: Propietarios/Create
        public IActionResult Create()
        {
            var model = new AddUserViewModel
            {
                Pro_Estado = 'A',
                TipoViviendaVM = _combosHelper.GetComboTipoVivienda(),
                TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion(),
                TipoPersonaVM = _combosHelper.GetComboTipoPersona()
            };
            ViewBag.showSuccessAlert = false;
            return View(model);
        }

        // POST: Propietarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                Userstbl user = new Userstbl
                {
                    Pro_Estado = 'A',
                    Pro_Lote = model.PRO_LOTE,
                    Pro_Nombres = model.PRO_NOMBRES,
                    Pro_Apellidos = model.PRO_APELLIDOS,
                    TipViv = await _context.TiposViviendatbls.FindAsync(model.TVId),
                    TipPer = await _context.TipoPersonastbls.FindAsync(model.TPId),
                    Pro_Observaciones = model.PRO_OBSERVACIONES,
                    Pro_Telefono = model.PRO_TELEFONO,
                    TipIde = await _context.TipoIdentificaciontbls.FindAsync(model.TIId),
                    Pro_Identificacion = model.PRO_IDENTIFICACION,
                    Email = model.Username,
                    UserName = model.Username
                };

                Microsoft.AspNetCore.Identity.IdentityResult response = await _userHelper.AddUserAsync(user, model.Password);
                if (response.Succeeded)
                {
                    Userstbl userInDB = await _userHelper.GetUserByEmailAsync(model.Username);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                    var propietario = new Propietariostbl
                    {
                        Vehiculos = new List<Vehiculostbl>(),
                        Negocio = new List<Negociostbl>(),
                        Pagos = new List<Pagostbl>(),
                        User = userInDB,
                    };

                    _context.Propietariostbls.Add(propietario);

                    try
                    {
                        await _context.SaveChangesAsync();
                        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                        var tokenLink = Url.Action("ConfirmEmail", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        //_mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                        //    $"To allow the user, " +
                        //    $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                        //ViewBag.Message = "The instructions to allow your user has been sent to email.";

                        #region
                        //MAIL CON FORMATO
                        _mailHelper.SendMail(model.Username, "Email confirmation",
                            $"<table style='max-width:600px; padding:10px; margin:0 auto; border-collapse:collapse;'>"+
                            $"<tr>"+
                            $"<td style='background-color:#34495e; text-align:center; padding:0'>" +
                            $"<a href='#'>"+
                            $"<img width='20%' style='display:block; margin:1.5% 3%' src='https://veterinarianuske.com/wp-content/uploads/2016/10/line_separator.png'>"+
                            $"</a>"+
                            $"</td>"+
                            $"</tr>"+
                            $"<tr>"+
                            $"<td style='padding:0'>"+
                            $"<img style='padding:0; display:block' src='' width='100%' >" +
                            $"</td>" +
                            $"</tr>" +
                            $"<tr>" +
                            $"<td style='background-color:#ecf0f1>" +
                            $"<div style='color:#34495e; margin:4% 10% 2%; text-align:justify; font-family:sans-serif'>" +
                            $"<h1 style='color:#e67e22; margin:0 0 7px'>Saludos,</h1>" +
                            $"<p style='margin: 2px; font-size:15px'>" +
                            $" La Urbanizacion Prados del Condado te da la Bienvenida<br>" +
                            $"<div style='width:100%; text-align:center'>"+
                            $"<h2 style='color:#e67e22; margin:0 0 7px'>Confirmar Email</h2>" +
                            $"Para activar su cuenta. Haga click en el siguiente link:</br></br>"+
                            $"<a style='text-decoration:none; border-radius:5px; padding:11px 23px; color:white; background-color:#3498db' href=\"{tokenLink}\">Confirmar Email</a>" +
                            $"</div>"+
                            $"</td>"+
                            $"</tr>"+
                            $"</table>");
                        #endregion


                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError(string.Empty, ex.ToString());
                        return View(model);
                    }
                }
                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
            }

            model.TipoViviendaVM = _combosHelper.GetComboTipoVivienda();
            model.TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion();
            model.TipoPersonaVM = _combosHelper.GetComboTipoPersona();
            ViewBag.showSuccessAlert = true;
            return View(model);
        }

        // GET: Propietarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietariostbls
                .Include(o => o.User)
                .Include(o => o.User.TipIde)
                .Include(o => o.User.TipPer)
                .Include(o => o.User.TipViv)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (propietario == null)
            {
                return NotFound();
            }


            var model = new EditUserViewModel
            {
                Pro_Lote = propietario.User.Pro_Lote,
                Pro_Nombres = propietario.User.Pro_Nombres,
                Pro_Apellidos = propietario.User.Pro_Apellidos,
                Pro_Observaciones = propietario.User.Pro_Observaciones,
                Pro_Telefono = propietario.User.Pro_Telefono,
                Pro_Identificacion = propietario.User.Pro_Identificacion,
                TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion(),
                TipoViviendaVM = _combosHelper.GetComboTipoIdentificacion(),
                TipoPersonaVM = _combosHelper.GetComboTipoPersona()
            };

            model.TipoViviendaVM = model.TipoIdentificacionVM;

            return View(model);
        }

        // POST: Propietarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owner = await _context.Propietariostbls
                    .Include(o => o.User)
                    .Include(o => o.User.TipIde)
                    .Include(o => o.User.TipPer)
                    .Include(o => o.User.TipViv)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                owner.User.Pro_Lote = model.Pro_Lote;
                owner.User.Pro_Nombres = model.Pro_Nombres;
                owner.User.Pro_Apellidos = model.Pro_Apellidos;
                owner.User.Pro_Observaciones = model.Pro_Observaciones;
                owner.User.Pro_Telefono = model.Pro_Telefono;
                owner.User.Pro_Identificacion = model.Pro_Identificacion;
                model.TipoViviendaVM = _combosHelper.GetComboTipoVivienda();
                model.TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion();
                model.TipoPersonaVM = _combosHelper.GetComboTipoPersona();

                await _userHelper.UpdateUserAsync(owner.User);
                return RedirectToAction(nameof(Index));
            }

            model.TipoViviendaVM = _combosHelper.GetComboTipoVivienda();
            model.TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion();
            model.TipoPersonaVM = _combosHelper.GetComboTipoPersona();
            return View(model);

        }

        // GET: Propietarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietariostbls
                .Include(p => p.User)
                .Include(p => p.Vehiculos)
                .Include(p => p.Negocio)
                .Include(p => p.Pagos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propietario == null)
            {
                return NotFound();
            }
            if (propietario.Vehiculos.Count > 0)
            {
                _flashMessage.Danger("La persona no se puede borrar por que tiene vehiculos registrados");
                //ModelState.AddModelError(string.Empty, "");
                return RedirectToAction(nameof(Index));
            }

            if (propietario.Negocio.Count > 0)
            {
                _flashMessage.Danger("La persona no se puede borrar por que tiene negocios registrados");
               // ModelState.AddModelError(string.Empty, "La persona no se puede borrar por que tiene negocios registrados");
                return RedirectToAction(nameof(Index));
            }

            if (propietario.Pagos.Count > 0)
            {
                _flashMessage.Danger("La persona no se puede borrar por que tiene pagos registrados");
               // ModelState.AddModelError(string.Empty, "La persona no se puede borrar por que tiene pagos registrados");
                return RedirectToAction(nameof(Index));
            }

            await _userHelper.DeleteUserAsync(propietario.User.Email);
            _context.Propietariostbls.Remove(propietario);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("La persona fue borrada");
            return RedirectToAction(nameof(Index));
        }

        // POST: Propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propietario = await _context.Propietariostbls.FindAsync(id);
            _context.Propietariostbls.Remove(propietario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropietarioExists(int id)
        {
            return _context.Propietariostbls.Any(e => e.Id == id);
        }

        #endregion

        #region VEHICULO

        public async Task<IActionResult> AddVehiculo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietariostbls.FindAsync(id.Value);

            if (propietario == null)
            {
                return NotFound();
            }

            var model = new VehiculoViewModel
            {
                Veh_Born = DateTime.Today,
                Veh_Estado = 'A',
                PropietarioId = propietario.Id,

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehiculo(VehiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);

                }

                
                try
                {
                    var vehiculo = await _converterHelper.ToVehiculoAsync(model, path, true);
                    _context.Vehiculostbls.Add(vehiculo);
                    _flashMessage.Confirmation("El vehiculo fue creado");
                    await _context.SaveChangesAsync();
                   return RedirectToAction($"Details/{model.PropietarioId}");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe ese numero de tag");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

            }

            return View(model);
        }

        public async Task<IActionResult> EditVehiculo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculostbls
                .Include(v => v.Propietario)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToVehiculoViewModel(vehiculo));
        }

        [HttpPost]
        public async Task<IActionResult> EditVehiculo(VehiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);

                }

                var vehiculo = await _converterHelper.ToVehiculoAsync(model, path,false);
                _context.Vehiculostbls.Update(vehiculo);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El vehiculo fue modificado");
                return RedirectToAction($"Details/{model.PropietarioId}");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteVehiculo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculostbls
                .Include(h => h.Propietario)
                .FirstOrDefaultAsync(h => h.Id == id.Value);
            if (vehiculo == null)
            {
                return NotFound();
            }

            vehiculo.Veh_Estado = 'I';
            //_context.Vehiculostbls.Remove(vehiculo);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El vehiculo fue borrado");
            return RedirectToAction($"{nameof(Details)}/{vehiculo.Propietario.Id}");
        }

        #endregion

        #region PAGOS

        public async Task<IActionResult> AddPago(int? id)


        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietariostbls.FindAsync(id.Value);

            if (propietario == null)
            {
                return NotFound();
            }

            var model = new PagoViewModel
            {
                PropietarioId = propietario.Id,
                PAG_FECHACREACION = DateTime.Today,
                PAG_ESTADO = 'A',
                Anios1 = _combosHelper.GetComboAnios(),
                Meses1 = _combosHelper.GetComboMeses(),
                Valores = _combosHelper.GetComboValores(),
                TiposPago =_combosHelper.GetComboValoresDescripcion(),
                Puntos = _combosHelper.GetComboPuntos(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPago(PagoViewModel model, char pag, PagoDeleteViewModel model1)
        {
            if (ModelState.IsValid)
            {
                var pago = await _converterHelper.ToPagoAsync(model, true, pag);
                var pagod = await _converterHelper.ToPagoDeleteAsync(model1, true, pag);
                var mo = model.AnioId;
                var mo1 = model.MesId;
                

                //if (ValueExists(txtAdd.Text.Trim()))
                if (AnioExists(mo) && MesExists(mo1))
                {
                    return RedirectToAction($"Index");
                }

                _context.Pagostbls.Add(pago);
                _context.PagosDeletetbls.Add(pagod);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"Details/{model.PropietarioId}");
                //if ((_context.Pagostbls.Any(x => x.Anio.Id == mo)) && (_context.Pagostbls.Any(y => y.Mes.Id == mo1)))
                //{
                //    ModelState.AddModelError(string.Empty, "This employee already exists.");
                //    return RedirectToAction($"Index");
                //}


            }

            model.Anios1 = _combosHelper.GetComboAnios();
            model.Meses1 = _combosHelper.GetComboMeses();
            model.Valores = _combosHelper.GetComboValores();
            model.TiposPago = _combosHelper.GetComboValoresDescripcion();
            model.Puntos = _combosHelper.GetComboPuntos();
            return View(model);
        }

        private bool AnioExists(int _Value)
        {
            return _context.Pagostbls.Any(x => x.Anio.Id == _Value); //Reducir el ámbito del contexto
        }

        private bool MesExists(int _Value1)
        {
            return _context.Pagostbls.Any(x => x.Mes.Id == _Value1); //Reducir el ámbito del contexto
        }


        public async Task<IActionResult> EditPago(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagostbls
               .Include(o => o.Propietario)
               .Include(p => p.Anio)
               .Include(p => p.Mes)
               .Include(p => p.Val)
               .Include(p => p.Tipos)
               .Include(p => p.PuntodePago)
               .FirstOrDefaultAsync(p => p.Id == id);

            if (pago == null)
            {
                return NotFound();
            }

            return View(_converterHelper.ToPagoViewModel(pago));
        }

        [HttpPost]
        public async Task<IActionResult> EditPago(PagoViewModel model, char pag)
        {
            if (ModelState.IsValid)
            {
                var pago = await _converterHelper.ToPagoAsync(model, false, pag);

                var mo = model.AnioId;
                var mo1 = model.MesId;

                    if ((_context.Pagostbls.Any(x => x.Anio.Id == mo)) && (_context.Pagostbls.Any(y => y.Mes.Id == mo1)) && (_context.Pagostbls.Any(z => z.PAG_ESTADO == 'A')))
                    {
                        return RedirectToAction($"Index");
                    }
                


                    _context.Pagostbls.Update(pago);
                    await _context.SaveChangesAsync();
                    var IdTipoDelRegistroActualizadoEnPagos = pago.Tipos.Id;
                    return RedirectToAction($"Details/{model.PropietarioId}");
            }

            model.Anios1 = _combosHelper.GetComboAnios();
            model.Meses1 = _combosHelper.GetComboMeses();
            model.Valores = _combosHelper.GetComboValores();
            model.TiposPago = _combosHelper.GetComboValoresDescripcion();
            model.Puntos = _combosHelper.GetComboPuntos();
            return View(model);
        }

        
        public async Task<IActionResult> DeletePagos(int? id, PagoViewModel model, char pag)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagostbls
                .Include(h => h.Propietario)
                .Include(p => p.Anio)
                .Include(p => p.Mes)
                .Include(p => p.Val)
                .Include(p => p.Tipos)
                .Include(p => p.PuntodePago)
                .FirstOrDefaultAsync(h => h.Id == id.Value);

            var pagod = await _context.PagosDeletetbls
               .Include(h => h.Propietario)
               .Include(p => p.Anio)
               .Include(p => p.Mes)
               .Include(p => p.Val)
               .Include(p => p.Tipos)
               .Include(p => p.PuntodePago)
               .FirstOrDefaultAsync(h => h.Id == id.Value);

            if (pago == null)
            {
                return NotFound();
            }

            pagod.PAG_ESTADO = 'I';
            pago.PAG_ESTADO = 'I';
            _context.Pagostbls.Remove(pago);
            _flashMessage.Confirmation("El pago fue borrado");
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{pago.Propietario.Id}");
        }

        #endregion

        #region NEGOCIOS

        public async Task<IActionResult> AddNegocio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietariostbls.FindAsync(id.Value);

            if (propietario == null)
            {
                return NotFound();
            }

            var model = new NegocioViewModel
            {
                Neg_FechaCreacion = DateTime.Today,
                Neg_Estado = 'A',
                PropietarioId = propietario.Id,

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNegocio(NegocioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsyncNegocio(model.ImageFile);

                }

                var negocio = await _converterHelper.ToNegocioAsync(model, path, true);
                _context.Negociostbls.Add(negocio);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.PropietarioId}");

            }

            return View(model);
        }

        public async Task<IActionResult> EditNegocio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var negocio = await _context.Negociostbls
                .Include(n => n.Propietarios)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (negocio == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToNegocioViewModel(negocio));
        }

        [HttpPost]
        public async Task<IActionResult> EditNegocio(NegocioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsyncNegocio(model.ImageFile);

                }

                var negocio = await _converterHelper.ToNegocioAsync(model, path, false);
                _context.Negociostbls.Update(negocio);
                await _context.SaveChangesAsync();
                return RedirectToAction($"Details/{model.PropietarioId}");
            }

            return View(model);
        }

        public async Task<IActionResult> DetailsNegocio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var propietario = await _context.Negociostbls
                .Include(p => p.Producto)
                .Include(n => n.Propietarios)
                .ThenInclude(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (propietario == null)
            {
                return NotFound();
            }
           
            return View(propietario);
        }

        public async Task<IActionResult> AddProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var negocio = await _context.Negociostbls.FindAsync(id.Value);
            if (negocio == null)
            {
                return NotFound();
            }

            var model = new ProductoViewModel
            {
                
                NegocioId = negocio.Id,
                Pro_FechaCreacion = DateTime.Now,
                Pro_Estado = 'A'
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProducto(ProductoViewModel model)
        {

            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsyncProducto(model.ImageFile);

                }

                var producto = await _converterHelper.ToProductoAsync(model, true, path);
                _context.Productostbls.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction($"DetailsNegocio/{model.NegocioId}");

            }

            return View(model);

        }

        public async Task<IActionResult> EditProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productostbls
                .Include(n => n.Negocio)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (producto == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToProductoViewModel(producto));
        }

        [HttpPost]
        public async Task<IActionResult> EditProducto(ProductoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsyncProducto(model.ImageFile);

                }

                var producto = await _converterHelper.ToProductoAsync(model, false, path);
                _context.Productostbls.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction($"DetailsNegocio/{model.NegocioId}");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productostbls
                .Include(h => h.Negocio)
                .FirstOrDefaultAsync(h => h.Id == id.Value);
            if (producto == null)
            {
                return NotFound();
            }

            producto.Pro_Estado = 'I' ;
            //_context.Productostbls.Remove(producto);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El producto fue borrado");
            return RedirectToAction($"{nameof(DetailsNegocio)}/{producto.Negocio.Id}");
        }

        public async Task<IActionResult> DeleteNegocio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var negocio = await _context.Negociostbls
                .Include(h => h.Propietarios)
                .Include(n => n.Producto)
                .FirstOrDefaultAsync(h => h.Id == id.Value);
            if (negocio == null)
            {
                return NotFound();
            }
        
        
            if (negocio.Producto.Count > 0)
            {
                _flashMessage.Danger("El negocio no se puede borrar por que tiene productos registrados");
                //ModelState.AddModelError(string.Empty, "El negocio no se puede borrar por que tiene productos registrados");
                return RedirectToAction($"{nameof(Details)}/{negocio.Propietarios.Id}");
            }

            negocio.Neg_Estado = 'I';
            // _context.Negociostbls.Remove(negocio);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El negocio fue borrado");
            return RedirectToAction($"{nameof(Details)}/{negocio.Propietarios.Id}");
        }

        #endregion

    }
}
