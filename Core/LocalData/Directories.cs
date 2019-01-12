using System.IO;
using Talesmith.Core.Utils;

namespace Talesmith.Core.LocalData
{
    public class Directories
    {
        public void Initialize()
        {
            // ------ SYSTEM -------------------------------------------------------------------------------
            // TOP LEVEL
            DirectoryInfo logs = Directory.CreateDirectory(Paths.LogPath);
            DirectoryInfo data = Directory.CreateDirectory(Paths.DataPath);
            DirectoryInfo saves = Directory.CreateDirectory(Paths.SavePath);
            DirectoryInfo profiles = Directory.CreateDirectory(Paths.ProfilePath);
        
            // SAVES
            DirectoryInfo cacheSave = Directory.CreateDirectory(Paths.CacheSavePath);
            DirectoryInfo localSaves = Directory.CreateDirectory(Paths.LocalSavePath);
                
            // ------ GODOT --------------------------------------------------------------------------------
            Godot.Directory dir = new Godot.Directory();
            
            //CONTENT
            if (!dir.DirExists(Paths.DefaultLangContentPath)) dir.MakeDir(Paths.DefaultLangContentPath);
            if (!dir.DirExists(Paths.DefaultContentPath)) dir.MakeDir(Paths.DefaultContentPath);


        }

        public DirectoryInfo GetSystemDirectory(SystemDirectory toGet)
        {
            switch (toGet)
            {
                case SystemDirectory.Logs:
                    return new DirectoryInfo(Paths.LogPath);
                case SystemDirectory.Data:
                    return new DirectoryInfo(Paths.DataPath);
                case SystemDirectory.Saves:
                    return new DirectoryInfo(Paths.SavePath);
                case SystemDirectory.Profiles:
                    return new DirectoryInfo(Paths.ProfilePath);
                    
                case SystemDirectory.CacheSave:
                    return new DirectoryInfo(Paths.CacheSavePath);
                case SystemDirectory.LocalSaves:
                    return new DirectoryInfo(Paths.LocalSavePath);
            }

            Debug.Log.Error("Directories.GetDirectory()", "No directory found with enum " + toGet + " - returning null");
            return null;
        }

        public enum SystemDirectory
        {
            Logs,
            Data,
            Saves,
            Profiles,
            
            CacheSave,
            LocalSaves
        }
    }
}