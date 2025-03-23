using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            var roleAdministrator = await roleManager.FindByNameAsync(Roles.Administrator.ToString());
            if (roleAdministrator == null)
                await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));

            var roleStudent = await roleManager.FindByNameAsync(Roles.Student.ToString());
            if (roleStudent == null)
                await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));

            var roleProfessor = await roleManager.FindByNameAsync(Roles.Professor.ToString());
            if (roleProfessor == null)
                await roleManager.CreateAsync(new IdentityRole(Roles.Professor.ToString()));
        }
    }
}
