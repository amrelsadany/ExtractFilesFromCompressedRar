using Aspose.Zip;
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
    public static class ExtractZip
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

            FileInfo[] rarFiles = dir.GetFiles("*.zip");
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

                        // Open ZIP file
                        using (FileStream zipFile = File.Open(rarFile.FullName, FileMode.Open))
                        {
                            // Decrypt using password
                            using (var archive = new Archive(zipFile, new ArchiveLoadOptions() { DecryptionPassword = passwords[i] }))
                            {
                                // Extract files to folder
                                archive.ExtractToDirectory(foldername);
                                extracted = true;
                            }
                        }
                       
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
