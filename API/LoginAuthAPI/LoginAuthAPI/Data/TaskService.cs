using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class TaskService
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Person> FetchUsersAsync(int id)
        {
             
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AuthDBContext>();
                var user = await  context.Users.FindAsync(id);
                return user;
            }
        }
    }
}
