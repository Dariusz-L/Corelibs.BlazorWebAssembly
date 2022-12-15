using Corelibs.BlazorShared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Corelibs.BlazorWebAssembly
{
    public class WebAuthUser : IAuthUser
    {
        private readonly IAccessTokenProvider _provider;
        private readonly NavigationManager _navigation;
        private readonly SignOutSessionStateManager _signOutManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        private bool? _isSignedIn = false;
        public async Task<bool> IsSignedIn()
        {
            var tokenResult = await _provider.RequestAccessToken();
            if (!tokenResult.TryGetToken(out var token))
                return false;

            _isSignedIn = true;
            return true;
        }

        private string _name = string.Empty;
        public string Name => _name;

        public event Action<bool> OnAuthenticatedStateChanged;

        public WebAuthUser(
            IAccessTokenProvider provider,
            NavigationManager navigation,
            SignOutSessionStateManager signOutManager,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _provider = provider;
            _navigation = navigation;
            _signOutManager = signOutManager;
            _authenticationStateProvider = authenticationStateProvider;

            _authenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var state = await task;
            _isSignedIn = state.User.Identity?.IsAuthenticated;
            
            if (_isSignedIn.Value)
                _name = state.User.Identity?.Name ?? string.Empty;

            OnAuthenticatedStateChanged?.Invoke(_isSignedIn.Value);
        }

        public Task SignIn()
        {
            _navigation.NavigateTo("authentication/login");
            return Task.CompletedTask;
        }

        public async Task SignOut()
        {
            await _signOutManager.SetSignOutState();
            _navigation.NavigateTo("authentication/logout");
        }
    }
}
