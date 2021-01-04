
namespace OnlineClinic.Core.DTOs
{
    public class DoctorUserCredentialDto
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public string EncryptedPassword { get; set; }
    }
}