using Dapper;
using Quora.DAL.Interface;
using Quora.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Quora.DAL
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly IDbConnection _dbConnection;
        public CategoryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            categories = _dbConnection.Query<Category>("SELECT CategoryId, CategoryName, ImageName FROM Category").ToList();

            return categories;
        }
        public List<QuestionsByCategory> GetCategotyPage(int userId, int categoryId, int pageSize, int pageIndex,out int totalQuestion)
        {
            List<QuestionsByCategory> questions = new List<QuestionsByCategory>();
            var p = new DynamicParameters();
            p.Add("@userId", userId);
            p.Add("@categoryId", categoryId);
            p.Add("@PageSize", pageSize);
            p.Add("@Index", pageIndex);

            questions = _dbConnection.Query<QuestionsByCategory>("sp_GetQuestionByCategory",p, commandType: CommandType.StoredProcedure).ToList();

            totalQuestion = _dbConnection.Query<int>(@"select count(q.QuestionId) from Question as q
                                                                                inner join	QuestionCategory as qc on q.QuestionId = qc.QuestionId
                                                                                inner join	Category as c on qc.CategoryId = c.CategoryId
                                                                                where c.CategoryId=@CategoryId", new { CategoryId  = categoryId }).Single();

            return questions;
        }
    }
}
