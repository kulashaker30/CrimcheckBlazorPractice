
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Authorization;
using OnlineClinic.Core.Services;
using OnlineClinic.Core.DTOs;

namespace BlazorUI.Pages.Authentication
{
    public partial class LoginPage: ComponentBase
    {
        [Parameter]
        public Task<AuthenticationState> AuthState { get; set; }

        [Parameter]
        public EventCallback<Task<AuthenticationState>> AuthStateChanged { get; set; }

        [Inject]
        protected CustomAuthStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IUserService UserService { get; set; }

        private LoginDto loginDto;

        private EditContext editContext;

        protected override void OnInitialized()
        {
            loginDto = new LoginDto(UserService);
            editContext = new EditContext(loginDto);
            base.OnInitialized();
        }

        private void Authenticate()
        {
            var isValid = editContext.Validate();

            if(isValid)
            {
                isValid = ((CustomAuthStateProvider)AuthenticationStateProvider).Authenticate(loginDto.Username, loginDto.Password, loginDto.IsAdmin);
                if(isValid)
                {
                    AuthState = ((AuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
                    AuthStateChanged.InvokeAsync(AuthState);
                    NavigationManager.NavigateTo(loginDto.IsAdmin? "doctors" : "consultations");
                }
            }
        }
    }
}