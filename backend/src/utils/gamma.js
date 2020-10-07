const axios = require("axios");

const gammaSettings = {
    clientId: "id",
    clientSecret: "secret",
    gammaUri: "http://gamma-backend:8081/api",
    tokenUri: "http://gamma-backend:8081/api/oauth/token",
    authorizationUri: "http://localhost:8081/api/oauth/authorize",
    redirectUri: "http://localhost:3001/auth/account/callback",
};

const getGammaUri = () => {
    const responseType = "response_type=code";
    const clientId = "client_id=" + gammaSettings.clientId;
    const redirectUri = "redirect_uri=" + gammaSettings.redirectUri;
    return (
        gammaSettings.authorizationUri +
        "?" +
        responseType +
        "&" +
        clientId +
        "&" +
        redirectUri
    );
};

async function postGammaToken(code) {
    const params = new URLSearchParams();
    params.append("grant_type", "authorization_code");
    params.append("client_id", gammaSettings.clientId);
    params.append("redirect_uri", gammaSettings.redirectUri);
    params.append("code", code);

    const c = Buffer.from(
        gammaSettings.clientId + ":" + gammaSettings.clientSecret
    ).toString("base64");

    return axios.post(gammaSettings.tokenUri + "?" + params.toString(), null, {
        headers: {
            "Content-Type": "application/x-www-form-urlencoded",
            Authorization: "Basic " + c,
        },
    });
}

const gammaGet = (endpoint, token) =>
    axios.get(gammaSettings.gammaUri + endpoint, {
        headers: {
            Authorization: "Bearer " + token,
        },
    });

const getMe = token => {
    return gammaGet("/users/me", token);
};

module.exports = {
    getGammaUri,
    postGammaToken,
    gammaGet,
    getMe,
};
