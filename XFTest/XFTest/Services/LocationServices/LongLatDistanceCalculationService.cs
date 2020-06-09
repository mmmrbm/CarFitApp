using System;

namespace XFTest.Services.LocationServices
{
	/// <summary>
	/// A concrete implementation of <see cref="IDistanceCalculationService"/>, which will be holding
	/// geo location services like calculating distance.
	/// </summary>
	/**
     * Below code is based on solution found at https://www.geodatasource.com/developers/c-sharp.
     * 
     * There are multiple Nuget packages which can be used to calculate geo distances, but did not
     * use them, due to uncertainty of impact of the solution.
     * For an example, when using Xamarin.Essentials package, as per below article, we need to change
     * few things in Android project. Thus, did not used.
     * https://www.c-sharpcorner.com/article/xamarin-forms-get-distance-between-two-coordinates-using-geolocation-using-xam/
     */
	public class LongLatDistanceCalculationService : IDistanceCalculationService
	{
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
		public double CalculateDistanceByLangLat(double lat1, double lon1, double lat2, double lon2, char unit)
		{
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(Deg2rad(lat1)) * Math.Sin(Deg2rad(lat2)) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Cos(Deg2rad(theta));
                dist = Math.Acos(dist);
                dist = Rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist *= 1.609344;
                }
                else if (unit == 'N')
                {
                    dist *= 0.8684;
                }
                return (Math.Round(dist, 2));
            }
        }

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="deg">Degree value</param>
        /// <returns>Radian value for the degree</returns>
        private double Deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /// <summary>
        /// Converts radians to degree
        /// </summary>
        /// <param name="rad">Radian value</param>
        /// <returns>Degree value</returns>
        private double Rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
