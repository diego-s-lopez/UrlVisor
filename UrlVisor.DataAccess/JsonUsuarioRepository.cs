﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UrlVisor.Model;

namespace UrlVisor.DataAccess
{
    public class JsonUsuarioRepository : UsuarioRepositoryBase
    {
        public override IEnumerable<Usuario> GetAll()
        {
            return _dataById.Values.Where(x=> !x.Borrado).ToList();
        }

        public override void Save(Usuario obj)
        {
            lock (_dataById)
            {
                if (_dataByUserName.ContainsKey(obj.User))
                    throw new ArgumentException("Nombre usuario existente");
                if (string.IsNullOrWhiteSpace(obj.User))
                    throw new ArgumentException("Nombre usuario no puede ser vacio");

                if (obj.Id == 0)
                {
                    obj.Id = _dataById.Count + 1;
                    _dataById.Add(obj.Id, obj);
                    _dataByUserName.Add(obj.User, obj);
                }
                else if (_dataById.ContainsKey(obj.Id))
                {
                    _dataById[obj.Id] = obj;
                    _dataByUserName[obj.User] = obj;
                }
                else throw new ArgumentException("Usuario desconocido");

                CommitChanges();
            }
        }

        public override Usuario GetById(int id)
        {
            if (_dataById.ContainsKey(id) && !_dataById[id].Borrado)
                return _dataById[id];
            else
                return null;
        }

        public override void Delete(Usuario obj)
        {
            obj.Borrado = true;
            Save(obj);
            CommitChanges();
        }

        public override Usuario GetByUserName(string usuario)
        {
            if (_dataByUserName.ContainsKey(usuario) && !_dataByUserName[usuario].Borrado)
                return _dataByUserName[usuario];
            else
                return null;
        }

        private void CommitChanges()
        {
            lock (_saveLock)
            {
                var text = JsonConvert.SerializeObject(_dataById.Values);
                File.WriteAllText(_opt.Value.JsonDbUsuarios, text);
            }
        }

        public JsonUsuarioRepository(IOptions<AppSettings> opt)
        {
            _opt = opt ??throw new ArgumentNullException(nameof(opt));

            if (!File.Exists(_opt.Value.JsonDbUsuarios))
                File.Create(_opt.Value.JsonDbUsuarios);

            var textUsuarios = File.ReadAllText(_opt.Value.JsonDbUsuarios);
            var userLists = !string.IsNullOrEmpty(textUsuarios)? JsonConvert.DeserializeObject<Usuario[]>(textUsuarios).ToList() : new List<Usuario>();
            _dataById = userLists.ToDictionary(x => x.Id, v => v);
            _dataByUserName = userLists.ToDictionary(x => x.User, v => v);
        }

        private readonly Dictionary<int, Usuario> _dataById;
        private readonly Dictionary<string, Usuario> _dataByUserName;
        private readonly IOptions<AppSettings> _opt;

        private object _saveLock = new object();
    }
}
