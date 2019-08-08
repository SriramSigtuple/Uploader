using System;

namespace Cloud_Models.Models
{
    [Serializable]
    public class LoginModel
    {
        /// <summary>
        /// The URL has the value of the login url to the cloud.
        /// </summary>

        public URL_Model login_URL = new URL_Model
        {
            API_URL = UploaderModel.API_URL,
            API_URL_Start_Point = "",
            API_URL_Mid_Point = "",
            API_URL_End_Point = "auth/signin"
        };
        /// <summary>
        /// The username is the value which is the public key to login to the cloud.
        /// </summary>
        public string username = string.Empty;
        /// <summary>
        /// password is the value which is the private key to login to the cloud.
        /// </summary>
        public string password = string.Empty;
        /// <summary>
        /// device_id is the value from which the images have been captured and uploaded to the cloud.
        /// </summary>
        public string device_id = string.Empty;

        /// <summary>
        /// cookie is the value used for all the APIs
        /// </summary>
        public string cookie = string.Empty;

    }
}
