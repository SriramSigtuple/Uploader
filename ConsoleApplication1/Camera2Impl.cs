using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class Camera2Impl:ICamera
    {
       public Camera2Impl()
       {

       }
        public  bool OpenCamera()
        {
            return true;
        }
        public  bool CloseCamera()
        {
            return true;
        }

        public  bool StartLive()
        {
            return true;
        }
        public  bool StopLive()
        {
            return true;
        }
        public  string cameraVendorInfo()
        {
            return "Data coming camera Camera2Impl";
        }
    }
}
