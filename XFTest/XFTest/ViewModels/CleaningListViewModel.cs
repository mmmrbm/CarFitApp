using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using XFTest.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using XFTest.Services;

namespace XFTest.ViewModels
{
	public class CleaningListViewModel : BindableBase, INotifyPropertyChanged
    {
        readonly IList<CleaningList> source;

        public ObservableCollection<CleaningList> CleaningTasks { get; private set; }


        public CleaningListViewModel( 
            IDialogService dialogService, 
            INavigationService navigationService,
            IDataService<CleaningList> cleaningListDataService)
        {
            source = cleaningListDataService.FetchDataForEntityAsync().Result;
            PopulateCleaningTaskList();
        }

        void PopulateCleaningTaskList()
        {
            CleaningTasks = new ObservableCollection<CleaningList>(source);
        }
    }
}
