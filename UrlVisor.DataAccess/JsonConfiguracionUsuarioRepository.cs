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
    public class JsonConfiguracionUsuarioRepository : ConfiguracionUsuarioRepositoryBase
    {
        public override IEnumerable<ConfiguracionUsuario> GetAll()
        {
            return _dataById.Values.ToList();
        }

        public override void Save(ConfiguracionUsuario obj)
        {
            lock (_dataById)
            {
                if (obj.Id == 0)
                {
                    obj.Id = _dataById.Count + 1;
                    _dataById.Add(obj.Id, obj);
                }
                else if (_dataById.ContainsKey(obj.Id))
                {
                    _dataById[obj.Id] = obj;
                }
                else throw new ArgumentException("Usuario desconocido");
            }
        }

        public override ConfiguracionUsuario GetById(int id)
        {
            if (_dataById.ContainsKey(id))
                return _dataById[id];
            else
                return null;
        }

        public override void Delete(ConfiguracionUsuario obj)
        {
            obj.Borrado = true;
            Save(obj);
        }

        public override ConfiguracionUsuario GetByUsuario(Usuario usuario)
        {
            return _dataById.Values.FirstOrDefault(x => x.Usuario.Id == usuario.Id);
        }

        public JsonConfiguracionUsuarioRepository(IOptions<AppSettings> opt)
        {
            _opt = opt ?? throw new ArgumentNullException(nameof(opt));
            var textConfig = File.ReadAllText(_opt.Value.JsonDbConfiguracion);
            var confgList = JsonConvert.DeserializeObject<ConfiguracionUsuario[]>(textConfig).ToList();
            _dataById = confgList.ToDictionary(x => x.Id, v => v);
        }

        private readonly Dictionary<int, ConfiguracionUsuario> _dataById;
        private readonly IOptions<AppSettings> _opt;
    }
}
