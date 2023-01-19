using Microsoft.EntityFrameworkCore;
using MPD_Manjineanu_Mihai.Data;
using MPD_Manjineanu_Mihai.Models;

namespace MPD_Manjineanu_Mihai.Repositories.Activity
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly DataContext _data;

        public ActivityRepository(DataContext data)
        {
            _data = data;
        }
        public async Task<List<ActivityModel>> GetAllByUsername(string username)
        {
            return  await _data.Activities.AsQueryable()
                .Where(x => x.User.Username == username).ToListAsync();
        }
        public async Task InsertActivity(ActivityModel activity)
        {
             _data.Activities.Add(activity);
             _data.SaveChanges();
        }
        public async Task<ActivityModel> GetActivityById(int activityId)
        {
            return await _data.Activities.Where(x => x.ActivityID == activityId).SingleOrDefaultAsync();
        }

        public async Task Update(ActivityModel activity)
        {
           _data.Activities.Update(activity);
           await _data.SaveChangesAsync();
        }
        public async Task Delete(int activityId)
        {
           var activity =  await _data.Activities.FirstOrDefaultAsync(x=>x.ActivityID == activityId);
            _data.Activities.Remove(activity);
            await _data.SaveChangesAsync();
        }
    }
}
