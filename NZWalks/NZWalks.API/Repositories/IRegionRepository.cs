using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAllRegionsAsync();

       Task<Region> GetRegionAsync(Guid id);

       Task<Region> AddRegionAsync(Region region);

       Task<Region> DeleteAsync(Guid id);

       Task<Region> UpdateAsync(Guid id, Region region);
    }
}
