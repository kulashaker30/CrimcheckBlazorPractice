using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineClinic.Core.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider, IHostEnvironmentAuthenticationStateProvider
{
    private readonly IUserService _userService;

    private AuthenticationState _authenticationState;

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

            _authenticationState = new AuthenticationState(new ClaimsPrincipal(identity));
            return true;
        }
        return false;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult<AuthenticationState>(_authenticationState);

    public void SetAuthenticationState(Task<AuthenticationState> authenticationStateTask)
    {
        authenticationStateTask = authenticationStateTask ?? throw new ArgumentNullException(nameof(authenticationStateTask));
        NotifyAuthenticationStateChanged(authenticationStateTask);
    }
}