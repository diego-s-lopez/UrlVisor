using System;
using System.Collections.Generic;
using System.Text;

namespace UrlVisor.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }

        public bool Borrado { get; set; }

        public IEnumerable<Pagina> Paginas { get; set; }
    }
}
