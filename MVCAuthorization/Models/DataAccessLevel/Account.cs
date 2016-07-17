namespace MVCAuthorization.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public Country Country { get; set; }
    }
}