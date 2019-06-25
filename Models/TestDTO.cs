using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerGenEnums.Models
{
    /// <summary>
    /// Description of TestDTO
    /// </summary>
    public class TestDTO
    {
        /// <summary>
        /// A normal timestamp
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// Description of the property
        /// </summary>
        public TestEnum Enum { get; set; }
    }
}
