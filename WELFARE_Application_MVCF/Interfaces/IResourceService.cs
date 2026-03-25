using WELFARE_Application_MVCF.Models;

namespace WELFARE_Application_MVCF.Interfaces
{
    public interface IResourceService
    {
        Task<IEnumerable<Resource>> GetAllResourcesAsync();
        Task<IEnumerable<Resource>> GetResourcesByProgramIdAsync(int programId);
        Task AddResourceAsync(Resource resource);
        Task UpdateResourceAsync(Resource resource);
    }
}
