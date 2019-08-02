using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
namespace WpfApplication1
{
   public abstract class ViewModelBase: INotifyPropertyChanged, IDisposable
    {
       protected virtual bool ThrowInvalidPropertyName { get; private set; }

       private void VerifyPropertyName(string propertyName)
       {
           if (TypeDescriptor.GetProperties(this)[propertyName] == null)
           {
               string msg = "Invalid Property Name = " + propertyName;
               if (this.ThrowInvalidPropertyName)
                   throw new Exception(msg);
               else
                   Debug.Fail(msg);
           }
       }

       private event PropertyChangedEventHandler PropertyChangedHandler;

       protected  void  OnPropertyChanged(string propertyName)
       {
           VerifyPropertyName(propertyName);

           PropertyChangedEventHandler handler = this.PropertyChangedHandler;
           if(handler != null)
           {
               var e = new PropertyChangedEventArgs(propertyName);
               handler(this, e);
           }
       }

       public void Dispose()
       {
           this.onDispose();
       }

       protected virtual void onDispose()
       {
       }

    }
}
