using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore] // data dönerken görünmesine gerek yok çünkü sonuç olarak zaten bir StatusCode dönecek. Ama kodlama aşamasında bu değer bana lazım olacağı için bu şekilde tutuyorum.
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; } // birden fazla hata olabildiği için list yaptık

        /*geriye dönecek static metotlar yazacağız. önce success olanları, sonra Fail olanları yazacağız*/
        public static CustomResponseDto<T> Success(int statusCode,T data)
        { 
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data };
        }
        // bir tane daha yapıyoruz. Çünkü geriye bir data dönmek zorunda olmadığımız durumlar da olabilir.
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        // Fail durumları için
        public static CustomResponseDto<T> Fail(int statusCode,List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode , Errors= errors};
        }
        // sadece bir tane Error gelme durumu için
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors =  new List<string> { error} };
        }

    }
}
