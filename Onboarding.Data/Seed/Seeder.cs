using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Onboarding.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Onboarding.Data.Seed
{
    public class Seeder
    {
        private static string path = Path.GetFullPath(@"../Onboarding.Data/Data.Json/");
        private const string adminPassword = "Secret@123";

        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            //Get the Database context
            var ctx = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<OnboardingDbContext>();

            if (ctx.Database.GetPendingMigrations().Any())
            {
                ctx.Database.Migrate();
            }
            if (ctx.Users.Any())
            {
                return;
            }
            else
            {
                var userManager = app.ApplicationServices.CreateScope()
                                              .ServiceProvider.GetRequiredService<UserManager<Customer>>();

                //Seed states with LGA json files
                var states = GetSampleData<State>(File.ReadAllText(path + "States.json"));
                await ctx.States.AddRangeAsync(states);

                //Seed the customers
                var customers = GetSampleData<Customer>(File.ReadAllText(path + "Customers.json"));

                foreach (var customer in customers)
                {
                    customer.UserName = customer.Email;
                    await userManager.CreateAsync(customer);
                    var token = await userManager.GenerateChangePhoneNumberTokenAsync(customer, customer.PhoneNumber);
                    await userManager.ChangePhoneNumberAsync(customer, customer.PhoneNumber, token);
                }

                await ctx.SaveChangesAsync();
            }
        }
        private static List<T> GetSampleData<T>(string location)
        {
            return JsonSerializer.Deserialize<List<T>>(location, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
