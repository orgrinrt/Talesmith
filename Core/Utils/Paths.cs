using Godot;
using Path = System.IO.Path;

namespace Talesmith.Core.Utils
{
    public class Paths
    {
        // SYSTEM:;
        public static string UserPath = GetUserPath();

        public static string LogPath = Path.Combine(UserPath, "Logs");
        
        public static string DataPath = Path.Combine(UserPath, "Data");
        
        public static string SavePath = Path.Combine(UserPath, "Saves");
        
        public static string ProfilePath = Path.Combine(UserPath, "Profiles");
        
        // SAVES
        
        public static string CacheSavePath = Path.Combine(SavePath, "Cache");   // Currently running save as an open folder structure
                                                                                // After saving, Temp save is compressed into a zip and moved either in Saves/Local or Saves/Hosted
                                                                                // After saving, Temp still stays populated until the game is closed or the server is closed, when it automatically zips it and saves as an autosave

        public static string LocalSavePath = Path.Combine(SavePath, "Local");   // Local saves are grouped by Character name, i.e:
                                                                                //     Saves/Local/Elayne
                                                                                //         AutoSave01.save
                                                                                //         AutoSave02.save
                                                                                //         QuickSave26.save
                                                                                //     Saves/Local/Óppe
                                                                                //         Ebin_peli_hehe.save
                                                                                //         QuickSave69.save
                                                                                //         Quicksave98.save
        
        
        
        // WITHIN GODOT'S RES SYSTEM

        public static string ContentPath = "res://Content";
        
        public static string DefaultContentPath = "res://Content/Default";
        
        public static string DefaultLangContentPath = "res://Content/Lang";
        
        public static string DefaultMasterContentPath = "res://Content/Default/Master";

        public static string ModPath = "res://Mods";
        
        
        
        private static string GetUserPath()
        {
            /*
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return Path.Combine("~", ".local", "share", gameFolderTitle);
            }
            else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                return Path.Combine("~", "Library", "Application Support", gameFolderTitle);
            }
            else
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameFolderTitle);
            }
            */
            return OS.GetUserDataDir();
        }

        public static string Combine(string a, string b, bool isGodotResPath = true)
        {
            // for now only combines internal godot paths (because / is valid there, but in global paths OS may react differently to / )
            return a + "/" + b;
        }
    }
}