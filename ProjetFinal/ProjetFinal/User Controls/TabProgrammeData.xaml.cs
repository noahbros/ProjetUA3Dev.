﻿using System;
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
        public ObservableCollection<Programme> listesProgrammes = new ObservableCollection<Programme>();
        public TabProgrammeData()
        {
            InitializeComponent();
            lvProgramme.ItemsSource = listesProgrammes;

        }
        public void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            int numeroProgramme;
            int moisProgramme;

            //Checks empty fields
            if (Numero.Text == "" || Nom.Text == "" || Mois.Text == "")
            {
                MessageBox.Show("S.V.P remplire tout les champs.", "Error 100 : Invalid Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Checks non-allowed values, and creates an object of the new Programme.
            if(int.TryParse(Numero.Text, out numeroProgramme) && int.TryParse(Mois.Text, out moisProgramme))
            {
                listesProgrammes.Add(new Programme { Numero = numeroProgramme, Nom = Nom.Text, Duree = moisProgramme });
                lvProgramme.ItemsSource = listesProgrammes;
            }
            else
            {
                MessageBox.Show("S.V.P remplire tout les champs avec les valeurs demander.", "Error 101 : Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
