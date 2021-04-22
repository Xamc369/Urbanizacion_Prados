using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    public class ManagerController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IFlashMessage _flashMessage;

        public ManagerController(
            DataContext dataContext,
            IUserHelper userHelper,
            IMailHelper mailHelper,
            IConverterHelper converterHelper,
            ICombosHelper combosHelper,
            IFlashMessage flashMessage)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _flashMessage = flashMessage;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Managerstbls.Include(m => m.User));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managerstbls
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        public IActionResult Create()
        {
            var model = new AddUserViewModel
            {
                Pro_Estado = 'A',
                TipoViviendaVM = _combosHelper.GetComboTipoVivienda(),
                TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion(),
                TipoPersonaVM = _combosHelper.GetComboTipoPersona()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel view)
        {
            if (ModelState.IsValid)
            {
                var user = await AddUser(view);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");
                    return View(view);
                }

                var manager = new Managerstbl { User = user };

                _dataContext.Managerstbls.Add(manager);
                await _dataContext.SaveChangesAsync();

                var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                var tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(view.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                    $"To allow the user, " +
                    $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        private async Task<Userstbl> AddUser(AddUserViewModel model)
        {
            var user = new Userstbl
            {
                Pro_Estado = 'A',
                Pro_Lote = model.PRO_LOTE,
                Pro_Nombres = model.PRO_NOMBRES,
                Pro_Apellidos = model.PRO_APELLIDOS,
                TipViv = await _dataContext.TiposViviendatbls.FindAsync(model.TVId),
                TipPer = await _dataContext.TipoPersonastbls.FindAsync(model.TPId),
                Pro_Observaciones = model.PRO_OBSERVACIONES,
                Pro_Telefono = model.PRO_TELEFONO,
                TipIde = await _dataContext.TipoIdentificaciontbls.FindAsync(model.TIId),
                Pro_Identificacion = model.PRO_IDENTIFICACION,
                Email = model.Username,
                UserName = model.Username
            };

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            var newUser = await _userHelper.GetUserByEmailAsync(model.Username);
            await _userHelper.AddUserToRoleAsync(newUser, "Admin");
            return newUser;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _dataContext.Managerstbls
                .Include(o => o.User)
                .Include(o => o.User.TipIde)
                .Include(o => o.User.TipPer)
                .Include(o => o.User.TipViv)
                .FirstOrDefaultAsync(o => o.Id == id.Value);

            if (manager == null)
            {
                return NotFound();
            }

            var view = new EditUserViewModel
            {
                Pro_Lote = manager.User.Pro_Lote,
                Pro_Nombres = manager.User.Pro_Nombres,
                Pro_Apellidos = manager.User.Pro_Apellidos,
                Pro_Observaciones = manager.User.Pro_Observaciones,
                Pro_Telefono = manager.User.Pro_Telefono,
                Pro_Identificacion = manager.User.Pro_Identificacion,
                TipoViviendaVM = _combosHelper.GetComboTipoVivienda(),
                TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion(),
                TipoPersonaVM = _combosHelper.GetComboTipoPersona()
            };

            view.TipoViviendaVM = _combosHelper.GetComboTipoVivienda();
            view.TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion();
            view.TipoPersonaVM = _combosHelper.GetComboTipoPersona();

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel view)
        {
            if (ModelState.IsValid)
            {
                var owner = await _dataContext.Managerstbls
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == view.Id);

                owner.User.Pro_Lote = view.Pro_Lote;
                owner.User.Pro_Nombres = view.Pro_Nombres;
                owner.User.Pro_Apellidos = view.Pro_Apellidos;
                owner.User.Pro_Observaciones = view.Pro_Observaciones;
                owner.User.Pro_Telefono = view.Pro_Telefono;
                owner.User.Pro_Identificacion = view.Pro_Identificacion;
                view.TipoViviendaVM = _combosHelper.GetComboTipoVivienda();
                view.TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion();
                view.TipoPersonaVM = _combosHelper.GetComboTipoPersona();

                await _userHelper.UpdateUserAsync(owner.User);
                return RedirectToAction(nameof(Index));
            }

            view.TipoViviendaVM = _combosHelper.GetComboTipoVivienda();
            view.TipoIdentificacionVM = _combosHelper.GetComboTipoIdentificacion();
            view.TipoPersonaVM = _combosHelper.GetComboTipoPersona();

            return View(view);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _dataContext.Managerstbls
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propietario == null)
            {
                return NotFound();
            }
          
            await _userHelper.DeleteUserAsync(propietario.User.Email);
            _dataContext.Managerstbls.Remove(propietario);
            await _dataContext.SaveChangesAsync();
            _flashMessage.Confirmation("El administrador fue borrado");
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _dataContext.Managerstbls.Any(e => e.Id == id);
        }
    }

}
