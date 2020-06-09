using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace XFTest.Dtos
{
	/// <summary>
	/// Depicts the data structure which will hold the Client information from a server response.
	/// 
	/// Class was generated using service provided from https://app.quicktype.io/?l=csharp 
	/// based on the provided JSON. Some types are changed for convenience.
	/// </summary>
	public class CarFitClientDto
	{
        [JsonProperty("visitId")]
        public Guid VisitId { get; set; }

        [JsonProperty("homeBobEmployeeId")]
        public Guid HomeBobEmployeeId { get; set; }

        [JsonProperty("houseOwnerId")]
        public Guid HouseOwnerId { get; set; }

        [JsonProperty("isBlocked")]
        public bool IsBlocked { get; set; }

        [JsonProperty("startTimeUtc")]
        public DateTimeOffset StartTimeUtc { get; set; }

        [JsonProperty("endTimeUtc")]
        public DateTimeOffset EndTimeUtc { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("isReviewed")]
        public bool IsReviewed { get; set; }

        [JsonProperty("isFirstVisit")]
        public bool IsFirstVisit { get; set; }

        [JsonProperty("isManual")]
        public bool IsManual { get; set; }

        [JsonProperty("visitTimeUsed")]
        public long VisitTimeUsed { get; set; }

        [JsonProperty("rememberToday")]
        public object RememberToday { get; set; }

        [JsonProperty("houseOwnerFirstName")]
        public string HouseOwnerFirstName { get; set; }

        [JsonProperty("houseOwnerLastName")]
        public string HouseOwnerLastName { get; set; }

        [JsonProperty("houseOwnerMobilePhone")]
        public string HouseOwnerMobilePhone { get; set; }

        [JsonProperty("houseOwnerAddress")]
        public string HouseOwnerAddress { get; set; }

        [JsonProperty("houseOwnerZip")]
        public string HouseOwnerZip { get; set; }

        [JsonProperty("houseOwnerCity")]
        public string HouseOwnerCity { get; set; }

        [JsonProperty("houseOwnerLatitude")]
        public double HouseOwnerLatitude { get; set; }

        [JsonProperty("houseOwnerLongitude")]
        public double HouseOwnerLongitude { get; set; }

        [JsonProperty("isSubscriber")]
        public bool IsSubscriber { get; set; }

        [JsonProperty("rememberAlways")]
        public object RememberAlways { get; set; }

        [JsonProperty("professional")]
        public string Professional { get; set; }

        [JsonProperty("visitState")]
        public string VisitState { get; set; }

        [JsonProperty("stateOrder")]
        public long StateOrder { get; set; }

        [JsonProperty("expectedTime")]
        public string ExpectedTime { get; set; }

        [JsonProperty("tasks")]
        public List<CarFitTaskDto> Tasks { get; set; }

        [JsonProperty("houseOwnerAssets")]
        public List<object> HouseOwnerAssets { get; set; }

        [JsonProperty("visitAssets")]
        public List<object> VisitAssets { get; set; }

        [JsonProperty("visitMessages")]
        public List<object> VisitMessages { get; set; }
    }
}
