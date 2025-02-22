using Microsoft.AspNetCore.Http;

namespace ProductRating.Data.Entities.WebAPI.Requests
{
    public record ProductRecognitionRequest
    {
        public string ImageBase64 { get; init; }
    }
}