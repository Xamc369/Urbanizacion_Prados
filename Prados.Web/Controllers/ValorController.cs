﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ValorController : Controller
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;

        public ValorController(DataContext context, IConverterHelper converterHelper, IFlashMessage flashMessage)
        {
            _context = context;
            _converterHelper = converterHelper;
            _flashMessage = flashMessage;
        }

        // GET: Valor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Valorestbls.ToListAsync());
        }

        public async Task<IActionResult> IndVal()
        {
            return View(await _context.Valorestbls.ToListAsync());
        }


        // GET: Valor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valorestbl = await _context.Valorestbls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valorestbl == null)
            {
                return NotFound();
            }

            return View(valorestbl);
        }

        // GET: Valor/Create
        public IActionResult Create()
        {
            var model = new ValorViewModel
            {
                Val_FechaCreacion = DateTime.Today,
                Val_Estado = 'A',
            };
            return View(model);
        }

        // POST: Valor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ValorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var valor = await _converterHelper.ToValorAsync(model, true);
                    _context.Add(valor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "El valor ya se encuentra registrado");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

            }
            return View(model);
        }

        // GET: Valor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valorestbl = await _context.Valorestbls.FindAsync(id);
            if (valorestbl == null)
            {
                return NotFound();
            }
            return View(valorestbl);
        }

        // POST: Valor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Val_Valor,Val_FechaCreacion,Val_Estado")] Valorestbl valorestbl)
        {
            if (id != valorestbl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valorestbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValorestblExists(valorestbl.Id))
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
            return View(valorestbl);
        }

        // GET: Valor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var valor = await _context.Valorestbls
                .Include(p => p.Pagos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (valor == null)
            {
                return NotFound();
            }

            if (valor.Pagos.Count > 0)
            {
                _flashMessage.Danger("El valor no se puede borrar por que tiene pagos registrados");
                // ModelState.AddModelError(string.Empty, "La persona no se puede borrar por que tiene pagos registrados");
                return RedirectToAction(nameof(Index));
            }
            //anio.a = 'I';
            _context.Valorestbls.Remove(valor);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El valor fue borrado");
            return RedirectToAction(nameof(Index));
        }

        // POST: Valor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var valorestbl = await _context.Valorestbls.FindAsync(id);
            _context.Valorestbls.Remove(valorestbl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValorestblExists(int id)
        {
            return _context.Valorestbls.Any(e => e.Id == id);
        }
    }
}
