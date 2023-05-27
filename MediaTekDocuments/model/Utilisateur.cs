using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Utilisateur
    /// </summary>
    public class Utilisateur
    {
        /// <summary>
        /// id d'un utilisateur
        /// </summary>
        public int IdUser { get; }
        /// <summary>
        /// id du service
        /// </summary>
        public int IdService { get; }
        /// <summary>
        /// login d'un utilisateur
        /// </summary>
        public string Login { get; }
        /// <summary>
        /// password d'un utilisateur
        /// </summary>
        public string Password { get; }
        /// <summary>
        /// Initialisation d'un nouvel objet Utilisateur
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idService"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public Utilisateur(int idUser, int idService, string login, string password) 
        {
            this.IdUser = idUser;
            this.IdService = idService;
            this.Login = login;
            this.Password = password;
        }
    }
}
