using BaseViewModel;
using Cloud_Models.Models;
using System.Windows.Input;

namespace IVLUploader.ViewModels
{
    /// <summary>
    /// Class which implements the check for internet connection by pinging to 8.8.8.8 of google
    /// </summary>
    public class UploadImagesViewModel : ViewBaseModel
    {
        UploadModel UploadModel;

        /// <summary>
        /// Constructor
        /// </summary>
        public UploadImagesViewModel(UploadModel uploadModel)
        {
            UploadModel = uploadModel;
            //SetValue = new RelayCommand(param=> SetValueMethod(param));

        }

        public ICommand SetValue
        {
            get;
            set;
        }


        private URL_Model loginURlModel;

        public URL_Model LoginURLModel
        {
            get { return loginURlModel; }
            set { loginURlModel = value; }
        }       




        public void SetValueMethod(object param)
        {
            //this.FileUploadStatus = 100;
        }

    }
}
