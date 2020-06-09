using System;

namespace XFTest.Models
{
	/// <summary>
	/// The entity to represent the date related information in the Calendar Widget.
	/// </summary>
	public class CalendarWidgetDate
	{
		public CalendarWidgetDate()
		{
			ColorCode = "#25A87B";
		}

		public DateTime DateTimeData { get; set; }

		public string DateRepresentation { get; set; }

		public string DayRepresentation { get; set; }

		public string ColorCode { get; set; }
	}
}
