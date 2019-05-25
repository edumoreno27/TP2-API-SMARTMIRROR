using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART_MIRROR.API.Data;
using SMART_MIRROR.API.Model;
using SMART_MIRROR.ENTITIES.Models;

namespace SMART_MIRROR.API.Controllers
{
    public class NewsController : Controller
    {
        public readonly ApiDbContext _context;

        public NewsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveNewsInformation")]
        public async Task<IActionResult> SaveNewsInformation([FromBody] NewsInformationViewModel model)
        {



            if (await _context.NewsInformationAction.AnyAsync(x => x.UserId == model.UserId))
            {
                var newsUserDetected = await _context.NewsInformationAction.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                newsUserDetected.Tittle = model.Tittle;
                newsUserDetected.UserId = model.UserId;
                newsUserDetected.Description = model.Description;
                newsUserDetected.Index = model.Index;      
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                var newinformation = new NewsInformation()
                {
                    Tittle = model.Tittle,
                    Description = model.Description,
                    UserId = model.UserId,
                    Index = model.Index
                };
                await _context.NewsInformationAction.AddAsync(newinformation);

                await _context.SaveChangesAsync();

                return Ok();
            }


            //var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            //booleanTable.Email = true; 


        }
        [HttpPost("SaveNewsNoUserInformation")]
        public async Task<IActionResult> SaveNewsNoUserInformation([FromBody] NewsInformationViewModel model)
        {



            if (await _context.NewsInformationNoUserAction.AnyAsync(x => x.MirrorId == model.MirrorId))
            {
                var newsUserDetected = await _context.NewsInformationNoUserAction.Where(x => x.MirrorId == model.MirrorId).FirstOrDefaultAsync();
                newsUserDetected.Tittle = model.Tittle;                
                newsUserDetected.Description = model.Description;
                newsUserDetected.MirrorId = model.MirrorId;
                newsUserDetected.Index = model.Index;
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                var newinformation = new NewsInformationNoUser()
                {
                    Tittle = model.Tittle,
                    Description = model.Description,
                    MirrorId = model.MirrorId,
                    Index = model.Index
                   
                };
                await _context.NewsInformationNoUserAction.AddAsync(newinformation);

                await _context.SaveChangesAsync();

                return Ok();
            }


            //var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            //booleanTable.Email = true; 


        }

        [HttpPost("GetNewsInformations2")]
        public async Task<IActionResult> GetNewsInformations2([FromBody] NewsInformationViewModel model)
        {

            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            var newsInformation = await _context.EmailInformations.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();

            if (!booleanTable.StartNews)
            {
                if (booleanTable.News)
                {
                    booleanTable.News = false;
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        status = 1,
                        index=newsInformation.Index
                    });
                }
                else
                {

                    return Ok(new
                    {
                        status = 2
                    });

                }
            }
            else
            {
                booleanTable.StartNews = false;
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = 3
                });
            }
        }

        [HttpPost("GetNewsNoUserInformations2")]
        public async Task<IActionResult> GetNewsNoUserInformations2([FromBody] NewsInformationViewModel model)
        {

            var booleanTable = await _context.NewsInformationNoUserAction.Where(x => x.MirrorId == model.MirrorId).FirstOrDefaultAsync();
            
            if (!booleanTable.StartNews)
            {
                if (booleanTable.News)
                {
                    booleanTable.News = false;
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        status = 1,
                        index=booleanTable.Index
                    });
                }
                else
                {

                    return Ok(new
                    {
                        status = 2
                    });

                }
            }
            else
            {
                booleanTable.StartNews = false;
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = 3
                });
            }
        }


        [HttpPost("GetNewsInformations")]
        public async Task<IActionResult> GetNewsInformations([FromBody] NewsInformationViewModel model)
        {


            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            booleanTable.News = true;
            await _context.SaveChangesAsync();
            if (!await _context.NewsInformationAction.AnyAsync(x => x.UserId == model.UserId))
            {

                return BadRequest("No existe este usuario en la tabla");
            }
            var newinformation = await _context.NewsInformationAction.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();

            return Ok(newinformation);

        }
        [HttpPost("GetNewsNoUserInformations")]
        public async Task<IActionResult> GetNewsNoUserInformations([FromBody] NewsInformationViewModel model)
        {


            var booleanTable = await _context.NewsInformationNoUserAction.Where(x => x.MirrorId == model.MirrorId).FirstOrDefaultAsync();
            booleanTable.News = true;
            await _context.SaveChangesAsync();
            if (!await _context.NewsInformationNoUserAction.AnyAsync(x => x.MirrorId == model.MirrorId))
            {

                return BadRequest("No existe este usuario en la tabla");
            }
            var newinformation = await _context.NewsInformationNoUserAction.Where(x => x.MirrorId == model.MirrorId).FirstOrDefaultAsync();

            return Ok(newinformation);

        }

        [HttpPost("SetStartNews")]
        public async Task<IActionResult> SetStartNews([FromBody] NewsInformationViewModel model)
        {

            var wordsPerMinute = 200;
            var arreglo = model.Description.Split(' ');
            var contador = arreglo.Count();
            var minutes = (Convert.ToDecimal(contador) / Convert.ToDecimal(wordsPerMinute)) * 60000;
            Thread.Sleep(Convert.ToInt32(minutes) - 1000);

            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            booleanTable.StartNews = true;
            await _context.SaveChangesAsync();



            return Ok();



        }
        [HttpPost("SetStartNewsNoUser")]
        public async Task<IActionResult> SetStartNewsNoUser([FromBody] NewsInformationViewModel model)
        {

            var wordsPerMinute = 200;
            var arreglo = model.Description.Split(' ');
            var contador = arreglo.Count();
            var minutes = (Convert.ToDecimal(contador) / Convert.ToDecimal(wordsPerMinute)) * 60000;
            Thread.Sleep(Convert.ToInt32(minutes) - 1000);

            var booleanTable = await _context.NewsInformationNoUserAction.Where(x => x.MirrorId == model.MirrorId).FirstOrDefaultAsync();
            booleanTable.StartNews = true;
            await _context.SaveChangesAsync();



            return Ok();



        }
    }
}