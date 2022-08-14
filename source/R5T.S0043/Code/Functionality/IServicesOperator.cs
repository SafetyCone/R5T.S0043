using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IServicesOperator : IFunctionalityMarker
	{
		private static ServiceCollection Services { get; set; }
		private static ServiceProvider ServiceProvider { get; set; }


		public void ConfigureStaticServices()
        {
			IServicesOperator.Services = this.NewServiceCollection_Configured();
			IServicesOperator.ServiceProvider = IServicesOperator.Services.BuildServiceProvider();
        }

		public void ConfigureServices(IServiceCollection services)
        {
			services
				.AddLogging(loggingBuilder =>
				{
					loggingBuilder
						.SetMinimumLevel(LogLevel.Debug)
						.AddConsole()
						;
				})
				;
        }

		public ServiceCollection ConfigureServices()
        {
			var services = this.NewServiceCollection_Empty();

			this.ConfigureServices(services);

			return services;
		}

		public ServicesContext GetServicesContext()
        {
			var services = this.ConfigureServices();

			var output = this.GetServicesContext(services);
			return output;
		}

		public ServicesContext GetServicesContext(ServiceCollection services)
        {
			var serviceProvider = services.BuildServiceProvider();

			var output = new ServicesContext(serviceProvider);
			return output;
		}

		public T GetService<T>()
        {
			var output = IServicesOperator.ServiceProvider.GetRequiredService<T>();
			return output;
        }

		public async Task InServicedContext(Func<Task> action)
        {
			this.ConfigureStaticServices();

			await action();

			await IServicesOperator.ServiceProvider.DisposeAsync();
        }

		public ServiceCollection NewServiceCollection_Configured()
        {
			var services = this.NewServiceCollection_Empty();

			this.ConfigureServices(services);

			return services;
        }

		public ServiceCollection NewServiceCollection_Empty()
		{
			var output = new ServiceCollection();
			return output;
		}
	}
}