using Corelibs.BlazorShared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Corelibs.BlazorWebAssembly
{
    public class WebSignInRedirector : ISignInRedirector
    {
        private readonly NavigationManager _navigation;

        public WebSignInRedirector(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        public void Redirect(Exception exception)
        {
            if (exception is AccessTokenNotAvailableException accessTokenException)
            {
                //accessTokenException.Redirect();
                //_navigation.NavigateTo(_navigation.BaseUri, forceLoad: true);
            }
        }
    }
}
