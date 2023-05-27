Feature: Recherches dans l'onglet Livre

  Scenario: Recherche par le titre
    Given je me trouve dans l'onglet Livre
    When je saisis le titre du livre 'Catastrophes au Brésil' dans txbLivresTitreRecherche
    Then les informations du livre sont affichées, avec le titre 'Catastrophes au Brésil' et l'auteur 'Philippe Masson'

  Scenario: Recherche par le numéro de document
    Given je me trouve dans l'onglet Livre
    Given je saisis le numéro de document '00017' dans txbLivresNumRecherche
    When je clique sur le bouton 'Rechercher' btnLivresNumRecherche
    Then les informations du livre sont affichées, avec le titre 'Catastrophes au Brésil' et l'auteur 'Philippe Masson'