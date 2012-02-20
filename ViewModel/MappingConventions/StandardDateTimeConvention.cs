using System;

namespace ViewModel.MappingConventions
{
    public class StandardDateTimeConvention : PropertyMappingConventionBase<DateTime>
    {
        public StandardDateTimeConvention()
        {
            mapper = dt => DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToLocalTime();
        }
    }
}