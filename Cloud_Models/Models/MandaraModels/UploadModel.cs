using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Models.Models
{
    [Serializable]
  public class UploadModel
    {
        /// <summary>
        /// url is the address to upload image/s to the cloud.
        /// </summary>
        public URL_Model uploadURL = new URL_Model {
            API_URL = UploaderModel.API_URL,
            API_URL_Start_Point = "analyses/",
            API_URL_Mid_Point = "",
            API_URL_End_Point = "/input"
        };

        public string url = string.Empty;
        /// <summary>
        /// image is the binary data of the image to be uploaded to the cloud.
        /// </summary>
        public string[] images;

        /// <summary>
        /// slide_id is the MRN value which is same as the sample_id used during the create analysis.
        /// </summary>
        public string slide_id = string.Empty;

        /// <summary>
        /// This is a fixed value of folder.
        /// </summary>
        public string upload_type = "folder";

        /// <summary>
        /// relative path of the image which indicates whether its a left or right eye the value is for eg :slide_id/eyetype/filename
        /// </summary>
        public string relative_path = string.Empty;

        /// <summary>
        /// The checksum values of the image file to indicates which is not tampered with while uploading.
        /// </summary>
        public string[] checksums ;
        /// <summary>
        /// The analysis id is the value obtained from create analysis api response.
        /// </summary>
        public string analysis_id = string.Empty;

        public UploadModel()
        {
            uploadURL.API_URL_Mid_Point = analysis_id;
            
        }
    }
}
