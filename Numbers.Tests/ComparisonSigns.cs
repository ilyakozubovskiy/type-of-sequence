using System;

namespace Numbers
{
    [Flags]
    public enum ComparisonSigns
    {
        LessThan = 1,
        MoreThan = 2,
        Equals = 4,
    }
}