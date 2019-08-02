using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Timers;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Media;

namespace IVLUploader.ViewModels
{
   public class MainWindowViewModel:ViewBaseModel
    {

       Timer fileCheckTimer;
       string DirectoryLocation = string.Empty;
       public FileUploadHelper fileUploader;
       DirectoryInfo SentDirPath;
       private const string serverNotRunning = "Cloud Server is not running";
       private const string serverRunning = "Cloud Server is running";
       public MainWindowViewModel()
       {
           UploadFiles = new BindingList<UploadFileViewModel>();
           UploadFiles.ListChanged += UploadFiles_ListChanged;
           string uploaderDetailsStr = string.Empty;
          
           if (File.Exists(@"UploaderDetails.json"))
           {
               try
               {
                   uploaderDetailsStr = File.ReadAllText(@"UploaderDetails.json");
                   UploaderDetails.uploadValues = (UploaderClass)JsonConvert.DeserializeObject(uploaderDetailsStr,typeof(UploaderClass));
                   if (UploaderDetails.uploadValues == null)
                   {
                       UploaderDetails.uploadValues = new UploaderClass();
                       uploaderDetailsStr = JsonConvert.SerializeObject(UploaderDetails.uploadValues);
                       File.WriteAllText(@"UploaderDetails.json", uploaderDetailsStr);
                   }
               }
               catch (Exception ex)
               {

                   UploaderDetails.uploadValues = new UploaderClass();
                   uploaderDetailsStr = JsonConvert.SerializeObject(UploaderDetails.uploadValues);
                   File.WriteAllText(@"UploaderDetails.json", uploaderDetailsStr);
               }
           }
          
           fileUploader = FileUploadHelper.GetInstance();
           
          HardWareID = UploaderDetails.uploadValues.HardwareID;


           fileUploader.PropertyChanged += fileUploader_PropertyChanged;
           RemoveFile = new RelayCommand(param=> this.RemoveFileFromCollection(param));
           BrowseFolderPath = new RelayCommand(param => this.OpenFileDialog(param));
           StartUploadCmd = new RelayCommand(param => this.Login(param));
           CloseAppCmd = new RelayCommand(param => this.WriteUploaderData(param));
 
           fileCheckTimer = new Timer(5000);

           fileCheckTimer.Elapsed += fileCheckTimer_Elapsed;
           fileCheckTimer.Start();
           if (!string.IsNullOrEmpty(UploaderDetails.uploadValues.UploadDirectoryPath))
           {
               if (Directory.Exists(UploaderDetails.uploadValues.UploadDirectoryPath))
                   FolderPath = UploaderDetails.uploadValues.UploadDirectoryPath;
           }
           else
           {
               FolderPath = new DirectoryInfo( System.Reflection.Assembly.GetExecutingAssembly().Location).Parent.FullName;
           }
           GetZippedFiles();

       }

      void fileUploader_PropertyChanged(object sender, PropertyChangedEventArgs e)
       {
           if (e.PropertyName.Contains("IsServerRunning"))
           {
               App.Current.Dispatcher.Invoke((Action)delegate
               {
                   IsServerRunning = fileUploader.IsServerRunning;
               });
           }
           if (e.PropertyName.Contains("IsLogin"))
           {
               App.Current.Dispatcher.Invoke((Action)delegate
               {
                  StartUpload();
               });
           }
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
      private bool isServerRunning ;
       public bool IsServerRunning
        {
            get { return true; }
            set
            {
                if (isServerRunning != value)
                isServerRunning = true;
                if (isServerRunning)
                    ServerRunningToolTip = serverRunning;
                else
                    ServerRunningToolTip = serverNotRunning;
                OnPropertyChanged("IsServerRunning");
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


       public string HardWareID
       {
           get { return UploaderDetails.uploadValues.HardwareID; }
           set { 
               UploaderDetails.uploadValues.HardwareID = value;
               OnPropertyChanged("HardWareID");
           }
       }
       private void Login(object param)
       {
           if (!fileUploader.IsLogin)
           {
               fileUploader.Login2Server();
           }
          
       }

       private void StartUpload()
       {
           if (fileUploader.IsLogin && IsServerRunning)
           {
               if (UploadFiles.Count > 0)
               {
                   if (!UploadFiles[0].IsUpload)
                   {
                       UploadFiles[0].IsUpload = true;
                       UploadingFile = UploadFiles[0].FileName;
                   }
               }
           }
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
           fileUploader.UpdateServerStatus();
           GetZippedFiles();
       }

       void GetZippedFiles()
       {
           FileInfo[] finfArr = new DirectoryInfo(FolderPath).GetFiles();
          
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
           StartUpload();
          

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
                   if (!FolderPath.Equals(dialog.SelectedPath))
                   {
                       FolderPath = dialog.SelectedPath;
                       
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
       private string folderPath;

       public string FolderPath
       {
           get { return folderPath; }
           set {
               folderPath = value;
               UploadFiles.Clear();
               DirectoryInfo dirInf = new DirectoryInfo(folderPath);
               if (!Directory.Exists(dirInf.Parent.FullName + Path.DirectorySeparatorChar + "SentItems"))
                   SentDirPath = Directory.CreateDirectory(dirInf.Parent.FullName + Path.DirectorySeparatorChar + "SentItems");
               else
                   SentDirPath = new DirectoryInfo(dirInf.Parent.FullName + Path.DirectorySeparatorChar + "SentItems");
               UploaderDetails.uploadValues.UploadDirectoryPath = FolderPath;
               OnPropertyChanged("FolderPath");

           }
       }
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
        public void WriteUploaderData(object param)
        {
          string  uploaderDetailsStr = JsonConvert.SerializeObject(UploaderDetails.uploadValues);
            File.WriteAllText(@"UploaderDetails.json", uploaderDetailsStr);
        }
      

    }
}
