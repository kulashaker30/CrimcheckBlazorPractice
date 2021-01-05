using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using OnlineClinic.Core.Services;

public class CustomAuthStateProvider : ServerAuthenticationStateProvider, IHostEnvironmentAuthenticationStateProvider
{
    private readonly IUserService _userService;

    public CustomAuthStateProvider(IUserService userService)
    {
        _userService = userService;
    }

    public bool Authenticate(string username, string password, bool AdminUser = false)
    {
        var isValid = _userService.Authenticate(username, password, AdminUser);
        if(isValid)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim("Username", username),
                new Claim("Role", AdminUser? "Administrator" : "Doctor"),
                new Claim(ClaimTypes.Role,  AdminUser? "Administrator" : "Doctor"),
                new Claim(ClaimTypes.NameIdentifier, AdminUser? "Administrator" : username)
            }, "Web", username,  AdminUser? "Administrator" : "Doctor");            

            var authenticationState = new AuthenticationState(new ClaimsPrincipal(identity));
            SetAuthenticationState(Task.FromResult<AuthenticationState>(authenticationState));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return true;
        }
        return false;
    }

    public void Logout()
    {
      var authenticationState = new AuthenticationState(new ClaimsPrincipal());
      SetAuthenticationState(Task.FromResult<AuthenticationState>(authenticationState));
      NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    
}