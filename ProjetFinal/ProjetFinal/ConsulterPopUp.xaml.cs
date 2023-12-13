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
        ObservableCollection<Stagiaire> searchCollectionComparer = new ObservableCollection<Stagiaire>(TabConsulterData.searchCollection); //Collection de référence pour la collection de filtrage dans consulter.cs
        Stagiaire stagiaire = TabConsulterData.stagiaireSelectioner;
        Programme programme = TabConsulterData.programmeSelectionner;


        public ConsulterPopUp()
        {
            InitializeComponent();

            string nom;
            string prenom;
            string nomDeProgramme;


            nom = stagiaire.NomDeFamille;
            prenom = stagiaire.Prenom;
            nomDeProgramme = programme.Nom;

            //on met à jour le nom de la fenêtre
            this.Title = "Consulter: " + prenom + " " + nom;

            //Cherche le programme et l'affiche.
            Programme p = programme;

            NomProgrammeLabel.Content = p.Nom;
            NumeroProgrammeLabel.Content = p.Numero;
            DureeProgrammeLabel.Content = p.Duree + " moi(s)";

            //Cherche le stagiaire et l'affiche.
            Stagiaire s = stagiaire;

            PrenomLabel.Content = s.Prenom;
            NomLabel.Content = s.NomDeFamille;
            NaissanceLabel.Content = s.DateDeNaissance.ToString();
            NumeroEtudiantLabel.Content = s.NumeroEtudiant.ToString();
            SexeLabel.Content = s.Sexe;
        }
    }
}
