# VsEat — Food Delivery Platform (BERZ)

> Web & CLI application for managing restaurant food deliveries in Valais, Switzerland, built with **ASP.NET Core 5 MVC** and an **N‑Tier** architecture.

---

## 📚 Project Description

VsEat lets end‑users order dishes from partner restaurants and have them delivered by BERZ couriers.
The solution is organised as three layers **DAL → BLL → MVC** on top of a **Microsoft SQL Server** database. A secondary branch contains a **CLI client** that re‑uses the same business logic.

## 🧪 Technologies Used

| Layer        | Technology / Tool     | Version |
| ------------ | --------------------- | ------- |
| Language     | C#                    | 9.0     |
| Framework    | .NET Core MVC         | 5.0     |
| ORM          | Entity Framework Core | 5.0     |
| Database     | Microsoft SQL Server  | 2019    |
| Front‑End    | Bootstrap             | 4.6     |
| Architecture | N‑Tier, MVC           | —       |

## 🎯 Learning Objectives

* Design and implement a layered architecture (DAL/BLL/MVC).
* Configure EF Core migrations against SQL Server.
* Secure an ASP.NET Core application (authentication & role management).
* Build Razor views and REST‑like controllers.
* Deploy both the Web UI and the CLI client.

## 🔧 Features

* **User registration & login** (client or staff).
* **Restaurant & menu browsing** with images.
* **Order creation** (15‑minute delivery windows, price calculated).
* **Delivery assignment** (maximum 5 orders per courier every 30 minutes).
* **Courier dashboard** to archive deliveries.
* **Profile management** (edit details, change location, deactivate account).

### 🔧 Bonus Features

* **Image storage**: compressed PNG (\~ 65 %) stored as `varbinary(MAX)`.
* **CLI client** available in the `SaveConsoleApp` branch.

## 🧠 Coding Principles

* ✅ Clear separation of concerns across layers.
* ✅ Dependency Injection everywhere.

### Error Handling

Global ASP.NET Core middleware plus local `try/catch`.

## 🏗 Project Structure

```
VsEat-Livraison-de-nourriture-BERZ/
├── DAL/                # Data Access Layer
├── BLL/                # Business Logic Layer
├── DTO/                # Data Transfer Objects
├── VSEatWebApp/        # ASP.NET Core MVC UI
└── PlatManagementTool/ # CLI Helper to upload images
```

## 📘 Documentation & Diagrams

