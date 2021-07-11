using System;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace Askalhorn.Settings
{
    public class Configuration
    {
        private const string FILENAME = "settings.json";

        public static Settings Settings { get; private set; }

        public static void Load()
        {
            try
            {
                // GetUserStoreForApplication doesn't work - can't identify
                // application unless published by ClickOnce or Silverlight
                IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
                using (IsolatedStorageFileStream stream =
                    new IsolatedStorageFileStream(FILENAME, FileMode.Open, storage))
                using (StreamReader reader = new StreamReader(stream))
                {
                    Settings = JsonConvert.DeserializeObject<Settings>(reader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                Settings = new Settings();
            }
        }

        public static void Save()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(FILENAME, FileMode.Create, storage))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(JsonConvert.SerializeObject(Settings));
            }
        }
    }
}