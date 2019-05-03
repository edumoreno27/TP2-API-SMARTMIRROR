using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART_MIRROR.API.Data;
using SMART_MIRROR.API.Model;
using SMART_MIRROR.ENTITIES.Models;

namespace SMART_MIRROR.API.Controllers
{
    public class DiaryGoogleController : Controller
    {
        public readonly ApiDbContext _context;

        public DiaryGoogleController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpPost("GetDiaries")]
        public async Task<IActionResult> GetDiaries([FromBody] DiaryViewModel model)
        {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            if (booleanTable.Diary)
            {
                var Diaries = await _context.DiaryGoogles.Where(x => x.UserId == model.UserId).ToListAsync();
                var counter = false;
                var counterInt = 0;                
                for (int i = 0; i < Diaries.Count; i++)
                {
                    if (Diaries[i].IsSelected)
                    {
                        counter = true;
                        counterInt = i;
                    }
                }

                if (counter)
                {
                    booleanTable.Diary = false;
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Order = Diaries[counterInt].Index,
                        Respuesta = 1
                        //ITEM QUE VAMOS A MOSTRAR
                    });
                }
                else
                {
                    booleanTable.Diary = false;
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Respuesta = 2
                        //SI NO HAY NADAA NO MOSTRAR INFO
                    });
                }

                
            }
            else
            {
                return Ok(new
                {
                    Respuesta = 3
                    //VALIDAR LA RECURRENCIA
                });
            }

        }
        [HttpPost("SaveDiaries")]
        public async Task<IActionResult> SaveDiaries([FromBody] DiaryViewModel model)
        {
            var diaryGoogles = await _context.DiaryGoogles.Where(x => x.UserId == model.UserId).ToListAsync();

            if (await _context.DiaryGoogles.AnyAsync(x=>x.UserId == model.UserId))
            {
                if (diaryGoogles != null)
                {
                    if (diaryGoogles.Count > 0)
                    {
                        foreach (var item in diaryGoogles)
                        {
                            _context.DiaryGoogles.Remove(item);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                for (int i = 0; i < model.List.Count; i++)
                {
                    var diary = new DiaryGoogle()
                    {
                        Index = model.List[i],
                        IsSelected = false,
                        Map = false,
                        UserId = model.UserId,

                    };
                    await _context.DiaryGoogles.AddAsync(diary);
                }                
  
                await _context.SaveChangesAsync();
                return Ok();                
            }
            else
            {
                for (int i = 0; i < model.List.Count; i++)
                {
                    var diary = new DiaryGoogle()
                    {
                        Index = model.List[i],
                        IsSelected = false,
                        Map = false,
                        UserId = model.UserId,

                    };
                    await _context.DiaryGoogles.AddAsync(diary);
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            
        }
        [HttpPost("UpdateDiaries")]
        public async Task<IActionResult> UpdateDiaries([FromBody] DiaryViewModel model)
        {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            booleanTable.Diary = true;
            await _context.SaveChangesAsync();

            var diary = await _context.DiaryGoogles.Where(x => x.IsSelected == true && x.UserId == model.UserId).FirstOrDefaultAsync();
            if (diary != null)
            {
                diary.IsSelected = false;
                await _context.SaveChangesAsync();                
            }
            var diarySelected = await _context.DiaryGoogles.Where(x => x.Index  == model.Order && x.UserId == model.UserId).FirstOrDefaultAsync();
            if (diarySelected != null)
            {
                diarySelected.IsSelected = true;
                await _context.SaveChangesAsync();
            }                        
            return Ok();
        }
        [HttpPost("SetAllDiary")]
        public async Task<IActionResult> SetAllDiary([FromBody] DiaryViewModel model) {
            var diaryGoogles = await _context.DiaryGoogles.Where(x=>x.UserId == model.UserId).ToListAsync();
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();

            foreach (var item in diaryGoogles)
            {
                item.IsSelected = false;
            }
            booleanTable.Diary = true;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("SaveDiaryInformations")]
        public async Task<IActionResult> SaveDiaryInformations([FromBody] ListDiaryInformationViewModel model)
        {
            if (await _context.DiaryInformations.AnyAsync(x => x.UserId == model.UserId))
            {
                var diaryUserDetected = await _context.DiaryInformations.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                _context.DiaryInformations.Remove(diaryUserDetected);
                await _context.SaveChangesAsync();
                var diaryInformation = new DiaryInformation()
                {
                    DateTime = model.ObjectReference.DateTime,
                    Description = model.ObjectReference.Description,
                    Location = model.ObjectReference.Location,
                    Summary = model.ObjectReference.Summary,
                    UserId = model.UserId
                };
                await _context.DiaryInformations.AddAsync(diaryInformation);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                var diaryInformation = new DiaryInformation()
                {
                    DateTime = model.ObjectReference.DateTime,
                    Description = model.ObjectReference.Description,
                    Location = model.ObjectReference.Location,
                    Summary = model.ObjectReference.Summary,
                    UserId = model.UserId
                };
                await _context.DiaryInformations.AddAsync(diaryInformation);

                await _context.SaveChangesAsync();
                return Ok();
            }

            
        }
        [HttpPost("GetDiaryInformations")]
        public async Task<IActionResult> GetDiaryInformations([FromBody] ListDiaryInformationViewModel model)
        {
            if (!await _context.DiaryInformations.AnyAsync(x=>x.UserId == model.UserId))
            {
                return BadRequest("No existe este usuario en la tabla");
            }
            var diaryInformation = await _context.DiaryInformations.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            return Ok(diaryInformation);
        }

    }
}