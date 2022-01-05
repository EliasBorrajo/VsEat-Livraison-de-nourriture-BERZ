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
            <td>la.pouette@dies.irae</td>
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
            <td>rz@dies.irae</td
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


<h2>Crédits</h2>
<h2>Licence</h2>


3. Table of Contents (Optional)
If your README is very long, you might want to add a table of contents to make it easy for users to navigate to different sections easily. It will make it easier for readers to move around the project with ease.

4. How to Install and Run the Project
If you are working on a project that a user needs to install or run locally in a machine like a "POS", you should include the steps required to install your project and also the required dependencies if any.

Provide a step-by-step description of how to get the development environment set and running.

5. How to Use the Project
Provide instructions and examples so users/contributors can use the project. This will make it easy for them in case they encounter a problem – they will always have a place to reference what is expected.

You can also make use of visual aids by including materials like screenshots to show examples of the running project and also the structure and design principles used in your project.

Also if your project will require authentication like passwords or usernames, this is a good section to include the credentials.

6. Include Credits
If you worked on the project as a team or an organization, list your collaborators/team members. You should also include links to their GitHub profiles and social media too.

Also, if you followed tutorials or referenced a certain material that might help the user to build that particular project, include links to those here as well.

This is just a way to show your appreciation and also to help others get a first hand copy of the project.

7. Add a License
For most README files, this is usually considered the last part. It lets other developers know what they can and cannot do with your project.

We have different types of licenses depending on the kind of project you are working on. Depending on the one you will choose it will determine the contributions your project gets.

The most common one is the GPL License which allows other to make modification to your code and use it for commercial purposes. If you need help choosing a license, use check out this link: https://choosealicense.com/

Up to this point what we have covered are the minimum requirements for a good README. But you might also want to consider adding the following sections to make it more eye catching and interactive.
