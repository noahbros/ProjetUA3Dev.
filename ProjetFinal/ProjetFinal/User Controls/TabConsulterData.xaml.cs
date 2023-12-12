using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
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
    /// <summary>
    /// Interaction logic for TabConsulterData.xaml
    /// </summary>
    /// 
    public partial class TabConsulterData : UserControl
    {
        public static ObservableCollection<Stagiaire> searchCollection = new ObservableCollection<Stagiaire>(); //Collection utilisé pour filtrer les items dans la collection stagiaires de stagiaires.cs pour l'afficher dans le listView de la tab Consulter.
        public static int lvConsulterIndex; //Variable utilisé pour que le pop-up identifie le stagiaire et ses données.
        public static DataTable tableData_Stagiaires = new DataTable();
        public static String ServerHostname = "127.0.0.1"; //addresse IP de la base de donnée

        //on déclare nos liste d'objets
        public static List<Stagiaire> listeStagiaires = new List<Stagiaire>();
        public List<Programme> listeProgrammes = new List<Programme>();



        //On obtient les donnes de la base de donnée
        private void liaisonBaseDonnee()
        {
            using (MySqlConnection connection = new MySqlConnection("SERVER=" + ServerHostname + ";DATABASE=projetfinaldev;UID=root;convert zero datetime=True"))
            {
                try
                {
                    MySqlCommand getAllProgrammes = new MySqlCommand("select * from programmes", connection);
                    MySqlCommand getAllStagiaires = new MySqlCommand("select * from stagiaires", connection);
                    connection.Open();



                    DataTable tableData_Stagiaires = new DataTable();
                    DataTable tableData_Programmes = new DataTable();

                    //on remplis les tableData avec les données obtenues lors de la query
                    tableData_Stagiaires.Load(getAllStagiaires.ExecuteReader());
                    tableData_Programmes.Load(getAllProgrammes.ExecuteReader());

                    //on vide les listes
                    listeStagiaires.Clear();
                    listeProgrammes.Clear();

                    //on passe à travers notre DataRow, on crée un objet de type stagiaire et on l'ajoute à listeStagiaires
                    foreach (DataRow row in tableData_Stagiaires.Rows)
                    {
                        listeStagiaires.Add(new Stagiaire
                        {
                            NumeroEtudiant = Convert.ToInt32(row["numeroStagiaire"]),
                            NomDeProgramme = row["programmeId"].ToString(),
                            Prenom = row["prenom"].ToString(),
                            NomDeFamille = row["nom"].ToString(),
                            DateDeNaissance = Convert.ToDateTime(row["naissance"]).ToString("dd/MM/yyyy"),
                            Sexe = row["sexe"].ToString()
                        });
                    }

                    //on passe à travers notre DataRow, on crée un objet de type programme et on l'ajoute à listeProgrammes
                    foreach (DataRow row in tableData_Programmes.Rows)
                    {
                        listeProgrammes.Add(new Programme
                        {
                            Numero = Convert.ToInt32(row["numeroProgramme"]),
                            Nom = row["nom"].ToString(),
                            Duree = Convert.ToInt32(row["duree"])
                        });
                    }



                    //on donne notre liste de stagières comme itemSource pour notre listView

                    lvConsulter.ItemsSource = listeStagiaires;

                    //on ferme la connection
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la connection à la base de donnée : {ex.Message}");
                }

            }

        }



        public TabConsulterData()
        {
            InitializeComponent();

            //on fait un appel à la base de donnée et on popule le listView
            liaisonBaseDonnee();
            //lvConsulter.ItemsSource = TabStagiaireData.listesStagiaires;

        }

        ///Fonctionalités pour le boutton "Rechercher" dans la tab Consulter.
        private void Boutton_Rechercher_Click(object sender, RoutedEventArgs e)
        {
            lvConsulter.ItemsSource = null;
            searchCollection.Clear();
            string nomSearch = searchNom.Text.ToLower();
            string prenomSearch = searchPrenom.Text.ToLower();
            string programmeSearch = cmbProgrammes.Text;

            //Affiche tout les données si les champs sont vide. (default view)
            if(nomSearch == "" && prenomSearch == "" && programmeSearch == "Aucun")
            {
                lvConsulter.ItemsSource = TabStagiaireData.listesStagiaires;
                return;
            }

            //Filtre la collection de stagiaires dans stagiaires et l'associe à la collection filtrer.
            foreach (Stagiaire s in TabStagiaireData.listesStagiaires)
            {
                //confirmateur pour savoir si nous avons effectivements un "match" dans la collection de stagiaires dans stagiaires.cs.
                int confirmerNom = 0;
                int confirmerPrenom = 0;
                int confirmerProgramme = 0;

                //variable pour vérifier si un champ à été utilisé/remplis.
                bool nomSearched = false;
                bool prenomSearched = false;
                bool programmeSearched = false;


                char[] valueNom = s.NomDeFamille.ToLower().ToCharArray();
                char[] searchedNom = nomSearch.ToCharArray();

                char[] valuePrenom = s.Prenom.ToLower().ToCharArray();
                char[] searchedPrenom = prenomSearch.ToCharArray();

                char[] valueProgramme = s.NomDeProgramme.ToCharArray();
                char[] searchedProgramme = programmeSearch.ToCharArray();

                //Checks if field "nom" has a value.
                if (nomSearch != "")
                {
                    nomSearched = true;

                    //Checks if the value is greater or equal to the search query. (if the search query is greater in length than the value, it is skipped.)
                    if (valueNom.Length >= searchedNom.Length)
                    {
                        for (int i = 0; i < searchedNom.Length; i++)
                        {
                            if (searchedNom[i] == valueNom[i]) //Verifies if every index of the value matches the search's indexes.
                            {
                                ++confirmerNom; //Increments a confirmer which, if the value is in the search's scope, the confirmer should be equal to the size of the search query.
                            }
                        }
                    }
                }

                //Checks if the field "prenom" has a value.
                if (prenomSearch != "")
                {
                    prenomSearched = true;

                    if (valuePrenom.Length >= searchedPrenom.Length)
                    {
                        for (int i = 0; i < searchedPrenom.Length; i++)
                        {
                            if (searchedPrenom[i] == valuePrenom[i])
                            {
                                ++confirmerPrenom;
                            }
                        }
                    }
                }

                //Checks if the comboBox is not set to "Aucun".
                if (programmeSearch != "Aucun")
                {
                    programmeSearched = true;

                    if (valueProgramme.Length >= searchedProgramme.Length)
                    {
                        for (int i = 0; i < searchedProgramme.Length; i++)
                        {
                            if (searchedProgramme[i] == valueProgramme[i])
                            {
                                ++confirmerProgramme;
                            }
                        }
                    }
                }
                //If all fields are used.
                if(nomSearched && prenomSearched && programmeSearched)
                {
                    if(confirmerProgramme == programmeSearch.Length && confirmerNom == nomSearch.Length && confirmerPrenom == prenomSearch.Length) //Filter
                    {
                        searchCollection.Add(s); 
                    }
                    continue;
                }
                //If only nom and prenom used.
                if(nomSearched && prenomSearched && !programmeSearched)
                {
                    if(confirmerNom ==  nomSearch.Length && confirmerPrenom == prenomSearch.Length) //Filter
                    {
                        searchCollection.Add(s);
                    }
                    continue;
                }
                //If only nom and programme used.
                else if(nomSearched && !prenomSearched && programmeSearched)
                {
                    if(confirmerNom == nomSearch.Length && confirmerProgramme == programmeSearch.Length) //Filter
                    {
                        searchCollection.Add(s);
                    }
                    continue;
                }
                //If only prenom and programme used.
                else if(!nomSearched && prenomSearched && programmeSearched)
                {
                    if(confirmerPrenom == prenomSearch.Length && confirmerProgramme == programmeSearch.Length)
                    {
                        searchCollection.Add(s);
                    }
                    continue;
                }
                //If only nom is used
                else if(nomSearched && !prenomSearched && !programmeSearched)
                {
                    if(confirmerNom == nomSearch.Length)
                    {
                        searchCollection.Add(s);
                    }
                    continue;
                }
                //If only prenom is used
                else if(!nomSearched && prenomSearched && !programmeSearched)
                {
                    if(confirmerPrenom == prenomSearch.Length)
                    {
                        searchCollection.Add(s);
                    }
                    continue;
                }
                //If only programme is used
                else
                {
                    if(confirmerProgramme == programmeSearch.Length)
                    {
                        searchCollection.Add(s);
                    }
                    continue;
                }

            }

            lvConsulter.ItemsSource = searchCollection;
        }

        private void lvConsulter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lvConsulterIndex = lvConsulter.SelectedIndex;  
            var formPopup = new ConsulterPopUp();
            formPopup.Show(); // if you need non-modal window

        }

        //Active lorsque quelqu'un ouvre le ComboBox
        private void cmbProgrammes_DropDownOpened(object sender, EventArgs e)
        {
            foreach (Programme p in listeProgrammes) //itère au travers des éléments dans la collection statique de programme dans programme.cs
            {
                if (cmbProgrammes.Items.Contains(p.Nom)) //Vérifie si le combobox contient déjà la valeur.
                {
                    continue;
                }
                cmbProgrammes.Items.Add(p.Nom); 
            }

            if(cmbProgrammes.HasItems == false) //Vérifie si le combobox n'a aucune valeur, si oui on restricte l'options d'ouvrire le combobox.
            {
                cmbProgrammes.IsDropDownOpen = false;
                return;
            }
        }
    }
}
