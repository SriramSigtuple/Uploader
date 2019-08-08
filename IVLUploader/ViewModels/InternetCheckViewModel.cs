using BaseViewModel;
using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Input;

namespace IVLUploader.ViewModels
{
    /// <summary>
    /// Class which implements the check for internet connection by pinging to 8.8.8.8 of google
    /// </summary>
    public class InternetCheckViewModel : ViewBaseModel
    {
        Timer PingDNSTimer;
        Ping myPing;
        PingOptions pingOptions;
        PingReply reply;
        string host = "8.8.8.8";
        byte[] buffer = new byte[32];
        int timeout = 1000;
        Boolean internetPresent;
        const int MaxRetryCount = 60;
        int retryCount = 0;
        /// <summary>
        /// Constructor
        /// </summary>
        public InternetCheckViewModel()
        {
            myPing = new Ping();
            pingOptions = new PingOptions();
            PingDNSTimer = new Timer(new TimerCallback(PingDNS), null, 0, 10000);
            //SetValue = new RelayCommand(param=> SetValueMethod(param));

        }
        /// <summary>
        /// Method to check internet connection
        /// </summary>
        /// <param name="state"></param>
        private void PingDNS(object state)
        {
            IPStatus status = IPStatus.Unknown;
            try
            {
                reply = myPing.Send(host, timeout, buffer, pingOptions);
                status = reply.Status;
            }
            catch (Exception)
            {

                status = IPStatus.Unknown;
            }
            finally
            {
                if (status == IPStatus.Success)
                {
                    RetryCount = 0;
                    InternetPresent = true;
                }
                else
                {
                    InternetPresent = false;
                    if (RetryCount == MaxRetryCount)
                    {
                        //showMessageBox();
                        RetryCount = 0;
                    }
                    else
                        RetryCount++;

                }
            }
        }

        public ICommand SetValue
        {
            get;
            set;
        }


        /// <summary>
        /// Property for to Display Internet connection
        /// </summary>
        public bool InternetPresent
        {
            get => internetPresent;
            set
            {
                internetPresent = value;
                OnPropertyChanged("InternetPresent");
            }
        }
        /// <summary>
        /// Retry Count used to Pop up for user internet
        /// </summary>
        public int RetryCount
        {
            get => retryCount;
            set
            {
                retryCount = value;
                OnPropertyChanged("RetryCount");
            }
        }

        public void SetValueMethod(object param)
        {
            //this.FileUploadStatus = 100;
        }

    }
}
