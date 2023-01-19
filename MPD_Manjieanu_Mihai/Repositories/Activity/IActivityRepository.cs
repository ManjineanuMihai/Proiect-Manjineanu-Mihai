using MPD_Manjineanu_Mihai.Models;

namespace MPD_Manjineanu_Mihai.Repositories.Activity
{
    public interface IActivityRepository
    {
        Task<List<ActivityModel>> GetAllByUsername(string username);
        Task<ActivityModel> GetActivityById(int activityId);
        Task InsertActivity(ActivityModel activity);
        Task Update(ActivityModel activity);
        Task Delete(int activityId);
    }
}