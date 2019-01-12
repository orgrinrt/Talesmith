using System;
using Godot;
using Godot.Collections;

namespace Talesmith.Core.Utils
{
    public class Files
    {
        public static bool TryGetSqlFilesInDirectory(string dirPath, out Array<string> filePaths, bool recursive = true)
        {
            Array<string> resultFilePaths = new Array<string>();

            using (Directory dir = new Directory())
            {
                if (dir.ChangeDir(dirPath) != Error.Ok)
                {
                    Debug.Log.Error("Files.TryGetSqlFilesInDirectory()", "The directory (" + dirPath + ") didn't exist", true);
                    // error switch here
                    filePaths = null;
                    return false;
                }
                
                dir.ListDirBegin(true, true);
                
                using (File file = new File())
                {
                    string currentItemName = dir.GetNext();
                
                    while (currentItemName != String.Empty)
                    {
                        string currentItemPath = Paths.Combine(dirPath, currentItemName);
                        
                        if (dir.CurrentIsDir())
                        {
                            if (recursive)
                            {
                                Array<string> recursiveResult = new Array<string>();
                                string currFolder = Paths.Combine(dirPath, currentItemName);
        
                                if (Debug.LoggingLevel > LoggingLevel.Default)
                                {
                                    Debug.Log.Print("Files.TryGetSqlFilesInDirectory()", "Directory (" + dirPath + ") contained a child directory (" + currFolder + "), loading it too (recursive=true)", true);
                                }
                                
                                if (TryGetSqlFilesInDirectory(currFolder, out recursiveResult, true))
                                {
                                    foreach (string path in recursiveResult)
                                    {
                                        resultFilePaths.Add(path);
                                    }
                                }
                                else
                                {
                                    Debug.Log.Error("Files.TryGetSqlFilesInDirectory()", "Tried to do a recursive search on sql files but there were no sql files", true);
                                }
                            }
                        }
                        else if (file.FileExists(currentItemPath))
                        {
                            if (currentItemPath.EndsWith(".sql") || currentItemPath.EndsWith(".sql.tres")) // .sql.tres is legacy but it's there just in case we change our mind about using them as gd resources
                            {
                                resultFilePaths.Add(currentItemPath);
                            }
                            else if (Debug.LoggingLevel > LoggingLevel.Default)
                            {
                                Debug.Log.Print("Files.TryGetSqlFilesInDirectory()", "Found a file (" + currentItemPath + "), but it wasn't an .sql file, not including it", true);
                            }
                        }
                        else
                        {
                            Debug.Log.Database("Files.GetFilesInDirectory()", "Tried to read a file (" + currentItemPath + ") but it didn't exist (this can cause unexpected crashes)", true);
                        }
                        currentItemName = dir.GetNext();
                    }
                }
            
            }
            
            if (resultFilePaths.Count > 0)
            {
                filePaths = resultFilePaths;
                
                if (Debug.LoggingLevel > LoggingLevel.Default)
                {
                    Debug.Log.Print("Files.TryGetSqlFilesInDirectory()", "Total amount of .sql files found&processed: " + filePaths.Count + "(" + dirPath + ")", true);
                }
                
                return true;
            }
            else
            {
                filePaths = null;
                return false;
            }
        }
    }
}