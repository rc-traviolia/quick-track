using QuickTrackWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickTrackWeb.Services
{
    public interface IQuickTrackRepository
    {
        ClassEntity GetClassEntity(string userName);
    }
}
