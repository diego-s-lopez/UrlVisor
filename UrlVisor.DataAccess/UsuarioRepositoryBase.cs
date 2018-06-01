using System;
using System.Collections.Generic;
using System.Text;
using UrlVisor.Model;

namespace UrlVisor.DataAccess
{
    public abstract class UsuarioRepositoryBase : IRepository<Usuario>
    {
        public abstract IEnumerable<Usuario> GetAll();
        public abstract void Save(Usuario obj);
        public abstract Usuario GetById(int id);
        public abstract void Delete(Usuario obj);

        public abstract Usuario GetByUserName(string usuario);
    }
}
