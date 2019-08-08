using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Models.Models
{
    [Serializable]
    public class GetAnalysisModel
    {
        /// <summary>
        /// Address to start the analysis in the cloud
        /// </summary>

        public URL_Model getAnalysisURL = new URL_Model
        {
            API_URL = UploaderModel.API_URL,
            API_URL_Start_Point = string.Empty,
            API_URL_Mid_Point = string.Empty,
            API_URL_End_Point = string.Empty
        };
        


    }
}
