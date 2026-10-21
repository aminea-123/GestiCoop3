using GestiCoop3.model;
using GestiCoop3.viewmodel;
using GestiCoop3.model;
using GestiCoop3.viewmodel;
using System.Text.RegularExpressions;

namespace Gesticoop3.viewmodel
{
    // viewmodel responsable du formulaire d'ajout d'un étudiant
    public class AjouterEtudiantVM : ViewModelBase
    {
        private string nom = "";
        private string prenom = "";
        private string courriel = "";
        private string programme = "";
        private string numeroEtudiant = "";
        private string messageErreur = "";

        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                OnPropertyChanged();
                ValiderFormulaire();
            }
        }

        public string Prenom
        {
            get => prenom;
            set
            {
                prenom = value;
                OnPropertyChanged();
                ValiderFormulaire();
            }
        }

        public string Courriel
        {
            get => courriel;
            set
            {
                courriel = value;
                OnPropertyChanged();
                ValiderFormulaire();
            }
        }

        public string Programme
        {
            get => programme;
            set
            {
                programme = value;
                OnPropertyChanged();
                ValiderFormulaire();
            }
        }

        public string NumeroEtudiant
        {
            get => numeroEtudiant;
            set
            {
                numeroEtudiant = value;
                OnPropertyChanged();
            }
        }

        public string MessageErreur
        {
            get => messageErreur;
            set
            {
                messageErreur = value;
                OnPropertyChanged();
            }
        }

        // indique si les informations obligatoires sont valides
        public bool EstValide { get; private set; }

        // crée un étudiant à partir des champs remplis
        public Etudiant CreerEtudiant()
        {
            return new Etudiant
            {
                Nom = Nom.Trim(),
                Prenom = Prenom.Trim(),
                Courriel = Courriel.Trim(),
                Programme = Programme,
                NumeroEtudiant = NumeroEtudiant.Trim()
            };
        }

        // vérifie les champs obligatoires et le format du courriel
        private void ValiderFormulaire()
        {
            if (string.IsNullOrWhiteSpace(Nom))
            {
                MessageErreur = "Le nom est obligatoire.";
                EstValide = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(Prenom))
            {
                MessageErreur = "Le prénom est obligatoire.";
                EstValide = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(Courriel) ||
                !Regex.IsMatch(Courriel, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageErreur = "Le courriel doit être valide.";
                EstValide = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(Programme))
            {
                MessageErreur = "Le programme est obligatoire.";
                EstValide = false;
                return;
            }

            MessageErreur = "";
            EstValide = true;
        }
    }
}