using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public  class FactoryCamera: FactoryCameraAbstract
    {
      public FactoryCamera()
      {

      }
      public override CameraAbtract CreateCamera(string CameraVendor)
      {
          CameraAbtract c;
          switch (CameraVendor)
          {
              case "Vendor1":
                  {
                      c = new Camera1();
                  }
                  break;
              case "Vendor2":
                  {
                      c = new Camera2();
                  }
                  break;
              
              default:
                  c = null;
                  break;

          }
          return c;

      }

    }
}
