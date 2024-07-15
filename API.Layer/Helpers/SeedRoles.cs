using Microsoft.AspNetCore.Identity;

namespace API.Layer.Helpers
{
	public static class SeedRoles
	{
		

		public static async Task SeedRolesAsync(RoleManager<IdentityRole> _roleManager, string roleName)
		{
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
				var adminRole = new IdentityRole(roleName);
				
				await _roleManager.CreateAsync(adminRole);
            }
			
		}
    }
}
