using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace QuickTrackWeb.Pages
{
    public class AuthorizationBase : ComponentBase
    {
        public UserManager<IdentityUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public AuthenticationStateProvider _authenticationStateProvider;
        public AuthorizationBase()
        // UserManager<IdentityUser> userManager,
        // RoleManager<IdentityRole> roleManager,
        //  AuthenticationStateProvider authenticationStateProvider)
        {
            //_userManager = userManager;
            //_roleManager = roleManager;
            // _authenticationStateProvider = authenticationStateProvider;
        }
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        public string ADMINISTRATION_ROLE = "Administrators";
        System.Security.Claims.ClaimsPrincipal CurrentUser;
        protected override async Task OnInitializedAsync()
        {
            // ensure there is a ADMINISTRATION_ROLE
            var RoleResult = await _roleManager.FindByNameAsync(ADMINISTRATION_ROLE);
            if (RoleResult == null)
            {
                // Create ADMINISTRATION_ROLE Role
                await _roleManager.CreateAsync(new IdentityRole(ADMINISTRATION_ROLE));
            }
            // Ensure a user named Admin@BlazorHelpWebsite.com is an Administrator
            var user = await _userManager.FindByNameAsync("rc-traviolia@wiu.edu");
            if (user != null)
            {
                // Is Admin@BlazorHelpWebsite.com in administrator role?
                var UserResult = await _userManager.IsInRoleAsync(user, ADMINISTRATION_ROLE);
                if (!UserResult)
                {
                    // Put admin in Administrator role
                    await _userManager.AddToRoleAsync(user, ADMINISTRATION_ROLE);
                }
            }
            // Get the current logged in user
            CurrentUser = (await authenticationStateTask).User;
        }
    }
}

