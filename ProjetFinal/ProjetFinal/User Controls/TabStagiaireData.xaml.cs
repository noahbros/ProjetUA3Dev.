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
        public static ObservableCollection<Stagiaire> listesStagiaires = new ObservableCollection<Stagiaire>(); ///Liste statique contenant tout les entrées (objet Stagiaire) de stagiaires.
        public ObservableCollection<Programme> listeDeProgramme = new ObservableCollection<Programme>(); ///Liste de réference pour la liste statique dans programme.cs.

        public TabStagiaireData()
        {
            InitializeComponent();
            listeStagiaire.ItemsSource = listesStagiaires; //Liste d'affichage dans la tab stagiaire.
            listeDeProgramme = TabProgrammeData.listesProgrammes; //Copie la liste dans programme pour être utiliser plus tard.
            programmeEtudiant.ItemsSource = listeDeProgramme; //ComboBox contenant l'information des entrées dans programmes en utilisant la liste de réference.
        }
        private void NumeroEtudiant_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NumeroEtudiant.Text == "0")
            {
                NumeroEtudiant.Text = "";
            }
        }
        private void NumeroEtudiant_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NumeroEtudiant.Text))
            {
                NumeroEtudiant.Text = "0";
            }
        }

        private void Btn_AugmenteNumEtudiant_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int numeroEtudiant = int.Parse(NumeroEtudiant.Text);
                if (numeroEtudiant == 9999999)
                {
                    MessageBox.Show("Le numéro d'étudiant doit être un nombre de 7 chiffre.");
                }
                else
                {
                    numeroEtudiant++;
                    NumeroEtudiant.Text = numeroEtudiant.ToString();
                }  
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
                    if (numeroEtudiant > 1000000)
                    {
                        numeroEtudiant--;
                        NumeroEtudiant.Text = numeroEtudiant.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Le numéro étudiant doit être un nombre entier de 7 chiffres.");
                    }
                }
                else
                {
                    MessageBox.Show("Le numéro étudiant doit être un nombre entier de 7 chiffres.");                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur c'est produite : "+ex.Message);
            }
        }

        //Fonctionalité du boutton "Ajouté" dans stagiaires.
        private void Btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            int champsNonRemplis =0;

            int numeroEtudiantAjouter = int.Parse(NumeroEtudiant.Text);  // Il y a une exception unhandled
            string prenomAjouter = prenomEtudiant.Text;
            string nomDeFamilleAjouter = nomEtudiant.Text;
            string dateDeNaissanceAjouter = dateNaissanceEtudiant.Text;                                                               
            string nomProgrammeAjouter = programmeEtudiant.Text;         
            string sexeAjouter;                                          

            //Reset la date après avoir pesé le boutton.
            if (dateDeNaissanceAjouter != null)
            {
                DateTime dateNaissance = dateNaissanceEtudiant.SelectedDate.Value;
                DateTime dateAujourdhui = DateTime.Today;

                if(dateNaissance > dateAujourdhui)
                {
                    MessageBox.Show("La date de naissance ne peut pas être dans le future.");
                    return;
                } 
            }
            else
            {
                champsNonRemplis++;
            }

            //Associe les données des entrées dans stagiaires à une variable temporaire.
            numeroEtudiantAjouter = int.Parse(NumeroEtudiant.Text);
            prenomAjouter = prenomEtudiant.Text;
            nomDeFamilleAjouter = nomEtudiant.Text;
            dateDeNaissanceAjouter = dateNaissanceEtudiant.Text;
            nomProgrammeAjouter = programmeEtudiant.Text;

            //Condition pour le sexe.
            if (sexeHomme.IsChecked == true)
            {
                sexeAjouter = "Homme";
            }
            else if (sexeFemme.IsChecked == true)
            {
                sexeAjouter = "Femme";
            }
            else
            {
                sexeAjouter = "Autres";
            }


            if (!string.IsNullOrWhiteSpace(NumeroEtudiant.Text) &&
                !string.IsNullOrWhiteSpace(prenomEtudiant.Text) &&
                !string.IsNullOrWhiteSpace(nomEtudiant.Text) &&
                !string.IsNullOrWhiteSpace(programmeEtudiant.Text) &&
                dateNaissanceEtudiant.SelectedDate != null) 
            {
                //int numEtudiant = Convert.ToInt32(NumeroEtudiant.Text);
                if (NumeroEtudiant.Text.Length != 7 || !int.TryParse(NumeroEtudiant.Text, out int numeroEtudiant))
                {
                    MessageBox.Show("Le numéro d'étudiant doit être un nombre entier de 7 chiffres.");  
                    return;
                }
                else
                {
                    listesStagiaires.Add(new Stagiaire { Prenom = prenomAjouter, NomDeFamille = nomDeFamilleAjouter, NumeroEtudiant = numeroEtudiantAjouter, DateDeNaissance = dateDeNaissanceAjouter, Sexe = sexeAjouter, NomDeProgramme = nomProgrammeAjouter }); //Utilise les variables précédente pour faire un objet Stagiaire et la stocke dans la liste de stagiaire.
                    listeStagiaire.ItemsSource = listesStagiaires; //Affiche le résultat.
                    //Resets data.
                    NumeroEtudiant.Text = "0";
                    prenomEtudiant.Text = "";
                    nomEtudiant.Text = "";
                    dateNaissanceEtudiant.SelectedDate = DateTime.Today.Date;
                    programmeEtudiant.SelectedItem = null;
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
        //Ajouter tout les programmes ajouter dans l'onglet Programme a la ComboBox du choix de programme
        private void programmeEtudiant_DropDownOpened(object sender, EventArgs e)
        {
            programmeEtudiant.ItemsSource = TabProgrammeData.listesProgrammes.Select(p => p.Nom);

            //Si il ne contient aucun item dans la liste de réference, le ComboBox ne peut être ouvert.
            if (programmeEtudiant.HasItems == false)
            {
                programmeEtudiant.IsDropDownOpen = false;
                return;
            }

        }

        //Fonctionalité pour le boutton "supprimer" dans stagiaires.
        private void Btn_Effacer_Click(object sender, RoutedEventArgs e)
        {
            //Vérifie si une sélection a été fait.
            if(listeStagiaire.SelectedItem != null)
            {
                Stagiaire stagiaireAEffacer = (Stagiaire)listeStagiaire.SelectedItem;

                listesStagiaires.Remove(stagiaireAEffacer);
            }
            //Sinon efface tout la liste et reset tout les.
            else
            {
                NumeroEtudiant.Text = "0";
                prenomEtudiant.Text = "";
                nomEtudiant.Text = "";
                dateNaissanceEtudiant.SelectedDate = DateTime.Today.Date;
                programmeEtudiant.SelectedItem = null;
                sexeHomme.IsChecked = false;
                sexeFemme.IsChecked = false;
                sexeAutre.IsChecked = false;
                listesStagiaires.Clear();
                listeStagiaire.ItemsSource = listesStagiaires;
            }
            

        }
    }
}


