using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prados.Web.Data;
using Prados.Web.Data.Entities;
using Prados.Web.Helpers;
using Vereyon.Web;

namespace Prados.Web.Controllers
{
    public class AnioController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public AnioController(DataContext context,
             IConverterHelper converterHelper,
             IFlashMessage flashMessage)
        {
            _context = context;
            _converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }

        // GET: Anio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aniostbls.ToListAsync());
        }

        // GET: Anio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aniostbl = await _context.Aniostbls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aniostbl == null)
            {
                return NotFound();
            }

            return View(aniostbl);
        }

        // GET: Anio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Anio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ani_Descripcion,Ani_Estado")] Aniostbl aniostbl)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    _context.Add(aniostbl);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "El año ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

            }
            return View(aniostbl);
        }

        // GET: Anio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aniostbl = await _context.Aniostbls.FindAsync(id);
            if (aniostbl == null)
            {
                return NotFound();
            }
            return View(aniostbl);
        }

        // POST: Anio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ani_Descripcion")] Aniostbl aniostbl)
        {
            if (id != aniostbl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aniostbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AniostblExists(aniostbl.Id))
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
            return View(aniostbl);
        }

        // GET: Anio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anio = await _context.Aniostbls
                .Include(p => p.Pagos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (anio == null)
            {
                return NotFound();
            }

            if (anio.Pagos.Count > 0)
            {
                _flashMessage.Danger("El año no se puede borrar por que tiene pagos registrados");
                // ModelState.AddModelError(string.Empty, "La persona no se puede borrar por que tiene pagos registrados");
                return RedirectToAction(nameof(Index));
            }
            //anio.a = 'I';
            _context.Aniostbls.Remove(anio);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El año fue borrado");
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

        private bool AniostblExists(int id)
        {
            return _context.Aniostbls.Any(e => e.Id == id);
        }
    }
}
