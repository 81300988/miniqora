using System;
using System.Collections.Generic;
using Quora.Model;

namespace Quora.DAL.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        List<QuestionsByCategory> GetCategotyPage(int userId, int categoryId, int pageSize, int pageIndex, out int totalQuestion);
    }
}
