using Quora.BLL.Interface;
using Quora.DAL;
using Quora.DAL.Interface;
using Quora.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public List<QuestionsByCategory> GetCategotyPage(int userId,int categoryId, int pageSize, int pageIndex, out int totalQuestion)
        {
            return _categoryRepository.GetCategotyPage(userId, categoryId,  pageSize,  pageIndex, out totalQuestion);
        }
    }
}
