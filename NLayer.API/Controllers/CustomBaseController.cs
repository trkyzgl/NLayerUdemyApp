using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        /*Bu Controller ı oluşturmamızdaki Temel amaç ; Endpointlerimizde hem Ok, Created gibi durum kodlarını, hemde 204,201 gibi durum kodlarını yazmak yerine 
         sadece 204,201,500 gibi durum kodlarını method içinde belirtmektir*/

        [NonAction] // Bu method bir endpoint değildir.
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            // değilse
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };


        }

    }
}
