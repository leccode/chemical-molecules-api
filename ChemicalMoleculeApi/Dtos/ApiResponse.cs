using System.Net;

namespace ChemicalMoleculeApi.Dtos
{
    public record ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = [];
        public T? Result { get; set; }
    }
}
