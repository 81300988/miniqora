using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL.Interface
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        List<QuestionsByCategory> GetCategotyPage(int userId,int categoryId, int pageSize, int pageIndex, out int totalQuestion);
    }
}
