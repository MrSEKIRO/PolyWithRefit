var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Where registering services
builder.Services.AddCors(policy => {
	policy.AddPolicy("OpenCorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//app.MapGet("/api/people", () =>
//{
//	var people = new List<Person>
//	{
//		new Person { Id = 1, Name = "John Doe", Age = 30 },
//		new Person { Id = 2, Name = "Jane Doe", Age = 25 },
//		new Person { Id = 3, Name = "Joe Doe", Age = 15 },
//		new Person { Id = 4, Name = "Jill Doe", Age = 40 },
//		new Person { Id = 5, Name = "Jack Doe", Age = 50 },
//	};


//	return people;
//});

//app configurations
app.UseCors("OpenCorsPolicy");

app.Run();
