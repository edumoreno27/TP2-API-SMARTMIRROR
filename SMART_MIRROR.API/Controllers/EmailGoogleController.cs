using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART_MIRROR.API.Data;
using SMART_MIRROR.API.Model;
using SMART_MIRROR.ENTITIES.Models;

namespace SMART_MIRROR.API.Controllers
{
    public class EmailGoogleController : Controller
    {
        public readonly ApiDbContext _context;

        public EmailGoogleController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpPost("SaveEmailInformations")]
        public async Task<IActionResult> SaveEmailInformations([FromBody] ListEmailInformationViewModel model)
        {

            
            
                if (await _context.EmailInformations.AnyAsync(x => x.UserId == model.UserId))
                {
                    var emailUserDetected = await _context.EmailInformations.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                    emailUserDetected.Message = model.ObjectReference.Message;
                    emailUserDetected.Sender = model.ObjectReference.Sender;
                    emailUserDetected.SenderAt = model.ObjectReference.SenderAt;
                    emailUserDetected.Subject = model.ObjectReference.Subject;
                    emailUserDetected.UserId = model.UserId;
                    await _context.SaveChangesAsync();
                    
                    return Ok();
                }
                else
                {
                    var emailInformation = new EmailInformation()
                    {
                        Message = model.ObjectReference.Message,
                        Sender = model.ObjectReference.Sender,
                        SenderAt = model.ObjectReference.SenderAt,
                        Subject = model.ObjectReference.Subject,
                        UserId = model.UserId
                    };
                    await _context.EmailInformations.AddAsync(emailInformation);

                    await _context.SaveChangesAsync();
                    
                    return Ok();
                }
            

                //var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                //booleanTable.Email = true; 
                 
            
        }
        [HttpPost("GetEmailInformations")]
        public async Task<IActionResult> GetEmailInformations([FromBody] ListEmailInformationViewModel model)
        {
        
                    
                    var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            booleanTable.Email = true;
            await _context.SaveChangesAsync();
            if (!await _context.EmailInformations.AnyAsync(x => x.UserId == model.UserId))
            {
                    
                return BadRequest("No existe este usuario en la tabla");
            }
            var emailInformation = await _context.EmailInformations.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                
                return Ok(emailInformation);
       
        }       
        [HttpPost("GetEmailInformations2")]
        public async Task<IActionResult> GetEmailInformations2([FromBody] ListEmailInformationViewModel model)
        {
           
                var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                if (!booleanTable.StartEmail)
                {
                    if (booleanTable.Email)
                    {
                        booleanTable.Email = false;
                        await _context.SaveChangesAsync();
                        
                        return Ok(new
                        {
                            status = 1
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
                    booleanTable.StartEmail = false;
                    await _context.SaveChangesAsync();
                    
                    return Ok(new
                    {
                        status = 3
                    });
                }
            

                
            
            
        }
        [HttpPost("SetStartEmail")]
        public async Task<IActionResult> SetStartEmail([FromBody] ListEmailInformationViewModel model)
        {
        
                var wordsPerMinute = 200;
                var arreglo = model.Description.Split(' ');
                var contador = arreglo.Count();
                var minutes = (Convert.ToDecimal(contador) / Convert.ToDecimal(wordsPerMinute)) * 60000;
                Thread.Sleep(Convert.ToInt32(minutes) - 1000);
            
            
                var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                booleanTable.StartEmail = true;
                await _context.SaveChangesAsync();
                
            
            return Ok();
         

          
        }

    }
}