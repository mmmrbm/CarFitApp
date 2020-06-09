namespace XFTest.Services.LocationServices
{
	/// <summary>
	/// Contract needs to be fulfilled by entities which will be providing location based services.
	/// </summary>
	/**
	 * Interface for distance calculation might be look like an over kill for the excercise, 
	 * but since this can be categorized as a location service, thought to use. Also, it will add uniformaty.
	 */
	public interface IDistanceCalculationService
	{
		/// <summary>
		/// Responsible to calculate distance between two geo locations, given and latitudes and longitudes
		/// of the locations.
		/// </summary>
		/// <param name="lat1">Latitude of the first geo location</param>
		/// <param name="lon1">Longitude of the first geo location</param>
		/// <param name="lat2">Latitude of the second geo location</param>
		/// <param name="lon2">Longitude of the second geo location</param>
		/// <param name="unit">Unit of measure to calculate the distance</param>
		/// <returns></returns>
		double CalculateDistanceByLangLat(double lat1, double lon1, double lat2, double lon2, char unit);
	}
}
