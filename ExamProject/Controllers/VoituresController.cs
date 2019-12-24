using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamProject.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting.Internal;
using ExamProject.Models.viewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using ExamProject.Helpers;
namespace ExamProject.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
    public class VoituresController : Controller
    {
        private readonly CarsRentalContext _context;
        private readonly HostingEnvironment _hostingEnvironment;
        [BindProperty]
        public CarViewModel VM { get; set; }

        public VoituresController(CarsRentalContext context ,HostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Voitures
        public IActionResult Index()
        {
            var voitures = _context.Voiture.Include(m => m.Marque).Include(m => m.Model);
            return View(voitures);
        }

        // GET: Voitures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voiture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }
        public List<Marque> FillSelectList()
        {
            var makes = _context.Marques.ToList();
            makes.Insert(0, new Marque { Id = -1, NomMarque = "--- Veuillez selectionner une marque--" });
            return makes;
        }
        public List<Model> FillSelectListModels()
        {
            var models = _context.Models.ToList();
            models.Insert(0, new Model { Id = -1, NomModel = "--- Veuillez selectionner un model--" });
            return models;
        }
        CarViewModel GetAllMakes()
        {
            var vmodel = new CarViewModel
            {
                Marques = FillSelectList(),
                Models = FillSelectListModels()
            };
            return vmodel;
        }
        // GET: Voitures/Create
        public IActionResult Create()
        {
            return View(GetAllMakes());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(VM);
            }
            _context.Voiture.Add(VM.Voiture);
            UploadImage();
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Voitures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voiture.FindAsync(id);
            if (voiture == null)
            {
                return NotFound();
            }
            return View(voiture);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrixParJour,Annee,Kilometrage,Couleur,ImagePath")] Voiture voiture)
        {
            if (id != voiture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voiture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoitureExists(voiture.Id))
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
            return View(voiture);
        }

        // GET: Voitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voiture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voiture = await _context.Voiture.FindAsync(id);
            _context.Voiture.Remove(voiture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoitureExists(int id)
        {
            return _context.Voiture.Any(e => e.Id == id);
        }


        private void UploadImage()
        {

            var CarID = VM.Voiture.Id;

            //Get wwwroot path to save the file on server 
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            //Get the aploaded files
            var files = HttpContext.Request.Form.Files;

            var SavedCar = _context.Voiture.Find(CarID);

            if (files.Count != 0)
            {
                var ImagePath = @"images\cars\";
                var Extension = Path.GetExtension(files[0].FileName);
                var RelativeImagePath = ImagePath + CarID + Extension;
                var AbsImagePath = Path.Combine(wwwrootPath, RelativeImagePath);
                //Upload the file on server
              using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                //Set the image path on database
               SavedCar.ImagePath = RelativeImagePath;

            }
        }

    }
}
