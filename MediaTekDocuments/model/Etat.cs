
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Etat (état d'usure d'un document)
    /// </summary>
    public class Etat
    {
        /// <summary>
        /// id de l'état
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// libelle de l'état
        /// </summary>
        public string Libelle { get; set; }
        /// <summary>
        /// Initialisation d'un nouvel objet Etat
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Etat(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

    }
}
