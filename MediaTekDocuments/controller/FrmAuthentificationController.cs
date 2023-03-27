using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System.Windows.Forms;

namespace MediaTekDocuments.controller
{
    public class FrmAuthentificationController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAuthentificationController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// récupere l'identifiant ainsi que le mot de passe d'un utilisateur
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>True si utilisateur et mot de passe trouvés</returns>
        public bool GetAuthentification(string login, string password)
        {
            Utilisateur utilisateur = access.GetAuthentification(login);
            if (utilisateur == null)
            {
                return false;
            }
            if (utilisateur.Password.Equals(password))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Récupère l'utilisateur qui s'est connecté
        /// </summary>
        /// <param name="login"></param>
        /// <returns>l'utilisateur</returns>
        public Utilisateur GetUtilisateur(string login)
        {
            Utilisateur utilisateur = access.GetAuthentification(login);
            return utilisateur;
        }
    }
}
