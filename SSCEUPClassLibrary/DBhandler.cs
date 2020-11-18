
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace SSCEUPClassLibrary
{
    class DBhandler
    {   
        
         private readonly string connectionString;

        public DBhandler(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<User> GetUser(string userName,string userPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<User>("SELECT UserName FROM USER WHERE UserName = @userName;" , new {UserName = userName});
                                                                                                                

            // Execute a query and map the first result to a dynamic list, and throws an exception if 
            //there is not exactly one element in the sequence.
            // string sql = "SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID;";

            //   using (var connection = new SqlConnection(FiddleHelper.GetConnectionStringSqlServerW3Schools()))
            //               {	
            //           var orderDetail = connection.QuerySingle(sql, new {OrderDetailID = 1});       
            }
        }
        internal IEnumerable<Survey> GetSurveysFromDB()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var output = connection.Query<Survey>("SELECT * FROM SURVEY").ToList();
                return output;
                
            }
        }

        internal void InsertIntoSurvey(Survey title)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO SURVEY (Title) values (@Title)",title);
            }
        }

        internal IEnumerable<Survey> GetSurveyIdFromDB(Survey title)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var output = connection.Query<Survey>("SELECT SurveyId FROM SURVEY");
                return output;
            }
        }

        internal void InsertIntoQuestion(List<Question> questionlist)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO QUESTION (Text) values (@Text)", questionlist);
            }
        }
    }
}