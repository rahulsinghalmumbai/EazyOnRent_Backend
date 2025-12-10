using EazyOnRent.Data;
using EazyOnRent.Model;
using EazyOnRent.Model.API_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EazyOnRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListerController : ControllerBase
    {
        public readonly EazyOnRentContext _context;
        public ListerController(EazyOnRentContext context)
        {
            _context = context;
        }

        [HttpGet("GetALLLister")]
        public async Task<dynamic> GetAllLister()
        {
            dynamic result = new ExpandoObject();

            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                var listerList = await _context.Listers
                    .Where(x => x.Status == true)
                    .ToListAsync();

                if (listerList.Count > 0)
                {
                    foreach (var item in listerList)
                    {
                        if (!string.IsNullOrEmpty(item.DefaultImage))
                        {
                            item.DefaultImage = $"{baseUrl}/images/{item.DefaultImage.Replace("\\", "/")}";
                        }
                    }

                    result.ResponseCode = "000";
                    result.ResponseMessage = "Success";
                    result.ListerList = listerList;
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No Lister found.";
                    result.ListerList = null;
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ListerList = null;
            }

            return result;
        }


        [HttpGet("GetListerById")]
        public async Task<dynamic> GetListerById(int ListerId)
        {
            dynamic result = new ExpandoObject();

            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                var ListerResult = await _context.Listers
                    .Where(x => x.ListerId == ListerId && x.Status == true)
                    .ToListAsync();

                if (ListerResult.Count > 0)
                {
                    string imageRelativePath = ListerResult[0].DefaultImage;

                    if (!string.IsNullOrEmpty(imageRelativePath))
                    {
                        ListerResult[0].DefaultImage =
                            $"{baseUrl}/images/{imageRelativePath.Replace("\\", "/")}";
                    }

                    result.ResponseCode = "000";
                    result.ResponseMessage = "Success";
                    result.ListerList = ListerResult;
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No Lister found.";
                    result.ListerList = null;
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ListerList = null;
            }

            return result;
        }






        // update
        [HttpPost("EditProfileById")]
     
        public async Task<dynamic> EditLister([FromForm] ListerModel item)
        {
            dynamic result = new ExpandoObject();

            try
            {
                if (item.ListerId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter valid ListerId.";
                    result.ListerList = null;
                    return result;
                }

                if (string.IsNullOrEmpty(item.Name))
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Name is required field.";
                    result.ListerList = null;
                    return result;
                }

                if (string.IsNullOrEmpty(item.Mobile))
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Mobile is required field.";
                    result.ListerList = null;
                    return result;
                }

                if (string.IsNullOrEmpty(item.Email))
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Email is required field.";
                    result.ListerList = null;
                    return result;
                }

                item.DefaultImage = "";

                if (item.ImageFile != null && item.ImageFile.Length > 0)
                {
                      var ext = Path.GetExtension(item.ImageFile.FileName).ToLower();
                //    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                //    {
                //        result.ResponseCode = "999";
                //        result.ResponseMessage = "Only .jpg, .jpeg, .png files allowed.";
                //        return result;
                //    }

                    string folderName = "listerDefaultImage";
                    string folderPath = Path.Combine(AppConfigModel.ImagePath, folderName);

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string newFileName = $"{Guid.NewGuid()}{ext}";
                    string filePath = Path.Combine(folderPath, newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await item.ImageFile.CopyToAsync(stream);
                    }

                    item.DefaultImage = folderName + "/" + newFileName;
                }

                var ListerResult = await _context.Listers
                    .Where(x => x.ListerId == item.ListerId && x.Status == true)
                    .FirstOrDefaultAsync();

                if (ListerResult != null)
                {
                    ListerResult.Name = item.Name;
                    ListerResult.CompanyName = item.CompanyName;
                    ListerResult.Tags = item.Tags;
                    ListerResult.Address = item.Address;
                    ListerResult.Email = item.Email;
                    if (!string.IsNullOrEmpty(item.DefaultImage))
                        ListerResult.DefaultImage = item.DefaultImage;

                    ListerResult.Descriptions = item.Descriptions;
                    ListerResult.Password = item.Password;
                    ListerResult.City = item.City;
                    ListerResult.Lat = item.Lat;
                    ListerResult.Long = item.Long;
                    ListerResult.LatLongAddress = item.LatLongAddress;
                    ListerResult.UpdatedOn = DateTime.Now;

                    await _context.SaveChangesAsync();

                    result.ResponseCode = "000";
                    result.ResponseMessage = "Success";
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter valid ListerId.";
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
            }

            return result;
        }



        [HttpDelete("DeleteLister")]
        public async Task<dynamic> DeleteLister(int ListerId)
        {
            dynamic result = new ExpandoObject();
            try
            {
                if (ListerId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid ListerId.";
                    result.ListerList = null;
                    return result;
                }
                var item = await _context.Listers.Where(x => x.ListerId == ListerId && x.Status == true).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.Status = false;
                    await _context.SaveChangesAsync();
                    result.ResponseCode = "000";
                    result.ResponseMessage = "Lister deleted successfully.";
                    result.ListerId = ListerId;
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No Lister found.";
                    result.ListerId = ListerId;
                }

            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ListerId = ListerId;
            }
            finally
            {

            }
            return result;
        }

        [HttpGet("GetAllItem")]
        public async Task<dynamic> ListerItem(int ListerId, int? Status)
        {
            dynamic result = new ExpandoObject();
            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                if (ListerId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid ListerId.";
                    return result;
                }

                if (Status != null && (Status < 0 || Status > 3))
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid status (For Visible:1, Not Visible:2, Discountune:3)";
                    result.ItemList = null;
                    return result;
                }

                var listerResult = await _context.Listers
                    .Where(x => x.ListerId == ListerId && x.Status == true)
                    .FirstOrDefaultAsync();
                if (listerResult == null)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ListerId is not found..";
                    result.ItemList = null;
                    return result;
                }

                List<ListerItem> itemlistDbResult;
                List<ListerItemResult> itemlist = new List<ListerItemResult>();

                if (Status == null)
                {
                    itemlistDbResult = await _context.ListerItems
                        .Where(x => x.ListerId == ListerId)
                        .ToListAsync();
                }
                else
                {
                    itemlistDbResult = await _context.ListerItems
                        .Where(x => x.ListerId == ListerId && x.Status == Status)
                        .ToListAsync();
                }

                foreach (var item in itemlistDbResult)
                {
                    // Get Images
                    var itemImageList = await _context.ItemImages
                        .Where(x => x.ListerItemId == item.ListerItemId)
                        .Select(x => new ItemImageResult
                        {
                            ImageId = x.ImageId,
                            // yaha URL banaya /images/ + relative path
                            ImageName = $"{baseUrl}/images/{x.ImageName.Replace("\\", "/")}"
                        })
                        .ToListAsync();
                    //End Get Images 

                    var viewCount = await _context.dbVieweds.CountAsync(x => x.ListerItemID == item.ListerItemId);
                    var bookCount = await _context.RenterItems.CountAsync(x => x.ItemId == item.ListerItemId);
                    var reviews = await _context.RenterItems
                        .Where(x => x.ItemId == item.ListerItemId && !string.IsNullOrEmpty(x.Review))
                        .Select(x => x.Review)
                        .ToListAsync();
                    var averageRating = await _context.RenterItems
                        .Where(x => x.ItemId == item.ListerItemId && x.Rating.HasValue)
                        .AverageAsync(x => x.Rating);

                    ListerItemResult list = new ListerItemResult();
                    list.ListerItemId = item.ListerItemId;
                    list.ItemName = item.ItemName;
                    list.ListerId = item.ListerId;
                    list.ItemCost = item.ItemCost;
                    list.ItemDescriptions = item.ItemDescriptions;
                    list.Availablefrom = item.Availablefrom;
                    list.Status = item.Status;
                    list.CategoryId = item.CategoryId;
                    list.CreatedDate = item.CreatedDate;
                    list.UpdatedOn = item.UpdatedOn;
                    list.CompanyName = item.Lister?.CompanyName ?? "No Company Name";
                    list.ItemImageList = itemImageList;
                    list.bookCount = bookCount;
                    list.viewCount = viewCount;
                    list.Review = reviews.Any() ? reviews : new List<string> { "No any Review yet!" };
                    list.StarRating = averageRating;

                    itemlist.Add(list);
                }

                if (itemlist.Count > 0)
                {
                    result.ResponseCode = "000";
                    result.ResponseMessage = "Success";
                    result.ItemList = itemlist;
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No Items found";
                    result.ItemList = null;
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ItemList = null;
            }
            return result;
        }


        [HttpGet("GetItemById")]
        public async Task<dynamic> GetItemById(int ListerId, int ItemId)
        {
            dynamic result = new ExpandoObject();
            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                if (ListerId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid ListerId.";
                    return result;
                }

                var listerResult = await _context.Listers
                    .Where(x => x.ListerId == ListerId && x.Status == true)
                    .FirstOrDefaultAsync();
                if (listerResult == null)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ListerId is not found..";
                    result.ItemList = null;
                    return result;
                }

                if (ItemId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid ItemId.";
                    return result;
                }

                var item = await _context.ListerItems
                    .Include(x => x.Lister)
                    .Where(x => x.ListerId == ListerId && x.ListerItemId == ItemId)
                    .FirstOrDefaultAsync();

                if (item != null)
                {
                    // Get Images
                    var itemImageList = await _context.ItemImages
                        .Where(x => x.ListerItemId == item.ListerItemId)
                        .Select(x => new ItemImageResult
                        {
                            ImageId = x.ImageId,
                            ImageName = $"{baseUrl}/images/{x.ImageName.Replace("\\", "/")}"
                        })
                        .ToListAsync();
                    //End Get Images 

                    var viewCount = await _context.dbVieweds.CountAsync(x => x.ListerItemID == item.ListerItemId);
                    var bookCount = await _context.RenterItems.CountAsync(x => x.ItemId == item.ListerItemId);
                    var reviews = await _context.RenterItems
                        .Where(x => x.ItemId == item.ListerItemId && !string.IsNullOrEmpty(x.Review))
                        .Select(x => x.Review)
                        .ToListAsync();
                    var averageRating = await _context.RenterItems
                        .Where(x => x.ItemId == item.ListerItemId && x.Rating.HasValue)
                        .AverageAsync(x => x.Rating);

                    List<ListerItemResult> itemlist = new List<ListerItemResult>();
                    ListerItemResult list = new ListerItemResult();
                    list.ListerItemId = item.ListerItemId;
                    list.ItemName = item.ItemName;
                    list.ListerId = item.ListerId;
                    list.ItemCost = item.ItemCost;
                    list.ItemDescriptions = item.ItemDescriptions;
                    list.Availablefrom = item.Availablefrom;
                    list.Status = item.Status;
                    list.CategoryId = item.CategoryId;
                    list.CreatedDate = item.CreatedDate;
                    list.UpdatedOn = item.UpdatedOn;
                    list.CompanyName = item.Lister?.CompanyName ?? "No Company Name";
                    list.bookCount = bookCount;
                    list.viewCount = viewCount;
                    list.Review = reviews.Any() ? reviews : new List<string> { "No any Review yet!" };
                    list.StarRating = averageRating;
                    list.ItemImageList = itemImageList;

                    itemlist.Add(list);

                    result.ResponseCode = "000";
                    result.ResponseMessage = "Success";
                    result.ItemList = itemlist;
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No item found.";
                    result.ItemList = null;
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ItemList = null;
            }
            return result;
        }



        [HttpPost("CreateItem")]
        public async Task<dynamic> CreateItem(ListerItem item)
        {
            dynamic result = new ExpandoObject();
            try
            {
                if (item.ListerId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid ListerId.";
                    return result;
                }

                var listerResult = await _context.Listers.Where(x => x.ListerId == item.ListerId && x.Status == true).FirstOrDefaultAsync();
                if (listerResult == null)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ListerId is not found..";
                    result.ItemList = null;
                    return result;
                }

                if (string.IsNullOrEmpty(item.ItemName))
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ItemName is required field.";
                    return result;
                }

                var itemResult = await _context.ListerItems.Where(x => x.ItemName == item.ItemName && x.ListerId == item.ListerId).FirstOrDefaultAsync();

                if (itemResult != null)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Item name already exists.";
                    result.ItemList = null;
                    return result;
                }

                if (item.CategoryId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "CategoryId is required field.";
                    return result;
                }

                if (string.IsNullOrEmpty(item.ItemDescriptions))
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ItemDescriptions is required field.";
                    return result;
                }

                if (item.ItemCost <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ItemCost value should be greater than 0.";
                    return result;
                }

                if (item.Status <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Status is required field";
                    return result;
                }

                if (item.Status != null && item.Status < 0 || item.Status > 3)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid status (For Visible:1, Not Visible:2, Discountune:3)";
                    return result;
                }

                if (item.Availablefrom == null)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Availablefrom is required field";
                    return result;
                }

                if (Convert.ToDateTime(item.Availablefrom).Date < DateTime.Now.Date)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Available from date should be future date.";
                    return result;
                }


                ListerItem list = new ListerItem();
                list.ItemName = item.ItemName;
                list.ListerId = item.ListerId;
                list.ItemCost = item.ItemCost;
                list.ItemDescriptions = item.ItemDescriptions;
                list.Availablefrom = item.Availablefrom;
                list.Status = item.Status;
                list.CreatedDate = DateTime.Now.Date;
                list.UpdatedOn = item.UpdatedOn;
                list.CategoryId = item.CategoryId;
                await _context.ListerItems.AddAsync(list);
                var itemid = await _context.SaveChangesAsync();
                result.ResponseCode = "000";
                result.ResponseMessage = "Success";
                result.ListerItemId = list.ListerItemId;
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ListerItemId = null;
            }
            finally
            {

            }
            return result;
        }


        [HttpPost("EditItem")]
        public async Task<dynamic> EditItem(ListerItem item)
        {
            dynamic result = new ExpandoObject();
            try
            {
                if (item.ListerId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid ListerId.";
                    return result;
                }

                if (string.IsNullOrEmpty(item.ItemName))
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ItemName is required field.";
                    return result;
                }

                if (item.CategoryId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "CategoryId is required field.";
                    return result;
                }


                if (string.IsNullOrEmpty(item.ItemDescriptions))
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ItemDescriptions is required field.";
                    return result;
                }

                if (item.ItemCost <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ItemCost value should be greater than 0.";
                    return result;
                }

                if (item.Status <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Status is required field";
                    return result;
                }

                if (item.Status != null && item.Status < 0 || item.Status > 3)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid status (For Visible:1, Not Visible:2, Discountune:3)";
                    return result;
                }

                if (item.Availablefrom == null)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Availablefrom is required field";
                    return result;
                }

                var ListerItems = await _context.ListerItems.Where(x => x.ListerId == item.ListerId && x.ListerItemId == item.ListerItemId).FirstOrDefaultAsync();
                if (ListerItems != null)
                {
                    ListerItems.ItemName = item.ItemName;
                    ListerItems.ItemCost = item.ItemCost;
                    ListerItems.ItemDescriptions = item.ItemDescriptions;
                    ListerItems.Availablefrom = item.Availablefrom;
                    ListerItems.Status = item.Status;
                    ListerItems.CategoryId = item.CategoryId;
                    ListerItems.UpdatedOn = DateTime.Now.Date;
                }
                var ListerItemId = await _context.SaveChangesAsync();
                result.ResponseCode = "000";
                result.ResponseMessage = "Success";
                result.ListerItemId = item.ListerItemId;
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ListerItemId = null;
            }
            finally
            {

            }
            return result;
        }

        // GET: api/<ListerController>
        [HttpDelete("DeleteItem")]
        public async Task<dynamic> DeleteItem(int ListerId, int ItemId)
        {
            dynamic result = new ExpandoObject();
            try
            {
                if (ListerId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid ListerId.";
                    return result;
                }

                if (ItemId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please enter is valid ItemId.";
                    return result;
                }


                var item = await _context.ListerItems.Where(x => x.ListerId == ListerId && x.ListerItemId == ItemId).FirstOrDefaultAsync();
                if (item != null)
                {
                    item.Status = 3; // Means - "Dis-Continue" 
                    var ListerItemId = await _context.SaveChangesAsync();
                    result.ResponseCode = "000";
                    result.ResponseMessage = "Item deleted successfully.";
                    result.ListerItemId = ListerItemId;
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No item found.";
                    result.ListerItemId = ItemId;
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ListerItemId = null;
            }
            finally
            {

            }
            return result;
        }
        [HttpPost("uploadItemImages")]
        public async Task<dynamic> uploadItemImages([FromForm] ItemImageResult item)
        {
            dynamic result = new ExpandoObject();

            try
            {
                if (item.ListerItemId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ListerItemId is required field.";
                    return result;
                }

                var itemResult = await _context.ListerItems
                    .Where(x => x.ListerItemId == item.ListerItemId && x.Status == 1)
                    .FirstOrDefaultAsync();

                if (itemResult == null)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ListerItemId is not found.";
                    result.ItemList = null;
                    return result;
                }

                if (item.ImageFiles == null || item.ImageFiles.Count == 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "Please upload at least one image.";
                    return result;
                }

                string folderName = "itemimages";
                string folderPath = Path.Combine(AppConfigModel.ImagePath, folderName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                List<int> savedImageIds = new List<int>();

                foreach (var file in item.ImageFiles)
                {
                    string ext = Path.GetExtension(file.FileName).ToLower();

                   
                    string newFileName =
                        $"{DateTime.Now:ddMMyyyyHHmmssfff}_{Guid.NewGuid()}{ext}";

                    string filePath = Path.Combine(folderPath, newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string dbPath = $"{folderName}/{newFileName}";

                    var imageModel = new ItemImage
                    {
                        ListerItemId = item.ListerItemId,
                        ImageName = dbPath
                    };

                    await _context.ItemImages.AddAsync(imageModel);
                    await _context.SaveChangesAsync();

                    savedImageIds.Add(imageModel.ImageId);
                }

                result.ResponseCode = "000";
                result.ResponseMessage = "Success";
                result.ImageIds = savedImageIds;
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
            }

            return result;
        }


        //[HttpPost("editItemImage")]
        //public async Task<dynamic> EditItemImage(ItemImageResult item)
        //{
        //    dynamic result = new ExpandoObject();
        //    try
        //    {
        //        if (item.ImageId <= 0)
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "ImageId is required.";
        //            return result;
        //        }

        //        var imageRecord = await _context.ItemImages.FirstOrDefaultAsync(x => x.ImageId == item.ImageId);
        //        if (imageRecord == null)
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "ImageId not found.";
        //            return result;
        //        }

        //        if (string.IsNullOrEmpty(item.ImageBase64Str) || string.IsNullOrEmpty(item.ImageFileExtn))
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "ImageBase64Str and ImageFileExtn are required.";
        //            return result;
        //        }

        //        if (!(item.ImageFileExtn.ToLower() == ".jpg" || item.ImageFileExtn.ToLower() == ".jpeg" || item.ImageFileExtn.ToLower() == ".png"))
        //        {
        //            result.ResponseCode = "999";
        //            result.ResponseMessage = "Only .jpg, .jpeg, .png extensions are allowed.";
        //            return result;
        //        }

        //        string folderName = "itemimages";
        //        string imageDirectory = Path.Combine(AppConfigModel.ImagePath, folderName);

        //        if (!Directory.Exists(imageDirectory))
        //        {
        //            Directory.CreateDirectory(imageDirectory);
        //        }

        //        // Delete old image file if exists
        //        if (!string.IsNullOrEmpty(imageRecord.ImageName))
        //        {
        //            string oldImagePath = Path.Combine(AppConfigModel.ImagePath, imageRecord.ImageName);
        //            if (System.IO.File.Exists(oldImagePath))
        //            {
        //                System.IO.File.Delete(oldImagePath);
        //            }
        //        }

        //        // Save new image
        //        string newFileName = DateTime.Now.ToString("ddMMyyyyhhmmssfff") + item.ImageFileExtn;
        //        string newImagePath = Path.Combine(imageDirectory, newFileName);
        //        System.IO.File.WriteAllBytes(newImagePath, Convert.FromBase64String(item.ImageBase64Str));

        //        // Update database
        //        imageRecord.ImageName = folderName + "\\" + newFileName;
        //        _context.ItemImages.Update(imageRecord);
        //        await _context.SaveChangesAsync();

        //        result.ResponseCode = "000";
        //        result.ResponseMessage = "Image updated successfully.";
        //        result.ImageId = imageRecord.ImageId;
        //        result.NewImagePath = imageRecord.ImageName;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ResponseCode = "999";
        //        result.ResponseMessage = ex.Message;
        //        result.ImageId = null;
        //    }

        //    return result;
        //}




        [HttpGet("getItemImages")]
        public async Task<dynamic> getItemImages(int ItemId)
        {
            dynamic result = new ExpandoObject();
            try
            {
                if (ItemId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ItemId is required field.";
                    return result;
                }

                var itemResult = await _context.ListerItems.Where(x => x.ListerItemId == ItemId && x.Status == 1).FirstOrDefaultAsync();

                if (itemResult == null)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "ItemId is not found.";
                    result.ItemList = null;
                    return result;
                }

                var ItemImagesResult = await _context.ItemImages.Where(x => x.ListerItemId == ItemId).ToListAsync();
                List<ItemImageResult> ItemImageList = new List<ItemImageResult>();
                foreach (var item in ItemImagesResult)
                {
                    ItemImageResult itemImage = new ItemImageResult();
                    var img = item.ImageName;
                    itemImage.ListerItemId = item.ListerItemId;
                    itemImage.ImageId = item.ImageId;
                    itemImage.ImageName = Utilities.GetBase64String(img);
                    ItemImageList.Add(itemImage);
                }

                if (ItemImageList.Count > 0)
                {
                    result.ResponseCode = "000";
                    result.ResponseMessage = "Success";
                    result.ItemImageList = ItemImageList;
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No Images found.";
                    result.ItemImageList = null;
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ItemImageList = null;
            }
            finally
            {

            }
            return result;
        }


        [HttpGet("GetGuestItems")]
        public async Task<dynamic> GetGuestItems()
        {
            dynamic result = new ExpandoObject();
            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                List<ListerItemResult> itemlist = new List<ListerItemResult>();
                var itemlistDbResult = await _context.ListerItems
                    .Include(x => x.Lister)
                    .Where(x => x.Status == 1)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(20)
                    .ToListAsync();

                foreach (var item in itemlistDbResult)
                {
                    // Get Images
                    var itemImageList = await _context.ItemImages
                        .Where(x => x.ListerItemId == item.ListerItemId)
                        .Select(x => new ItemImageResult
                        {
                            ImageId = x.ImageId,
                            ImageName = $"{baseUrl}/images/{x.ImageName.Replace("\\", "/")}"
                        })
                        .ToListAsync();

                    var viewCount = await _context.dbVieweds.CountAsync(x => x.ListerItemID == item.ListerItemId);
                    var bookCount = await _context.RenterItems.CountAsync(x => x.ItemId == item.ListerItemId);
                   
                    var reviews = await _context.RenterItems
                        .Where(x => x.ItemId == item.ListerItemId && !string.IsNullOrEmpty(x.Review))
                        .Select(x => x.Review)
                        .ToListAsync();
                  
                    var averageRating = await _context.RenterItems
                        .Where(x => x.ItemId == item.ListerItemId && x.Rating.HasValue)
                        .AverageAsync(x => x.Rating);

                    ListerItemResult list = new ListerItemResult();
                    list.ListerItemId = item.ListerItemId;
                    list.ItemName = item.ItemName;
                    list.ListerId = item.ListerId;
                    list.ItemCost = item.ItemCost;
                    list.ItemDescriptions = item.ItemDescriptions;
                    list.Availablefrom = item.Availablefrom;
                    list.Status = item.Status;
                    list.CategoryId = item.CategoryId;
                    list.CreatedDate = item.CreatedDate;
                    list.UpdatedOn = item.UpdatedOn;
                    list.ItemImageList = itemImageList;
                    list.CompanyName = item.Lister?.CompanyName ?? "No Company Name";
                    list.bookCount = bookCount;
                    list.viewCount = viewCount;
                    list.Review = reviews.Any() ? reviews : new List<string> { "No any Review yet!" };
                    list.StarRating = averageRating;

                    itemlist.Add(list);
                }
                if (itemlist.Count > 0)
                {
                    result.ResponseCode = "000";
                    result.ResponseMessage = "Success";
                    result.ItemList = itemlist;
                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No Items found";
                    result.ItemList = null;
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ItemList = null;
            }
            return result;
        }


        [HttpGet("GetSimilarItems")]
        public async Task<dynamic> GetSimilarItems(int CategoryId)
        {
            dynamic result = new ExpandoObject();
            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                if (CategoryId <= 0)
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "CategoryId Is Reduired.";
                    return result;
                }

                var SimItems = await _context.ListerItems
                    .Include(x => x.Lister)
                    .Where(x => x.CategoryId == CategoryId && x.Status == 1)
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(5)
                    .ToListAsync();

                if (SimItems == null || !SimItems.Any())
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No any items in this Category";
                    result.ItemList = null;
                    return result;
                }

                List<ListerItemSortInfo> itemlist = new List<ListerItemSortInfo>();
                foreach (var item in SimItems)
                {
                    ListerItemSortInfo list = new ListerItemSortInfo();

                    var itemImageList = await _context.ItemImages
                        .Where(x => x.ListerItemId == item.ListerItemId)
                        .Select(x => new ItemImageResult
                        {
                            ImageId = x.ImageId,
                            ImageName = $"{baseUrl}/images/{x.ImageName.Replace("\\", "/")}"
                        })
                        .ToListAsync();

                    list.CompanyName = item.Lister.CompanyName;
                    list.ItemName = item.ItemName;
                    list.ListerItemId = item.ListerItemId;
                    list.ListerId = item.ListerId;
                    list.CategoryId = item.CategoryId;
                    list.ItemImageList = itemImageList;

                    itemlist.Add(list);
                }

                result.ResponseCode = "000";
                result.ResponseMessage = "Success";
                result.ItemList = itemlist;
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.ItemList = null;
            }
            return result;
        }



        [HttpPost("bookItem")]
        public async Task<IActionResult> BookItem([FromBody] RenterItem model)
        {
            //if (model == null)
            //{
            //    return BadRequest(new { ResponseCode = "400", ResponseMessage = "Invalid Data." });
            //}

            // Validation check
            if (model.RenterId == null || model.RenterId <= 0)
            {
                return Ok(new { ResponseCode = "401", ResponseMessage = "Invalid ListerId" });
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


        [HttpGet("bookHistory")]
        public async Task<IActionResult> GetBookedItemHistoryByLister(int ListerId)
        {
            try
            {
                if (ListerId <= 0)
                {
                    return Ok(new
                    {
                        ResponseCode = "999",
                        ResponseMessage = "Please enter valid ListerId.",
                        Data = new List<BookedItemHistory>()
                    });
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                var history = await (
                    from renter in _context.RenterItems
                    join item in _context.ListerItems
                        on renter.ItemId equals item.ListerItemId
                    join lister in _context.Listers
                        on item.ListerId equals lister.ListerId
                    where item.ListerId == ListerId
                    select new BookedItemHistory
                    {
                        RenterItemId = renter.RenterItemId,
                        // RenterId = renter.RenterId ?? 0,
                        ListerId = lister.ListerId,
                        ItemId = item.ListerItemId,

                        RentFromDate = renter.RentFromDate,
                        RentToDate = renter.RentToDate,
                        BookingStatus = renter.Status,

                        ItemName = item.ItemName,
                        ItemCost = item.ItemCost,
                        ItemDescriptions = item.ItemDescriptions,

                        
                        ListerName = lister.Name,
                        CompanyName = lister.CompanyName
                    }
                )
                .OrderByDescending(x => x.RentFromDate)
                .ToListAsync();

                if (!history.Any())
                {
                    return Ok(new
                    {
                        ResponseCode = "999",
                        ResponseMessage = "No booking history found.",
                        Data = new List<BookedItemHistory>()
                    });
                }

                var itemIds = history.Select(h => h.ItemId).Distinct().ToList();

                var imagesDict = await _context.ItemImages
                     .Where(x => x.ListerItemId.HasValue
                         && itemIds.Contains(x.ListerItemId.Value))
                     .GroupBy(x => x.ListerItemId!.Value)
                     .ToDictionaryAsync(
                         g => g.Key,
                         g => g.Select(img =>
                             $"{baseUrl}/images/{img?.ImageName.Replace("\\", "/")}"
                         ).ToList()
                     );


                foreach (var h in history)
                {
                    if (h.ItemId.HasValue &&
                        imagesDict.TryGetValue(h.ItemId.Value, out var imgList))
                    {
                        h.ItemImages = imgList;
                    }
                    else
                    {
                        h.ItemImages = new List<string>();
                    }
                }


                return Ok(new
                {
                    ResponseCode = "000",
                    ResponseMessage = "Success",
                    Data = history
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "Error: " + ex.Message,
                    Data = new List<BookedItemHistory>()
                });
            }
        }




    }
}
