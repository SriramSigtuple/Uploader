using BaseViewModel;
using Cloud_Models.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using System.Windows.Media;

namespace IVLUploader.ViewModels
{
    public class MainWindowViewModel:ViewBaseModel
    {

       Timer fileCheckTimer;
       Timer AnalysisStatusCheckTimer;
       string DirectoryLocation = string.Empty;
       public FileUploadHelper fileUploader;
       private InternetCheckViewModel internetCheckViewModel;
       DirectoryInfo SentDirPath;
       private const string serverNotRunning = "Cloud Server is not running";
       private const string serverRunning = "Cloud Server is running";
        
       public MainWindowViewModel()
       {
            InternetCheckViewModel = new InternetCheckViewModel();
           //UploadFiles = new BindingList<UploadFileViewModel>();
           //UploadFiles.ListChanged += UploadFiles_ListChanged;
           //string uploaderDetailsStr = string.Empty;
          
           //if (File.Exists(@"UploaderDetails.json"))
           //{
           //    try
           //    {
           //        uploaderDetailsStr = File.ReadAllText(@"UploaderDetails.json");
           //        UploaderDetails.uploadValues = (UploaderModel)JsonConvert.DeserializeObject(uploaderDetailsStr,typeof(UploaderModel));
           //        if (UploaderDetails.uploadValues == null)
           //        {
           //            UploaderDetails.uploadValues = new UploaderModel();
           //            uploaderDetailsStr = JsonConvert.SerializeObject(UploaderDetails.uploadValues);
           //            File.WriteAllText(@"UploaderDetails.json", uploaderDetailsStr);
           //        }
           //    }
           //    catch (Exception ex)
           //    {

           //        UploaderDetails.uploadValues = new UploaderModel();
           //        uploaderDetailsStr = JsonConvert.SerializeObject(UploaderDetails.uploadValues);
           //        File.WriteAllText(@"UploaderDetails.json", uploaderDetailsStr);
           //    }
           //}
          
          // fileUploader = FileUploadHelper.GetInstance();
           
          //HardWareID = UploaderDetails.uploadValues.device_id;


          // fileUploader.PropertyChanged += fileUploader_PropertyChanged;
          // RemoveFile = new RelayCommand(param=> this.RemoveFileFromCollection(param));
          // BrowseFolderPath = new RelayCommand(param => this.OpenFileDialog(param));
          // StartUploadCmd = new RelayCommand(param => this.Login(param));
          // CloseAppCmd = new RelayCommand(param => this.WriteUploaderData(param));
 
          // fileCheckTimer = new Timer(5000);

          // fileCheckTimer.Elapsed += fileCheckTimer_Elapsed;
          // fileCheckTimer.Start();


          //  AnalysisStatusCheckTimer = new Timer(5000);
          //  AnalysisStatusCheckTimer.Elapsed += AnalysisStatusCheckTimer_Elapsed;
          // if (!string.IsNullOrEmpty(UploaderDetails.uploadValues.UploadDirectoryPath))
          // {
          //     if (Directory.Exists(UploaderDetails.uploadValues.UploadDirectoryPath))
          //         OutboxFolderPath = UploaderDetails.uploadValues.UploadDirectoryPath;
          // }
          // else
          // {
          //     OutboxFolderPath = new DirectoryInfo( System.Reflection.Assembly.GetExecutingAssembly().Location).Parent.FullName;
          // }
          // GetOutboxFiles();

       }

        private void AnalysisStatusCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }


        void fileUploader_PropertyChanged(object sender, PropertyChangedEventArgs e)
       {
           //if (e.PropertyName.Contains("IsServerRunning"))
           //{
           //    App.Current.Dispatcher.Invoke((Action)delegate
           //    {
           //        IsServerRunning = fileUploader.IsServerRunning;
           //    });
           //}
           //if (e.PropertyName.Contains("IsLogin"))
           //{
           //    App.Current.Dispatcher.Invoke((Action)delegate
           //    {
           //       StartUpload();
           //    });
           //}
       }

      private string serverRunningToolTip = serverNotRunning;

      public string ServerRunningToolTip
      {
          get { return serverRunningToolTip; }
          set { 
              serverRunningToolTip = value;
              OnPropertyChanged("ServerRunningToolTip");
                }
      }

        private System.Windows.Media.Color offsetColor2 = System.Windows.Media.Colors.Red;
        public Color OffsetColor2
        {
            get
            {
                return offsetColor2;
            }

            set
            {
                offsetColor2 = value;
                OnPropertyChanged("OffsetColor2");
            }
        }
        private int uploadStatusFile;

       public int UploadStatusFile
       {
           get { return uploadStatusFile; }
           set {
               uploadStatusFile = value;
               OnPropertyChanged("UploadStatusFile");
               }
       }
       public ICommand StartUploadCmd
       {
           get;
           set;
       }


       //public string HardWareID
       //{
       //    get { return UploaderDetails.uploadValues.device_id; }
       //    set { 
       //        UploaderDetails.uploadValues.device_id = value;
       //        OnPropertyChanged("HardWareID");
       //    }
       //}
       private void Login(object param)
       {
           if (!fileUploader.IsLogin)
           {
               fileUploader.Login2Server();
           }
          
       }

       private void StartUpload()
       {
           //if (fileUploader.IsLogin && IsServerRunning)
           //{
           //    if (UploadFiles.Count > 0)
           //    {
           //        if (!UploadFiles[0].IsUpload)
           //        {
           //            UploadFiles[0].IsUpload = true;
           //            UploadingFile = UploadFiles[0].FileName;
           //        }
           //    }
           //}
       }

