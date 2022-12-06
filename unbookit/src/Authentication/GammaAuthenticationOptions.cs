using System;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace UnBookIT.Authentication;

class GammaAuthenticationOptions : OAuthOptions
{
	public GammaAuthenticationOptions()
	{
		var gammaDomain = Environment.GetEnvironmentVariable("GAMMA_DOMAIN") ?? "http://localhost:8081";

		AuthorizationEndpoint = $"{gammaDomain}/api/oauth/authorize";
		CallbackPath = Environment.GetEnvironmentVariable("GAMMA_CALLBACK_PATH") ?? "/auth/account/callback";
		ClientId = Environment.GetEnvironmentVariable("GAMMA_CLIENT_ID") ?? "id";
		ClientSecret = Environment.GetEnvironmentVariable("GAMMA_CLIENT_SECRET") ?? "secret";
		TokenEndpoint = $"{gammaDomain}/api/oauth/token";
		UserInformationEndpoint = $"{gammaDomain}/api/users/me";
		CorrelationCookie.SameSite = SameSiteMode.Strict;
	}
}
