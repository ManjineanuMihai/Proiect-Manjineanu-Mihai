using Microsoft.EntityFrameworkCore;
using MPD_Manjineanu_Mihai.Data;
using MPD_Manjineanu_Mihai.Models;

namespace MPD_Manjineanu_Mihai.Repositories.Category
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DataContext _data;

        public CategoryRepository(DataContext data)
        {
            _data = data;
        }
        public async Task<List<CategoryModel>> GetAll()
        {
            return  await _data.Categories.AsQueryable().ToListAsync();
        }
        public async Task<CategoryModel> GetById(int id)
        {
            return await _data.Categories.Where(x=> x.CategoryID == id).FirstOrDefaultAsync();
        }
        public async Task Update(CategoryModel category)
        {
            _data.Categories.Update(category);
            await _data.SaveChangesAsync();
        }
        public async Task Insert(CategoryModel category)
        {
            _data.Categories.Add(category);
            await _data.SaveChangesAsync();
        }

    }
}
