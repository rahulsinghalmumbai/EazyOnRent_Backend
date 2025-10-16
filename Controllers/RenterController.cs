using EazyOnRent.Data;
using EazyOnRent.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EazyOnRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RenterController : ControllerBase
    {
        public readonly EazyOnRentContext _context;
        public RenterController(EazyOnRentContext context)
        {
            _context = context;
        }


        [HttpGet("GetALLRenter")]
        public async Task<List<Renter>> GellALLRenter()
        {
            var renter = await _context.Renters.Where(x => x.Status == true).ToListAsync();
            return renter;
        }

        // GET: api/<RenterController>
        [HttpGet("GetRenterById")]
        public async Task<Renter> GetRenterId(int RenterId)
        {
            var result = await _context.Renters.Where(x => x.RenterId == RenterId && x.Status == true).ToListAsync();
            if (result.Count > 0)
                return result[0];
            else return new Renter();
        }

        // update
        [HttpPut("EditRenterById")]
        public async Task<ObjectResult> EditRenter(Renter item)
        {
            var result = await _context.Renters.Where(x => x.RenterId == item.RenterId && x.Status == true).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Name = item.Name;
                result.Email = item.Email;
                result.DefaultImage = item.DefaultImage;
                result.Password = item.Password;
                result.Lat = item.Lat;
                result.Long = item.Long;
                result.LatLongAddress = item.LatLongAddress;
                result.UpdatedOn = item.UpdatedOn;
                result.Status = item.Status;
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Renter Not Found.");
            }
        }

        // GET: api/<ListerController>
        [HttpDelete("RenterId")]
        public async Task<ObjectResult> DRenter(int RenterId)
        {
            var item = await _context.Renters.Where(x => x.RenterId == RenterId && x.Status == true).FirstOrDefaultAsync();
            if (item != null)
            {
                item.Status = false;
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "Success");
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "BadRequest");
            }
        }

        [HttpGet("GetAllItem")]
        public async Task<List<RenterItem>> RenterItem(int RenterId)
        {
            var renter = await _context.Renters.Where(x => x.RenterId == RenterId && x.Status == true).FirstOrDefaultAsync();
            var Items = await _context.RenterItems.Where(x => x.RenterId == RenterId && x.Status == true).ToListAsync();

            List<RenterItem> listInfo = new List<RenterItem>();
            if (renter != null && Items != null)
            {
                foreach (var item in Items)
                {
                    RenterItem list = new RenterItem();
                    list.RenterItemId = item.RenterItemId;
                    list.RenterId = item.RenterId;
                    list.ItemId = item.ItemId;
                    list.RentFromDate = item.RentFromDate;
                    list.RentToDate = item.RentToDate;
                    list.Review = item.Review;
                    list.Rating = item.Rating;
                    list.Status = item.Status;
                    listInfo.Add(list);
                }
            }
            return listInfo;
        }

        [HttpGet("GetItemById")]
        public async Task<RenterItem> ItemById(int RenterId, int ItemId)
        {
            var renter = await _context.Renters.Where(x => x.RenterId == RenterId && x.Status == true).FirstOrDefaultAsync();
            RenterItem list = new RenterItem();
            if (renter != null)
            {
                var item = await _context.RenterItems.Where(x => x.RenterId == RenterId && x.RenterItemId == ItemId).FirstOrDefaultAsync();

                if (item != null)
                {
                    list.RenterItemId = item.RenterItemId;
                    list.RenterId = item.RenterId;
                    list.ItemId = item.ItemId;
                    list.RentFromDate = item.RentFromDate;
                    list.RentToDate = item.RentToDate;
                    list.Review = item.Review;
                    list.Rating = item.Rating;
                    list.Status = item.Status;
                }
            }

            return list;
        }


        [HttpPost("CreateItem")]
        public async Task<IActionResult> ItemId(RenterItem Litem)
        {
            var renter = await _context.RenterItems.Where(x => x.RenterId == Litem.RenterId && x.Status == true).FirstOrDefaultAsync();
            RenterItem list = new RenterItem();
            if (renter != null)
            {
                list.RenterId = Litem.RenterId;
                list.ItemId = Litem.ItemId;
                list.RentFromDate = Litem.RentFromDate;
                list.RentToDate = Litem.RentToDate;
                list.Status = true;
                await _context.RenterItems.AddAsync(list);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "Item Created");
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Renter Not Found.");
            }
        }

        [HttpPost("EditItemById")]
        public async Task<ObjectResult> EditItemId(int ListerId, RenterItem Litem)
        {
            var renter = await _context.Renters.Where(x => x.RenterId == Litem.RenterId && x.Status == true).FirstOrDefaultAsync();
            if (renter != null)
            {
                var item = await _context.RenterItems.Where(x => x.RenterId == Litem.RenterId && x.RenterItemId == Litem.ItemId && x.Status == true).FirstOrDefaultAsync();

                if (item != null)
                {
                    item.RentFromDate = Litem.RentFromDate;
                    item.RentToDate = Litem.RentToDate;
                    item.Rating = Litem.Rating != null ? Litem.Rating : item.Rating;
                    item.Review = Litem.Review != null ? Litem.Review : item.Review;
                }
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "Success");
            }
            return StatusCode(StatusCodes.Status400BadRequest, "BadRequest");
        }

        // GET: api/<ListerController>
        [HttpDelete("Item")]
        public async Task<ObjectResult> DItem(int RenterId, int ItemId)
        {
            var renter = await _context.Renters.Where(x => x.RenterId == RenterId && x.Status == true).FirstOrDefaultAsync();
            if (renter != null)
            {
                var item = await _context.RenterItems.Where(x => x.RenterId == RenterId && x.RenterItemId == ItemId && x.Status == true).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.Status = false;
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, "Success");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "BadRequest");
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "BadRequest");
            }
        }

        [HttpPost("bookRenterItem")]
        public async Task<IActionResult> AddRenterItem([FromBody] RenterItem model)
        {
            if (model == null)
            {
                return BadRequest(new { ResponseCode = "400", ResponseMessage = "Invalid Data." });
            }

            // Validation check
            if (model.RenterId == null || model.RenterId <= 0)
            {
                return Ok(new { ResponseCode = "401", ResponseMessage = "Invalid RenterId" });
            }

            if (model.ItemId == null || model.ItemId <= 0)
            {
                return Ok(new { ResponseCode = "402", ResponseMessage = "Invalid ItemId" });
            }

            if (model.RentFromDate == null || model.RentToDate == null)
            {
                return Ok(new { ResponseCode = "403", ResponseMessage = "RentFromDate And RentToDate both is required " });
            }

            if (model.RentFromDate > model.RentToDate)
            {
                return Ok(new { ResponseCode = "404", ResponseMessage = "RentFromDate must occur before RentToDate." });
            }

            try
            {
                await _context.RenterItems.AddAsync(model);
                await _context.SaveChangesAsync();

                return Ok(new { ResponseCode = "200", ResponseMessage = "Renter Item Book Successfully.", Data = model });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "500", ResponseMessage = "Error: " + ex.Message });
            }
        }

    }
}
