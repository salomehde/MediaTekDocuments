using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class Utilisateur
    {
        public int IdUser { get; }
        public int IdService { get; }
        public string Login { get; }
        public string Password { get; }

        public Utilisateur(int idUser, int idService, string login, string password) 
        {
            this.IdUser = idUser;
            this.IdService = idService;
            this.Login = login;
            this.Password = password;
        }
    }
}
