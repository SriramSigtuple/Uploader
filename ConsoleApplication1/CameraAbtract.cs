using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public abstract class CameraAbtract
    {

        public abstract bool OpenCamera();
        public abstract bool CloseCamera();
        public abstract bool StartLive();
        public abstract bool StopLive();
        public abstract string cameraVendorInfo();

        public virtual void virtualMethod()
        {
            Console.WriteLine("this is a virtual method from base");
        }

    }
}
