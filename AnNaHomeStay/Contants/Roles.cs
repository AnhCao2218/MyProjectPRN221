namespace AnNaHomeStay.Contants
{
    public class Roles
    {
        public static List<string> instance = new List<string>()
        {
            Models.Role.USER.ToString(),
            Models.Role.ADMIN.ToString()
        };
    }
}
