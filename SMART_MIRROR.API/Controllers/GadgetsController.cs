using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SMART_MIRROR.API.Data;
using SMART_MIRROR.API.Model;

namespace SMART_MIRROR.API.Controllers
{
    [AllowAnonymous]
    public class GadgetsController : Controller
    {
        public readonly ApiDbContext _context;

        public GadgetsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpPost("GetGadgetStatus")]
        public async Task<IActionResult> GetStatusGadgetsList([FromBody] UserViewModel model)
        {

            var lstStatusGadgets = await _context.UserGadgets.Include(x => x.Gadget).Where(x => x.UserId == model.Id)
                .OrderBy(x => x.Gadget.StaticNumber)
                .Select(x => new {
                    x.GadgetId,
                    x.Gadget.Description,                    
                    x.Order,
                    x.IsActive,
                    Selected = true,
                    Position = 1
                })
                .ToListAsync();

            return Ok(lstStatusGadgets);
        }
        [HttpPost("GetGadgetStatusSmart")]
        public async Task<IActionResult> GetStatusGadgetsListSmart([FromBody] UserViewModel model)
        {
            var booleanTable = await _context.BooleanTables.Where(x=>x.UserId== model.Id).FirstOrDefaultAsync();
            if (booleanTable.Status)
            {
                var lstStatusGadgets = await _context.UserGadgets.Include(x => x.Gadget).Where(x => x.UserId == model.Id)
                .OrderBy(x => x.Gadget.StaticNumber)
                .Select(x => new {
                    x.GadgetId,
                    x.Gadget.Description,
                    x.IsActive,
                    x.Order,
                    Selected = true,
                    Position = 1,
                    Status = false

                })
                .ToListAsync();
                booleanTable.Status = false;
                await _context.SaveChangesAsync();
                return Ok(lstStatusGadgets);
            }
            else
            {
                return Ok( new {
                    Status = true
                });
            }
            
        }


        [HttpPost("GetGadgetOrder")]
        public async Task<IActionResult> GetOrderGadgetsList([FromBody] UserViewModel model)
        {

            var lstOrderGadgets = await _context.UserGadgets.Include(x => x.Gadget).Where(x => x.UserId == model.Id)
                .OrderBy(x => x.Order)
                .Select(x => new {
                    x.GadgetId,
                    x.Gadget.Description,
                    x.IsActive,
                    x.Order,
                    Selected = true,
                    Position = 1,
                    x.Gadget.ContentHtml
                })
                .ToListAsync();

            return Ok(lstOrderGadgets);
        }

        [HttpPost("GetGadgetOrderSmart")]
        public async Task<IActionResult> GetOrderGadgetsListSmart([FromBody] UserViewModel model)
        {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.Id).FirstOrDefaultAsync();
            if (booleanTable.Order)
            {
                var lstOrderGadgets = await _context.UserGadgets.Include(x => x.Gadget).Where(x => x.UserId == model.Id)
               .OrderBy(x => x.Order)
               .Select(x => new {
                   x.GadgetId,
                   x.Gadget.Description,
                   x.IsActive,
                   x.Order,
                   Selected = true,
                   Position = 1,
                   x.Gadget.ContentHtml
               })
               .ToListAsync();
                booleanTable.Order = false;                
                await _context.SaveChangesAsync();

                return Ok(lstOrderGadgets);

            }
            else
            {
                return Ok(new
                {
                    Order = true
                });
            }
           

            
        }


        [HttpPost("EditGadgetStatus")]
        public async Task<IActionResult> GetStatusGadgets([FromBody] ModelBooleanList lstStatus)
        {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == lstStatus.UserId).FirstOrDefaultAsync();

            if (lstStatus == null)
            {
                return BadRequest("No existe la lista");
            }


            var lstUserGadgets = await _context.UserGadgets.Include(x => x.Gadget).Where(x => x.UserId == lstStatus.UserId).ToListAsync();

            foreach (var userGadget in lstUserGadgets)
            {
                foreach (var inside in lstStatus.LstInside)
                {
                    if (userGadget.GadgetId == inside.GadgetId)
                    {
                        userGadget.IsActive = inside.IsActive;
                    }
                }

            }
            booleanTable.Status = true;
            await _context.SaveChangesAsync();

            var lstOrderGadgetsAct = await _context.UserGadgets.Include(x => x.Gadget).Where(x => x.UserId == lstStatus.UserId)
                .OrderBy(x => x.Gadget.StaticNumber)
                .Select(x => new {
                    x.Gadget.Id,
                    x.Gadget.Description,
                    x.IsActive,
                    x.Order,
                    Selected = true,
                    Position = 1
                }).ToListAsync();
            return Ok(lstStatus.LstInside);

        }

        [HttpPost("EditGadgetOrder")]
        public async Task<IActionResult> GetOrderGadgets([FromBody] ModelIntList lstOrder)
        {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == lstOrder.UserId).FirstOrDefaultAsync();
            if (lstOrder == null)
            {
                return BadRequest("No existe la lista");
            }
            var lstUserGadgets = await _context.UserGadgets.Include(x => x.Gadget).Where(x => x.UserId == lstOrder.UserId)
                                                                                .OrderBy(x => x.Gadget.StaticNumber)
                                                                                .ToListAsync();


            foreach (var userGadget in lstUserGadgets)
            {
                foreach (var inside in lstOrder.LstInside)
                {
                    if (userGadget.GadgetId == inside.GadgetId)
                    {
                        userGadget.Order = inside.Order;
                    }

                }

            }
            //foreach (var inside in lstOrder.LstInside)
            //{
            //    var userGadget = await _context.UserGadgets.Where(x => x.GadgetId == inside.GadgetId && x.UserId == lstOrder.UserId).FirstOrDefaultAsync();
            //    userGadget.Order = inside.Order;
            //    await _context.SaveChangesAsync();
            //}

            booleanTable.Order = true;
            await _context.SaveChangesAsync();

            var lstOrderGadgetsAct = await _context.UserGadgets.Include(x => x.Gadget).Where(x => x.UserId == lstOrder.UserId)
                .OrderBy(x => x.Order)
                .Select(x => new {
                    GadgetId= x.Gadget.Id,
                    x.Gadget.Description,
                    x.Order,
                    Selected = true,
                    Position = 1,
                    x.Gadget.ContentHtml
                }).ToListAsync();

            return Ok(lstOrderGadgetsAct);

        }

        [HttpPost("UpdateBooleans")]
        public async Task<IActionResult> UpdateBooleans([FromBody] UserViewModel model) {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.Id).FirstOrDefaultAsync();
            booleanTable.Status = true;
            booleanTable.Order = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

    }

}