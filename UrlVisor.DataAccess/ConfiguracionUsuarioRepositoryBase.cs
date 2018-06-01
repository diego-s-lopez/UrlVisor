using System;
using System.Collections.Generic;
using System.Text;
using UrlVisor.Model;

namespace UrlVisor.DataAccess
{
    public abstract class ConfiguracionUsuarioRepositoryBase : IRepository<ConfiguracionUsuario>
    {
        public abstract IEnumerable<ConfiguracionUsuario> GetAll();
        public abstract void Save(ConfiguracionUsuario obj);
        public abstract ConfiguracionUsuario GetById(int id);
        public abstract void Delete(ConfiguracionUsuario obj);

        public abstract ConfiguracionUsuario GetByUsuario(Usuario usuario);
    }
}
