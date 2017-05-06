using System.ComponentModel;
using System.Runtime.CompilerServices;
using Bossr.Mobile.Annotations;
using Bossr.Mobile.Models;

namespace Bossr.Mobile.ViewModels
{
    public class StatusPageViewModel : INotifyPropertyChanged
    {
        public StatusPageViewModel(World selectedWorld)
        {
            SelectedWorld = selectedWorld;
        }

        private World selectedWorld;
        public World SelectedWorld
        {
            get { return selectedWorld; }
            set
            {
                selectedWorld = value;
                OnPropertyChanged(nameof(SelectedWorld));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
