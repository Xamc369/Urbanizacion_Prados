using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prados.Web.Data;
using Prados.Web.Helpers;
using Prados.Web.Models;
using Vereyon.Web;

namespace Prados.Web.Controllers
{
    public class EgresoController : Controller
    {
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IFlashMessage _flashMessage;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public EgresoController(DataContext context, IImageHelper imageHelper, IConverterHelper converterHelper, IFlashMessage flashMessage, ICombosHelper combosHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }

        // GET: Noticia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Egresostbls
                .Include(e => e.Anio)
                .Include(e => e.Mes)
                .Include(e => e.TiposG)
                .ToListAsync());
        }

        // GET: Noticia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egresostbl = await _context.Egresostbls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (egresostbl == null)
            {
                return NotFound();
            }

            return View(egresostbl);
        }

        // GET: Noticia/Create
        public async Task<IActionResult> Create()
        {
            var model = new EgresoViewModel
            {
                Egr_FechadeRegistro = DateTime.Today,
                Egr_Estado = 'A',
                Anios1 = _combosHelper.GetComboAnios(),
                Meses1 = _combosHelper.GetComboMeses(),
                Tipos1 = _combosHelper.GetComboTipoGasto()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EgresoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var egreso = await _converterHelper.ToEgresoAsync(model, true);
                _context.Egresostbls.Add(egreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            model.Anios1 = _combosHelper.GetComboAnios();
            model.Meses1 = _combosHelper.GetComboMeses();
            model.Tipos1 = _combosHelper.GetComboTipoGasto();
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egreso = await _context.Egresostbls
                .Include(e => e.Anio)
                .Include(e => e.Mes)
                .Include(e => e.TiposG)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (egreso == null)
            {
                return NotFound();
            }
            return View(_converterHelper.ToEgresoViewModel(egreso));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EgresoViewModel model)
        {
            if (ModelState.IsValid)
            {
                 var egreso = await _converterHelper .ToEgresoAsync(model, false);
                _context.Egresostbls.Update(egreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var egreso = await _context.Egresostbls
                .Include(e => e.Anio)
                .Include(e => e.Mes)
                .Include(e => e.TiposG)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (egreso == null)
            {
                return NotFound();
            }

            var dateval = DateTime.Now;
            var currentyear = dateval.Year;

            if (egreso.Anio.Ani_Descripcion != currentyear.ToString())
            {
                _flashMessage.Danger("El egreso no se puede borrar por que es anterior al año actual");
                // ModelState.AddModelError(string.Empty, "La persona no se puede borrar por que tiene pagos registrados");
                return RedirectToAction(nameof(Index));
            }

            egreso.Egr_Estado = 'I';
            _context.Egresostbls.Update(egreso);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El egreso fue borrado");
            return RedirectToAction(nameof(Index));
        }


        private bool EgresostblExists(int id)
        {
            return _context.Egresostbls.Any(e => e.Id == id);
        }
    }
}

