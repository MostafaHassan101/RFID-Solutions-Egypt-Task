using RFID.SimpleTask.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using RFID.SimpleTask.Application.Common.Models;

namespace RFID.SimpleTask.Infrastructure.Persistence;

public class IdentityDataInitializer
{
    public static async Task InitializeIdentityDataAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await SeedRoles(roleManager);
        await SeedUsers(userManager);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(SysRoles.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(SysRoles.Admin));
        }
        if (!await roleManager.RoleExistsAsync("Salesman"))
        {
            await roleManager.CreateAsync(new IdentityRole("Salesman"));
        }
        if (!await roleManager.RoleExistsAsync("Customer"))
        {
            await roleManager.CreateAsync(new IdentityRole("Customer"));
        }
    }

    private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
    {
        if (await userManager.FindByEmailAsync("admin@rfid.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin@rfid.com",
                Email = "admin@rfid.com"
            };

            var result = await userManager.CreateAsync(user, "Admin@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, SysRoles.Admin);
            }
        }
        if (await userManager.FindByEmailAsync("salesman@rfid.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "salesman@rfid.com",
                Email = "salesman@rfid.com"
            };

            var result = await userManager.CreateAsync(user, "salesman@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, SysRoles.Salesman);
            }
        }

        if (await userManager.FindByEmailAsync("customer@rfid.com") == null)
        {
            var user = new ApplicationUser
            {
                UserName = "customer@rfid.com",
                Email = "customer@rfid.com"
            };

            var result = await userManager.CreateAsync(user, "customer@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, SysRoles.Customer);
            }
        }
    }
}