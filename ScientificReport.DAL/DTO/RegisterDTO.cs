namespace ScientificReport.DAL.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Cathedra { get; set; }
        
        public string Faculty { get; set; }
    }
}
