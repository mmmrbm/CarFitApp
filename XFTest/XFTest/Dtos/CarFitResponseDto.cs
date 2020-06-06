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