       private string uploadingFile = string.Empty;

       public string UploadingFile
       {
           get { return uploadingFile; }
           set { uploadingFile = value;
           OnPropertyChanged("UploadingFile");
           }
       }
       void fileCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
       {
           //fileUploader.UpdateServerStatus();
          // GetOutboxFiles();
       }

       FileInfo GetActiveFileFromOutboxFiles()
       {
           FileInfo[] finfArr = new DirectoryInfo(OutboxFolderPath).GetFiles();
          
           foreach (FileInfo item in finfArr)
           { 
               App.Current.Dispatcher.Invoke((Action)delegate
                   {
               if (UploadFiles.Where(x => x.FileName == item.FullName).ToList().Count == 0)
               {
                  
                       UploadFiles.Add(new UploadFileViewModel() { FileName = item.FullName, FileUploadStatus = 0 });
                   
               }
                   });
           }
            if (finfArr.Any())
               return finfArr[0];
            return new FileInfo("");

       }
        void GetFileFromActiveDirectory()
        {
           FileInfo[] finfArr = new DirectoryInfo(ActiveFileFolderPath).GetFiles();
            if (!finfArr.Any())
            {
                GetActiveFileFromOutboxFiles();
            }
               
        }
        void CreateCloudViewModel()
        {

        }
       void UploadFiles_ListChanged(object sender, ListChangedEventArgs e)
       {
           if (e.PropertyDescriptor != null)
           {
               App.Current.Dispatcher.Invoke((Action)delegate
               {
                   if (e.PropertyDescriptor.DisplayName == "FileUploadStatus")
                   {
                       if (UploadFiles[e.NewIndex].FileUploadStatus == 100)
                       {
                           if(!File.Exists(SentDirPath.FullName + Path.DirectorySeparatorChar + new FileInfo(UploadFiles[0].FileName).Name))
                           File.Move(UploadFiles[0].FileName, SentDirPath.FullName + Path.DirectorySeparatorChar + new FileInfo(UploadFiles[0].FileName).Name);
                           UploadFiles.RemoveAt(e.NewIndex);
                           UploadedFileCount += 1;
                           StartUpload();
                       }
                       //else
                       //   UploadFiles[e.NewIndex].FileUploadStatus = fileUploader.Progress;
                   }
                   else if (e.PropertyDescriptor.DisplayName == "IsUpload")
                   {
                       UploadingFile = UploadFiles[e.NewIndex].FileName;
                   }
               });
           }
       }
       public ICommand BrowseFolderPath
       {
           get;
           set;
       }
       public void OpenFileDialog(object param)
       {
           using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
           {
               System.Windows.Forms.DialogResult result = dialog.ShowDialog();
               if (result == System.Windows.Forms.DialogResult.OK)
               {
                   if (!OutboxFolderPath.Equals(dialog.SelectedPath))
                   {
                       OutboxFolderPath = dialog.SelectedPath;
                       
                   }
               }
           }
       }

       private int uploadedFileCount;

       public int UploadedFileCount
       {
           get { return uploadedFileCount; }
           set {
               uploadedFileCount = value;
               OnPropertyChanged("UploadedFileCount");
           }
       }
       private string outboxfolderPath;

       public string OutboxFolderPath
       {
           get { return outboxfolderPath; }
           set {
               outboxfolderPath = value;
               UploadFiles.Clear();
               DirectoryInfo dirInf = new DirectoryInfo(outboxfolderPath);
               if (!Directory.Exists(dirInf.Parent.FullName + Path.DirectorySeparatorChar + "SentItems"))
                   SentDirPath = Directory.CreateDirectory(dirInf.Parent.FullName + Path.DirectorySeparatorChar + "SentItems");
               else
                   SentDirPath = new DirectoryInfo(dirInf.Parent.FullName + Path.DirectorySeparatorChar + "SentItems");
               UploaderDetails.uploadValues.UploadDirectoryPath = OutboxFolderPath;
               OnPropertyChanged("OutboxFolderPath");

           }
       }

        private string activeFileFolderPath;
       public ICommand RemoveFile
       {
           get;
           set;
       }

       private BindingList<UploadFileViewModel> uploadFiles;

       public BindingList<UploadFileViewModel> UploadFiles
       {
           get { return uploadFiles; }
           set
           {
               uploadFiles = value;
               OnPropertyChanged("UploadFiles");
           }
       }

       private int selectedIndex;

       public int SelectedIndex
       {
           get { return selectedIndex; }
           set { 
               selectedIndex = value;
               OnPropertyChanged("SelectedIndex");
           }
       }

       

        public void RemoveFileFromCollection(object param)
       {
           UploadFiles.RemoveAt(SelectedIndex);

       }
        public ICommand CloseAppCmd
        {
            get;
            set;
        }
        public string ActiveFileFolderPath { get => activeFileFolderPath; set => activeFileFolderPath = value; }
        public InternetCheckViewModel InternetCheckViewModel { get => internetCheckViewModel;
            set
            {
                internetCheckViewModel = value;
                OnPropertyChanged("InternetCheckViewModel");
            }
           
        }

        public void WriteUploaderData(object param)
        {
          string  uploaderDetailsStr = JsonConvert.SerializeObject(UploaderDetails.uploadValues);
            File.WriteAllText(@"UploaderDetails.json", uploaderDetailsStr);
        }
      

    }
}
