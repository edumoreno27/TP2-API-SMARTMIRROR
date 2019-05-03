using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Util.Store;
using Microsoft.EntityFrameworkCore;
using SMART_MIRROR.API.Data;

namespace SMART_MIRROR.API.Util
{
    public class GoogleOauthUtility
    {
        public readonly ApiDbContext _context;

        public GoogleOauthUtility(ApiDbContext context)
        {
            _context = context;
        }
        public static string ApplicationName = "Test";
        public static string ClientId = "541429281292-8v02kma7fl8fhmiih66kdqnlh8b6opmn.apps.googleusercontent.com";
        public static string ClientSecret = "XbyAoBAIlUlqBbS1Lal0GrAF";

        public static ClientSecrets GoogleClienSecrets = new ClientSecrets()
        {
            ClientId = ClientId,
            ClientSecret  = ClientSecret
        };

        public static string[] Scopes = new string[] {
            GmailService.Scope.GmailReadonly,
            //CalendarService.Scope.CalendarEventsReadonly 
        };

        public static UserCredential GetUserCredentialByRefreshToken(ApiDbContext _context,  out string error)
        {
            UserCredential credential = null;
            TokenResponse responseToken = null;
            string refreshToken = string.Empty;
            string flowError;
            error = string.Empty;

            try
            {
                IAuthorizationCodeFlow flow = GoogleAuthorizationCodeFlow(out flowError);

                var user = _context.Users.FirstOrDefault();
                refreshToken = user.Refreshtoken;
                responseToken = new TokenResponse() { RefreshToken = refreshToken };
                credential = new UserCredential(flow, "user", responseToken);
                if (credential!=null)
                {
                    bool success = credential.RefreshTokenAsync(CancellationToken.None).Result;
                }
                
            }

            catch (Exception e)
            {

                credential = null;
                error = "Fallo autorizacion : " + e.Message;
            }
            return credential;
        }

        private static TokenResponse TokenResponse()
        {
            throw new NotImplementedException();
        }

        public static IAuthorizationCodeFlow GoogleAuthorizationCodeFlow(out string error) {
            IAuthorizationCodeFlow flow = null;
            error = string.Empty;
            try
            {
                flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                { 
                    ClientSecrets = GoogleClienSecrets,
                    Scopes = Scopes,
                    
                });
            }
            catch (Exception e)
            {

                flow = null;
                error = "Fallo el authorizationCodeFlow Initialization" + e.Message;
            }
            return flow;
        }

        public  static UserCredential GetUserCredential(out string error)
        {
            UserCredential credential = null;           
            error = string.Empty;

            try
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = ClientId,
                        ClientSecret = ClientSecret,
                    },
                    Scopes,
                    Environment.UserName,
                    CancellationToken.None,                   
                    new FileDataStore("Google Oauth2 Client App")).Result;                    
        
            }

            catch (Exception e)
            {

                credential = null;
                error = "Fallo autorizacion : " + e.Message;
            }
            return credential;
        }

    }
}
