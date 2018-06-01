using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UrlVisor.DataAccess;
using UrlVisor.Model;

namespace UrlVisor.Bussiness
{
    public class LoginAdmin : ILoginAdmin
    {
        public Usuario LogIn(string user, string pass)
        {
            var usuario = _usuarioRepository.GetByUserName(user);
            if (usuario == null) throw new LogInException("Usuario inexistente");

            var passHashed = "";
            using (var sha = new SHA512Managed())
            {
                passHashed = GetStringFromHash(sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(pass)));


                if (usuario.Pass != passHashed) throw new LogInException("Password incorrecta");

                usuario.FechaToken = DateTime.Now;
                usuario.Token = GetStringFromHash(sha.ComputeHash(BitConverter.GetBytes(usuario.GetHashCode())));
            }

            return usuario;
        }

        public Usuario CreateUser(string user, string pass, string passRepeated)
        {
            var existe = _usuarioRepository.GetByUserName(user);
            if (existe!=null) throw new LogInException("El nombre de usuario ya existe");

            if (pass != passRepeated) throw new LogInException("El password repetido no coincide");

            var passHashed = "";
            using (var sha = new SHA512Managed())
            {
                passHashed = GetStringFromHash(sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(pass)));
            }

            var usuario = new Usuario
            {
                User = user,
                Pass = passHashed
            };

            _usuarioRepository.Save(usuario);

            return usuario;
        }

        public bool ChangePassword(string user, string oldPass, string newPass, string newPassRepeatead)
        {
            if (newPass != newPassRepeatead) throw new LogInException("El password repetido no coincide");
            var oldUser = LogIn(user, oldPass);
            if (oldUser!=null)
            {
                using (var sha = new SHA512Managed())
                {
                    oldUser .Pass = GetStringFromHash(sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(newPass)));
                }

                _usuarioRepository.Save(oldUser);
            }

            return true;
        }

        public LoginAdmin(UsuarioRepositoryBase usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        private readonly UsuarioRepositoryBase _usuarioRepository;
    }

    public class LogInException : Exception
    {
        public LogInException(string msg) : base(msg)
        {
        }
    }
}
