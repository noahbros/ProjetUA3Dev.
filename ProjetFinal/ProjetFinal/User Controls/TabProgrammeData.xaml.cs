using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// <summary>
    /// Interaction logic for TabProgrammeData.xaml
    /// </summary>
    /// 
    public class Programme
    {
        public int Numero{ get; set; }
        public string Nom { get; set; } 
        public int Duree { get; set; }
    }
    public partial class TabProgrammeData : UserControl
    {
        public static ObservableCollection<Programme> listesProgrammes = new ObservableCollection<Programme>(); //Collection statique qui stocke tout les entrées (objet Programme) de programmes.
        public static DataTable dt_programme = new DataTable();
        public static String ServerHostname = "192.168.2.19";

        //Liaison de la base de données
        private void linkdb()
        {
            MySqlConnection connection = new MySqlConnection("SERVER="+ServerHostname + ";DATABASE=projetfinaldev;UID=root;PASSWORD=");
            MySqlCommand getAll = new MySqlCommand("select * from programmes", connection);
            connection.Open();
            DataTable dt_programme = new DataTable();
            dt_programme.Load(getAll.ExecuteReader());
            dataGrid_programmes.ItemsSource = dt_programme.DefaultView;
            connection.Close();
        }


        public TabProgrammeData()
        {
            InitializeComponent();
            linkdb();

        }
        
        ///Fonctionalité pour le boutton "Ajouter" dans la tab "programmes".
        public void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            int numeroProgramme;
            int moisProgramme;
            string nomProgramme;

            //Checks empty fields
            if (Numero.Text == "" || Nom.Text == "" || Mois.Text == "")
            {
                MessageBox.Show("S.V.P remplire tout les champs.", "Error 100 : Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //on assigne une valeur à nomProgramme
            nomProgramme = Nom.Text;

            //Checks non-allowed values, and creates an object of the new Programme if the values are accepted.
            if (int.TryParse(Numero.Text, out numeroProgramme) && int.TryParse(Mois.Text, out moisProgramme))
            {
                if(numeroProgramme.ToString().Count() < 7 || numeroProgramme.ToString().Count() > 7 || moisProgramme <= 0 || moisProgramme > 60 || !nomProgramme.All(Char.IsLetter)) //Specific constraints for variables.
                {
                    MessageBox.Show("S.V.P respecter tout les contraintes imposer pour chaques champs", "Error 101 : Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                
                //Vérifie si le numéro de programme est déjà dans la BDD.
                MySqlConnection conn = new MySqlConnection("SERVER="+ServerHostname+";DATABASE=projetfinaldev;UID=root;PASSWORD=");
                conn.Open();
                MySqlCommand uniqueChecker = new MySqlCommand();

                uniqueChecker.CommandText = "SELECT * FROM programmes WHERE numeroProgramme = @numero";
                uniqueChecker.Parameters.AddWithValue("@numero", numeroProgramme);
                uniqueChecker.Connection = conn;
                var checker = uniqueChecker.ExecuteScalar();
                if(checker != null)
                {
                    MessageBox.Show("Une des valeurs dans les champs contient une valeur déjà existante dans la BDD, veuillez réessayer.", "Error 200 : Duplicate variables", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //Rajoute les données dans la BDD
                MySqlCommand addProgramme = new MySqlCommand();
                addProgramme.CommandText = "INSERT INTO programmes(numeroProgramme, nom, duree) VALUES (@numero, @nom, @duree)";
                addProgramme.Parameters.AddWithValue("@numero", numeroProgramme);
                addProgramme.Parameters.AddWithValue("@nom", nomProgramme);
                addProgramme.Parameters.AddWithValue("@duree", moisProgramme);
                addProgramme.Connection = conn;
                int success = addProgramme.ExecuteNonQuery();
                if(success == 1)
                {
                    Console.WriteLine("Data added successfully.");
                    linkdb();
                }
                else
                {
                    Console.WriteLine("ERROR whilst adding data to database.");
                }

               
                Numero.Text = "";
                Mois.Text = "";
                Nom.Text = "";
            }
            else //executes when Numero and Mois are not numbers.
            {   
                MessageBox.Show("S.V.P respecter tout les contraintes imposer pour chaques champs", "Error 101 : Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        ///Fonctionalité pour le boutton "Supprimer" dans la tab "programmes".
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("SERVER="+ServerHostname+";DATABASE=projetfinaldev;UID=root;PASSWORD=");
            conn.Open();
            DataRowView item = (DataRowView)dataGrid_programmes.SelectedItem;
            
            //If nothing is selected and we press the button.
            if(item == null)
            {
                //Verifies with user if they want to proceed.
                string message = "Voulez-vous effacer tout les éléments de la table programmes?";
                MessageBoxResult result = MessageBox.Show(message, "Message de confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    MySqlCommand removeAllProgramme = new MySqlCommand();
                    removeAllProgramme.CommandText = "DELETE FROM programmes";
                    removeAllProgramme.Connection = conn;
                    removeAllProgramme.ExecuteNonQuery();
                    linkdb();
                }
                else
                {
                    return;
                }
                return;
            }
            else //If one element is selected and we press the button.
            {
                //Verifies with user if they want to proceed.
                string message = "Voulez-vous effacer l'élément sélectionner de la table programmes?";
                MessageBoxResult result = MessageBox.Show(message, "Message de confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    string itemId = item.Row[0].ToString();

                    MySqlCommand removeSelectedProgramme = new MySqlCommand();
                    removeSelectedProgramme.CommandText = "DELETE FROM programmes WHERE numeroProgramme = @numselect";
                    removeSelectedProgramme.Parameters.AddWithValue("@numselect", itemId);
                    removeSelectedProgramme.Connection = conn;
                    removeSelectedProgramme.ExecuteNonQuery();
                    linkdb();
                }
                else
                {
                    return;
                }
            }
            conn.Close();
        }
    }
}
