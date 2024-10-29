using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaComprasMVC.Data;
using SistemaComprasMVC.Models;

namespace SistemaComprasMVC.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly SistemaComprasContext _context;

        public ArticuloController(SistemaComprasContext context)
        {
            _context = context;
        }

        // GET: Articulo
        public async Task<IActionResult> Index()
        {
            var sistemaComprasContext = _context.Articulos.Include(a => a.UnidadDeMedida);
            return View(await sistemaComprasContext.ToListAsync());
        }

        // GET: Articulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.UnidadDeMedida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // GET: Articulo/Create
        public IActionResult Create()
        {
            ViewData["UnidadDeMedidaId"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion");
            return View();
        }

        // POST: Articulo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Marca,UnidadDeMedidaId,Existencia,Estado")] Articulo articulo)
        {
            if (!ModelState.IsValid) //debug
            {
                // Aquí puedes registrar o inspeccionar el estado del modelo si no es válido
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Depurar errores de validación
                }
                ViewData["UnidadDeMedidaId"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion", articulo.UnidadDeMedidaId);
                return View(articulo);
            }

            _context.Add(articulo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Articulo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            ViewData["UnidadDeMedidaId"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion", articulo.UnidadDeMedidaId);
            return View(articulo);
        }

        // POST: Articulo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Marca,UnidadDeMedidaId,Existencia,Estado")] Articulo articulo)
        {
            if (id != articulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.Id))
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
            ViewData["UnidadDeMedidaId"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion", articulo.UnidadDeMedidaId);
            return View(articulo);
        }

        // GET: Articulo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articulos == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.UnidadDeMedida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // POST: Articulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articulos == null)
            {
                return Problem("Entity set 'SistemaComprasContext.Articulos' is null.");
            }
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
            return (_context.Articulos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
