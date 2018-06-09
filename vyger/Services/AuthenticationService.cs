using System;
using System.Net;
using Newtonsoft.Json;
using vyger.Models;

namespace vyger.Services
{
    #region Interface

    public interface IAuthenticationService
    {
        AuthenticationToken VerifyGoogleAuthentication(string tokenId);
    }

    #endregion

    public class AuthenticationService : IAuthenticationService
    {
        #region Methods

        public AuthenticationToken VerifyGoogleAuthentication(string tokenId)
        {
            using (WebClient web = new WebClient())
            {
                try
                {
                    string url = $"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={tokenId}";

                    string json = web.DownloadString(url);

                    return JsonConvert.DeserializeObject<AuthenticationToken>(json);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #endregion
    }
}
