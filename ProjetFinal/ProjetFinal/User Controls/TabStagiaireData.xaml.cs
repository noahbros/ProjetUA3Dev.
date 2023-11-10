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
        public string NomDeProgramme { get; set; }
    }

    /// <summary>
    /// Interaction logic for TabStagiaireData.xaml
    /// </summary>
    public partial class TabStagiaireData : UserControl
    {
        public static ObservableCollection<Stagiaire> listesStagiaires = new ObservableCollection<Stagiaire>();
        public ObservableCollection<Programme> listeDeProgramme = new ObservableCollection<Programme>();

        public TabStagiaireData()
        {
            InitializeComponent();            
        }

        private void Btn_AugmenteNumEtudiant_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int numeroEtudiant = int.Parse(NumeroEtudiant.Text);
                numeroEtudiant++; // Incrémente le numéro d'étudiant de 1
                NumeroEtudiant.Text = numeroEtudiant.ToString(); // Met à jour la zone de texte avec la nouvelle valeur
            }
            catch (FormatException)
            {
                MessageBox.Show("Le numéro d'étudiant doit être un nombre entier.");
            }

        }

        private void Btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            string prenomAjouter;
            string nomDeFamilleAjouter;
            int numeroEtudiantAjouter;
            string dateDeNaissanceAjouter;
            string sexeAjouter;
            string nomProgrammeAjouter;


            numeroEtudiantAjouter = int.Parse(NumeroEtudiant.Text);
            prenomAjouter = prenomEtudiant.Text;
            nomDeFamilleAjouter = nomEtudiant.Text;
            dateDeNaissanceAjouter = dateNaissanceEtudiant.Text;
            nomProgrammeAjouter = programmeEtudiant.Text;

            if(sexeHomme.IsChecked == true)
            {
                sexeAjouter = "M";
            }
            else if (sexeFemme.IsChecked == true)
            {
                sexeAjouter = "F";
            }
            else
            {
                sexeAjouter = "Autres";
            }


            if (!string.IsNullOrWhiteSpace(NumeroEtudiant.Text) && //Regarder pour le vrai code pour # etudiant
                !string.IsNullOrWhiteSpace(prenomEtudiant.Text) &&
                !string.IsNullOrWhiteSpace(nomEtudiant.Text) &&
                dateNaissanceEtudiant.SelectedDate != null) //Acceder au sexe de l'etudiant et au programme
            {
                int numEtudiant = Convert.ToInt32(NumeroEtudiant.Text);
                if (numEtudiant < 0)
                {
                    MessageBox.Show("Numéro étudiant entré invalide");
                    // Sinon creer une espace pour mettre le message d'erreur dans la grille
                }
                else
                {
                    listesStagiaires.Add(new Stagiaire { Prenom = prenomAjouter, NomDeFamille = nomDeFamilleAjouter, NumeroEtudiant = numeroEtudiantAjouter, DateDeNaissance = dateDeNaissanceAjouter, Sexe = sexeAjouter, NomDeProgramme = nomProgrammeAjouter});
                    listeStagiaire.ItemsSource = listesStagiaires;
                    //Resets data.
                    NumeroEtudiant.Text = "";
                    prenomEtudiant.Text = "";
                    nomEtudiant.Text = "";
                    dateNaissanceEtudiant.SelectedDate = DateTime.Today.Date;
                    sexeHomme.IsChecked = false;
                    sexeFemme.IsChecked = false;
                    sexeAutre.IsChecked = false;


                    // Ne pas oublier de mettre la variable recevente en .Text pour permettre la concatenation
                    //Resultat.Text += Nom_Prenom.Text + " , " + Date_Naissance.SelectedDate + " , " + Nom_Cour.Text + " , " + Note_Finale.Text + "%" + "\n";
                    // Avec le += les resultats vont s'accumuler dans la zone de texte sinon avec juste = a chaque ajout on va ecraser la valeur precedente
                }
            }
            else
            {
                MessageBox.Show("Il y a des champs vide. Veuillez remplir toutes le champs demander.");
            }
        }

        private void Btn_Effacer_Click(object sender, RoutedEventArgs e)
        {
            listesStagiaires.Clear();
            listeStagiaire.ItemsSource = listesStagiaires;

        }

        private void programmeEtudiant_DropDownOpened(object sender, EventArgs e)
        {
            programmeEtudiant.ItemsSource = TabProgrammeData.listesProgrammes.Select(p => p.Nom);

            if (programmeEtudiant.HasItems == false)
            {
                programmeEtudiant.IsDropDownOpen = false;
                return;
            }

        }
    }
}


