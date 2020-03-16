using System.Collections.Generic;

namespace OnlineEducation.Common
{
    public class FilterResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public long ItemsCount { get; set; }
    }
}
