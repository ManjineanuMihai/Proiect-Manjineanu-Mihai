using MPD_Manjineanu_Mihai.Models;

namespace MPD_Manjineanu_Mihai.Repositories.Category
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAll();
        Task<CategoryModel> GetById(int id);
        Task Insert(CategoryModel category);
        Task Update(CategoryModel category);
    }
}