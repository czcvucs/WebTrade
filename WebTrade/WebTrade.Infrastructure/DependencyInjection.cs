using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebTrade.Domain.Interfaces;
using WebTrade.Infrastructure.Repositories;

namespace WebTrade.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           string connectionString)
        {
            services.AddDbContext<WebTradeDbContext>(opt => opt.UseSqlite(connectionString,
                opts => opts.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IMarketRepository, MarketRepository>();
            return services;
        }
    }
}
