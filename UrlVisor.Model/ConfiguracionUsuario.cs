using System;
using System.Collections.Generic;
using System.Text;

namespace UrlVisor.Model
{
    public class ConfiguracionUsuario
    {
        public int Id { get; set; }
        public bool Activa { get; set; }
        public bool Borrado { get; set; }
        public int CantidadColumnas { get; set; }
        public int HeightPaginas { get; set; }
        public IEnumerable<Pagina> Paginas { get; set; }

        public Usuario Usuario { get; set; }
    }
}