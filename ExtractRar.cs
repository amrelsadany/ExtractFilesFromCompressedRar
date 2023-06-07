using Aspose.Zip.Rar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractFilesFromCompressedRar
{
    public static class ExtractRar
    {
        public static void ExtractFiles()
        {
            var extractpath = ConfigurationManager.AppSettings["ExtractFolder"];
            if (!Directory.Exists(extractpath))
            {
                Directory.CreateDirectory(extractpath);
            }
            var passwords = ConfigurationManager.AppSettings["Passwords"].Split(',');



            DirectoryInfo dir = new DirectoryInfo(ConfigurationManager.AppSettings["RarFolder"]);

            FileInfo[] rarFiles = dir.GetFiles("*.rar");
            foreach (FileInfo rarFile in rarFiles)
            {
                int i = 0;
                bool extracted = false;
                var foldername = extractpath + Path.GetFileNameWithoutExtension(rarFile.FullName);

                #region Create Extracted Folder
                if (!Directory.Exists(foldername))
                {
                    Directory.CreateDirectory(foldername);
                }
                else
                {
                    var folderCreated = false;
                    int foldernumber = 0;
                    while (!folderCreated)
                    {
                        var tempFolderName = foldername + foldernumber;
                        if (Directory.Exists(tempFolderName))
                        {
                            foldernumber++;
                            continue;
                        }
                        else
                        {
                            Directory.CreateDirectory(tempFolderName);
                            foldername = tempFolderName;
                            folderCreated = true;
                        }
                    }
                }

                #endregion

                while (!extracted)
                {
                    try
                    {
                        RarArchive archive = new RarArchive(rarFile.FullName);

                        // Unrar or extract password protected files from the archive
                        // Specify password as String at second argument of method




                        archive.ExtractToDirectory(foldername, passwords[i]);
                        extracted = true;
                    }
                    catch (Exception ex)
                    {
                        // Try another password
                        i++;
                        continue;
                    }
                }
            }
        }
    }
}
