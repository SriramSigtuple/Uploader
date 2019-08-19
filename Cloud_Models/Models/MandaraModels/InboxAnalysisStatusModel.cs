﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Models.Models
{
    [Serializable]
   public class InboxAnalysisStatusModel
    {
        public List<ImageAnalysisResultModel> RightEyeDetails;
        public List<ImageAnalysisResultModel> LeftEyeDetails;

        public string Status;

        public string RightAIImpressions;
        public string LeftAIImpressions;

        public int visitID;
        public int patientID;
        public int reportID;
        public int cloudID;
        public InboxAnalysisStatusModel()
        {
            RightEyeDetails = new List<ImageAnalysisResultModel>();
            LeftEyeDetails = new List<ImageAnalysisResultModel>();
        }


    }
}
