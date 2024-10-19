using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        Tour Get(long id);
        List<Tour> GetByStatus(string status);
        PagedResult<Tour> GetPaged(int page, int pageSize);
        void Delete(long id);
    }
}
