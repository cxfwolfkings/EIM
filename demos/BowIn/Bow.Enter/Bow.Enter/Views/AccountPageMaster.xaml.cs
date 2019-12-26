using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bow.Enter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPageMaster : ContentPage
    {
        public ListView ListView;

        public AccountPageMaster()
        {
            InitializeComponent();

            BindingContext = new AccountPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class AccountPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AccountPageMasterMenuItem> MenuItems { get; set; }

            public AccountPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<AccountPageMasterMenuItem>(new[]
                {
                    new AccountPageMasterMenuItem { Id = 0, Title = "Page 1" },
                    new AccountPageMasterMenuItem { Id = 1, Title = "Page 2" },
                    new AccountPageMasterMenuItem { Id = 2, Title = "Page 3" },
                    new AccountPageMasterMenuItem { Id = 3, Title = "Page 4" },
                    new AccountPageMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}