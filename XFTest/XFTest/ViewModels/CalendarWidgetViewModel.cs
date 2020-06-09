using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace XFTest.ViewModels
{
	public class CalendarWidgetViewModel : BindableBase, INotifyPropertyChanged
	{
		#region Private members
		private INavigationService _navigationService;

		private IEventAggregator _eventAggregator;

		private static string _carriageReturn = "\r\n";
		#endregion

		#region Properties
		private DateTime _selectedDateOfInterest;

		public DateTime SelectedDateOfInterest
		{
			get { return _selectedDateOfInterest; }
			set 
			{ 
				SetProperty(ref _selectedDateOfInterest, value);
				this._eventAggregator.GetEvent<PubSubEvent<DateTime>>().Publish(this.SelectedDateOfInterest);
			}
		}

		private DateTime _dateOfSelectedOffsetWeek;

		public DateTime DateOfSelectedOffsetWeek
		{
			get { return _dateOfSelectedOffsetWeek; }
			set { _dateOfSelectedOffsetWeek = value; }
		}


		private string _year;

		public string Year
		{
			get { return _year; }
			set { SetProperty(ref _year, value); }
		}

		private string _month;

		public string Month
		{
			get { return _month; }
			set { SetProperty(ref _month, value); }
		}

		private int _currentWeek;

		public int CurrentWeek
		{
			get { return _currentWeek; }
			set { SetProperty(ref _currentWeek, value); }
		}

		private DateTime _weekStartDate;

		public DateTime WeekStartDate
		{
			get { return _weekStartDate; }
			set { SetProperty(ref _weekStartDate, value); }
		}

		private DateTime _weekEndDate;

		public DateTime WeekEndDate
		{
			get { return _weekEndDate; }
			set { SetProperty(ref _weekEndDate, value); }
		}

		private string _dayOneInfo;

		public string DayOneInfo
		{
			get { return _dayOneInfo; }
			set { SetProperty(ref _dayOneInfo, value); }
		}

		private string _dayTwoInfo;

		public string DayTwoInfo
		{
			get { return _dayTwoInfo; }
			set { SetProperty(ref _dayTwoInfo, value); }
		}

		private string _dayThreeInfo;

		public string DayThreeInfo
		{
			get { return _dayThreeInfo; }
			set { SetProperty(ref _dayThreeInfo, value); }
		}

		private string _dayFourInfo;

		public string DayFourInfo
		{
			get { return _dayFourInfo; }
			set { SetProperty(ref _dayFourInfo, value); }
		}

		private string _dayFiveInfo;

		public string DayFiveInfo
		{
			get { return _dayFiveInfo; }
			set { SetProperty(ref _dayFiveInfo, value); }
		}

		private string _daySixInfo;

		public string DaySixInfo
		{
			get { return _daySixInfo; }
			set { SetProperty(ref _daySixInfo, value); }
		}

		private string _daySevenInfo;

		public string DaySevenInfo
		{
			get { return _daySevenInfo; }
			set { SetProperty(ref _daySevenInfo, value); }
		}

		private string _dayOneButtonColor;

		public string DayOneButtonColor
		{
			get { return _dayOneButtonColor; }
			set { SetProperty(ref _dayOneButtonColor, value); }
		}

		private string _dayTwoButtonColor;

		public string DayTwoButtonColor
		{
			get { return _dayTwoButtonColor; }
			set { SetProperty(ref _dayTwoButtonColor, value); }
		}

		private string _dayThreeButtonColor;

		public string DayThreeButtonColor
		{
			get { return _dayThreeButtonColor; }
			set { SetProperty(ref _dayThreeButtonColor, value); }
		}

		private string _dayFourButtonColor;

		public string DayFourButtonColor
		{
			get { return _dayFourButtonColor; }
			set { SetProperty(ref _dayFourButtonColor, value); }
		}

		private string _dayFiveButtonColor;

		public string DayFiveButtonColor
		{
			get { return _dayFiveButtonColor; }
			set { SetProperty(ref _dayFiveButtonColor, value); }
		}

		private string _daySixButtonColor;

		public string DaySixButtonColor
		{
			get { return _daySixButtonColor; }
			set { SetProperty(ref _daySixButtonColor, value); }
		}

		private string _daySevenButtonColor;

		public string DaySevenButtonColor
		{
			get { return _daySevenButtonColor; }
			set { SetProperty(ref _daySevenButtonColor, value); }
		}
		#endregion

		#region Commands
		public DelegateCommand<string> DateSelectCommand { get; set; }
		public DelegateCommand MoveWeekBackwardCommand { get; set; }
		public DelegateCommand MoveWeekForwardCommand { get; set; }
		#endregion

		/// <summary>
		/// Constructs <see cref="CalendarWidgetViewModel"/>
		/// </summary>
		/// <param name="navigationService">Instance of <see cref="INavigationService"/></param>
		/// <param name="eventAggregator">Instance of <see cref="IEventAggregator"/></param>
		public CalendarWidgetViewModel(
			INavigationService navigationService,
			IEventAggregator eventAggregator)
		{
			_navigationService = navigationService;
			_eventAggregator = eventAggregator;

			DateSelectCommand = new DelegateCommand<string>(DateSelectCommandHandler);
			MoveWeekBackwardCommand = new DelegateCommand(MoveWeekBackwardCommandHandler);
			MoveWeekForwardCommand = new DelegateCommand(MoveWeekForwardCommandHandler);

			DateOfSelectedOffsetWeek = DateTime.Now;
			SelectedDateOfInterest = DateTime.Now;
			SetupDateInformation();
		}

		#region Methods
		/// <summary>
		/// Responsible to set up the date related information in the calendar.
		/// </summary>
		private void SetupDateInformation()
		{
			Year = DateOfSelectedOffsetWeek.Year.ToString();
			Month = DateOfSelectedOffsetWeek.ToString("MMMM", CultureInfo.InvariantCulture);

			CultureInfo ciCurr = CultureInfo.CurrentCulture;
			CurrentWeek = ciCurr.Calendar.GetWeekOfYear(DateOfSelectedOffsetWeek, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

			WeekStartDate = DateOfSelectedOffsetWeek.AddDays(-(int)(DateOfSelectedOffsetWeek.DayOfWeek - 1));
			WeekEndDate = WeekStartDate.AddDays(6);

			DayOneInfo = BuildDayInfo(WeekStartDate, 0);
			DayTwoInfo = BuildDayInfo(WeekStartDate, 1);
			DayThreeInfo = BuildDayInfo(WeekStartDate, 2);
			DayFourInfo = BuildDayInfo(WeekStartDate, 3);
			DayFiveInfo = BuildDayInfo(WeekStartDate, 4);
			DaySixInfo = BuildDayInfo(WeekStartDate, 5);
			DaySevenInfo = BuildDayInfo(WeekStartDate, 6);

			SetupDateSelectorColors();
		}

		/// <summary>
		/// Responsible to build the day information content.
		/// </summary>
		/// <param name="startDateOfSelectedWeek">Start day of the week selected by user</param>
		/// <param name="forwardDates">Offset of the dates</param>
		/// <returns></returns>
		private string BuildDayInfo(DateTime startDateOfSelectedWeek, int forwardDates)
		{
			StringBuilder dayInfo = new StringBuilder(startDateOfSelectedWeek.AddDays(forwardDates).Day.ToString());
			dayInfo.Append(_carriageReturn);
			dayInfo.Append(startDateOfSelectedWeek.AddDays(forwardDates).DayOfWeek.ToString().Substring(0, 3));
			return dayInfo.ToString();
		}

		/// <summary>
		/// Responsible setup the color scehemes for date information holders
		/// </summary>
		private void SetupDateSelectorColors()
		{
			SetupDateSelectorDefaulColor();
			SetupDateSelectedColor((int)(SelectedDateOfInterest.DayOfWeek - 1));
		}

		/// <summary>
		/// Responsible to set up the default color scheme for date information holders
		/// </summary>
		private void SetupDateSelectorDefaulColor()
		{
			DayOneButtonColor = "#25A87B";
			DayTwoButtonColor = "#25A87B";
			DayThreeButtonColor = "#25A87B";
			DayFourButtonColor = "#25A87B";
			DayFiveButtonColor = "#25A87B";
			DaySixButtonColor = "#25A87B";
			DaySevenButtonColor = "#25A87B";
		}

		/// <summary>
		/// Responsible to set up the color scheme for user selected date holder
		/// </summary>
		private void SetupDateSelectedColor(int index)
		{
			if (index == 0)
			{
				DayOneButtonColor = "#368268";
			}
			if (index == 1)
			{
				DayTwoButtonColor = "#368268";
			}
			if (index == 2)
			{
				DayThreeButtonColor = "#368268";
			}
			if (index == 3)
			{
				DayFourButtonColor = "#368268";
			}
			if (index == 4)
			{
				DayFiveButtonColor = "#368268";
			}
			if (index == 5)
			{
				DaySixButtonColor = "#368268";
			}
			if (index == 6)
			{
				DaySevenButtonColor = "#368268";
			}
		}

		/// <summary>
		/// Command handler to handle the action of user moving forward a week
		/// </summary>
		private void MoveWeekForwardCommandHandler()
		{
			DateOfSelectedOffsetWeek = DateOfSelectedOffsetWeek.AddDays(+7);
			SetupDateInformation();

			if (CheckUserSelectedWeekAgainstOffsetDayWeek())
			{
				SetupDateSelectorColors();
			}
			else
			{
				SetupDateSelectorDefaulColor();
			}
		}

		/// <summary>
		/// Command handler to handle the action of user moving backward a week
		/// </summary>
		private void MoveWeekBackwardCommandHandler()
		{
			DateOfSelectedOffsetWeek = DateOfSelectedOffsetWeek.AddDays(-7);
			SetupDateInformation();

			if (CheckUserSelectedWeekAgainstOffsetDayWeek())
			{
				SetupDateSelectorColors();
			}
			else
			{
				SetupDateSelectorDefaulColor();
			}
		}

		/// <summary>
		/// Responsible to compare the week containing user selected day against the current week displayed in app
		/// </summary>
		/// <returns>A boolean flag of the comparison</returns>
		private bool CheckUserSelectedWeekAgainstOffsetDayWeek()
		{
			CultureInfo ciCurr = CultureInfo.CurrentCulture;
			var userSelectedDayWeek = ciCurr.Calendar.GetWeekOfYear(SelectedDateOfInterest, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
			var selectedOffsetDayWeek = ciCurr.Calendar.GetWeekOfYear(DateOfSelectedOffsetWeek, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

			return userSelectedDayWeek == selectedOffsetDayWeek;
		}

		/// <summary>
		/// Command handler to handle the action of user selecting a week
		/// </summary>
		/// <param name="index">The index which will passed from UI to indicate the button pressed by user</param>
		private void DateSelectCommandHandler(string index)
		{
			SelectedDateOfInterest = WeekStartDate.AddDays(int.Parse(index));
			SetupDateSelectorColors();
		}
		#endregion
	}
}
