using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.eShopWeb.Infrastructure.Identity;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.eShopWeb.Infrastructure.Data.Ignite;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class LoginService : IDisposable
    {
        private IIgniteAdapter _ignite;

        public LoginService()
        {
            _ignite = TestUtils.GetIgnite();
        }
        
        [Fact]
        public async Task LogsInSampleUser()
        {
            var services = new ServiceCollection();
                
            services
                .AddSingleton(_ignite)
                .AddLogging()
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddUserStore<IgniteUserStore>()
                .AddRoleStore<IgniteRoleStore>()
                .AddDefaultTokenProviders();

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;

            // seed sample user data
            var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

            await AppIdentityDbContextSeed.SeedAsync(userManager);

            var signInManager = scopedServices.GetRequiredService<SignInManager<ApplicationUser>>();
            signInManager.Context = new DefaultHttpContext
            {
                RequestServices = scopedServices
            };

            var email = "demouser@microsoft.com";
            var password = "Pass@word1";

            var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

            Assert.True(result.Succeeded);
        }

        public void Dispose()
        {
            _ignite?.Dispose();
        }
    }
}
