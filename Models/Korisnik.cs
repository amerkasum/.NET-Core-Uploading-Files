using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vjezba14042021.Models
{
    public class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Spol { get; set; }
        public bool JeAktivan { get; set; }
        public string NazivSlike { get; set; }
        [NotMapped]
        public IFormFile SlikaFile { get; set; }
    }
}
