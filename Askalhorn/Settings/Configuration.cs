using System;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace Askalhorn.Settings
{
    public class Configuration
    {
        private const string FILENAME = "settings.json";

        public static Options Options { get; private set; }

        public delegate void OptionsChanged(Options options);

        public static event OptionsChanged OnOptionsChange;

        public static void SetDefaultOptions()
        {
            Options = new Options();
            Change();
            Save();
        }

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
                    Options = JsonConvert.DeserializeObject<Options>(reader.ReadToEnd());
                }
            }
            catch (Exception)
            {
                SetDefaultOptions();
            }
        }

        public static void Change()
        {
            OnOptionsChange?.Invoke(Options);
        }

        public static void Save()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(FILENAME, FileMode.Create, storage))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(JsonConvert.SerializeObject(Options));
            }
        }
    }
}