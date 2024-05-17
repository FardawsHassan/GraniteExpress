//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace GraniteExpress.Data
//{
//    // Add profile data for application users by adding properties to the ApplicationUser class
//    public  static class DbContextFactory
//    {
//        public static Dictionary<string, string> ConnectionStrings { get; set; }
//        public static void SetConnectionString(Dictionary<string, string> connString)
//        {
//            ConnectionStrings = connString;
//            Create("DB1");
//        }

//        public static ApplicationDbContext Create(string connId)
//        {
//            if (!string.IsNullOrEmpty(connId))
//            {
//                var connstr = ConnectionStrings[connId];
//                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//                optionsBuilder.UseSqlServer(connstr);
//                return new ApplicationDbContext(optionsBuilder.Options);
//            }
//            else
//            {
//                throw new ArgumentNullException("connecitonid is null");
//            }
//        }
//    }


//}
