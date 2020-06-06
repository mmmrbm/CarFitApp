using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XFTest.Dtos
{
    /**
     * Class was generated using service provided from https://app.quicktype.io/?l=csharp 
     * based on the provided JSON.
     * */
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
