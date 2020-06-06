using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using XFTest.Dtos;
using XFTest.Views;

namespace XFTest.Services
{
	internal class FetchDataService
	{
		/**
		 * Below code is based on the solutions and information provided on below discussions.
		 * https://stackoverflow.com/questions/60878223/xamarin-forms-read-local-json-file-and-display-in-picker
		 * https://stackoverflow.com/questions/38762368/embedded-resource-in-net-core-libraries
		 */
		internal List<CarFitClientDto> FetchClientData()
		{
			CarFitResponseDto response = null;
			List<CarFitClientDto> carFitClients = null;

			var assembly = typeof(MainPage).GetTypeInfo().Assembly;

			foreach (var resource in assembly.GetManifestResourceNames())
			{
				if (resource.Contains("XF-Test-Json"))
				{
					Stream stream = assembly.GetManifestResourceStream(resource);

					using (var reader = new StreamReader(stream))
					{
						string data = reader.ReadToEnd();
						Console.WriteLine(data);
						response = JsonConvert.DeserializeObject<CarFitResponseDto>(data);

						if (response == null)
						{
							throw new JsonException("Bad JSON response from Server.");
						}

						carFitClients = response.Clients;
					}
				}
			}

			return carFitClients;
		}
	}
}
