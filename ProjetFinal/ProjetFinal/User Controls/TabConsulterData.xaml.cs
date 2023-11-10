using System;
using System.Collections.Generic;
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

    /*public class Stagiaire
    {
        public string Prenom { get; set; }
        public string NomDeFamille { get; set; }
        public int NumeroEtudiant { get; set; }
        public string DateDeNaissance { get; set; }
        public string Sexe {  get; set; }
        public string Programme { get; set; }
    }*/

    public partial class TabConsulterData : UserControl
    {
        public TabConsulterData()
        {
            InitializeComponent();

            List<Stagiaire> items = new List<Stagiaire>();
            items.Add(new Stagiaire() { Prenom = "Olivier", NomDeFamille = "Caron", NumeroEtudiant = 2640183, DateDeNaissance = "2002/11/10", Sexe = "Male", NomDeProgramme = "Programmation Informatique" });
            items.Add(new Stagiaire() { Prenom = "Noah", NomDeFamille = "Brosseau", NumeroEtudiant = 1234567, DateDeNaissance = "2002/11/10", Sexe = "Male", NomDeProgramme = "Programmation Informatique avec Coop" });
            items.Add(new Stagiaire() { Prenom = "Amélie", NomDeFamille = "Racine", NumeroEtudiant = 9876543, DateDeNaissance = "2003/08/15", Sexe = "Femelle", NomDeProgramme = "Programmation Informatique avec Coop" });
            lvConsulter.ItemsSource = items;


            List<string> programmes = new List<string>();
            programmes.Add("Aucun");
            programmes.Add("Informatique");
            programmes.Add("Technique en génie électrique");
            programmes.Add("Pattiserie");
            programmes.Add("Services 911");
            programmes.Add("Programmation Informatique avec Coop");
            cmbProgrammes.ItemsSource = programmes;
        }

        private void Boutton_Rechercher_Click(object sender, RoutedEventArgs e)
        {
            var formPopup = new ConsulterPopUp();
            formPopup.Show(); // if you need non-modal window

        }

        private void lvConsulter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var formPopup = new ConsulterPopUp();
            formPopup.Show(); // if you need non-modal window

        }
    }
}
