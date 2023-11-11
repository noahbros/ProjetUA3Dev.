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
    /// <summary>
    /// Interaction logic for TabConsulterData.xaml
    /// </summary>
    /// 
    public partial class TabConsulterData : UserControl
    {
        public ObservableCollection<Stagiaire> searchCollection = new ObservableCollection<Stagiaire>();
        public TabConsulterData()
        {
            InitializeComponent();
            lvConsulter.ItemsSource = TabStagiaireData.listesStagiaires;
        }

        private void Boutton_Rechercher_Click(object sender, RoutedEventArgs e)
        {
            lvConsulter.ItemsSource = null;
            searchCollection.Clear();
            string nomSearch = searchNom.Text.ToLower();
            string prenomSearch = searchPrenom.Text.ToLower();
            string programmeSearch = cmbProgrammes.Text;

            
            if(nomSearch == "" && prenomSearch == "" && programmeSearch == "Aucun")
            {
                lvConsulter.ItemsSource = TabStagiaireData.listesStagiaires;
                return;
            }


            foreach (Stagiaire s in TabStagiaireData.listesStagiaires)
            {
                int confirmerNom = 0;
                int confirmerPrenom = 0;
                int confirmerProgramme = 0;
                bool nomSearched = false;
                bool prenomSearched = false;
                bool programmeSearched = false;

                char[] valueNom = s.NomDeFamille.ToLower().ToCharArray();
                char[] searchedNom = nomSearch.ToCharArray();

                char[] valuePrenom = s.Prenom.ToLower().ToCharArray();
                char[] searchedPrenom = prenomSearch.ToCharArray();

                char[] valueProgramme = s.NomDeProgramme.ToCharArray();
                char[] searchedProgramme = programmeSearch.ToCharArray();

                //Checks if "nom" has a value.
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

                //Checks if "prenom" has a value.
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
                    if(confirmerProgramme == programmeSearch.Length && confirmerNom == nomSearch.Length && confirmerPrenom == prenomSearch.Length)
                    {
                        searchCollection.Add(s);
                    }
                    continue;
                }
                //If only nom and prenom used.
                if(nomSearched && prenomSearched && !programmeSearched)
                {
                    if(confirmerNom ==  nomSearch.Length && confirmerPrenom == prenomSearch.Length)
                    {
                        searchCollection.Add(s);
                    }
                    continue;
                }
                //If only nom and programme used.
                else if(nomSearched && !prenomSearched && programmeSearched)
                {
                    if(confirmerNom == nomSearch.Length && confirmerProgramme == programmeSearch.Length)
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
            var formPopup = new ConsulterPopUp();
            formPopup.Show(); // if you need non-modal window

        }

        private void cmbProgrammes_DropDownOpened(object sender, EventArgs e)
        {
           
            foreach(Programme p in TabProgrammeData.listesProgrammes)
            {
                if (cmbProgrammes.Items.Contains(p.Nom))
                {
                    continue;
                }
                cmbProgrammes.Items.Add(p.Nom);
            }

            if(cmbProgrammes.HasItems == false)
            {
                cmbProgrammes.IsDropDownOpen = false;
                return;
            }
        }
    }
}
