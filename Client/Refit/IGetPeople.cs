using Refit;

namespace Client.Refit
{
	[Headers("Content-Type: application/json")]
	public interface IGetPeople
	{
		[Get("/api/people")]
		Task<List<Person>> GetAllStudent();
	}

	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
	}
}
