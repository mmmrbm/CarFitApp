using System.Collections.Generic;
using System.Threading.Tasks;

namespace XFTest.DataServices
{
	/// <summary>
	/// Contract needs to be fulfilled by entities which will be providing entity data fetching services.
	/// A seperate interface was created just for data fetching to adhere to Interface segregation principle.
	/// </summary>
	/// <typeparam name="T">Type of data which will be fetched from the entity</typeparam>
	public interface IDataFetchService<T>
	{
		/// <summary>
		/// Responsible to manage the logic to fetch a certain kind of entity of type <see cref="T"> asynchronously.
		/// </summary>
		/// <returns>A <see cref="IList"/> of data itme of type <see cref="T"></returns>
		Task<List<T>> FetchDataForEntityAsync();
	}
}
