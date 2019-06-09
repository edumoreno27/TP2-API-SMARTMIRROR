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
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART_MIRROR.API.Data;
using SMART_MIRROR.API.Util;



namespace SMART_MIRROR.API.Controllers
{
    [AllowAnonymous]
    [EnableCors("MyPolicy")]
    public class GmailApiIntegrationController : Controller
    {

        public readonly ApiDbContext _context;

        public GmailApiIntegrationController(ApiDbContext context)
        {
            _context = context;
        }



        [HttpGet("GetEmails")]
        public async Task<IActionResult> GetEmailsAsync()
        {
            string error = string.Empty;
            string refreshtoken = "";
            
            var user = await _context.Users.Where(x=>x.Email == "Gabosh140@gmail.com").FirstOrDefaultAsync();

            //var first_credential = GoogleOauthUtility.GetUserCredential(out error);

            //if (first_credential != null)
            //{
            //    refreshtoken = first_credential.Token.RefreshToken;
            //    user.Refreshtoken = refreshtoken;
            //    await _context.SaveChangesAsync();
            //}

            //var credential = GoogleOauthUtility.GetUserCredentialByRefreshToken(_context, out error);
            //var token = new TokenResponse
            //{
            //    AccessToken = SavedAccount.Properties["access_token"],
            //    ExpiresInSeconds = Convert.ToInt64(SavedAccount.Properties["expires_in"]),
            //    RefreshToken = SavedAccount.Properties["refresh_token"],
            //    Scope = GoogleDriveScope,
            //    TokenType = SavedAccount.Properties["token_type"],
            //};


            //var flow = new  GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            //{
            //    ClientId = GoogleOauthUtility.ClientId,
            //    ClientSecret = GoogleOauthUtility.ClientSecret
            //},
            //    GoogleOauthUtility.Scopes,
            //    Environment.UserName,
            //    CancellationToken.None,
            //    new FileDataStore("Store")).Result;

            //var token = new TokenResponse
            //{
            //    RefreshToken = "AEu4IL1_mW8H366ME0aZbi6lJes0TtGR2D4PLDWPEaOhvlb0XwA7-T8Ww0c1dGnS6OCN7A4VgBpEA70QWwMeVIJTEnuSyidxCIOSXXgzeEfhong9yKENQ7-WGzCyBkU87zDHH3xuzH-O3pVX4lX1gkIkFxL7RV1u_WgFK6zdziQG-cMqDYJAzLt5eCkrbE2vM22guFsONJN0q3kxIQylvtVmJHxF46uKLeDJjFkLlo3TyXmqIBb_5uu8ihLpzPtMVgWu5250-ckAhk-rsiEzErrFidFJwvyzq--05l-A8UvzHao2eIO1nF1UnaaUvQlE8WMMl86wnIu_X8BpATEf_y9rNXdyyjd2ZOvz_VBVShLpX43vVdSbVWAFdqd_rQNUfHvsusmQPIM_VvGFPfHN3Iu-h_-WqB53Sw",
            //    AccessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImZmMWRmNWExNWI1Y2Y1ODJiNjFhMjEzODVjMGNmYWVkZmRiNmE3NDgiLCJ0eXAiOiJKV1QifQ.eyJuYW1lIjoiR2FicmllbCBCZW5hdmVudGUiLCJwaWN0dXJlIjoiaHR0cHM6Ly9saDMuZ29vZ2xldXNlcmNvbnRlbnQuY29tLy1PNTNxckZqZXA1ay9BQUFBQUFBQUFBSS9BQUFBQUFBQUFBQS9BQ0hpM3JmU1htdGtNNW9OZWNEdmNudVIxZlNpYVF4aGJnL3M5Ni1jL3Bob3RvLmpwZyIsImlzcyI6Imh0dHBzOi8vc2VjdXJldG9rZW4uZ29vZ2xlLmNvbS9wcm95ZWN0b3RkcC1kOWU2ZCIsImF1ZCI6InByb3llY3RvdGRwLWQ5ZTZkIiwiYXV0aF90aW1lIjoxNTUzOTIzNzA2LCJ1c2VyX2lkIjoiMHp6N1B2Smh3Y1dxZHFYS3pMZVpLU1RwVDY4MiIsInN1YiI6IjB6ejdQdkpod2NXcWRxWEt6TGVaS1NUcFQ2ODIiLCJpYXQiOjE1NTM5MjM3MDYsImV4cCI6MTU1MzkyNzMwNiwiZW1haWwiOiJnYWJvc2gxNDBAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsImZpcmViYXNlIjp7ImlkZW50aXRpZXMiOnsiZ29vZ2xlLmNvbSI6WyIxMDE2MTg4MzY5NDg5NzQ5MDY0NDIiXSwiZW1haWwiOlsiZ2Fib3NoMTQwQGdtYWlsLmNvbSJdfSwic2lnbl9pbl9wcm92aWRlciI6Imdvb2dsZS5jb20ifX0.aIdMr9Q7TSYIjoT-t5i8df8NjGOk4bfs1xvelzUWqV3BDEnn5Q4HuPLJWxDMTxmmrRmthtG1c-XBuL55F5ytypAR1THR-RazblUNveOnnZMRCofS9UDr6UG9XDg-xNjNwzy7VHnIajvUNO27voBxFvO3RrTHd2Yflb84H2GQpSl2WR6guGnM6VBvX3qwcV0-a3_G5OcPt-58mxkpzdmnLot8ROAi-APsIOlbc0Hq9X87OHC3rhMmTO6r38fK8Vm__vKOB84myz-6TSkque90AEKKG_5eBVUSAlo7RGtwS9YAIvj5VfdjLS_0IZjOfpAF2bTS3o9XBSFcWSZh_RKqpg",

            //};

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = GoogleOauthUtility.ClientId,
                    ClientSecret = GoogleOauthUtility.ClientSecret
                },
                Scopes = GoogleOauthUtility.Scopes,
                DataStore = new FileDataStore("Store")
            });

            var token = new TokenResponse
            {
                RefreshToken = "AEu4IL0nm6AE25Fsko1-GNDc6hjcTK5McA0_wqT_qBbpZ3eZQUg753jigLm2ONOJCXrIB5N5tR7GqxEubEbhv2b8VcG3GxJXZT-KmMDzwmd_7IeZvJSzVSbpuznybLCmzmvR3GpiVvoSlAA7pqjuMSVvSgDOrxklIw0WQ5-QrNi5RyoH7YqrnDQxDalBxgIR5XqrNz2tScdbzFTFDu9Qbmxzt1grERqORWl8NOh1TQJ7PG6swrRa8kCwjgPvr1c23Y8TH44g4RaaCzDPlv9ur3KrAGSCksikaKMpgwSFiaCFSGV0hGVoy_rFBDdFjWpnhbji1FmTxHZGxxtCTCD_pIdgkj_CcerxIKJDnQCbd64a4mKlARbAfDioBI60GFoIK0N3k6Jq62jwYIrv1NfzgOOKMQkB3LqGDg",
                AccessToken = "ya29.GlvcBnSFbkXKEvepXTLhd2txGwr4pG_4zRVYewRzLUy3R_EBiz5isyGMpqQAG9mirB66xuQFBzq9DCtDGXgFVH3EIPBTnQobKxBAUYm8LvsLxZa5PQLXMk7ec4A7"
            };

            var credential = new UserCredential(flow, "Gabosh140@gmail.com", token);


            //var credential = new UserCredential(GoogleOauthUtility.GoogleAuthorizationCodeFlow(out error), "user", token);
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
               
                ApplicationName = GoogleOauthUtility.ApplicationName,
            });
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");


            return Ok();

        }
    }        
    
}