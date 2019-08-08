using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Models.Models
{
    public class ResponseModel
    {
        public int status = 0;
        public string message = string.Empty;
        public string token = string.Empty;
        public string cookie = string.Empty;
        public ResponseModel()
        {

        }
    }
}
