using Client.Refit;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;
using Refit;

namespace Client.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGetPeople poepleSupplier;
		private readonly HttpClient _httpClinet;
		private readonly FallbackPolicy<List<Person>> _fallBackPollicy;
		private readonly RetryPolicy<List<Person>> _retryPollicy;
		private readonly CircuitBreakerPolicy<List<Person>> _circuitBreakerPollicy;

		//private readonly AsyncFaa _policy;

		public HomeController(IHttpClientFactory httpClientFactory)
		{
			poepleSupplier = RestService.For<IGetPeople>("https://localhost:6000");
			_httpClinet = httpClientFactory.CreateClient();

			_fallBackPollicy = Policy<List<Person>>.Handle<Exception>()
				.FallbackAsync(new List<Person>()
				{
					new Person()
					{
						Id = 1,
						Name = "Fallback",
						Age = 20
					}
				});

			_retryPollicy = Policy<List<Person>>.Handle<Exception>()
				.RetryAsync(3);

			_circuitBreakerPollicy = Policy<List<Person>>.Handle<Exception>()
				.CircuitBreakerAsync(1, TimeSpan.FromSeconds(10));
		}
		[HttpGet("/api/get")]
		public async Task<List<Person>> Index()
		{
			#region CircuitBreakerPollicy
			return await _circuitBreakerPollicy.ExecuteAsync(() =>
			{
				var people = poepleSupplier.GetAllStudent().GetAwaiter().GetResult();

				return Task.FromResult(people);
			});
			#endregion

			#region RetryPollicy
			//return await _retryPollicy.ExecuteAsync(() =>
			//{
			//	var people = poepleSupplier.GetAllStudent().GetAwaiter().GetResult();

			//	return Task.FromResult(people);
			//}); 
			#endregion

			#region FallBack
			//return await _fallBackPollicy.ExecuteAsync(() =>
			//{
			//	var people = poepleSupplier.GetAllStudent().GetAwaiter().GetResult();

			//	return Task.FromResult(people);
			//}); 
			#endregion

			#region NormalCall
			//var people = poepleSupplier.GetAllStudent().GetAwaiter().GetResult();

			//return people;
			#endregion
		}
	}
}
