﻿
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Rayon (rayon de classement du document) hérite de Categorie
    /// </summary>
    public class Rayon : Categorie
    {
        /// <summary>
        /// Initialisation d'un nouvel objet Rayon
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Rayon(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
