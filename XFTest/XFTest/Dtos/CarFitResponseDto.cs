using Newtonsoft.Json;
using System.Collections.Generic;

namespace XFTest.Dtos
{
	/// <summary>
	/// Depicts the data structure of a server response from CarFit application.
	/// 
	/// Class was generated using service provided from https://app.quicktype.io/?l=csharp 
	/// based on the provided JSON. Some types are changed for convenience.
	/// </summary>
	public class CarFitResponseDto
	{
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<CarFitClientDto> Clients { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }
    }
}
