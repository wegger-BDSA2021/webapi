using Data;

namespace Services
{
    public record Result
    {
        public Response Response { get; init; }
        public string Message { get; init; }

        #nullable enable
        public object? DTO { get; init; }
    }
    
}