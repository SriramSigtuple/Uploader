using BaseViewModel;
using Cloud_Models.Models;
using System.Windows.Input;

namespace IVLUploader.ViewModels
{
    /// <summary>
    /// Class which implements the check for internet connection by pinging to 8.8.8.8 of google
    /// </summary>
    public class InitiateAnalysisViewModel : ViewBaseModel
    {
        InitiateAnalysisModel initiateAnalysisModel;

        /// <summary>
        /// Constructor
        /// </summary>
        public InitiateAnalysisViewModel(InitiateAnalysisModel intiateAnalysisModel)
        {
            InitiateAnalysisModel = intiateAnalysisModel;
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

        public InitiateAnalysisModel InitiateAnalysisModel { get => initiateAnalysisModel;
            set
            {
                initiateAnalysisModel = value;
                OnPropertyChanged("InitiateAnalysisModel");
            }
        }

        public void SetValueMethod(object param)
        {
            //this.FileUploadStatus = 100;
        }

    }
}
