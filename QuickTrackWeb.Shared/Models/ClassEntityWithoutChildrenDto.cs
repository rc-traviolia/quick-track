using System;
using System.Collections.Generic;
using System.Text;

namespace QuickTrackWeb.Shared.Models
{
    public class ClassEntityWithoutChildrenDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerIdentityName { get; set; }

    }
}