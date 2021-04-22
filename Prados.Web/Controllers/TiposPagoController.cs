using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prados.Web.Data;
using Prados.Web.Data.Entities;
using Vereyon.Web;

namespace Prados.Web.Controllers
{
    public class TiposPagoController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public TiposPagoController(DataContext context, IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }

        // GET: TiposPago
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposPagotbl.ToListAsync());
        }

        // GET: TiposPago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposPagotbl = await _context.TiposPagotbl
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tiposPagotbl == null)
            {
                return NotFound();
            }

            return View(tiposPagotbl);
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
        public async Task<IActionResult> Create([Bind("Id,Tip_Descripcion")] TiposPagotbl tiposPagotbl)
        {
            if (ModelState.IsValid)
            {
                try { 
                _context.Add(tiposPagotbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "El tipo de pago ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

            }
            return View(tiposPagotbl);
        }

        // GET: TiposPago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposPagotbl = await _context.TiposPagotbl.FindAsync(id);
            if (tiposPagotbl == null)
            {
                return NotFound();
            }
            return View(tiposPagotbl);
        }

        // POST: TiposPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tip_Descripcion")] TiposPagotbl tiposPagotbl)
        {
            if (id != tiposPagotbl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposPagotbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposPagotblExists(tiposPagotbl.Id))
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
            return View(tiposPagotbl);
        }

        // GET: TiposPago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipop = await _context.TiposPagotbl
                .Include(p => p.Pagos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tipop == null)
            {
                return NotFound();
            }

            if (tipop.Pagos.Count > 0)
            {
                _flashMessage.Danger("El tipo de pago no se puede borrar por que tiene pagos registrados");
                // ModelState.AddModelError(string.Empty, "La persona no se puede borrar por que tiene pagos registrados");
                return RedirectToAction(nameof(Index));
            }
            //anio.a = 'I';
            _context.TiposPagotbl.Remove(tipop);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El tipo de pago fue borrado");
            return RedirectToAction(nameof(Index));
        }

        // POST: TiposPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposPagotbl = await _context.TiposPagotbl.FindAsync(id);
            _context.TiposPagotbl.Remove(tiposPagotbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposPagotblExists(int id)
        {
            return _context.TiposPagotbl.Any(e => e.Id == id);
        }
    }
}
