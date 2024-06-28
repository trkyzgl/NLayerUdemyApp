using System.Text.Json.Serialization;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        [JsonIgnore] // data dönerken görünmesine gerek yok çünkü sonuç olarak zaten bir StatusCode dönecek. Ama kodlama aşamasında bu değer bana lazım olacağı için bu şekilde tutuyorum.
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; } // birden fazla hata olabildiği için list yaptık
        /*geriye dönecek static metotlar yazacağız. önce success olanları, sonra Fail olanları yazacağız*/
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {// bir tane daha yapıyoruz. Çünkü geriye bir data dönmek zorunda olmadığımız durumlar da olabilir.

            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {// Fail durumları için
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {// sadece bir tane Error gelme durumu için
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }



    }
}
