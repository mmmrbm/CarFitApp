using Newtonsoft.Json;
using System;

namespace XFTest.Dtos
{
	/// <summary>
	/// Depicts the data structure which will hold the Car Wash Job Item from a server response.
	/// 
	/// Class was generated using service provided from https://app.quicktype.io/?l=csharp 
	/// based on the provided JSON. Some types are changed for convenience.
	/// </summary>
	public class CarFitTaskDto
	{
        [JsonProperty("taskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("isTemplate")]
        public bool IsTemplate { get; set; }

        [JsonProperty("timesInMinutes")]
        public int TimesInMinutes { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("paymentTypeId")]
        public Guid PaymentTypeId { get; set; }

        [JsonProperty("createDateUtc")]
        public DateTimeOffset CreateDateUtc { get; set; }

        [JsonProperty("lastUpdateDateUtc")]
        public DateTimeOffset LastUpdateDateUtc { get; set; }

        [JsonProperty("paymentTypes")]
        public object PaymentTypes { get; set; }
    }
}
