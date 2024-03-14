using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FloreriaMvc.Models;

namespace FloreriaMvc.Controllers
{
    public class ProductosFinalesController : Controller
    {
        private readonly DBFloreriaContext _context;

        public ProductosFinalesController(DBFloreriaContext context)
        {
            _context = context;
        }

        // GET: ProductosFinales
        public async Task<IActionResult> Index()
        {
            var dBFloreriaContext = _context.ProductosFinales.Include(p => p.Categoria);
            return View(await dBFloreriaContext.ToListAsync());
        }

        // GET: ProductosFinales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosFinale = await _context.ProductosFinales
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoFinalId == id);
            if (productosFinale == null)
            {
                return NotFound();
            }

            return View(productosFinale);
        }

        // GET: ProductosFinales/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId");
            return View();
        }

        // POST: ProductosFinales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoFinalId,Descripcion,EsActivo,Costo,Factor,CategoriaId,PrecioVenta")] ProductosFinale productosFinale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productosFinale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", productosFinale.CategoriaId);
            return View(productosFinale);
        }

        // GET: ProductosFinales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosFinale = await _context.ProductosFinales.FindAsync(id);
            if (productosFinale == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", productosFinale.CategoriaId);
            return View(productosFinale);
        }

        // POST: ProductosFinales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoFinalId,Descripcion,EsActivo,Costo,Factor,CategoriaId,PrecioVenta")] ProductosFinale productosFinale)
        {
            if (id != productosFinale.ProductoFinalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productosFinale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosFinaleExists(productosFinale.ProductoFinalId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", productosFinale.CategoriaId);
            return View(productosFinale);
        }

        // GET: ProductosFinales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productosFinale = await _context.ProductosFinales
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoFinalId == id);
            if (productosFinale == null)
            {
                return NotFound();
            }

            return View(productosFinale);
        }

        // POST: ProductosFinales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productosFinale = await _context.ProductosFinales.FindAsync(id);
            if (productosFinale != null)
            {
                _context.ProductosFinales.Remove(productosFinale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosFinaleExists(int id)
        {
            return _context.ProductosFinales.Any(e => e.ProductoFinalId == id);
        }
    }
}
