﻿using IntuUploader.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntuUploader
{
 public static class GlobalMethods
    {
        

        //public static FileInfo[] GetActiveFileNames()
        //{
        //    FileInfo[] fileInfos = new DirectoryInfo(GlobalVariables.CloudPath + Path.DirectorySeparatorChar + GlobalVariables.activefileDirStr).GetFiles();
        //    return fileInfos;
        //}

        public static string GetDirPath(DirectoryEnum directoryEnum)
        {
            var dirName = string.Empty;
            switch(directoryEnum)
            {
                case DirectoryEnum.OutboxDir:
                    dirName = GlobalVariables.CloudPaths.outboxStr;
                    break;
                case DirectoryEnum.ActiveDir:
                    dirName = GlobalVariables.CloudPaths.activefileDirStr;
                    break;
                case DirectoryEnum.SentItemsDir:
                    dirName = GlobalVariables.CloudPaths.sentItemsStr;
                    break;
                case DirectoryEnum.ProcessedDir:
                    dirName = GlobalVariables.CloudPaths.ProcessedStr;
                    break;
                case DirectoryEnum.InboxDir:
                    dirName = GlobalVariables.CloudPaths.inboxStr;
                    break;
                case DirectoryEnum.ReadDir:
                    dirName = GlobalVariables.CloudPaths.ReadStr;
                    break;

            }
            return Path.Combine(GlobalVariables.CloudPaths.CloudPath, dirName);

        }

    }
}
