using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prados.Web.Data;
using Prados.Web.Data.Entities;
using Prados.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace Prados.Web.Controllers
{
    public class TiposGastoController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public TiposGastoController(DataContext context, IConverterHelper converterHelper, IFlashMessage flashMessage)
        {
            _context = context;
            _converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }

        // GET: TiposPago
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposGastotbls.ToListAsync());
        }

        // GET: TiposPago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposGastotbl = await _context.TiposGastotbls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tiposGastotbl == null)
            {
                return NotFound();
            }

            return View(tiposGastotbl);
        }

        // GET: TiposPago/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tip_Descripcion")] TiposGastotbl tiposGastotbl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tiposGastotbl);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "El tipo de gasto ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

            }
            return View(tiposGastotbl);
        }

        // GET: TiposPago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposGastotbl = await _context.TiposGastotbls.FindAsync(id);
            if (tiposGastotbl == null)
            {
                return NotFound();
            }
            return View(tiposGastotbl);
        }

        // POST: TiposPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tip_Descripcion")] TiposGastotbl tiposGastotbl)
        {
            if (id != tiposGastotbl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposGastotbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposGastotblExists(tiposGastotbl.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tiposGastotbl);
        }

        // GET: TiposPago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.TiposGastotbls
                .Include(p => p.Egresos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gasto == null)
            {
                return NotFound();
            }

            if (gasto.Egresos.Count > 0)
            {
                _flashMessage.Danger("El tipo de gasto no se puede borrar por que tiene egresos registrados");
                // ModelState.AddModelError(string.Empty, "La persona no se puede borrar por que tiene pagos registrados");
                return RedirectToAction(nameof(Index));
            }
            //anio.a = 'I';
            _context.TiposGastotbls.Remove(gasto);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El tipo de gasto fue borrado");
            return RedirectToAction(nameof(Index));
        }

        // POST: TiposPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposGastotbl = await _context.TiposGastotbls.FindAsync(id);
            _context.TiposGastotbls.Remove(tiposGastotbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposGastotblExists(int id)
        {
            return _context.TiposGastotbls.Any(e => e.Id == id);
        }
    }
}
