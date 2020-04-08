using QuickTrackWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services
{
    public class DefaultQuickTrackRepository : IQuickTrackRepository
    {
        ApplicationDbContext _applicationDbContext;
        public DefaultQuickTrackRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public ClassEntity GetClassEntity(string userName)
        {
            return _applicationDbContext.ClassEntities.Where(ce => ce.OwnerIdentityName == userName).FirstOrDefault();
        }
    }
}
