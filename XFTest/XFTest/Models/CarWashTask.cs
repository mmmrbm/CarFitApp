using System;
using System.Collections.Generic;
using System.Text;
using XFTest.Dtos;

namespace XFTest.Models
{
	public class CarWashTask
	{
		public Guid TaskId { get; set; }

		public string TaskDetail { get; set; }

		public int PlannedTimeSpan { get; set; }

		public float Price { get; set; }

		public CarWashVisit CarWashVisit { get; set; }
	}
}
