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
    public class HotelServiceController : Controller
    {
        public readonly ApiDbContext _context;

        public HotelServiceController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpPost("SaveHotelServices")]
        public async Task<IActionResult> SaveHotelServices([FromBody] HotelServiceViewModel model)
        {            
            if (await _context.HotelServices.AnyAsync(x=>x.UserId == model.UserId))
            {
                var lstHotelServices = await _context.HotelServices.Where(x => x.UserId == model.UserId).ToListAsync();
                foreach (var item in lstHotelServices)
                {
                    _context.HotelServices.Remove(item);
                }
                await _context.SaveChangesAsync();
                foreach (var item in model.ListHotelServices)
                {
                    var hotelService = new HotelService
                    {
                        Index = item.Index,
                        ServiceId = item.ServiceId,
                        UserId = model.UserId
                    };
                    await _context.HotelServices.AddAsync(hotelService);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                foreach (var item in model.ListHotelServices)
                {
                    var hotelService = new HotelService
                    {
                        Index = item.Index,
                        ServiceId = item.ServiceId,
                        UserId = model.UserId
                    };
                    await _context.HotelServices.AddAsync(hotelService);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            
        }
        [HttpPost("GetHotelServices")]
        public async Task<IActionResult> GetHotelServices([FromBody] HotelServiceViewModel model)
        {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            if (booleanTable.HotelService)
            {
                var HotelServices = await _context.HotelServices.Where(x => x.UserId == model.UserId).ToListAsync();
                var counter = false;
                var counterInt = 0;
                for (int i = 0; i < HotelServices.Count; i++)
                {
                    if (HotelServices[i].IsSelected)
                    {
                        counter = true;
                        counterInt = i;
                    }
                }

                if (counter)
                {
                    booleanTable.HotelService = false;
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Order = HotelServices[counterInt].Index,
                        Respuesta = 1
                        //ITEM QUE VAMOS A MOSTRAR
                    });
                }
                else
                {
                    booleanTable.HotelService = false;
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
        [HttpPost("UpdateServicesHotels")]
        public async Task<IActionResult> UpdateServiceHotels([FromBody] HotelServiceViewModel model)
        {
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            booleanTable.HotelService = true;
            await _context.SaveChangesAsync();

            var hotelservices = await _context.HotelServices.Where(x => x.IsSelected == true && x.UserId == model.UserId).FirstOrDefaultAsync();
            if (hotelservices != null)
            {
                hotelservices.IsSelected = false;
                await _context.SaveChangesAsync();
            }
            var hotelserviceSelected = await _context.HotelServices.Where(x => x.Index == model.Order && x.UserId == model.UserId).FirstOrDefaultAsync();
            if (hotelserviceSelected != null)
            {
                hotelserviceSelected.IsSelected = true;
                await _context.SaveChangesAsync();
            }
            return Ok( new {
                serviceId = hotelserviceSelected.ServiceId
            });

        }
        [HttpPost("SetAllHotelServices")]
        public async Task<IActionResult> SetAllHotelServices([FromBody] HotelServiceViewModel model)
        {
            var hotelServices = await _context.HotelServices.Where(x => x.UserId == model.UserId).ToListAsync();
            var booleanTable = await _context.BooleanTables.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();

            foreach (var item in hotelServices)
            {
                item.IsSelected = false;
            }
            booleanTable.HotelService = true;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("GetServiceId")]
        public async Task<IActionResult> GetServiceId([FromBody] HotelServiceViewModel model)
        {
            var hotelService = await _context.HotelServices.Where(x => x.UserId == model.UserId && x.Index == model.Order).FirstOrDefaultAsync();
            return Ok(new
            {
                serviceId = hotelService.ServiceId
            });
        }
    }
}