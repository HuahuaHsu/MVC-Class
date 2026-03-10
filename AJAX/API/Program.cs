
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<NorthwindContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Northwind"));
			});

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("MVC", policy =>
				{
					//全開放
					//允許任何標頭、來源、方法
					policy.AllowAnyHeader()
					.WithOrigins("https://localhost:7002")
					.AllowAnyMethod();
				});
			});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseCors("MVC");//沒傳入為尚未套用功能，有傳入為套用功能，這裡套用剛剛建立的CORS政策
			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
