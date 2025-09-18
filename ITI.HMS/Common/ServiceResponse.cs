namespace ITI.HMS.Common
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; } 
        public string? Message { get; set; } 
    }
}