* **ER (Entity‑Relationship) Diagram**: [`MPD_VSEat.pdf`](https://github.com/EliasBorrajo/VsEat-Livraison-de-nourriture-BERZ/blob/master/Annexes/MPD_VSEat.pdf)
* **Requirements document**: [`Annexes/623‑1 Project - MVC - VSEat.docx`](https://github.com/EliasBorrajo/VsEat-Livraison-de-nourriture-BERZ/blob/master/Annexes/623-1%20Project%20-%20MVC%20-%20VSEat.docx)

## 📌 Success Criteria

| Criterion                                             | Status     | Notes                                                |
| ----------------------------------------------------- | ---------- | ---------------------------------------------------- |
| Core functionalities working (login, order, delivery) | ✅ Done     | All mandatory user stories implemented               |
| Deployment on school server (production)              | ✅ Done     | Deployed Jan 2022; link now offline                  |
| Clean code & correct MVC / N‑Tier design              | ✅ Done | Meets course guidelines but some refactoring needed  |
| SQL schema & commands quality                         | ✅ Done     | Normalised schema and explicit queries               |
| UI/UX (CSS "look & feel")                             | 🟡 Partial | Functional but design could be improved              |
| Bug‑free operation                                    | 🟡 Partial | Minor known issues, no blocking bugs                 |
| Documentation (README + ER diagram + logbook)         | ✅ Done | README, ER present and logbook                       |
|                                                       |            |                                                      |

## 🔒 Security Note 🔒 Security Note

`ConnectionStrings.DefaultConnection` contains the IP, user id and password for the school test server.
This was acceptable for the practical assignment, **but it is not good practice** in production.
Secrets should be stored in *dotnet user‑secrets*, environment variables or a `appsettings.Development.json` file ignored by Git.

## 🌐 Branches

| Branch           | Content               |
| ---------------- | --------------------- |
| `master`         | MVC web application   |
| `SaveConsoleApp` | Historical CLI client |

## 👥 Test Accounts

> Accounts were seeded directly into the database and are **not** part of the SQL script.

### Staff

| Name               | Email                                                           | Password | Active |
| ------------------ | --------------------------------------------------------------- | -------- | ------ |
| Venec Le Bandit    | [esclaves.pascher@dies.irae](mailto:esclaves.pascher@dies.irae) | 3337     | ✅      |
| Kadoc Le Caillou   | [la.poulette@dies.irae](mailto:la.poulette@dies.irae)           | 0996     | ✅      |
| Aconia De la Villa | [fidele.romaine@dies.irae](mailto:fidele.romaine@dies.irae)     | 3844     | ✅      |
| Sven Le Viking     | [valhalla.awayts@dies.irae](mailto:valhalla.awayts@dies.irae)   | 8765     | ✅      |
| Demetra Du Château | [favorite@dies.irae](mailto:favorite@dies.irae)                 | 2964     | ✅      |
| Loth Orcanie       | [traitre@dies.irae](mailto:traitre@dies.irae)                   | 7754     | ❌      |

### Clients

| Name                    | Email                                                                     | Password | Active |
| ----------------------- | ------------------------------------------------------------------------- | -------- | ------ |
| Zacharie Renna          | [rz@dies.irae](mailto:rz@dies.irae)                                       | 1234     | ✅      |
| Arthur Pendragon        | [sanglier.cornouaille@dies.irae](mailto:sanglier.cornouaille@dies.irae)   | 4200     | ✅      |
| Lancelot Du Lac         | [chevalier.errant@dies.irae](mailto:chevalier.errant@dies.irae)           | 1922     | ✅      |
| Père Blaise             | [quinte.juste@dies.irae](mailto:quinte.juste@dies.irae)                   | 6669     | ❌      |
| Dame Du Lac             | [invisible.aux.mortels@dies.irae](mailto:invisible.aux.mortels@dies.irae) | 2119     | ✅      |
| Merlin Druide           | [coco.asticot@dies.irae](mailto:coco.asticot@dies.irae)                   | 2500     | ✅      |
| Léodagan Carmélide      | [chevalier.sanguinaire@dies.irae](mailto:chevalier.sanguinaire@dies.irae) | 3024     | ✅      |
| Yvain Chevalier au Lion | [petit.pedestre@dies.irae](mailto:petit.pedestre@dies.irae)               | 7843     | ✅      |
| Perceval Le Gallois     | [pas.faux@dies.irae](mailto:pas.faux@dies.irae)                           | 6597     | ✅      |
| Karadoc De Vannes       | [semi.croustillant@dies.irae](mailto:semi.croustillant@dies.irae)         | 0874     | ✅      |
| Bohort De Gaunes        | [mecreants@dieas.irae](mailto:mecreants@dieas.irae)                       | 5591     | ✅      |
| Elias De Kelliwich      | [enchanteur.du.nord@dies.irae](mailto:enchanteur.du.nord@dies.irae)       | 6660     | ✅      |

## 📈 Scenarios

* **Clients** can:

  * Access their profile to edit details, address and deactivate the account.
  * Browse restaurants and create orders for a chosen date & time.
  * View past orders.
  * Cancel an order at least 3 hours in advance, as long as no courier has accepted it.

* **Staff** can:

  * Edit their profile, delivery area and deactivate the account.
  * See the order history.
  * Accept / validate a customer order.

A customer places an order, a courier handles it. The customer can cancel 3 hours in advance while the order is still pending. Payment happens on delivery at the customer’s address.

## 📝 Notes

### Images

Given the small DB size, product & restaurant pictures are stored directly in the database as `varbinary(MAX)`. The PNG files are compressed (\~ 65 %) beforehand to keep performance acceptable.
A helper project called [PlatManagementTool](https://github.com/EliasBorrajo/VsEat-Livraison-de-nourriture-BERZ/tree/SaveConsoleApp/PlateformeLivraison) makes it easy to upload or change images.

### SQL Queries

Queries in the DAL are written explicitly (`SELECT column1, column2 …`) rather than `SELECT *`, to improve performance.

## 👥 Credits

Project by **Zacharie Renna** and **Elias Borrajo**.
Tested with care by **Céline Vialard**.







---

<details>
 <summary>
  <h2>
   Original Readme archive (FR)
  </h2>
 </summary>



<h1>Plateforme de livraison de nourriture - BERZ</h1> 
<h2>Description</h2>
<h3>Vue d'ensemble :</h3>
Ce projet est fait dans le cadre de notre 3ème semestre d'études en tempa spartiel à la HES-So - Informatique de gestion pour le module de <b>Software Development - SI - Implémentation du système d'information<b>.

Au cours du semestre, nous aprennons à développer une application en utilisant plusieurs couches. 
On implemente une couche de données (DAL), une couche métier (BLL) et enfin on apprend à utiliser ASP.NET MVC (model View Controller) comme interface utilisateur.
L'idée du projet est de modifier le DAL et le BLL créés pendant les leçons et de créer une nouvelle interface utilisateur développée comme une application MVC. 

Les technologies utilisées durant le projet sont :
- C# :  Car c'est un langage proche du Java que nous conaissons déja. Environ 70% de l'application.

- Framework .NET Core 5.0 : Le framework CORE permet lancer l'application sur differents OS.
 Les differentes couches (DAL/ BLL / DTO / ConsoleApp / MVC) utilisent ce framework
    * De plus, l'installation depuis "NuGet" des extensions suivantes ont été nécessaires, c'est un gestionnaire de package pour la solution.
      * Les extensions de "NuGet" s'installent depuis : 
        * Barre d'outils --> Outils --> gestionnaire de package NuGet --> Gerer les packets. 
      * Puis installer :
        * Microsoft.Extensions.Configuration
        * Microsoft.Extensions.Configuration.Abstractions
        * Microsoft.Extensions.Configuration.FileExtensions
        * Microsoft.Extensions.Configuration.Json
        * System.Data.SqlClient

- BootStrap 4 : Nous permet de créer des vues grâce à des templates déja existant et d'avoir une web-app esthetique et agréable à l'utisation. C'est un gain de temps énorme sur la partie front end du projet. 
Bootstrap a l'avantage  d'avoir du razorcode qui interprêtera du code C# et le traduira en adequation. 
L'autre avantage de bootstrap qui peut sembler pareil que le razorcode, c'est les ASP commands qui seront aussi utilisées lors du projet.
    * HTML / CSS / SCSS / JavaScript 
Les professeurs sont nos clients lors de la remise du projet, il ne faut donc pas que le projet plante durant son utilisation.

<h3>Database :</h3>
La base de données est de type : SQL Server de Microsoft.

La base de données stocke des données à gérer :
- les plats vendus par les restaurants
- les commandes des clients
- le personnel de BERZ responsable de la livraison dans les villes
- login du personnel
- login des clients

<h3>User Stories principales :</h3>

- Login : Un client doit créer un compte avec son adresse avant d'utiliser le site web
- Commande : Un client connecté peut choisir des plats dans une liste donnée pour chaque restaurant disponible sur le site web pour former une commande. 
Il (le client) ajoutera le délai de livraison (toutes les 15 minutes) pour sa commande. A la fin de la commande, le prix que le client doit payer au coursier sera affiché.

- Gestion des livraisons : Le système attribue la livraison d'une commande à un coursier disponible dans la même ville que le restaurant où la commande est passée. Un coursier ne peut pas avoir plus de 5 commandes à livrer toutes les 30 minutes.

- Interface de livraison : chaque coursier peut se connecter au système pour voir ses prochaines livraisons. Lorsqu'une livraison est effectuée, le livreur l'archive en appuyant sur un bouton de l'interface de livraison.


<h2>Installation & Lancement</h2>
<h3>Version deployée :</h3>
Pour commencer, le projet est deployé sur le serveur de la HES du professeur. Pour pouvoir y accéder, il est nécessaire d'être connecté au réseau de la HES en étant sur place ou via le VPN de l'école.

L'adresse du site déployé sur le serveur est : [WebApp - BerzEat](http://153.109.124.35:81/BERZ)

<h3>Cloner le projet :</h3>

* Afin de pouvoir se connecter au serveur du professeur, il faut avoir un fichier de configuration, apellé : _appsettings.json_ placé à la racine du projet console ou MVC.
  Dans ce fichier, nous avons une "ConnectionStrings", il faut y donner l'adresse IP du serveur du professeur, le nom de la DBO à laquelle l'on veut se connecter, un ID & Password pour avoir accès au serveur. 
  
  1 : Voici l'exemple simplifié de l'accès avec l'application _console_ : 

        ```
        {
        "ConnectionStrings": {
            "DefaultConnection": "Data Source=153.109.124.35;Initial Catalog=VSEAT_BERZ;Persist Security Info=True;User ID=6231db;Password=Pwd46231."
        }
        }
        ```

  2 : Et voici ce même fichier, mais dans le _MVC de la web-app_ : 

            ```
            {
            "ConnectionStrings": {
                "DefaultConnection": "Data Source=153.109.124.35;Initial Catalog=VSEAT_BERZ;Persist Security Info=True;User ID=6231db;Password=Pwd46231."
            },
            "Logging": {
                "LogLevel": {
                "Default": "Information",
                "Microsoft": "Warning",
                "Microsoft.Hosting.Lifetime": "Information"
                }
            },
            "AllowedHosts": "*"
            }

            ```


<h2>Manuel d'utilisation</h2>
Une fois l'accès à la page principale de la webapp, il faut choisir si l'on veut se connecter en tant que client, ou staff (qui seront les livreurs).


<h4>Comptes staff</h4>
Voici une liste des STAFF existants pour tenter une connection : 
<table>
        <tr>
            <th>Nom, Prénom</th>
            <th>Email</th>
            <th>Mot de passe</th>
            <th>Compte Actif</th>
        </tr>
        <tr>
            <td>Le Bandit, Venec</td>
            <td>esclaves.pascher@dies.irae</td>
            <td>3337</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Le Caillou, Kadoc</td>
            <td>la.poulette@dies.irae</td>
            <td>0996</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>De la Villa, Aconia</td>
            <td>fidele.romaine@dies.irae</td>
            <td>3844</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Le Viking, Sven</td>
            <td>valhalla.awayts@dies.irae</td>
            <td>8765</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Du Château, Demetra</td>
            <td>favorite@dies.irae</td>
            <td>2964</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Orcanie, Loth</td>
            <td>traitre@dies.irae</td>
            <td>7754</td>
            <td>Non</td> 
        </tr>
</table>


<h4>Comptes client</h4>
Voici une liste des CLIENTS existants pour tenter une connection : 
<table>
        <tr>
            <th>Nom, Prénom</th>
            <th>Email</th>
            <th>Mot de passe</th>
            <th>Compte Actif</th>
        </tr>
        <tr>
            <td>Renna, Zacharie</td>
            <td>rz@dies.irae</td>
            <td>1234</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Pendragon, Arthur</td>
            <td>sanglier.cornouaille@dies.irae</td>
            <td>4200</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Du Lac, Lancelot</td>
            <td>chevalier.errant@dies.irae</td>
            <td>1922</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Blaise, Père</td>
            <td>quinte.juste@dies.irae</td>
            <td>6669</td>
            <td>Non</td>
        </tr>
        <tr>
            <td>Du Lac, Dame</td>
            <td>invisible.aux.mortels@dies.irae</td>
            <td>2119</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Druide, Merlin</td>
            <td>coco.asticot@dies.irae</td>
            <td>2500</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Carmélide, Léodagan</td>
            <td>chevalier.sanguinaire@dies.irae</td>
            <td>3024</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Chevalier au Lion, Yvain</td>
            <td>petit.pedestre@dies.irae</td>
            <td>7843</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>Le Gallois, Perceval</td>
            <td>pas.faux@dies.irae</td>
            <td>6597</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>De Vannes, Karadoc</td>
            <td>semi.croustillant@dies.irae</td>
            <td>874</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>De Gaunes, Bohort</td>
            <td>mecreants@dieas.irae</td>
            <td>5591</td>
            <td>Oui</td>
        </tr>
        <tr>
            <td>De Kelliwich, Elias</td>
            <td>enchanteur.du.nord@dies.irae</td>
            <td>6660</td>
            <td>Oui</td>
        </tr>
</table>


<h2>Scénarios</h2>

- Les clients peuvent donc se connecter puis :
    * Accéder à leur profil afin de modifier leurs informations, leur localité & désactiver leur compte.
    * Voir les restaurants existants, et créer une nouvelle commande pour une date & heure voulue.
    * Voir la l'historique des commandes.
    * Annuler une commande en cours au moins 3h à l'avance & qui n'a pas encore été validé par le livreur.

- Les staffs peuvent donc se connecter puis : 
    * Accéder à leur profil afin de modifier leurs informations, leur localité de livraisons & désactiver leur compte.
    * Voir la l'historique des commandes.
    * Valider une commande passée par un client.

Un client va donc passer une commande, et un staff s'occupera de celle-ci. Le client peut annuler sa commande 3h à l'avance et tant qu'aucun staff ne la valide. 
Le client paie sa commande au staff lorsque le staff arrive au lieu de livraison. 
On part du principe que le client veut se faire livrer à son domicile, donc à son adresse.

<h2>Notes </h2>
<h3>Images :</h3>
Etant donné que nous possédons une petite DB, et que le projet reste relativement petit, nous avons décidé de stocker les images directement dans la DB sous forme de "VarBinary". Ce sera donc un tabelau de bytes stocké que nous convertissons lors de la récuperation de l'enregistrement. Ce tableau de bytes sera converti en StringBase64 pour l'affichage.
Ces images sont en format .png afin d'être moins lourd que du .jpeg. Et par souci d'optimisation, les images ont été compressés d'environ 65% avant d'entre ajoutées à la DB. Ainsi, nous n'avons pas de pertes de performances.

Un projet a été crée s'apellant : [PlatManagementTool](https://gitlab.com/EliasKelliwich/livraisonnourriture/-/tree/master/PlatManagementTool). 
C'est un outil permettant de visualiser quel restaurant et quels plats possèdent des images, et de pouvoir aller chercher des images sur le disque du PC de l'utilisateur facilement, et de les upload dans la DB.

<h3>Requêtes SQL</h3>
Les requêtes SQL écrites dans la DAL ont aussi été optimisées. Nous ne faisons pas un " SELECT * " qui prendrait toutes les données de la table, mais un " SELECT name .... " avec chaque élément voulu. C'est plus efficace parce que la requête sait directement quelle colonne récuperér.

<h2>Améliorations futures</h2>
A cause de la durée du projet qui est limtié, nous n'avons pas pu implementér toutes les fonctionnalitées voulues.

- Ajouter un horaire aux restaurants, pour éviter les commandes hors horaires.
- Ajouter des catégories pours les plats : Viande / Poisson / Burger / Boissons / Vegan.
- Ajouter une taxe à chaque commande pour le revenu des dev de la plateforme.
- Dans la gestion des exceptions, si une erreur survient et nous ramène sur une autre page, afficher l'erreur dans une pop-up de l'app.
- Améliorer le design des pages.

<h2>Crédits</h2>
Projet réalisé par : 
[Renna Zacharie](https://gitlab.com/renna.zacharie) & [Borrajo Elias](https://gitlab.com/EliasKelliwich)
et testé avec amour par [Céline Vialard](https://gitlab.com/celine.vialard1)



 
</details>
