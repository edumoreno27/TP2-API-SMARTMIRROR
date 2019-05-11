using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System.Web;
using SMART_MIRROR.API.Data;
using SMART_MIRROR.API.Model;
using SMART_MIRROR.ENTITIES.Models;
using SMART_MIRROR.API.Util;

namespace SMART_MIRROR.API.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        public readonly ApiDbContext _context;

        public UserController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserViewModel model)
        {
            if (await _context.Users.AnyAsync(s => s.Email == model.Email && s.IdReference == model.IdReference))
            {                
                var userExist = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();                                                           

                var client = new RestClient("https://www.googleapis.com");
                var tokenRequest = new RestRequest("oauth2/v4/token", Method.POST);

                // tokenRequest.AddHeader("Host", "www.googleapis.com");
                tokenRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                tokenRequest.AddParameter("code", model.Refreshtoken);
                tokenRequest.AddParameter("client_id", GoogleOauthUtility.ClientId);
                tokenRequest.AddParameter("client_secret", GoogleOauthUtility.ClientSecret);
                tokenRequest.AddParameter("redirect_uri", "urn:ietf:wg:oauth:2.0:oob");
                tokenRequest.AddParameter("grant_type", "authorization_code");

                var response = client.Execute<AuthCodeResponse>(tokenRequest);


                userExist.Refreshtoken = response.Data.refresh_token;
                userExist.Accesstoken = response.Data.access_token;
                userExist.Token_type = response.Data.token_type;
                userExist.Expires_in = response.Data.expires_in;
                userExist.MirrorId = model.MirrorId;
                userExist.RoomNumber = model.RoomNumber;
                await _context.SaveChangesAsync();
                return Ok(userExist);
            }
            else
            {

                var client = new RestClient("https://www.googleapis.com");
                var tokenRequest = new RestRequest("oauth2/v4/token", Method.POST);

                // tokenRequest.AddHeader("Host", "www.googleapis.com");
                tokenRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                tokenRequest.AddParameter("code", model.Refreshtoken);
                tokenRequest.AddParameter("client_id", GoogleOauthUtility.ClientId);
                tokenRequest.AddParameter("client_secret", GoogleOauthUtility.ClientSecret);
                tokenRequest.AddParameter("redirect_uri", "urn:ietf:wg:oauth:2.0:oob");
                tokenRequest.AddParameter("grant_type", "authorization_code");

                var response = client.Execute<AuthCodeResponse>(tokenRequest);


                try
                {
                    var user = new User()
                    {
                        Email = model.Email,                        
                        Refreshtoken = response.Data.refresh_token,
                        Expires_in = response.Data.expires_in,
                        Accesstoken = response.Data.access_token,
                        IdReference = model.IdReference,
                        MirrorId = model.MirrorId,
                        RoomNumber=model.RoomNumber
                    };
                    

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    var booleanTable = new BooleanTable()
                    {
                        Order = false,
                        Status = true,
                        Diary = true,
                        Email = false,
                        Sesion = true,
                        Music = false,
                        HotelService = false,
                        StartNews = false,
                        StartEmail = false,
                        UserId = user.Id
                    };
                    await _context.BooleanTables.AddAsync(booleanTable);
                    await _context.SaveChangesAsync();

                    var lstGadgets = await _context.Gadgets.ToListAsync();
                    foreach (var item in lstGadgets)
                    {
                        var userGadget = new UserGadgets()
                        {
                            GadgetId = item.Id,
                            UserId = user.Id,                            
                            Order = item.StaticNumber,
                            IsActive = true

                        };
                        await _context.UserGadgets.AddAsync(userGadget);
                    }

                    var musicAction = new MusicAction()
                    {
                        UserId = user.Id,
                        Action = "",
                        MirrorId = model.MirrorId,
                        MusicBool = false
                    };
                    await _context.MusicActions.AddAsync(musicAction);

                    await _context.SaveChangesAsync();
                    return Ok(user);

                }
                catch (Exception e)
                {

                    throw e;
                }

            }


        }

        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUserByRoomNumber([FromBody] UserViewModel model)
        {
            var user = await _context.Users.Where(x => x.MirrorId == model.MirrorId).FirstOrDefaultAsync();
            if(user != null)
            {
                return Ok(user);
            }
            else
            {
                return Ok(new { status = false });
            }

            
        }

        [HttpPost("ValidateCurrency")]
        public async Task<IActionResult> ValidateCurrency([FromBody] UserViewModel model)
        {
            var booleanResult = await _context.Users.AnyAsync(x => x.MirrorId == model.MirrorId);
            return Ok(booleanResult);
        }
        //[HttpGet("GetUserGet/{roomNumber}")]
        //public async Task<IActionResult> GetUserGet([FromRoute] int roomNumber)
        //{
        //    var user = await _context.UserGadgets.Include(x => x.User).Where(x => x.RoomNumber == roomNumber)
        //        .Select(x => x.User).FirstOrDefaultAsync();
        //    return Ok(user);
        //}
        [HttpPost("AfterLogout")]
        public async Task<IActionResult> LogoutActions([FromBody] UserViewModel model)
        {
            try
            {
                var user = await _context.Users.Where(x => x.IdReference == model.RoomNumber).FirstOrDefaultAsync();
                var usergadgets = await _context.UserGadgets.Where(x => x.UserId == user.Id).ToListAsync();
                foreach (var item in usergadgets)
                {
                    _context.UserGadgets.Remove(item);
                }


                var booleanTable = await _context.BooleanTables.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                if (booleanTable != null)
                {
                    _context.BooleanTables.Remove(booleanTable);
                }
                

                var emailinformations = await _context.EmailInformations.Where(x => x.UserId == user.Id).ToListAsync();
                foreach (var item in emailinformations)
                {
                    _context.EmailInformations.Remove(item);
                }
                var diaryinformations = await _context.DiaryInformations.Where(x => x.UserId == user.Id).ToListAsync();
                foreach (var item in diaryinformations)
                {
                    _context.DiaryInformations.Remove(item);
                }

                var diarygoogles = await _context.DiaryGoogles.Where(x => x.UserId == user.Id).ToListAsync();
                foreach (var item in diarygoogles)
                {
                    _context.DiaryGoogles.Remove(item);
                }

                var musicAction = await _context.MusicActions.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                if (musicAction != null)
                {
                    _context.MusicActions.Remove(musicAction);
                }

                var newInformation = await _context.NewsInformationAction.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                if (newInformation != null)
                {
                    _context.NewsInformationAction.Remove(newInformation);
                }

                var hotelserviceAction = await _context.HotelServiceInformations.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                
                if (hotelserviceAction != null)
                {
                    _context.HotelServiceInformations.Remove(hotelserviceAction);
                }
                var hotelService = await _context.HotelServices.Where(x => x.UserId == user.Id).ToListAsync();
                foreach (var item in hotelService)
                {
                    _context.HotelServices.Remove(item);
                }
                //booleanTable.Sesion = true;
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(new { status = true }
                    );

            }
            catch (Exception e)
            {
                return Ok(new
                {
                    status = false,
                    error = e
                }
                    );
            }
        }
    
    }
}