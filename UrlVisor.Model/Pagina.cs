using System;

namespace UrlVisor.Model
{
    public class Pagina
    {
        public int Order { get; set; }
        public string Url { get; set; }
        public bool IsVisible { get; set; }
        public Usuario Usuario { get; set; }

        public Pagina(int order, string url, bool isVisible, Usuario usuario)
        {
            Order = order;
            Url = url;
            IsVisible = isVisible;
            Usuario = usuario;
        }
    }
}
