using EazyOnRent.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace EazyOnRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly EazyOnRentContext _context;
        public CategoriesController(EazyOnRentContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GellALLCategories")]
        public async Task<dynamic> GellALLCategories()
        {
            dynamic result = new ExpandoObject();
            try
            {
                var categories = await _context.Categories.Where(x => x.Status == true).ToListAsync();
                if (categories.Count > 0)
                {
                    result.ResponseCode = "000";
                    result.ResponseMessage = "Success";
                    result.CategoriesList = categories;

                }
                else
                {
                    result.ResponseCode = "999";
                    result.ResponseMessage = "No categories list found.";
                    result.CategoriesList = null;
                    //result.categories = null;
                }
            }
            catch (Exception ex)
            {
                result.ResponseCode = "999";
                result.ResponseMessage = ex.Message;
                result.CategoriesList = null;
            }
            finally
            {

            }
            return result;
        }
    }
}
