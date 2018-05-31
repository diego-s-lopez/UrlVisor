using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UrlVisor.Model;

namespace UrlVisor.DataAccess
{
    public class JsonUsuarioRepository : IRepository<Usuario>
    {
        public IEnumerable<Usuario> GetAll()
        {
            return _data.Values.ToList();
        }

        public void Save(Usuario obj)
        {
            lock (_data)
            {
                if (obj.Id == 0)
                {
                    obj.Id = _data.Count + 1;
                    _data.Add(obj.Id, obj);
                }
                else if (_data.ContainsKey(obj.Id))
                {
                    _data[obj.Id] = obj;
                }
                else throw new ArgumentException("Usuario desconocido");
            }
        }

        public Usuario GetById(int id)
        {
            if (_data.ContainsKey(id))
                return _data[id];
            else
                return null;
        }

        public void Delete(Usuario obj)
        {
            obj.Borrado = true;
            Save(obj);
        }

        public JsonUsuarioRepository(IOptions<AppSettings> opt)
        {
            _opt = opt ??throw new ArgumentNullException(nameof(opt));
            var textUsuarios = File.ReadAllText(_opt.Value.JsonDbUsuarios);
            var userLists = JsonConvert.DeserializeObject<Usuario[]>(textUsuarios).ToList();
            _data = userLists.ToDictionary(x => x.Id, v => v);
        }

        private readonly Dictionary<int, Usuario> _data;
        private readonly IOptions<AppSettings> _opt;
        
    }
}
