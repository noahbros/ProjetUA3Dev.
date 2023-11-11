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
        public ObservableCollection<Stagiaire> listesStagiaires = new ObservableCollection<Stagiaire>();
        public ObservableCollection<Programme> listeDeProgramme = new ObservableCollection<Programme>();

        public TabStagiaireData()
        {
            InitializeComponent();
            listeStagiaire.ItemsSource = listesStagiaires;
            listeDeProgramme = TabProgrammeData.listesProgrammes;
            programmeEtudiant.ItemsSource = listeDeProgramme;
        }

        private void Btn_AugmenteNumEtudiant_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int numeroEtudiant = int.Parse(NumeroEtudiant.Text);
                numeroEtudiant++;
                NumeroEtudiant.Text = numeroEtudiant.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Le numéro d'étudiant doit être un nombre entier.");
            }
        }

        private void Btn_DecrementeNumEtudiant_Click(object sender, RoutedEventArgs e)//Diminue le numero etudiant de -1
        {
            try
            {
                if (NumeroEtudiant.Text.Length == 7 && int.TryParse(NumeroEtudiant.Text, out int numeroEtudiant))
                {
                    if (numeroEtudiant > 0)
                    {
                        numeroEtudiant--;
                        NumeroEtudiant.Text = numeroEtudiant.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Le numéro d'étudiant ne peut pas être négatif.");
                    }
                }
                else
                {
                    MessageBox.Show("Le numéro d'étudiant doit être un nombre entier de 7 chiffres.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            private void Btn_Ajouter_Click(object sender, RoutedEventArgs e)
            {
                string prenomAjouter;
                string nomDeFamilleAjouter;
                int numeroEtudiantAjouter;
                string dateDeNaissanceAjouter;
                string sexeAjouter;
                string nomProgrammeAjouter;

                if (dateDeNaissanceAjouter == null)
                {
                    champsNonRemplis++;
                }

                numeroEtudiantAjouter = int.Parse(NumeroEtudiant.Text);
                prenomAjouter = prenomEtudiant.Text;
                nomDeFamilleAjouter = nomEtudiant.Text;
                dateDeNaissanceAjouter = dateNaissanceEtudiant.Text;
                nomProgrammeAjouter = programmeEtudiant.Text;

                if (sexeHomme.IsChecked == true)
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
                        listesStagiaires.Add(new Stagiaire { Prenom = prenomAjouter, NomDeFamille = nomDeFamilleAjouter, NumeroEtudiant = numeroEtudiantAjouter, DateDeNaissance = dateDeNaissanceAjouter, Sexe = sexeAjouter, NomDeProgramme = nomProgrammeAjouter });
                        listeStagiaire.ItemsSource = listesStagiaires;
                        //Resets data.
                        NumeroEtudiant.Text = "0";
                        prenomEtudiant.Text = "";
                        nomEtudiant.Text = "";
                        dateNaissanceEtudiant.SelectedDate = DateTime.Today.Date;
                        sexeHomme.IsChecked = false;
                        sexeFemme.IsChecked = false;
                        sexeAutre.IsChecked = false;
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
}


