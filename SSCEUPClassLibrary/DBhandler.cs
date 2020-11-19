
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


        public IEnumerable<User> GetUser(string userName, string userPass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<User>("SELECT UserName,UserPass,IsAdmin FROM [USER] WHERE UserName = @userName AND UserPass = @userPass", new { UserName = userName, UserPass = userPass });
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

        internal void InsertIntoSurvey(Survey survey)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO SURVEY (Title, SurveyCode) values (@Title, @SurveyCode)", survey);
            }
        }

        internal int GetSurveyIdFromDB(string title)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var output = connection.Query<Survey>("SELECT SurveyId FROM SURVEY WHERE Title = @Title", new { Title = title }).FirstOrDefault();
                return output.SurveyId;
            }
        }

        internal string GetSurveyCodeFromDB(string surveyCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var output = connection.Query<Survey>("SELECT SurveyCode FROM SURVEY WHERE SurveyCode = @SurveyCode", new { SurveyCode = surveyCode}).FirstOrDefault();
                return output.SurveyCode;
            }
        }

        internal void InsertIntoQuestion(List<Question> questionlist)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                foreach (var question in questionlist)
                {
                    connection.Execute("INSERT INTO QUESTION (SurveyId, IsYesNoQuestion,Text) values (@SurveyId,@IsYesNoQuestion,@Text)", question);
                }
            }
        }
    }
}
//The reference assemblies for Office are exposed via the dynamic return type.
//To be able to compile you need to add a reference to Microsoft.CSharp.dll.