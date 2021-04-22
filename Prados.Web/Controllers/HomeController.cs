using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prados.Web.Data;
using Prados.Web.Data.Entities;
using Prados.Web.Models;
using Vereyon.Web;

namespace Prados.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;

        public HomeController(DataContext context,
                              IFlashMessage flashMessage)
        {
            _context = context;
            _flashMessage = flashMessage;
        }

        public IActionResult Index()
        {
            return View(_context.Noticiastbls);
        }

        public IActionResult Index1()
        {
            return View(_context.Noticiastbls);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }


        [Authorize(Roles = "Customer")]
        public IActionResult MisPagos()
        {
            return View(_context.Pagostbls
                .Include(p => p.Anio)
                .Include(p => p.Mes)
                .Include(p => p.Val)
                .Include(p => p.Tipos)
                .Include(p => p.PuntodePago)
                .Include(p => p.Propietario)
                .Where(p => p.Propietario.User.Email.ToLower().Equals(User.Identity.Name.ToLower())));
        }

        [Authorize(Roles = "Customer")]
        public IActionResult MisVehiculos()
        {
            return View(_context.Vehiculostbls
                .Where(p => p.Propietario.User.Email.ToLower().Equals(User.Identity.Name.ToLower())));
        }

        [Authorize(Roles = "Customer")]
        public IActionResult MisNegocios()
        {
            return View(_context.Negociostbls
                .Include(p => p.Producto)
                .Where(p => p.Propietarios.User.Email.ToLower().Equals(User.Identity.Name.ToLower())));
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MisProductos(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neg = await _context.Negociostbls
                .Include(o => o.Producto)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (neg == null)
            {
                return NotFound();
            }

            return View(neg);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateProducto(int? id)
        {
            var owner = await _context.Propietariostbls
                .FirstOrDefaultAsync(o => o.User.Email.ToLower().Equals(User.Identity.Name.ToLower()));
            if (owner == null)
            {
                return NotFound();
            }

            var negocio = await _context.Negociostbls.FindAsync(id.Value);
            if (negocio == null)
            {
                return NotFound();
            }


            var view = new ProductoViewModel
            {
                Id = owner.Id,
                NegocioId =  negocio.Id,
                Pro_FechaCreacion = DateTime.Today,
                Pro_Estado = 'A',
                ImageUrl = negocio.ImageUrl,

            };

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducto(ProductoViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Productos",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Productos/{file}";
                }

                var prod = new Productostbl
                {
                    Negocio = await _context.Negociostbls.FindAsync(view.NegocioId),
                    Pro_Nombre = view.Pro_Nombre,
                    Pro_Precio = view.Pro_Precio,
                    Pro_FechaCreacion = DateTime.Today,
                    Pro_Estado = 'A',
                    ImageUrl = path,

                };

                _context.Productostbls.Add(prod);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(MisProductos)}");
            }

            return View(view);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> EditProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prod = await _context.Productostbls
                .Include(p => p.Negocio)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (prod == null)
            {
                return NotFound();
            }

            var view = new ProductoViewModel
            {
                Id = prod.Id,
                ImageUrl = prod.ImageUrl,
                Pro_Nombre = prod.Pro_Nombre,
                Pro_Precio = prod.Pro_Precio,
                NegocioId = prod.Negocio.Id,
                Pro_FechaCreacion = DateTime.Now,
                Pro_Estado = 'A'
            };

            return View(view);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProducto(ProductoViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = view.ImageUrl;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Productos",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Productos/{file}";
                }

                var prod = new Productostbl
                {
                    Id = view.Id,
                    Negocio = await _context.Negociostbls.FindAsync(view.NegocioId),
                    Pro_Nombre = view.Pro_Nombre,
                    Pro_Precio = view.Pro_Precio,
                    Pro_FechaCreacion = DateTime.Today,
                    Pro_Estado = 'A',
                    ImageUrl = path,
                };

                _context.Productostbls.Update(prod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MisNegocios));
            }

            return View(view);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prod = await _context.Productostbls
                .Include(p => p.Negocio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prod == null)
            {
                return NotFound();
            }

            _context.Productostbls.Remove(prod);
            await _context.SaveChangesAsync();
            _flashMessage.Confirmation("El producto fue eliminado");
            return RedirectToAction($"{nameof(MisProductos)}/{prod.Negocio.Id}");
        }


    }
}
