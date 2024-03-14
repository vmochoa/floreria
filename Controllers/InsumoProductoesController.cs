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
    public class InsumoProductoesController : Controller
    {
        private readonly DBFloreriaContext _context;

        public InsumoProductoesController(DBFloreriaContext context)
        {
            _context = context;
        }

        // GET: InsumoProductoes
        public async Task<IActionResult> Index()
        {
            var dBFloreriaContext = _context.InsumoProductos.Include(i => i.Insumo).Include(i => i.ProductoFinal);
            return View(await dBFloreriaContext.ToListAsync());
        }

        // GET: InsumoProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos
                .Include(i => i.Insumo)
                .Include(i => i.ProductoFinal)
                .FirstOrDefaultAsync(m => m.InsumoProductoId == id);
            if (insumoProducto == null)
            {
                return NotFound();
            }

            return View(insumoProducto);
        }

        // GET: InsumoProductoes/Create
        public IActionResult Create()
        {
            ViewData["InsumoId"] = new SelectList(_context.Insumos, "InsumoId", "Nombre");
            ViewData["ProductoFinalId"] = new SelectList(_context.ProductosFinales, "ProductoFinalId", "Descripcion");
            return View();
        }

        // POST: InsumoProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsumoProductoId,CantidadNecesaria,InsumoId,ProductoFinalId")] InsumoProducto insumoProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insumoProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsumoId"] = new SelectList(_context.Insumos, "InsumoId", "InsumoId", insumoProducto.InsumoId);
            ViewData["ProductoFinalId"] = new SelectList(_context.ProductosFinales, "ProductoFinalId", "ProductoFinalId", insumoProducto.ProductoFinalId);
            return View(insumoProducto);
        }

        // GET: InsumoProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos.FindAsync(id);
            if (insumoProducto == null)
            {
                return NotFound();
            }
            ViewData["InsumoId"] = new SelectList(_context.Insumos, "InsumoId", "InsumoId", insumoProducto.InsumoId);
            ViewData["ProductoFinalId"] = new SelectList(_context.ProductosFinales, "ProductoFinalId", "ProductoFinalId", insumoProducto.ProductoFinalId);
            return View(insumoProducto);
        }

        // POST: InsumoProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsumoProductoId,CantidadNecesaria,InsumoId,ProductoFinalId")] InsumoProducto insumoProducto)
        {
            if (id != insumoProducto.InsumoProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insumoProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsumoProductoExists(insumoProducto.InsumoProductoId))
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
            ViewData["InsumoId"] = new SelectList(_context.Insumos, "InsumoId", "InsumoId", insumoProducto.InsumoId);
            ViewData["ProductoFinalId"] = new SelectList(_context.ProductosFinales, "ProductoFinalId", "ProductoFinalId", insumoProducto.ProductoFinalId);
            return View(insumoProducto);
        }

        // GET: InsumoProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos
                .Include(i => i.Insumo)
                .Include(i => i.ProductoFinal)
                .FirstOrDefaultAsync(m => m.InsumoProductoId == id);
            if (insumoProducto == null)
            {
                return NotFound();
            }

            return View(insumoProducto);
        }

        // POST: InsumoProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insumoProducto = await _context.InsumoProductos.FindAsync(id);
            if (insumoProducto != null)
            {
                _context.InsumoProductos.Remove(insumoProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsumoProductoExists(int id)
        {
            return _context.InsumoProductos.Any(e => e.InsumoProductoId == id);
        }
    }
}
