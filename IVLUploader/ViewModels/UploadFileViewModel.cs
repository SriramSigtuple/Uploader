using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseViewModel;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace IVLUploader.ViewModels
{
  public class UploadFileViewModel : ViewBaseModel
    {
        FileUploadHelper fileUploader;// = FileUploadHelper.GetInstance();

        public FileUploadHelper FileUploader
        {
            get { return fileUploader; }
            set { fileUploader = value; }
        }

        public UploadFileViewModel()
      {
          FileUploader = FileUploadHelper.GetInstance();
          FileUploader.PropertyChanged += FileUploader_PropertyChanged;
            SetValue = new RelayCommand(param=> SetValueMethod(param));

      }

        private void FileUploader_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                if (e.PropertyName == "Progress")
                {
                    if (FileUploader.Progress != 100)
                        FileUploadStatus = FileUploader.Progress;
                }
                if (e.PropertyName == "IsServerRunning")
                {
                    if (!FileUploader.IsServerRunning)
                    {
                        FileUploadStatus = 0;
                        IsUpload = false;
                    }
                }
            });
        }

        public ICommand SetValue
      {
          get;
          set;
      }
      string fileName = string.Empty;

      public string FileName
      {
          get { return fileName; }

          set
             {
              fileName = value;
              OnPropertyChanged("FileName");
             }
      }

      int fileUploadStatus = 0;

      public int FileUploadStatus
      {
          get { return fileUploadStatus; }
          set {
              fileUploadStatus = value;
              OnPropertyChanged("FileUploadStatus");
              }
      }
      private bool isUpload = false;

public bool IsUpload
{
  get { return isUpload; }
  set { isUpload = value;
  OnPropertyChanged("IsUpload");
  if (isUpload)
  {
      System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(p =>
      {
          System.IO.FileStream fs = System.IO.File.OpenRead(this.FileName);
          Task<JToken> result = fileUploader.UploadFiles(fs, this.FileName);
          Dictionary<string, string> resultValuesDic = new Dictionary<string, string>();
          foreach (JProperty release in result.Result.Children())
              resultValuesDic.Add(release.Name, release.Value.ToString());
          if (resultValuesDic.ContainsKey("status"))
          {
              if (resultValuesDic["status"] == "200")
              {
                  if (resultValuesDic["message"].Contains(FileUploadHelper.fileUploadedSuccessfully))
                  {
                      fs.Close();
                      fs.Dispose();
                      FileUploadStatus = 100;
                  }
              }
          }
      }));
  }
  }
}
      public void SetValueMethod(object param)
      {
          this.FileUploadStatus = 100;
      }

    }
}
