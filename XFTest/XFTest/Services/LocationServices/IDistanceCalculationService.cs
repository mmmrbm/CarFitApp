using System;
using System.Collections.Generic;
using System.Text;

namespace XFTest.Services.LocationServices
{
	/**
	 * Interface for distance calculation might be an over kill, but since this can be categorized as 
	 * a location service, thought to use. Also, it will add uniformaty.
	 */
	public interface IDistanceCalculationService
	{
		double CalculateDistanceByLangLat(double lat1, double lon1, double lat2, double lon2, char unit);
	}
}
