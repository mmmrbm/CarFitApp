using System;
using System.Collections.Generic;
using System.Text;

namespace XFTest.Models
{
	public class CarWashVisit
	{
		public string ClientFirstName { get; set; }

		public string ClientLastName { get; set; }

		public string ClientContactNo { get; set; }

		public string ClientAddress { get; set; }

		public string ClientZipCode { get; set; }

		public string ClientCity { get; set; }

		public double ClientLocationLatitude { get; set; }

		public double ClientLocationLongitude { get; set; }

		public string JobState { get; set; }

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		public string ExpectedCompletionTime { get; set; }


	}
}
