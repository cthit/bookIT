using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UnBookIT.Authentication;

class GammaAuthenticationHandler : OAuthHandler<GammaAuthenticationOptions>
{
	public GammaAuthenticationHandler(
		IOptionsMonitor<GammaAuthenticationOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder,
		ISystemClock clock)
		: base(options, logger, encoder, clock)
	{
	}

	protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(OAuthCodeExchangeContext context)
	{
		var tokenRequestParameters = new Dictionary<string, string>()
		{
			["grant_type"] = "authorization_code",
			["code"] = context.Code,
#if DEBUG
			["redirect_uri"] = context.RedirectUri,
#else
			["redirect_uri"] = context.RedirectUri.Replace("http://", "https://"),
#endif
			["client_id"] = Options.ClientId,
			["client_secret"] = Options.ClientSecret,
		};

		// PKCE https://tools.ietf.org/html/rfc7636#section-4.5, see BuildChallengeUrl
		if (context.Properties.Items.TryGetValue(OAuthConstants.CodeVerifierKey, out var codeVerifier))
		{
			tokenRequestParameters.Add(OAuthConstants.CodeVerifierKey, codeVerifier!);
			context.Properties.Items.Remove(OAuthConstants.CodeVerifierKey);
		}

		var requestMessage = new HttpRequestMessage(HttpMethod.Post, Options.TokenEndpoint);
		requestMessage.Headers.Add("Accept", "application/json");
		requestMessage.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Options.ClientId}:{Options.ClientSecret}"))}");
		requestMessage.Content = new FormUrlEncodedContent(tokenRequestParameters);
		requestMessage.Version = Backchannel.DefaultRequestVersion;
		var response = await Backchannel.SendAsync(requestMessage, Context.RequestAborted);
		var body = await response.Content.ReadAsStringAsync();

		return response.IsSuccessStatusCode switch
		{
			true => OAuthTokenResponse.Success(JsonDocument.Parse(body)),
			false => OAuthTokenResponse.Failed(new Exception($"OAuth token endpoint failure: Status: {response.StatusCode};Headers: {response.Headers};Body: {body};")),
		};
	}

#if !DEBUG
	protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
	{
		redirectUri = redirectUri.Replace("http://", "https://");
		return base.BuildChallengeUrl(properties, redirectUri);
	}
#endif
}
