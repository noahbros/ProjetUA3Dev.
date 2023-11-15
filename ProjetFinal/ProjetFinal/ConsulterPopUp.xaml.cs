using ProjetFinal.User_Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjetFinal
{
    /// <summary>
    /// Interaction logic for ConsulterPopUp.xaml
    /// </summary>
    public partial class ConsulterPopUp : Window
    {
        List<Programme> infoProgramme = new ObservableCollection<Programme>(TabProgrammeData.listesProgrammes).ToList(); //Liste de référence pour la collection statique de programme dans programme.cs
        List<Stagiaire> infoStagiaire = new ObservableCollection<Stagiaire>(TabStagiaireData.listesStagiaires).ToList(); //Liste de référence pour la collection statique de stagiaires dans stagiaires.cs
        ObservableCollection<Stagiaire> searchCollectionComparer = new ObservableCollection<Stagiaire>(TabConsulterData.searchCollection); //Collection de référence pour la collection de filtrage dans consulter.cs
        int index = TabConsulterData.lvConsulterIndex; //Integer de référence de la variable integer statique dans consulter.cs (elle est utilisé pour trouver l'index où l'utilisateur à pesé sur la listView pour avoir plus d'information)
        public ConsulterPopUp()
        {
            InitializeComponent();

            string nom;
            string prenom;
            string nomDeProgramme;

            if(searchCollectionComparer.Count > 0) //Vérifie que la collection de filtrage n'est pas vide. Si oui elle prend la liste de filtrage comme référence
            {
                nom = searchCollectionComparer[index].NomDeFamille;
                prenom = searchCollectionComparer[index].Prenom;
                nomDeProgramme = searchCollectionComparer[index].NomDeProgramme;
            }
            else //Sinon elle prend la liste de Stagiaires comme référence.
            {
                nom = TabStagiaireData.listesStagiaires[index].NomDeFamille;
                prenom = TabStagiaireData.listesStagiaires[index].Prenom;
                nomDeProgramme = TabStagiaireData.listesStagiaires[index].NomDeProgramme;
            }

            //Predicat créer pour plus tard trouver l'informations associés à l'index pesé.
            Predicate<Programme> validProgramme = x => x.Nom == nomDeProgramme; 
            Predicate<Stagiaire> validStagiaire = x => x.NomDeFamille == nom && x.Prenom == prenom;

            //on met à jour le nom de la fenêtre
            this.Title = "Consulter: " + prenom + " " + nom;

            //Cherche le programme et l'affiche.
            Programme p = infoProgramme.Find(validProgramme); 

            NomProgrammeLabel.Content = p.Nom;
            NumeroProgrammeLabel.Content = p.Numero;
            DureeProgrammeLabel.Content = p.Duree + " moi(s)";

            //Cherche le stagiaire et l'affiche.
            Stagiaire s = infoStagiaire.Find(validStagiaire);

            PrenomLabel.Content = s.Prenom;
            NomLabel.Content = s.NomDeFamille;
            NaissanceLabel.Content = s.DateDeNaissance.ToString();
            NumeroEtudiantLabel.Content = s.NumeroEtudiant.ToString();
            SexeLabel.Content = s.Sexe;
        }
    }
}
