<h1>Plateforme de livraison de nourriture - BERZ</h1> 
<h2>Description</h2>
<h3>Vue d'ensemble :</h3>
Ce projet est fait dans le cadre de notre 3ème semestre d'études en tempa spartiel à la HES-So - Informatique de gestion pour le module de <b>Software Development - SI - Implémentation du système d'information<b>.

Au cours du semestre, nous aprennons à développer une application en utilisant plusieurs couches. 
On implemente une couche de données (DAL), une couche métier (BLL) et enfin on apprend à utiliser ASP.NET MVC (model View Controller) comme interface utilisateur.
L'idée du projet est de modifier le DAL et le BLL créés pendant les leçons et de créer une nouvelle interface utilisateur développée comme une application MVC. 

Les technologies utilisées durant le projet sont :
- C# :  Car c'est un langage proche du Java que nous conaissons déja. Environ 70% de l'application.
- BootStrap 4 : Nous permet de créer des vues grâce à des templates déja existant et d'avoir une web-app esthetique et agréable à l'utisation. C'est un gain de temps énorme sur la partie front end du projet. 
Bootstrap a l'avantage  d'avoir du razorcode qui interprêtera du code C# et le traduira en adequation. 
L'autre avantage de bootstrap qui peut sembler pareil que le razorcode, c'est les ASP commands qui seront aussi utilisées lors du projet.
    * HTML / CSS / SCSS / JavaScript 
- Framework .NET Core 5.0 : Les differentes couches (DAL/ BLL / DTO / ConsoleApp / MVC) utilisent ce framework
    * De plus, l'installation de l'extension "NuGet" est nécessaire.

Les professeurs sont nos clients lors de la remise du projet, il ne faut donc pas que le projet plante durant son utilisation.

<h3>Database :</h3>
La base de données est de type : SQL Server de Microsoft.

La base de données stockue des données à gérer :
- les plats vendus par les restaurants
- les commandes des clients
- le personnel de BERZ responsable de la livraison dans les villes
- login du personnel
- login des clients

<h3>User Stories principales :</h3>

- Login : Un client doit créer un compte avec son adresse avant d'utiliser le site web
- Commande : Un client connecté peut choisir des plats dans une liste donnée par chaque restaurant disponible sur le site web pour former une commande. 
Il (le client) ajoutera le délai de livraison (toutes les 15 minutes) pour sa commande. A la fin de la commande, le prix que le client doit payer au coursier sera affiché.

- Gestion des livraisons : Le système attribue la livraison d'une commande à un coursier disponible dans la même ville que le restaurant où la commande est passée. Un coursier ne peut pas avoir plus de 5 commandes à livrer toutes les 30 minutes.

- Interface de livraison : chaque coursier peut se connecter au système pour voir ses prochaines livraisons. Lorsqu'une livraison est effectuée, le livreur l'archive en appuyant sur un bouton de l'interface de livraison.


<h2>Installation & Lancement</h2>
<h3>Version deployé :</h3>
Pour commencer, le projet est deployé sur le serveur de la HES du professeur. Pour pouvoir y acceder, il est nécessaire d'être connecté au réseau de la HES en étant sur place ou via le VPN de l'école.

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
Une fois l'accès à la page principale de la webapp, il faut choisir si l'on veut se connecter en tant que client, ou staff(qui seront les livreurs).


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


<h2>Scenarios</h2>

- Les clients peuvent donc se connecter puis :
    * Acceder à leur profil afin de modifier leurs informations, leur localité & désactiver leur compte.
    * Voir les restaurants existant, et créer une nouvelle commande pour une date & heure voulue.
    * Voir la l'historique des commandes.
    * Annuler une commande en cours qui n'a pas encore été validée par le livreur.

- Les staffs peuvent donc se connecter puis : 
    * Acceder à leur profil afin de modifier leurs informations, leur localité de livraisons & désactiver leur compte.
    * Voir la l'historique des commandes.
    * Valider une commande passé par un client.

Un client va donc passer une commande, et un staff s'occupera de celle-ci. Le client peut annuler sa commande tant qu'aucun staff ne la valide. 
Le client paye sa commande au staff lorsque le staff arrive au lieu de livraison. 
On part du prinipe que le client veut se faire livrer à son domicile, donc à son adresse.

<h2>Notes </h2>
<h3>Images :</h3>
Etant donné que nous possedons une petite DB, et que le projet reste relativement petit, nous avons décidé de stocker les images directement dans la DB sous forme de "VarBinary". Ce sera donc une string de bytes stocké que nous convertissons lors de l'affichage.
Ces images sont en format .png afin d'être moins lourd que du .jpeg. Et par souci d'optimisation, les images ont été compressés d'environ 65% avant d'entre ajoutées à la DB. Ainsi, nous n'avons aps de pertes de performances.

Un projet a été crée s'apellant : [PlatManagementTool](https://gitlab.com/EliasKelliwich/livraisonnourriture/-/tree/master/PlatManagementTool). 
C'est un outil permettant de visualiser quel restaurant et ses plats possedent des images, et de pouvoir aller chercher des images sur le disue du PC de l'utilisateur facilement, et de les upload dans la DB.

<h2>Crédits</h2>
Projet réalisé par : 
[Rennaz Zacharie](https://gitlab.com/renna.zacharie)
[Borrajo Elias](https://gitlab.com/EliasKelliwich)

<h2>Licence</h2>



4. How to Install and Run the Project
If you are working on a project that a user needs to install or run locally in a machine like a "POS", you should include the steps required to install your project and also the required dependencies if any.

Provide a step-by-step description of how to get the development environment set and running.

5. How to Use the Project
Provide instructions and examples so users/contributors can use the project. This will make it easy for them in case they encounter a problem – they will always have a place to reference what is expected.

You can also make use of visual aids by including materials like screenshots to show examples of the running project and also the structure and design principles used in your project.

Also if your project will require authentication like passwords or usernames, this is a good section to include the credentials.


