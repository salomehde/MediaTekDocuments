using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Exemplaire (exemplaire d'une revue)
    /// </summary>
    public class Exemplaire
    {
        /// <summary>
        /// numéro d'un exemplaire
        /// </summary>
        public int Numero { get; set; }
        /// <summary>
        /// photo d'un exemplaire
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// date de l'achat d'un exemplaire
        /// </summary>
        public DateTime DateAchat { get; set; }
        /// <summary>
        /// id de l'état
        /// </summary>
        public string IdEtat { get; set; }
        /// <summary>
        /// id de l'exemplaire
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Initialisation d'un nouvel objet Exemplaire
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="dateAchat"></param>
        /// <param name="photo"></param>
        /// <param name="idEtat"></param>
        /// <param name="idDocument"></param>
        public Exemplaire(int numero, DateTime dateAchat, string photo, string idEtat, string idDocument)
        {
            this.Numero = numero;
            this.DateAchat = dateAchat;
            this.Photo = photo;
            this.IdEtat = idEtat;
            this.Id = idDocument;
        }

    }
}
