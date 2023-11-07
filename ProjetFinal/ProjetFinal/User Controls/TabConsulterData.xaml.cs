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

    public class Stagiaire
    {
        public string Prenom { get; set; }
        public string NomDeFamille { get; set; }
        public int NumeroEtudiant { get; set; }
        public string DateDeNaissance { get; set; }
        public string Sexe {  get; set; }
        public string Programme { get; set; }
    }

    public partial class TabConsulterData : UserControl
    {
        public TabConsulterData()
        {
            InitializeComponent();

            List<Stagiaire> items = new List<Stagiaire>();
            items.Add(new Stagiaire() { Prenom = "Olivier", NomDeFamille = "Caron", NumeroEtudiant = 2680133, DateDeNaissance = "2002/11/10", Sexe = "Male", Programme = "Programmation Informatique" });
            lvConsulter.ItemsSource = items;
        }
    }
}
