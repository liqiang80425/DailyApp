using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF
{

    public class ApiResponse
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public object Result { get; set; }
    }

    public class ApiResponse<T>
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public T Result { get; set; }
    }
}
