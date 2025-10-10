using FCG.Infra.Security.Constants;
using FCG.Infra.Security.Models;
using Microsoft.AspNetCore.Identity;

namespace FCG.Infra.Security.Seeds
{
    public static class IdentitySeed
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Roles.ObterListaRoles())
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminUser(UserManager<IdentityCustomUser> userManager, string email, string password)
        {
            await SeedUser(userManager,
                "Administrador",
                email,
                password,
                [
                    Roles.USUARIO,
                    Roles.ADMINISTRADOR
                ]);
        }

        public static async Task SeedTestingData(UserManager<IdentityCustomUser> userManager)
        {
            await SeedUser(userManager,
                "Administrador",
                "admin@fcg.com",
                "Admin123!",
                [
                    Roles.USUARIO,
                    Roles.ADMINISTRADOR
                ]);
            await SeedUser(userManager,
                "Danilo",
                "danilo@fcg.com",
                "Danilo123!",
                [
                    Roles.USUARIO
                ]);
        }

        #region AUXILIARES

        private static async Task SeedUser(UserManager<IdentityCustomUser> userManager,
            string nome,
            string email,
            string senha,
             List<string> roles)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityCustomUser
                {
                    Nome = nome,
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, senha);
                if (result.Succeeded)
                {
                    foreach (var role in roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }

        #endregion
    }
}