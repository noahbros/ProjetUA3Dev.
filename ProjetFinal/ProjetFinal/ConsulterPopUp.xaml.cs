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
        List<Programme> infoProgramme = new ObservableCollection<Programme>(TabProgrammeData.listesProgrammes).ToList();
        List<Stagiaire> infoStagiaire = new ObservableCollection<Stagiaire>(TabStagiaireData.listesStagiaires).ToList();
        ObservableCollection<Stagiaire> searchCollectionComparer = new ObservableCollection<Stagiaire>(TabConsulterData.searchCollection);
        int index = TabConsulterData.lvConsulterIndex;
        public ConsulterPopUp()
        {
            InitializeComponent();

            string nom;
            string prenom;
            string nomDeProgramme;

            if(searchCollectionComparer.Count > 0)
            {
                nom = searchCollectionComparer[index].NomDeFamille;
                prenom = searchCollectionComparer[index].Prenom;
                nomDeProgramme = searchCollectionComparer[index].NomDeProgramme;
            }
            else
            {
                nom = TabStagiaireData.listesStagiaires[index].NomDeFamille;
                prenom = TabStagiaireData.listesStagiaires[index].Prenom;
                nomDeProgramme = TabStagiaireData.listesStagiaires[index].NomDeProgramme;
            }

            Predicate<Programme> validProgramme = x => x.Nom == nomDeProgramme;
            Predicate<Stagiaire> validStagiaire = x => x.NomDeFamille == nom && x.Prenom == prenom;


            Programme p = infoProgramme.Find(validProgramme);

            NomProgrammeLabel.Content = p.Nom;
            NumeroProgrammeLabel.Content = p.Numero;
            DureeProgrammeLabel.Content = p.Duree + " moi(s)";

            Stagiaire s = infoStagiaire.Find(validStagiaire);

            PrenomLabel.Content = s.Prenom;
            NomLabel.Content = s.NomDeFamille;
            NaissanceLabel.Content = s.DateDeNaissance.ToString();
            NumeroEtudiantLabel.Content = s.NumeroEtudiant.ToString();
            SexeLabel.Content = s.Sexe;
        }
    }
}
