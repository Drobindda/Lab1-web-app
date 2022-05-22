using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lab1_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DBBookingContext _context;

        public ChartController(DBBookingContext context)
        {
            this._context = context;
        }

        [HttpGet("JsonDataAccomodationTypes")]
        public JsonResult JsonDataAccomodationTypes()
        {
            var accomodationTypes = _context.AccomodationTypes.Include(at => at.Accomodations).ToList();
            List<object> accType = new List<object>();
            accType.Add(new[] { "Тип", "Кількість" });
            foreach(var accomodationType in accomodationTypes)
            {
                accType.Add(new object[] {accomodationType.Name, accomodationType.Accomodations.Count});
            }
            return new JsonResult(accType);
        }

        [HttpGet("JsonDataAccomodationReviews")]
        public JsonResult JsonDataAccomodationReviews(int accomodationTypeId)
        {
            var accomodations = _context.Accomodations.Where(a => a.TypeId == accomodationTypeId).Include(a => a.Reviews).ToList();
            List<object> accReviews = new List<object>();
            accReviews.Add(new[] { "Житло", "Кількість відгуків" });
            foreach (var accomodation in accomodations)
            {
                accReviews.Add(new object[] { accomodation.Name, accomodation.Reviews.Count });
            }
            return new JsonResult(accReviews);
        }
    }
}
