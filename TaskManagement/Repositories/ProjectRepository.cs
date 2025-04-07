using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TaskManagement.DatabaseContext;
using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;

namespace TaskManagement.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ILogger<ProjectRepository> _logger;
        private readonly AppDbContext _context;

        public ProjectRepository(ILogger<ProjectRepository> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _context = appDbContext;
        }

        public async Task<bool> Create(Project project)
        {
            try
            {
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Project with id {project.Id} was successfuly created");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while creating project", ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                int deletedRedcordsCount = await _context.Projects.Where(i => i.Id == Id).ExecuteDeleteAsync();

                if (deletedRedcordsCount == 0)
                {
                    _logger.LogWarning($"No project with id {Id} was found");
                } else
                {
                    _logger.LogInformation($"Project with id {Id} was successfuly deleted");
                }
                        
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while deleting project", ex.Message);
                return false;
            }
        }

        public async Task<Project> Get(Guid Id)
        {
            Project project = null;
            try
            {
                project = await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(i => i.Id == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while fetching project", ex.Message);
            }
            return project;
        }


        public async Task<GetRecordsResult<Project>?> GetAll(int pageNumber, int pageSize)
        {
            List<Project> allProjects = null;
            int totalProjectsCount = 0;
            int totalPages = 0;
            try
            {

                allProjects = await _context.Projects.Include(p => p.Tasks).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                totalProjectsCount = await _context.Projects.CountAsync();
                totalPages = (int)Math.Ceiling(totalProjectsCount / (double)pageSize);

                return new GetRecordsResult<Project>(totalProjectsCount, totalPages, pageNumber, pageSize, allProjects);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while fecthing paginated projects", ex.Message);
                return null;
            }
        }

        public async Task<bool> Update(Project project)
        {
            try
            {
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Project with id {project.Id} was updated successfuly");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while updating project", ex.Message);
                return false;
            }
        }
    }
}
