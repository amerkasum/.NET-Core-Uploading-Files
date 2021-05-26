using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vjezba14042021.ViewModel
{
    public class IndexVM
    {
        public List<Row> podaci { get; set; }

        public class Row
        {
            public int korisnik_id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public DateTime datum_rodjenja { get; set; }
            public bool je_aktivan { get; set; }
            public string spol { get; set; }
            public string naziv_slike { get; set; }
            public IFormFile slika_file { get; set; }
        }
    }
}
