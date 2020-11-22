
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


        public IEnumerable<User> GetUser(string userName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<User>("SELECT UserId,UserName,UserPass,IsAdmin FROM [USER] WHERE UserName = @userName", new { UserName = userName });
            }
        }

        internal IEnumerable<Question> GetSurveyQuestionsFromDB(string surveyCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var output = connection.Query<Question>("EXECUTE [dbo].[spGetSurveyQuestions] @FindCode ", new { FindCode = surveyCode }).ToList();
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

        internal int GetSurveyIdFromDB(string surveyCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var output = connection.Query<Survey>("SELECT SurveyId FROM SURVEY WHERE SurveyCode = @SurveyCode", new { SurveyCode = surveyCode }).FirstOrDefault();
                return output.SurveyId;
            }
        }

        internal IEnumerable<Survey> GetSurveyCodeFromDB(string surveyCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                return connection.Query<Survey>("SELECT SurveyCode, Title FROM SURVEY WHERE SurveyCode = @SurveyCode", new { SurveyCode = surveyCode });

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

        internal void InsertIntoAnswer(List<Answer> answerlist)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                foreach (var answer in answerlist)
                {
                    connection.Execute("EXEC [dbo].[spAnswer_SaveAnswer] @YoNAnswer, @ScaleAnswer, @QuestionId", new { answer.YoNAnswer, answer.ScaleAnswer, answer.QuestionId });
                }
            }
        }

        internal IEnumerable<Statistic> GetStatisticFromDB(int surveyId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                return connection.Query<Statistic>("EXEC [dbo].[spQuestion_GetStatistic] @SurveyId", new { SurveyId = surveyId }).ToList();

            }
        }

        internal IEnumerable<Survey> GetSurveysFromDB()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Survey>("SELECT SurveyId, Title, SurveyCode FROM SURVEY");
            }
        }
    }
}

