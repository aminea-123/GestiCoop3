using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GestiCoop3.model;

namespace GestiCoop3.view
{
    public partial class Etudiants : Window
    {
        // collection des étudiants affichés
        private readonly ObservableCollection<Etudiant> etudiants = new();
        private ICollectionView vueEtudiants = null!;

        public Etudiants()
        {
            // préparer la fenêtre et les données de départ
            InitializeComponent();

            etudiants.Add(new Etudiant
            {
                Nom = "Tremblay", Prenom = "Sophie",
                Programme = "Informatique", Courriel = "s.tremblay@etud.lacite.ca"
            });
            etudiants.Add(new Etudiant
            {
                Nom = "Nguyen", Prenom = "Minh",
                Programme = "Informatique", Courriel = "m.nguyen@etud.lacite.ca"
            });
            etudiants.Add(new Etudiant
            {
                Nom = "Dubois", Prenom = "Élodie",
                Programme = "Génie logiciel", Courriel = "e.dubois@etud.lacite.ca"
            });
            etudiants.Add(new Etudiant
            {
                Nom = "Okonkwo", Prenom = "Chidi",
                Programme = "Cybersécurité", Courriel = "c.okonkwo@etud.lacite.ca"
            });
            etudiants.Add(new Etudiant
            {
                Nom = "Lavoie", Prenom = "Xavier",
                Programme = "Informatique", Courriel = "x.lavoie@etud.lacite.ca"
            });

            // relier la liste à la collection
            vueEtudiants = CollectionViewSource.GetDefaultView(etudiants);
            vueEtudiants.Filter = FiltrerEtudiant;
            ListeEtudiants.ItemsSource = vueEtudiants;
            MettreAJourNombre();
        }

        // décider quels étudiants restent visibles
        private bool FiltrerEtudiant(object element)
        {
            if (element is not Etudiant etudiant)
                return false;

            string recherche = RechercheTextBox?.Text.Trim() ?? "";
            string programme = (FiltreProgrammeComboBox?.SelectedItem as ComboBoxItem)
                ?.Content?.ToString() ?? "Tous les programmes";

            bool correspondRecherche =
                etudiant.Nom.Contains(recherche, StringComparison.OrdinalIgnoreCase) ||
                etudiant.Prenom.Contains(recherche, StringComparison.OrdinalIgnoreCase) ||
                etudiant.Courriel.Contains(recherche, StringComparison.OrdinalIgnoreCase) ||
                etudiant.Programme.Contains(recherche, StringComparison.OrdinalIgnoreCase);

            bool correspondProgramme =
                programme == "Tous les programmes" || etudiant.Programme == programme;

            return correspondRecherche && correspondProgramme;
        }

        // appliquer la recherche saisie
        private void Recherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualiserListe();
        }

        // appliquer le programme choisi
        private void FiltreProgramme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vueEtudiants != null)
                ActualiserListe();
        }

        // rafraîchir la liste et son compteur
        private void ActualiserListe()
        {
            vueEtudiants.Refresh();
            MettreAJourNombre();
        }

        // afficher le nombre de résultats visibles
        private void MettreAJourNombre()
        {
            if (NombreEtudiantsTextBlock != null && vueEtudiants != null)
                NombreEtudiantsTextBlock.Text = $"{vueEtudiants.Cast<object>().Count()} étudiants";
        }

        // ouvrir le formulaire au centre de cette fenêtre
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterEtudiantWindow fenetre = new AjouterEtudiantWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            if (fenetre.ShowDialog() == true)
            {
                Etudiant? nouvelEtudiant = fenetre.EtudiantAjoute;
                if (nouvelEtudiant != null)
                {
                    etudiants.Add(nouvelEtudiant);
                    ActualiserListe();
                }
            }
        }

        // afficher les informations de l'étudiant choisi
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is not Etudiant etudiant)
                return;

            MessageBox.Show(
                $"Étudiant : {etudiant.Prenom} {etudiant.Nom}\nCourriel : {etudiant.Courriel}",
                "Modifier un étudiant",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        // demander confirmation avant de retirer un étudiant
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is not Etudiant etudiant)
                return;

            MessageBoxResult resultat = MessageBox.Show(
                $"Voulez-vous supprimer {etudiant.Prenom} {etudiant.Nom} ?",
                "Confirmer la suppression",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultat == MessageBoxResult.Yes)
            {
                etudiants.Remove(etudiant);
                ActualiserListe();
            }
        }
    }
}
