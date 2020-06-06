using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using XFTest.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using XFTest.DataServices;

namespace XFTest.ViewModels
{
	public class CleaningListViewModel : BindableBase, INotifyPropertyChanged
    {
        readonly IList<CleaningListJobItem> source;

        public ObservableCollection<CleaningListJobItem> CleaningTasks { get; private set; }


        public CleaningListViewModel( 
            IDialogService dialogService, 
            INavigationService navigationService,
            IDataService<CleaningListJobItem> cleaningListDataService)
        {
            source = cleaningListDataService.FetchDataForEntityAsync().Result;
            PopulateCleaningTaskList();
        }

        void PopulateCleaningTaskList()
        {
            CleaningTasks = new ObservableCollection<CleaningListJobItem>(source);
        }
    }
}
