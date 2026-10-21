using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using GestiCoop3.model;

namespace GestiCoop3.view
{
    public partial class AjouterEtudiantWindow : Window
    {
        // résultat transmis à la liste principale
        public Etudiant? EtudiantAjoute { get; private set; }

        public AjouterEtudiantWindow()
        {
            // construire les éléments du formulaire
            InitializeComponent();
            ValiderFormulaire();
        }

        // fermer le formulaire avec la croix
        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        // annuler sans transmettre de données
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        // revérifier les champs après chaque modification
        private void Champ_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValiderFormulaire();
        }

        // revérifier après le choix du programme
        private void Champ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInitialized)
                ValiderFormulaire();
        }

        // contrôler les champs et l'adresse courriel
        private void ValiderFormulaire()
        {
            if (NomTextBox == null || PrenomTextBox == null ||
                CourrielTextBox == null || ProgrammeComboBox == null ||
                NumeroTextBox == null || EnregistrerButton == null)
                return;

            string nom = NomTextBox.Text.Trim();
            string prenom = PrenomTextBox.Text.Trim();
            string courriel = CourrielTextBox.Text.Trim();
            string numero = NumeroTextBox.Text.Trim();

            bool courrielValide = Regex.IsMatch(
                courriel, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            // afficher l'avertissement uniquement après une saisie invalide
            ErreurTextBlock.Visibility =
                courriel.Length > 0 && !courrielValide
                ? Visibility.Visible
                : Visibility.Collapsed;

            bool numeroValide =
                numero.Length == 0 || Regex.IsMatch(numero, @"^\d{7}$");

            // bloquer l'enregistrement tant que le formulaire est incorrect
            EnregistrerButton.IsEnabled =
                nom.Length > 0 &&
                prenom.Length > 0 &&
                courrielValide &&
                ProgrammeComboBox.SelectedItem != null &&
                numeroValide;
        }

        // créer l'objet lorsque les données sont valides
        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            ValiderFormulaire();

            if (!EnregistrerButton.IsEnabled)
                return;

            if (ProgrammeComboBox.SelectedItem is not ComboBoxItem programme ||
                programme.Content == null)
                return;

            EtudiantAjoute = new Etudiant
            {
                Nom = NomTextBox.Text.Trim(),
                Prenom = PrenomTextBox.Text.Trim(),
                Courriel = CourrielTextBox.Text.Trim(),
                Programme = programme.Content.ToString() ?? "",
                NumeroEtudiant = NumeroTextBox.Text.Trim()
            };

            // confirmer la saisie et fermer la fenêtre
            DialogResult = true;
        }
    }
}
