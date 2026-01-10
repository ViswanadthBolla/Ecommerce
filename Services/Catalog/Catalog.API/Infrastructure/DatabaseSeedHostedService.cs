using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Catalog.Infrastructure.Data;

namespace Catalog.API.Infrastructure
{
	public class DatabaseSeedHostedService : IHostedService
	{
		private readonly IServiceProvider _services;
		private readonly ILogger<DatabaseSeedHostedService> _logger;

		public DatabaseSeedHostedService(IServiceProvider services, ILogger<DatabaseSeedHostedService> logger)
		{
			_services = services;
			_logger = logger;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_ = Task.Run(() => SeedWithRetryAsync(cancellationToken), cancellationToken);
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

		private async Task SeedWithRetryAsync(CancellationToken cancellationToken)
		{
			var maxAttempts = 10;
			for (var attempt = 1; attempt <= maxAttempts && !cancellationToken.IsCancellationRequested; attempt++)
			{
				try
				{
					using var scope = _services.CreateScope();
					var context = scope.ServiceProvider.GetRequiredService<ICatalogContext>();

					await BrandCotextSeed.SeedData(context.Brands);
					await TypeContextSeed.SeedData(context.Types);
					await CatalogContextSeed.SeedData(context.Products);

					_logger.LogInformation("Database seeding completed on attempt {Attempt}", attempt);
					break;
				}
				catch (Exception ex)
				{
					var delaySeconds = Math.Min(30, (int)Math.Pow(2, attempt));
					_logger.LogWarning(ex, "Seeding attempt {Attempt} failed. Retrying in {Delay}s...", attempt, delaySeconds);
					await Task.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken);
				}
			}
		}
	}
}
