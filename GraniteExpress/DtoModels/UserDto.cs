namespace GraniteExpress.DtoModels
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public Dictionary<string,string> Claims { get; set; }
    }
}
