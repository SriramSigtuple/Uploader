using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public abstract class FactoryCameraAbstract
    {
       public abstract CameraAbtract CreateCamera(string CameraVendor); 
    }
}
