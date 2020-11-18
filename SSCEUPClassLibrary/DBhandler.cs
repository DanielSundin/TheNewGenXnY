// using System.Collections.Generic;
// using System.Data.SqlClient;
// using Dapper;

// namespace SSCEUPClassLibrary
// {
//     class DBhandler
//     {
//         private readonly string connectionString;

//         public DBhandler(string connectionString)
//         {
//             this.connectionString = connectionString;
//         }

//         public IEnumerable<User> GetPlayers()
//         {
//             using (SqlConnection connection = new SqlConnection(connectionString))
//             {
//                 return connection.Query<User>("SELECT Id, FirstName FROM F8");
//             }
//         }
         


//     }
// }