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
    public class OrdenDeCompraController : Controller
    {
        private readonly SistemaComprasContext _context;

        public OrdenDeCompraController(SistemaComprasContext context)
        {
            _context = context;
        }

        // GET: OrdenDeCompra
        public async Task<IActionResult> Index()
        {
            var sistemaComprasContext = _context.OrdenesDeCompra.Include(o => o.Articulo).Include(o => o.UnidadDeMedida);
            return View(await sistemaComprasContext.ToListAsync());
        }

        // GET: OrdenDeCompra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdenesDeCompra == null)
            {
                return NotFound();
            }

            var ordenDeCompra = await _context.OrdenesDeCompra
                .Include(o => o.Articulo)
                .Include(o => o.UnidadDeMedida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenDeCompra == null)
            {
                return NotFound();
            }

            return View(ordenDeCompra);
        }

        // GET: OrdenDeCompra/Create
        public IActionResult Create()
        {
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Descripcion");
            ViewData["UnidadDeMedidaId"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id");
            return View();
        }

        // POST: OrdenDeCompra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroOrden,FechaOrden,Estado,ArticuloId,UnidadDeMedidaId,Cantidad,CostoUnitario")] OrdenDeCompra ordenDeCompra)
        {
           // if (ModelState.IsValid)
           // {
                _context.Add(ordenDeCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           // }
           // ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Descripcion", ordenDeCompra.ArticuloId);
           // ViewData["UnidadDeMedidaId"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", ordenDeCompra.UnidadDeMedidaId);
           // return View(ordenDeCompra);
        }

        // GET: OrdenDeCompra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdenesDeCompra == null)
            {
                return NotFound();
            }

            var ordenDeCompra = await _context.OrdenesDeCompra.FindAsync(id);
            if (ordenDeCompra == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Descripcion", ordenDeCompra.ArticuloId);
            ViewData["UnidadDeMedidaId"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", ordenDeCompra.UnidadDeMedidaId);
            return View(ordenDeCompra);
        }

        // POST: OrdenDeCompra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroOrden,FechaOrden,Estado,ArticuloId,UnidadDeMedidaId,Cantidad,CostoUnitario")] OrdenDeCompra ordenDeCompra)
        {
            if (id != ordenDeCompra.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(ordenDeCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenDeCompraExists(ordenDeCompra.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Descripcion", ordenDeCompra.ArticuloId);
            //ViewData["UnidadDeMedidaId"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", ordenDeCompra.UnidadDeMedidaId);
            //return View(ordenDeCompra);
        }

        // GET: OrdenDeCompra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdenesDeCompra == null)
            {
                return NotFound();
            }

            var ordenDeCompra = await _context.OrdenesDeCompra
                .Include(o => o.Articulo)
                .Include(o => o.UnidadDeMedida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenDeCompra == null)
            {
                return NotFound();
            }

            return View(ordenDeCompra);
        }

        // POST: OrdenDeCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenesDeCompra == null)
            {
                return Problem("Entity set 'SistemaComprasContext.OrdenesDeCompra'  is null.");
            }
            var ordenDeCompra = await _context.OrdenesDeCompra.FindAsync(id);
            if (ordenDeCompra != null)
            {
                _context.OrdenesDeCompra.Remove(ordenDeCompra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenDeCompraExists(int id)
        {
          return (_context.OrdenesDeCompra?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
