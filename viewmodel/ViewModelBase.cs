using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestiCoop3.viewmodel
{
    // classe de base pour les viewmodels qui utilisent la liaison de données
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? nomPropriete = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}