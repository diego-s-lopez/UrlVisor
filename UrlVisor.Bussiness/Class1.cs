using System;

namespace UrlVisor.Bussiness
{
    public interface IUsuarioAccessAdmin
    {
        bool LogIn(string user, string pass);
        void CreateUser(string user, string pass, string passRepeated);
        void ChangePassword(string user, string oldPass, string newPass, string newPassRepeatead);
    }
}
