using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Diagnostics;
namespace ConsoleApplication1
{
   public abstract class ViewModelBase : INotifyPropertyChanged,IDisposable
    {

       public ViewModelBase()
       {

       }
       public void VerifyPropertyName(string propertyName)
       {
           if (TypeDescriptor.GetProperties(this)[propertyName] == null)
           {
               
           }
       }
       public event PropertyChangedEventHandler PropertyChangedHandler;
       protected virtual void onPropertyChanged(string propertyName)
       {
           this.VerifyPropertyName(propertyName);
           PropertyChangedEventHandler handler = this.PropertyChangedHandler;
           if (handler != null)
           {
               var e = new PropertyChangedEventArgs(propertyName);
               handler(this, e);
           }
       }
    }


}
