using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vjezba14042021.EF;
using Vjezba14042021.Models;
using Vjezba14042021.ViewModel;

namespace Vjezba14042021.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly MojContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public KorisnikController(MojContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IndexVM model = new IndexVM
            {
                podaci = _context.korisnici.Select(s => new IndexVM.Row
                {
                    korisnik_id = s.KorisnikID,
                    username = s.Username,
                    password = s.Password,
                    datum_rodjenja = s.DatumRodjenja,
                    spol = s.Spol,
                    je_aktivan = s.JeAktivan,
                    naziv_slike = s.NazivSlike
                }).ToList()
            };
            return View(model);
        }

        public IActionResult DodajKorisnika()
        {
            return View();
        }

        public IActionResult SpremiKorisnika(DodajKorisnikaVM model)
        {
            Korisnik k = new Korisnik
            {
                Username = model.username,
                Password = model.password,
                DatumRodjenja = model.datum_rodjenja,
                JeAktivan = true,
                Spol = model.spol,
                NazivSlike = "default.png"
            };
            _context.Add(k);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Detalji(int id)
        {
            Korisnik k = _context.korisnici.Find(id);
            DetaljiVM model = new DetaljiVM
            {
                username = k.Username,
                password = k.Password,
                korisnik_id = k.KorisnikID,
                je_aktivan = k.JeAktivan,
                spol = k.Spol,
                naziv_slike = k.NazivSlike,
                slika_file = k.SlikaFile,
                datum_rodjenja = k.DatumRodjenja
            };
            return View(model);
        }

        public IActionResult UpdateKorisnik(DetaljiVM model)
        {
            Korisnik k = _context.korisnici.Find(model.korisnik_id);
            if (model.slika_file != null)
            {
                
                string RootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(model.slika_file.FileName);
                string extension = Path.GetExtension(model.slika_file.FileName);
                model.naziv_slike = fileName = fileName + extension;
                string path = RootPath + "/Image/" + fileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.slika_file.CopyTo(fileStream);
                }
                k.NazivSlike = model.naziv_slike;
                k.SlikaFile = model.slika_file;
            }

                
             k.Username = model.username;
             k.Password = model.password;
             k.JeAktivan = model.je_aktivan;
             k.Spol = model.spol;
             k.DatumRodjenja = model.datum_rodjenja;
                

             _context.korisnici.Update(k);
             _context.SaveChanges();
             return RedirectToAction("Index");
        }

        public IActionResult IzbrisiSliku(int id)
        {
            Korisnik k = _context.korisnici.Find(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", k.NazivSlike);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            k.NazivSlike = "default.png";
            _context.Update(k);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Izbrisi(int id)
        {
            Korisnik k = _context.korisnici.Find(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", k.NazivSlike);
            if(k.NazivSlike != "default.png")
              if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            _context.Remove(k);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
