using TaskManagement.Models;

namespace TaskManagement.Services.Interfaces
{
    public interface IProjectService
    {
        public Task<GetRecordsResult<Project?>?> GetAll(int pageNumber, int pageSize);
        public Task<Project?> Get(Guid Id);
        public Task<bool> Create(Project project); 
        public Task<bool> Update(Project project);
        public Task<bool> Delete(Guid Id);
    }
}
