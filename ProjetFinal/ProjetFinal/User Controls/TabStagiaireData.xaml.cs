using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        public static DataTable tableData_Stagiaires = new DataTable();
        public static String ServerHostname = "localhost"; //hostname de la base de donnéem "localhost" dans la majorité des cas

        //Liaison de la base de données
        private void liaisonBaseDonnee()
        {
            using (MySqlConnection connection = new MySqlConnection("SERVER="+ServerHostname +";DATABASE=projetfinaldev;UID=root;convert zero datetime=True"))
            {
                try
                {
                    MySqlCommand getAllStagiaires = new MySqlCommand("select * from stagiaires", connection);
                    connection.Open();
                    DataTable tableData_Stagiaires = new DataTable();
                    tableData_Stagiaires.Load(getAllStagiaires.ExecuteReader());
                    listesStagiaires.Clear();

                    foreach (DataRow row in tableData_Stagiaires.Rows)
                    {
                        listesStagiaires.Add(new Stagiaire
                        {
                            NumeroEtudiant = Convert.ToInt32(row["numeroStagiaire"]),
                            NomDeProgramme = row["programmeId"].ToString(),
                            Prenom = row["prenom"].ToString(),
                            NomDeFamille = row["nom"].ToString(),
                            DateDeNaissance = Convert.ToDateTime(row["naissance"]).ToString("dd/MM/yyyy"),
                            Sexe = row["sexe"].ToString()
                        });
                    }
                    listeStagiaire.ItemsSource = listesStagiaires;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la connection à la base de donnée : {ex.Message}");
                }

            }

        }

        public TabStagiaireData()
        {
            InitializeComponent();
            listeStagiaire.ItemsSource = listesStagiaires;
            listeDeProgramme = TabProgrammeData.listesProgrammes;
            programmeEtudiant.ItemsSource = listeDeProgramme;
            liaisonBaseDonnee();
            listeStagiaire.AlternationCount = 2;
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
                MessageBox.Show("Une erreur c'est produite : " + ex.Message);
            }
        }
        private void Btn_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            int champsNonRemplis = 0;

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

                if (dateNaissanceValue > dateAujourdhui)
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

                    //Vérifie si le numéro de stagiaire est déjà dans la BDD.
                    MySqlConnection connecter = new MySqlConnection("SERVER="+ ServerHostname + ";DATABASE=projetfinaldev;UID=root;convert zero datetime=True");
                    connecter.Open();
                    MySqlCommand verificationUnique = new MySqlCommand();

                    verificationUnique.CommandText = "SELECT * FROM stagiaires WHERE numeroStagiaire = @numero";
                    verificationUnique.Parameters.AddWithValue("@numero", numeroEtudiant);
                    verificationUnique.Connection = connecter;
                    var verifier = verificationUnique.ExecuteScalar();
                    if (verifier != null)
                    {
                        MessageBox.Show("Une des valeurs dans les champs contient une valeur déjà existante dans la BDD, veuillez réessayer.", "Error 200 : Duplicate variables", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    MySqlCommand chercherNumeroProgramme = new MySqlCommand("SELECT numeroProgramme FROM programmes WHERE nom = @nomProgramme", connecter);
                    chercherNumeroProgramme.Parameters.AddWithValue("@nomProgramme", nomProgrammeAjouter);

                    int programmeId = Convert.ToInt32(chercherNumeroProgramme.ExecuteScalar());

                    //Rajoute les données dans la BDD
                    MySqlCommand addStagiaire = new MySqlCommand();
                    addStagiaire.CommandText = "INSERT INTO stagiaires(numeroStagiaire, prenom, nom, naissance, sexe, programmeId ) VALUES (@numeroStagiaire, @prenom, @nom, @naissance, @sexe, @programmeId)";
                    addStagiaire.Parameters.AddWithValue("@numeroStagiaire", numeroEtudiant);
                    addStagiaire.Parameters.AddWithValue("@prenom", prenomAjouter);
                    addStagiaire.Parameters.AddWithValue("@nom", nomDeFamilleAjouter);
                    addStagiaire.Parameters.AddWithValue("@naissance", dateNaissanceEtudiant.SelectedDate?.ToString("yyyy-MM-dd"));
                    addStagiaire.Parameters.AddWithValue("@sexe", sexeAjouter);
                    addStagiaire.Parameters.AddWithValue("@programmeId", programmeId);
                    addStagiaire.Connection = connecter;
                    int success = addStagiaire.ExecuteNonQuery();
                    if (success == 1)
                    {
                        MessageBox.Show("Les données ont été ajoutées avec succès.", "Succès");
                        liaisonBaseDonnee();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'ajout à la base de données.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

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
            try
            {
                using (MySqlConnection connection = new MySqlConnection("SERVER=" + ServerHostname + ";DATABASE=projetfinaldev;UID=root;"))
                {
                    connection.Open();
                    MySqlCommand requeteNomProgramme = new MySqlCommand("SELECT Nom FROM programmes", connection);
                    List<string> programmeNoms = new List<string>();

                    using (var reader = requeteNomProgramme.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            programmeNoms.Add(reader.GetString(0));
                        }
                    }
                    programmeEtudiant.ItemsSource = programmeNoms;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche des noms des programmes : {ex.Message}");
            }

            //programmeEtudiant.ItemsSource = TabProgrammeData.listesProgrammes.Select(p => p.Nom);

            if (programmeEtudiant.HasItems == false)
            {
                programmeEtudiant.IsDropDownOpen = false;
                return;
            }

        }
        //Efface les donnees dans les champs
        private void Btn_Effacer_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("SERVER=" + ServerHostname + ";DATABASE=projetfinaldev;UID=root;PASSWORD=");
            connection.Open();


            if (listeStagiaire.SelectedItem != null) // Efface un seule stagiaire saisi dans la listView
            {

                if (listeStagiaire.SelectedItem is Stagiaire stagiaireAEffacer)
                {
                    String message = "Voulez-vous effacer «" + stagiaireAEffacer.Prenom + " " + stagiaireAEffacer.NomDeFamille + "» de la liste des stagiaires et de la base de donnée ? ";

                    MessageBoxResult result = MessageBox.Show(message, "Message de confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {

                        MySqlCommand effacerStagiaireChoisi = new MySqlCommand();
                        effacerStagiaireChoisi.CommandText = "DELETE FROM stagiaires WHERE numeroStagiaire = @numeroStagiaire";
                        effacerStagiaireChoisi.Parameters.AddWithValue("@numeroStagiaire", stagiaireAEffacer.NumeroEtudiant);
                        effacerStagiaireChoisi.Connection = connection;
                        effacerStagiaireChoisi.ExecuteNonQuery();
                        liaisonBaseDonnee();

                        listesStagiaires.Remove(stagiaireAEffacer);
                    }


                }
                else         //Efface tous les stagiaires dans la listView si aucun stagiaire n'a ete saisi
                {
                    //On confirme que l'utilisateur veut bel et bien effacer la liste
                    MessageBoxResult result = MessageBox.Show("Voulez-vous effacer la liste des stagiaires existants ? Vous effacerez tout les stagiaires dans la base de donnée aussi.", "Message de confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

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

                        MySqlCommand effacerToutLesStagiaires = new MySqlCommand();
                        effacerToutLesStagiaires.CommandText = "DELETE FROM stagiaires";
                        effacerToutLesStagiaires.Connection = connection;
                        effacerToutLesStagiaires.ExecuteNonQuery();
                        liaisonBaseDonnee();
                    }

                }


            }
        }
    }
}




