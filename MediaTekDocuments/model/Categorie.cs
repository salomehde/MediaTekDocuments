﻿
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Categorie (réunit les informations des classes Public, Genre et Rayon)
    /// </summary>
    public class Categorie
    {
        /// <summary>
        /// id d'une catégorie
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// libelle d'une catégorie
        /// </summary>
        public string Libelle { get; }
        /// <summary>
        /// Initialisation d'un nouvel objet Categorie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Categorie(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

        /// <summary>
        /// Récupération du libellé pour l'affichage dans les combos
        /// </summary>
        /// <returns>Libelle</returns>
        public override string ToString()
        {
            return this.Libelle;
        }

    }
}
