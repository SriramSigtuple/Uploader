using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Models.Models
{
    [Serializable]
    public class CloudModel
    {
        public LoginModel LoginModel;

        public CreateAnalysisModel CreateAnalysisModel;

        public UploadModel UploadModel;

        public InitiateAnalysisModel InitiateAnalysisModel;

        public GetAnalysisModel GetAnalysisModel;

        public CloudModel()
        {

        }
    }
}
