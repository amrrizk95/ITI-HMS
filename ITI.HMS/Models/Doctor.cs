namespace ITI.HMS.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Specialty { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}
