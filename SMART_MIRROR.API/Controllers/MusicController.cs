﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART_MIRROR.API.Data;
using SMART_MIRROR.API.Model;

namespace SMART_MIRROR.API.Controllers
{
    public class MusicController : Controller
    {
        public readonly ApiDbContext _context;

        public MusicController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpPost("GetMusicAction")]
        public async Task<IActionResult> GetMusicAction([FromBody] MusicActionViewModel model)
        {
            var userMusic = await _context.MusicActions.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            if (booleanTable.Music)
            {
                booleanTable.Music = false;
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    action = userMusic.Action,
                    status = true

                });
            }
            else
            {
                return Ok(new
                {
                    status = false
                });
            }

        }
        [HttpPost("UpdateMusicAction")]
        public async Task<IActionResult> UpdateMusicAction([FromBody] MusicActionViewModel model)
        {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            var userMusic = await _context.MusicActions.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            userMusic.Action = model.Action;
            booleanTable.Music = true;
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}


        