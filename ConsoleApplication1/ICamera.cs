using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public interface ICamera
    {
         bool OpenCamera();
         bool CloseCamera();
         bool StartLive();
         bool StopLive();
         string cameraVendorInfo();
    }
}
