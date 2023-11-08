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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetFinal.User_Controls
{


    public class Stagiaire
    {
        public string Prenom { get; set; }
        public string NomDeFamille { get; set; }
        public int NumeroEtudiant { get; set; }
        public string DateDeNaissance { get; set; }
        public string Sexe { get; set; }
        public string Programme { get; set; }
    }

    /// <summary>
    /// Interaction logic for TabStagiaireData.xaml
    /// </summary>
    public partial class TabStagiaireData : UserControl
    {
        public ObservableCollection<Stagiaire> listesStagiaires = new ObservableCollection<Stagiaire>();
        public ObservableCollection<Programme> listeDeProgramme = new ObservableCollection<Programme>();

        public TabStagiaireData()
        {
            InitializeComponent();
            listeStagiaire.ItemsSource = listesStagiaires;
            listeDeProgramme = TabProgrammeData.listesProgrammes;
            programmeEtudiant.ItemsSource = listeDeProgramme;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Your event handling code goes here
            // You can handle the selection change in the ListView here
        }


        private void Btn_AugmenteNumEtudiant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_DecrementeNumEtudiant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            string prenomAjouter;
            string nomDeFamilleAjouter;
            int numeroEtudiantAjouter;
            string dateDeNaissanceAjouter;
            string sexeAjouter;
            Programme programmeAjouter; 


            numeroEtudiantAjouter = int.Parse(NumeroEtudiant.Text);
            prenomAjouter = prenomEtudiant.Text;
            nomDeFamilleAjouter = nomEtudiant.Text;
            dateDeNaissanceAjouter = dateNaissanceEtudiant.Text;


        }

        private void Btn_Effacer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
