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
            listeStagiaire.ItemsSource = listesStagiaires;
            listeDeProgramme = TabProgrammeData.listesProgrammes;
            programmeEtudiant.ItemsSource = listeDeProgramme;
        }
        private void NumeroEtudiant_GotFocus(object sender, RoutedEventArgs e) //Lorsque l'utilisateur clique sur le textBox NumeroEtudiant le default texte "0" va disparaitre
        {
            if (NumeroEtudiant.Text == "0")
            {
                NumeroEtudiant.Text = "";
            }
        }
        private void NumeroEtudiant_LostFocus(object sender, RoutedEventArgs e) //Lorsque l'utilisateur sort du textBox NumeroEtudiant s'il n'y a pas eu de donnee entre la valeur par defaut "0" va s'afficher
        {
            if (string.IsNullOrWhiteSpace(NumeroEtudiant.Text))
            {
                NumeroEtudiant.Text = "0";
            }
        }

        private void Btn_AugmenteNumEtudiant_Click(object sender, RoutedEventArgs e) //Le NumeroEtudiant va augmenter de +1  seulement si le numero est inferieur a 9999999 et est un entier
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

        private void Btn_DecrementeNumEtudiant_Click(object sender, RoutedEventArgs e) //Le NumeroEtudiant va diminuer de -1 seulement si le numero est superieur a 1000000 et un entier
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
        private void Btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            int champsNonRemplis =0;

            //int numeroEtudiantAjouter;
            string prenomAjouter;
            string nomDeFamilleAjouter;
            string dateDeNaissanceAjouter;                                                               
            string nomProgrammeAjouter;         
            string sexeAjouter;

            //Verification de la date de naissance
            DateTime? dateNaissance = dateNaissanceEtudiant.SelectedDate; 
            if (dateNaissance != null)
            {                
                DateTime dateNaissanceValue = dateNaissance.Value;
                DateTime dateAujourdhui = DateTime.Today;

                if(dateNaissanceValue > dateAujourdhui)
                {
                    MessageBox.Show("La date de naissance ne peut pas être dans le future.");
                    return;
                } 
            }
            else
            {
                champsNonRemplis++;
                MessageBox.Show("Veuillez saisir une date de naissance.");
                    return;
            }

            
            //numeroEtudiantAjouter = int.Parse(NumeroEtudiant.Text);


            prenomAjouter = prenomEtudiant.Text;
            nomDeFamilleAjouter = nomEtudiant.Text;
            dateDeNaissanceAjouter = dateNaissanceEtudiant.Text;
            nomProgrammeAjouter = programmeEtudiant.Text;

            //Saisir le sexe pour l'affichage
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

            //Verification des champs obligatoires
            if (!string.IsNullOrWhiteSpace(NumeroEtudiant.Text) &&
                !string.IsNullOrWhiteSpace(prenomEtudiant.Text) &&
                !string.IsNullOrWhiteSpace(nomEtudiant.Text) &&
                !string.IsNullOrWhiteSpace(programmeEtudiant.Text) &&
                dateNaissanceEtudiant.SelectedDate != null) 
            {
                //Verification du format du NumeroEtudiant
                if (NumeroEtudiant.Text.Length != 7 || !int.TryParse(NumeroEtudiant.Text, out int numeroEtudiant))
                {
                    MessageBox.Show("Le numéro d'étudiant doit être un nombre entier de 7 chiffres.");  
                    return;
                }
                else
                {
                    //Ajout du nouveau stagiaire a la liste
                    listesStagiaires.Add(new Stagiaire { Prenom = prenomAjouter, NomDeFamille = nomDeFamilleAjouter, NumeroEtudiant = numeroEtudiant, DateDeNaissance = dateDeNaissanceAjouter, Sexe = sexeAjouter, NomDeProgramme = nomProgrammeAjouter });
                    listeStagiaire.ItemsSource = listesStagiaires;

                    //Resets data.
                    NumeroEtudiant.Text = "0";
                    prenomEtudiant.Text = "";
                    nomEtudiant.Text = "";
                    dateNaissanceEtudiant.SelectedDate = null;
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
        //Afficher tout les programmes ajouter dans l'onglet Programme a la ComboBox du choix de programme
        private void programmeEtudiant_DropDownOpened(object sender, EventArgs e)
        {
            programmeEtudiant.ItemsSource = TabProgrammeData.listesProgrammes.Select(p => p.Nom);

            if (programmeEtudiant.HasItems == false)
            {
                programmeEtudiant.IsDropDownOpen = false;
                return;
            }

        }
        //Efface les donnees dans les champs
        private void Btn_Effacer_Click(object sender, RoutedEventArgs e)
        {

            if (listeStagiaire.SelectedItem != null) // Efface un seule stagiaire saisi dans la listView
            {
                //on obtient la stagiaire à effacer
                Stagiaire stagiaireAEffacer = (Stagiaire)listeStagiaire.SelectedItem;

                String message = "Voulez-vous effacer «" + stagiaireAEffacer.Prenom + " " + stagiaireAEffacer.NomDeFamille + "» de la liste des stagiaires";

                MessageBoxResult result = MessageBox.Show(message, "Message de confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    listesStagiaires.Remove(stagiaireAEffacer);
                }
            }
            else         //Efface tous les stagiaires dans la listView si aucun stagiaire n'a ete saisi
            {
                //On confirme que l'utilisateur veut bel et bien effacer la liste
                MessageBoxResult result = MessageBox.Show("Voulez-vous effacer la liste des stagiaires existants?", "Message de confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    NumeroEtudiant.Text = "0";
                    prenomEtudiant.Text = "";
                    nomEtudiant.Text = "";
                    dateNaissanceEtudiant.SelectedDate = null;
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
}


