using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)  // eğer Id değeri null ise kodun yürütümü devam edecek.. NOT: bURASI SONRADAN AYRICA KONTROL EDİLECEK VE BAŞKA HATALAR İNCELENECEK
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);
            if (anyEntity)// eğer Entity var ise kod yürütümü devam edecek.
            {
                await next.Invoke();
                return;
            }
            var errorViewModel = new ErrorViewModel();
            errorViewModel.Errors.Add($"{typeof(T).Name}({id}) not found");
            context.Result = new RedirectToActionResult("Error", "Home", errorViewModel);

            //context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name} ({id}) not found"));   // Bu satır API için geçerli ama MVC de değiştreceğiz
            //throw new NotImplementedException();        }
        }
    }
}
