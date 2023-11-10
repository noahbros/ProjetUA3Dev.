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
            string nomSearch = searchNom.Text;
            string prenomSearch = searchPrenom.Text;
            string programmeSearch = cmbProgrammes.Text;

            if (nomSearch == "" && prenomSearch == "" && programmeSearch == "Aucun")
            {
                lvConsulter.ItemsSource = TabStagiaireData.listesStagiaires;
                return;
            }

            if(nomSearch != "")
            {
                foreach(Stagiaire s in TabStagiaireData.listesStagiaires)
                {
                    int confirmer = 0;
                    char[] value = s.NomDeFamille.ToCharArray();
                    char[] searched = nomSearch.ToCharArray(); 

                    if(value.Length >= searched.Length)
                    {
                        for (int i = 0; i < searched.Length; i++)
                        {
                            if (searched[i] == value[i])
                            {
                                ++confirmer;
                            }
                        }
                        if (confirmer == searched.Length)
                        {
                            searchCollection.Add(s);
                        }
                    }
                }
            }

            if(prenomSearch != "")
            {
                foreach (Stagiaire s in TabStagiaireData.listesStagiaires)
                {
                    int confirmer = 0;
                    char[] value = s.Prenom.ToCharArray();
                    char[] searched = prenomSearch.ToCharArray();

                    if (value.Length >= searched.Length)
                    {
                        for (int i = 0; i < searched.Length; i++)
                        {
                            if (searched[i] == value[i])
                            {
                                ++confirmer;
                            }
                        }
                        if (confirmer == searched.Length)
                        {
                            searchCollection.Add(s);
                        }
                    }
                }
            }

            if(programmeSearch != "")
            {
                foreach (Stagiaire s in TabStagiaireData.listesStagiaires)
                {
                    int confirmer = 0;
                    char[] value = s.NomDeProgramme.ToCharArray();
                    char[] searched = programmeSearch.ToCharArray();

                    if (value.Length >= searched.Length)
                    {
                        for (int i = 0; i < searched.Length; i++)
                        {
                            if (searched[i] == value[i])
                            {
                                ++confirmer;
                            }
                        }
                        if (confirmer == searched.Length)
                        {
                            searchCollection.Add(s);
                        }
                    }
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
