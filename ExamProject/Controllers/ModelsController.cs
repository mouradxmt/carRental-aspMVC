using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamProject.Models;
using ExamProject.Models.viewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using ExamProject.Helpers;
namespace ExamProject.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
    public class ModelsController : Controller
    {
        private readonly CarsRentalContext _context;
        [BindProperty]
        public ModelVM VM { get; set; }

        public ModelsController(CarsRentalContext context)
        {
            _context = context;
            VM = new ModelVM()
            {
                Marques = _context.Marques.ToList(),
                Model = new Models.Model()
            };
        }

        // GET: Models
        public IActionResult Index()
        {
            var model = _context.Models.Include(m => m.Marque);
            return View(model.ToList());
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        public List<Marque> FillSelectList()
        {
            var makes = _context.Marques.ToList();
            makes.Insert(0, new Marque { Id = -1, NomMarque = "--- Please select a Make--" });
            return makes;
        }
        ModelVM GetAllMakes()
        {
            var vmodel = new ModelVM
            {
                Marques = FillSelectList()
            };
            return vmodel;
        }
        // GET: Models/Create
        public IActionResult Create()
        {
            return View(GetAllMakes());
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(VM);
            }
            _context.Models.Add(VM.Model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomModel")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.Id))
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
            return View(model);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Models.FindAsync(id);
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
        [AllowAnonymous]
        [HttpGet("api/models/{MarqueID}")]
        public IEnumerable<Model> Models(int MarqueID)
        {
            return _context.Models.ToList()
            .Where(m => m.MarqueID == MarqueID);
        }
    }
}
