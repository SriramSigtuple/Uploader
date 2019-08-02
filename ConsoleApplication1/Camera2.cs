using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class Camera2 : CameraAbtract
    {
      public Camera2()
      {

      }
      public override bool OpenCamera()
      {
          return true;
      }
      public override bool CloseCamera()
      {
          return true;
      }

      public override bool StartLive()
      {
          return true;
      }
      public override bool StopLive()
      {
          return true;
      }
      public override string cameraVendorInfo()
      {
          return "Data coming camera vendor2";
      }
      public override void virtualMethod()
      {
          Console.WriteLine("virtual method from camera2");
          base.virtualMethod();
      }
    }
}
