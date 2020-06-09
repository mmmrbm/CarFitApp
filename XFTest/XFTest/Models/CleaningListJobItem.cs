using System;

namespace XFTest.Models
{
	/// <summary>
	/// The business object which depicts the nature and information of a car wash job item.
	/// </summary>
    public class CleaningListJobItem
    {
        public string ClientName { get; set; }

		public string JobDescription { get; set; }

		public DateTime JobStartTime { get; set; }

		public string ExpectedTimeSlotOfAvailablity { get; set; }

		public string TimeInformationOnJob { get; set; }

		public int ExpectedTimeAllocatedForJob { get; set; }

		public string ExpectedTimeAllocatedInfo { get; set; }

		public string ClientAddressInformation { get; set; }

		public double ClientLocationLatitude { get; set; }

		public double ClientLocationLongitude { get; set; }

		public string DistanceInformation { get; set; }

		public string JobStatus { get; set; }

		public string JobBackgroundColor { get; set; }
	}
}
