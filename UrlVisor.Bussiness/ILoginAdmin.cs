using System;
using UrlVisor.Model;

namespace UrlVisor.Bussiness
{
    public interface ILoginAdmin
    {
        Usuario LogIn(string user, string pass);
        Usuario CreateUser(string user, string pass, string passRepeated);
        bool ChangePassword(string user, string oldPass, string newPass, string newPassRepeatead);
    }
}
