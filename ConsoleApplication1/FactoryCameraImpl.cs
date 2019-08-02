using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
   public class FactoryCameraImpl:ICameraFactory
    {
       public FactoryCameraImpl()
       {

       }
       public ICamera CreateCamera(string VendorName)
       {
           ICamera c;
           switch (VendorName)
           {
               case "Vendor1":
                   {
                       c = new Camera1Impl();
                   }
                   break;
               case "Vendor2":
                   {
                       c = new Camera2Impl();
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
