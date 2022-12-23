using Corelibs.BlazorShared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Corelibs.BlazorWebAssembly
{
    public static class BlazorWebAssemblyExtensions
    {
        public static void AddAuthorizationAndSignInRedirection(
           this IServiceCollection services, string baseAddress)
        {
            services.AddAuthorizationAndSignInRedirection<
                WebAuthUser, WebSignInRedirector, BaseAddressAuthorizationMessageHandler>(
                baseAddress);
        }
    }
}
