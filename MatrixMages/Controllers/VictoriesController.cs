using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MatrixMages.Data;
using MatrixMages.Models;

namespace MatrixMages.Controllers
{
    public class VictoriesController : Controller
    {
        private readonly MatrixMagesdbContext _context;

        public VictoriesController(MatrixMagesdbContext context)
        {
            _context = context;
        }

        // GET: Victories
        public async Task<IActionResult> Index()
        {
            var matrixMagesdbContext = _context.Victories.Include(v => v.BattleMage).Include(v => v.SpaceMage);

            return View(await matrixMagesdbContext.ToListAsync());
        }

        // GET: Victories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var victory = await _context.Victories
                .Include(v => v.BattleMage)
                .Include(v => v.SpaceMage)
                .FirstOrDefaultAsync(m => m.SpaceMageId == id);
            if (victory == null)
            {
                return NotFound();
            }

            return View(victory);
        }

        // GET: Victories/Create
        public IActionResult Create()
        {
            ViewData["BattleMageId"] = new SelectList(_context.BattleMageStrategies, "Id", "StrategyName");
            ViewData["SpaceMageId"] = new SelectList(_context.SpaceMageStrategies, "Id", "StrategyName");
            return View();
        }

        // POST: Victories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpaceMageId,BattleMageId,Victory1")] Victory victory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(victory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BattleMageId"] = new SelectList(_context.BattleMageStrategies, "Id", "StrategyName", victory.BattleMageId);
            ViewData["SpaceMageId"] = new SelectList(_context.SpaceMageStrategies, "Id", "StrategyName", victory.SpaceMageId);
            return View(victory);
        }

        // GET: Victories/Edit/5
        public async Task<IActionResult> Edit(long? battlemageId, long? spacemageId)
        {
            if (battlemageId == null||spacemageId==null)
            {
                return NotFound();
            }

            var victory = await _context.Victories.FirstOrDefaultAsync(e =>
                e.BattleMageId == battlemageId && e.SpaceMageId == spacemageId);
            if (victory == null)
            {
                return NotFound();
            }
            ViewData["BattleMageId"] = new SelectList(_context.BattleMageStrategies, "Id", "StrategyName", victory.BattleMageId);
            ViewData["SpaceMageId"] = new SelectList(_context.SpaceMageStrategies, "Id", "StrategyName", victory.SpaceMageId);
            return View(victory);
        }

        // POST: Victories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? battlemageId, long? spacemageId, [Bind("SpaceMageId,BattleMageId,Victory1")] Victory victory)
        {
            if (battlemageId == null || spacemageId == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(victory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VictoryExists(victory.BattleMageId, victory.SpaceMageId))
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
            ViewData["BattleMageId"] = new SelectList(_context.BattleMageStrategies, "Id", "StrategyName", victory.BattleMageId);
            ViewData["SpaceMageId"] = new SelectList(_context.SpaceMageStrategies, "Id", "StrategyName", victory.SpaceMageId);
            return View(victory);
        }

        // GET: Victories/Delete/5
        public async Task<IActionResult> Delete(long? battlemageId, long? spacemageId)
        {
            if (battlemageId == null || spacemageId == null)
            {
                return NotFound();
            }

            var victory = await _context.Victories
                .Include(v => v.BattleMage)
                .Include(v => v.SpaceMage)
                .FirstOrDefaultAsync(e =>
            e.BattleMageId == battlemageId && e.SpaceMageId == spacemageId);
            if (victory == null)
            {
                return NotFound();
            }

            return View(victory);
        }

        // POST: Victories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? battlemageId, long? spacemageId)
        {
            var victory = await _context.Victories.FirstOrDefaultAsync(e =>
                e.BattleMageId == battlemageId && e.SpaceMageId == spacemageId);
            _context.Victories.Remove(victory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VictoryExists(long? battlemageId, long? spacemageId)
        {
            return _context.Victories.Any(e => e.BattleMageId==battlemageId&& e.SpaceMageId == spacemageId);
        }
    }
}
