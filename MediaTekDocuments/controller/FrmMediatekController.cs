using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System;
using System.Linq;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    public class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }


        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            return access.GetExemplairesRevue(idDocument);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// getter sur la liste des suivis
        /// </summary>
        /// <returns>Liste d'objets suivi</returns>
        public List<Suivi> GetAllSuivis()
        {
            return access.GetAllSuivis();
        }

        /// <summary>
        /// récupère les commandes d'un document
        /// </summary
        /// <param name="idDocument">id du document concerné</param>
        /// <returns>Liste d'objets Commandedocument</returns>
        public List<CommandeDocument> GetCommandesDocument(string idDocument)
        {
            return access.GetCommandeDocument(idDocument);
        }

        /// <summary>
        /// Créer la commande d'un livre ou dvd dans la bdd
        /// </summary>
        /// <param name="commandeDocument">l'objet commande document concerné</param>
        /// <returns>true si la création a pu se faire</returns>
        public bool CreerCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.CreerCommandeDocument(commandeDocument);
        }

        /// <summary>
        /// Modifier la commande d'un livre dans la bdd
        /// </summary>
        /// <param name="commandeDocument">l'objet commande document concerné</param>
        /// <returns>true si la modification a pu se faire</returns>
        public bool ModifierCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.ModifierCommandeDocument(commandeDocument);
        }

        /// <summary>
        /// Supprimer une commande document dans la bdd
        /// </summary>
        /// <param name="commandeDocument">l'objet commande document concerné</param>
        /// <returns>true si la suppression a pu se faire</returns>
        public bool SupprimerCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.SupprimerCommandeDocument(commandeDocument);
        }

        /// <summary>
        /// Récupère les commandes d'une revue dans la bdd
        /// </summary>
        /// <param name="idDocument"></param>
        /// <returns></returns>
        public List<Abonnement> GetAbonnement(string idDocument)
        {
            return access.GetAbonnement(idDocument);
        }

        /// <summary>
        /// Crée une commande d'une revue dans la bdd
        /// </summary>
        /// <param name="id">L'objet Commande concerné</param>
        /// <param name="dateFinAbonnement">L'objet Commande concerné</param>
        /// <param name="idRevue">L'objet Commande concerné</param>
        /// <returns>true si la création a pu se faire</returns>
        public bool CreerCommandeRevue(Abonnement abonnement)
        {
            return access.CreerCommandeRevue(abonnement);
        }

        /// <summary>
        /// Supprimer une commande revue dans la bdd
        /// </summary>
        /// <param name="Abonnement">l'objet commande revue concerné</param>
        /// <returns>true si la suppression a pu se faire</returns>
        public bool SupprimerCommandeRevue(Abonnement abonnement)
        {
            return access.SupprimerCommandeRevue(abonnement);
        }

        /// <summary>
        /// Teste si dateParution est compris entre dateCommande et dateFinAbonnement
        /// </summary>
        /// <param name="dateCommande">Date de commande d'un abonnement</param>
        /// <param name="dateFinAbonnement">Date de fin d'un abonnement</param>
        /// <param name="dateParution">Date d'achat d'un exemplaire</param>
        /// <returns>true si la date de parution est entre les 2 autres</returns>
        public bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateParution)
        {
            return (DateTime.Compare(dateCommande, dateParution) < 0 && DateTime.Compare(dateParution, dateFinAbonnement) < 0);
        }

        /// <summary>
        /// Récupère les exemplaires d'une revue concerné par un abonnement
        /// puis demande s'ils font partie de l'abonnement
        /// </summary>
        /// <param name="abonnement">l'abonnement concerné</param>
        /// <returns>True si un exemplaire est rattaché à l'abonnement</returns>
        public bool ExemplaireAbonnement(Abonnement abonnement)
        {
            List<Exemplaire> Exemplaires = GetExemplairesRevue(abonnement.IdRevue);
            bool parution = false;
            foreach (Exemplaire exemplaire in Exemplaires)
            {
                if(ParutionDansAbonnement(abonnement.DateCommande, abonnement.DateFinAbonnement, exemplaire.DateAchat))
                {
                    parution = true;
                }
            }
            return parution;
        }

        /// <summary>
        /// Récupère les abonnements se terminant dans moins de 30 jours
        /// </summary>
        /// <returns></returns>
        public List<FinAbonnement> GetFinAbonnement()
        {
            return access.GetFinAbonnement();
        }
    }
}
