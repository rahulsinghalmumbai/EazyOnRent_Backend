using EazyOnRent.Data;
using EazyOnRent.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EazyOnRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly EazyOnRentContext _context;

        public UserController(EazyOnRentContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
        //[HttpGet("GetAllByCustomerType")]
        //public async Task<dynamic> CustomerType(string CustomerType)
        //{
        //    dynamic result = new ExpandoObject();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(CustomerType))
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "CustomerType is required field.";
        //            result.ListerList = null;
        //            return result;
        //        }

        //        if (CustomerType.ToUpper() == "LISTER")
        //        {
        //            var ListersResult = await _context.Listers.ToListAsync();
        //            UserInformation userInformation = new UserInformation();
        //            List<Lister> listers = new List<Lister>();
        //            foreach (var item in ListersResult)
        //            {
        //                Lister li = new Lister();
        //                li.ListerId = item.ListerId;
        //                li.Name = item.Name;
        //                li.CompanyName = item.CompanyName;
        //                li.Tags = item.Tags;
        //                li.Address = item.Address;
        //                li.Mobile = item.Mobile;
        //                li.Email = item.Email;
        //                li.DefaultImage = item.DefaultImage;
        //                li.Descriptions = item.Descriptions;
        //                li.Status = item.Status;
        //                li.Password = item.Password;
        //                li.CreatedDate = item.CreatedDate;
        //                li.City = item.City;
        //                li.Lat = item.Lat;
        //                li.Long = item.Long;
        //                li.LatLongAddress = item.LatLongAddress;
        //                li.UpdatedOn = item.UpdatedOn;
        //                listers.Add(li);
        //            }
        //            if (listers.Count > 0)
        //            {
        //                result.ResponseCode = "000";
        //                result.ResponseMessage = "Success";
        //                result.ListerList = listers;
        //                return result;
        //            }
        //            else
        //            {
        //                result.ResponseCode = "999";
        //                result.ResponseMessage = "No Lister found.";
        //                result.ListerList = null;
        //                return result;
        //            }

        //        }
        //        else if (CustomerType.ToUpper() == "RENTER")
        //        {
        //            var RentersResult = await _context.Renters.ToListAsync();
        //            List<Renter> renters = new List<Renter>();

        //            foreach (var item in RentersResult)
        //            {
        //                Renter li = new Renter();
        //                li.RenterId = item.RenterId;
        //                li.Name = item.Name;
        //                li.Mobile = item.Mobile;
        //                li.Email = item.Email;
        //                li.Password = item.Password;
        //                li.CreatedDate = item.CreatedDate;
        //                li.Lat = item.Lat;
        //                li.Long = item.Long;
        //                li.LatLongAddress = item.LatLongAddress;
        //                li.UpdatedOn = item.UpdatedOn;
        //                renters.Add(li);
        //            }

        //            if (renters.Count > 0)
        //            {
        //                result.ResponseCode = "000";
        //                result.ResponseMessage = "Success";
        //                result.RenterList = renters;
        //                return result;
        //            }
        //            else
        //            {
        //                result.ResponseCode = "999";
        //                result.ResponseMessage = "No Renter found.";
        //                result.RenterList = null;
        //                return result;
        //            }
        //        }
        //        else
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "Customer type should be only Renter or Lister.";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.ResponseCode = "999";
        //        result.ResponseMessage = ex.Message;
        //    }
        //    finally
        //    {

        //    }
        //    return result;
        //}


        //[HttpGet("GetAllByCustomerTypeID")]
        //public async Task<dynamic> GetAllByCustomerTypeID(string CustomerType, int Id)
        //{
        //    dynamic result = new ExpandoObject();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(CustomerType))
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "CustomerType is required field.";
        //            return result;
        //        }

        //        if (Id <= 0)
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "Id is required field.";
        //            return result;
        //        }

        //        if (CustomerType.ToUpper() == "LISTER")
        //        {
        //            var ListersResult = await _context.Listers.Where(x => x.ListerId == Id).FirstOrDefaultAsync();
        //            List<Lister> listers = new List<Lister>();
        //            if (ListersResult != null)
        //            {
        //                Lister li = new Lister();
        //                li.ListerId = ListersResult.ListerId;
        //                li.Name = ListersResult.Name;
        //                li.CompanyName = ListersResult.CompanyName;
        //                li.Tags = ListersResult.Tags;
        //                li.Address = ListersResult.Address;
        //                li.Mobile = ListersResult.Mobile;
        //                li.Email = ListersResult.Email;
        //                li.DefaultImage = ListersResult.DefaultImage;
        //                li.Descriptions = ListersResult.Descriptions;
        //                li.Status = ListersResult.Status;
        //                li.Password = ListersResult.Password;
        //                li.CreatedDate = ListersResult.CreatedDate;
        //                li.City = ListersResult.City;
        //                li.Lat = ListersResult.Lat;
        //                li.Long = ListersResult.Long;
        //                li.LatLongAddress = ListersResult.LatLongAddress;
        //                li.UpdatedOn = ListersResult.UpdatedOn;
        //                listers.Add(li);

        //                result.ResponseCode = "000";
        //                result.ResponseMessage = "Success";
        //                result.ListerList = listers;
        //                return result;
        //            }
        //            else
        //            {
        //                result.ResponseCode = "999";
        //                result.ResponseMessage = "No Lister found.";
        //                result.ListerList = null;
        //                return result;
        //            }

        //        }
        //        else if (CustomerType.ToUpper() == "RENTER")
        //        {
        //            var RentersResult = await _context.Renters.Where(x => x.RenterId == Id).FirstOrDefaultAsync();
        //            List<Renter> renters = new List<Renter>();
        //            if (RentersResult != null)
        //            {
        //                Renter li = new Renter();
        //                li.RenterId = RentersResult.RenterId;
        //                li.Name = RentersResult.Name;
        //                li.Mobile = RentersResult.Mobile;
        //                li.Email = RentersResult.Email;
        //                li.Password = RentersResult.Password;
        //                li.CreatedDate = RentersResult.CreatedDate;
        //                li.Lat = RentersResult.Lat;
        //                li.Long = RentersResult.Long;
        //                li.LatLongAddress = RentersResult.LatLongAddress;
        //                li.UpdatedOn = RentersResult.UpdatedOn;
        //                renters.Add(li);

        //                result.ResponseCode = "000";
        //                result.ResponseMessage = "Success";
        //                result.RenterList = renters;
        //                return result;
        //            }
        //            else
        //            {
        //                result.ResponseCode = "999";
        //                result.ResponseMessage = "No Renter found.";
        //                result.RenterList = null;
        //                return result;
        //            }
        //        }
        //        else
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "Customer type should be only Renter or Lister.";

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.ResponseCode = "999";
        //        result.ResponseMessage = ex.Message;

        //    }
        //    finally
        //    {

        //    }
        //    return result;
        //}

        //[HttpPost("Login")]
        //public async Task<dynamic> Login(UserLogin user)
        //{
        //    dynamic result = new ExpandoObject();
        //    try
        //    {

        //        if (string.IsNullOrEmpty(user.Mobile))
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "Mobile is required field.";
        //            return result;
        //        }

        //        if (string.IsNullOrEmpty(user.Password))
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "Password is required field.";
        //            return result;
        //        }

        //        if (string.IsNullOrEmpty(user.CustomerType))
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "CustomerType is required field.";
        //            return result;
        //        }

        //        if (user.CustomerType.ToUpper() == "LISTER")
        //        {
        //            var ListersResult = await _context.Listers.Where(x => x.Mobile == user.Mobile && x.Password == user.Password && x.Status == true).ToListAsync();
        //            if (ListersResult.Count > 0)
        //            {
        //                result.ResponseCode = "000";
        //                result.ResponseMessage = "Success";
        //                result.ListerId = ListersResult[0].ListerId;
        //                result.Name = ListersResult[0].Name;
        //                result.UserType = "LISTER";
        //            }
        //            else
        //            {
        //                result.ResponseCode = "999";
        //                result.ResponseMessage = "Please enter valid credentials";
        //            }
        //        }
        //        else if (user.CustomerType.ToUpper() == "RENTER")
        //        {
        //            var RenterResult = await _context.Renters.Where(x => x.Mobile == user.Mobile && x.Password == user.Password && x.Status == true).ToListAsync();
        //            if (RenterResult.Count > 0)
        //            {
        //                result.ResponseCode = "000";
        //                result.ResponseMessage = "Success";
        //                result.RenterId = RenterResult[0].RenterId;
        //                result.Name = RenterResult[0].Name;
        //                result.UserType = "RENTER";
        //            }
        //            else
        //            {
        //                result.ResponseCode = "999";
        //                result.ResponseMessage = "Please enter valid credentials";
        //            }
        //        }
        //        else
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "Customer type should be only Renter or Lister.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ResponseCode = "999";
        //        result.ResponseMessage = ex.Message;
        //    }
        //    finally
        //    {

        //    }
        //    return result;
        //}

        [HttpPost("UserRegister")]
        public async Task<dynamic> SignUp([FromBody] UserLogin mobile)
        {
            try
            {
                if (string.IsNullOrEmpty(mobile.Mobile) || mobile.Mobile.Length != 10)
                {
                    return new
                    {
                        ResponseCode = "999",
                        ResponseMessage = "Please enter valid mobile number.",
                        ExistUser = (Lister)null
                    };
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                var listerExist = await _context.Listers
                    .Where(x => x.Mobile == mobile.Mobile)
                    .FirstOrDefaultAsync();

                if (listerExist != null)
                {
                    if (!string.IsNullOrEmpty(listerExist.DefaultImage))
                    {
                        listerExist.DefaultImage = $"{baseUrl}/images/{listerExist.DefaultImage.Replace("\\", "/")}";
                    }

                    return new
                    {
                        ResponseCode = "200",
                        ResponseMessage = "Lister mobile number already registered.",
                        ExistUser = listerExist
                    };
                }

                // NEW REGISTER
                Lister newLister = new Lister
                {
                    Mobile = mobile.Mobile,
                    CreatedDate = DateTime.Now,
                    Status = true
                };

                await _context.Listers.AddAsync(newLister);
                await _context.SaveChangesAsync();

                return new
                {
                    ResponseCode = "200",
                    ResponseMessage = "User registered successfully.",
                    ListerId = newLister.ListerId
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    ResponseCode = "999",
                    ResponseMessage = ex.Message,
                    ExistUser = (Lister)null
                };
            }
        }





        [HttpPost("AddViewed")]
        public async Task<IActionResult> AddViewed([FromBody] dbViewedDto model)
        {
            if (model == null || string.IsNullOrEmpty(model.UserType))
            {
                
                return Ok(new { ResponseCode = "300", ResponseMessage = " Not Found Any UserType. Database unchanged." });
            }

            var viewObj = new dbViewed();

            if (model.UserType.ToUpper().Contains("LISTER"))
            {
                viewObj.viewerID = model.viewerID;
                viewObj.ViewerCategory = 1;   // LISTER
                viewObj.ListerItemID = model.ListerItemID;
            }
            else if (model.UserType.ToUpper().Contains("RENTER"))
            {
                viewObj.viewerID = model.viewerID;
                viewObj.ViewerCategory = 2;   // RENTER
                viewObj.ListerItemID = model.ListerItemID;
            }
            else
            {
                return Ok(new { ResponseCode = "301", ResponseMessage = "Invalid UserType. Database unchanged." });
            }

            try
            {
                await _context.dbVieweds.AddAsync(viewObj);
                await _context.SaveChangesAsync();

                return Ok(new { ResponseCode = "200", ResponseMessage = "Save Data Successfully", Data = viewObj });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ResponseCode = "500", ResponseMessage = "Error: " + ex.Message });
            }
        }


    }
}
