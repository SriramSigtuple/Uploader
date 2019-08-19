using BaseViewModel;
using Cloud_Models.Models;
using IntuUploader;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Input;

namespace IVLUploader.ViewModels
{
    /// <summary>
    /// Class which implements the check for internet connection by pinging to 8.8.8.8 of google
    /// </summary>
    public class OutboxViewModel : ViewBaseModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        Timer OutboxFileChecker;
        int timeout = 10000;// TODO : to be configured
        int timerTick = 20000;// TODO : to be configured
        CloudViewModel activeFileCloudVM;

        int retryCount = 0;
        private static OutboxViewModel _outboxViewModel;
        /// <summary>
        /// Constructor
        /// </summary>
        private OutboxViewModel()
        {
            logger.Info("");

            //SetValue = new RelayCommand(param=> SetValueMethod(param));
            OutboxFileChecker = new System.Threading.Timer(OutBoxTimerCallback, null, 0, timerTick);
            logger.Info("");


        }
        /// <summary>
        /// Implementing singleton pattern in order to handle to values across the module
        /// </summary>
        /// <returns></returns>
        public static OutboxViewModel GetInstance()
        {
            logger.Info("");

            if (_outboxViewModel == null)
                _outboxViewModel = new OutboxViewModel();
            logger.Info("");

            return _outboxViewModel;
        }

        /// <summary>
        /// Method to get Files from outbox to active directory
        /// </summary>
        /// <param name="state"></param>
        private void OutBoxTimerCallback(object state)
        {
            logger.Info("");

            FileInfo[] outboxDirFileInfoArr = new DirectoryInfo(GlobalMethods.GetDirPath(DirectoryEnum.OutboxDir)).GetFiles();
            Console.WriteLine(outboxDirFileInfoArr.Length);
            FileInfo[] activeDirFileInfoArr = new DirectoryInfo(GlobalMethods.GetDirPath(DirectoryEnum.ActiveDir)).GetFiles();
            if (!(activeDirFileInfoArr.Any()) && outboxDirFileInfoArr.Any())
            {
                outboxDirFileInfoArr[0].MoveTo(Path.Combine(GlobalMethods.GetDirPath(DirectoryEnum.ActiveDir), outboxDirFileInfoArr[0].Name));
                GetFileFromActiveDir(activeDirFileInfoArr);

            }
            else if (activeFileCloudVM == null)
            {
                GetFileFromActiveDir(activeDirFileInfoArr);
            }
            logger.Info("");

        }

        private void GetFileFromActiveDir(FileInfo[] activeDirFileInfos)
        {
            logger.Info("");

            activeDirFileInfos = new DirectoryInfo(GlobalMethods.GetDirPath(DirectoryEnum.ActiveDir)).GetFiles();
            if (activeDirFileInfos.Any() && activeFileCloudVM == null)
            {
                StreamReader st = new StreamReader(activeDirFileInfos[0].FullName);
                var json = st.ReadToEnd();
                st.Close();
                CloudModel activeFileCloudModel = JsonConvert.DeserializeObject<CloudModel>(json);
                activeFileCloudVM = new CloudViewModel(activeFileCloudModel);
                activeFileCloudVM.ActiveFnf = activeDirFileInfos[0];

                activeFileCloudVM.StartAnalsysisFlow();
            }
            logger.Info("");

        }
        public ICommand SetValue
        {
            get;
            set;
        }


        public void SetValueMethod(object param)
        {
            //this.FileUploadStatus = 100;
        }

    }
}
