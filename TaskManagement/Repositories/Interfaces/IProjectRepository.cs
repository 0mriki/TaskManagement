using TaskManagement.Models;

namespace TaskManagement.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Returns all existing projects with pagination
        /// </summary>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">How many records will be in each page</param>
        /// <returns></returns>
        public Task<GetRecordsResult<Project?>?> GetAll(int pageNumber, int pageSize);
        /// <summary>
        /// Returns a project by it's id
        /// </summary>
        /// <param name="Id">Project id</param>
        /// <returns></returns>
        public Task<Project?> Get(Guid Id);
        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="project"></param>
        /// <returns>Is Successful</returns>
        public Task<bool> Create(Project project);
        /// <summary>
        /// Updates an existing project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public Task<bool> Update(Project project);
        /// <summary>
        /// Deletes existing project by it's id
        /// </summary>
        /// <param name="Id">Id of project</param>
        /// <returns>Is Successful</returns>
        public Task<bool> Delete(Guid Id);
    }
}
