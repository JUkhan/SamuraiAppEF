using System;
using Microsoft.EntityFrameworkCore;

namespace SamuraiApp.Data
{
    public class SamuraiContextNoChecking : SamuraiContext
    {
        public SamuraiContextNoChecking()
        {

            base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
