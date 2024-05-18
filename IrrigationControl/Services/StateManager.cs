using nanoFramework.Json;
using System.IO;
using System.Collections;

namespace IrrigationControl.Services
{
    public class StateManager
    {
        private static StateManager _instance;
        private static readonly object _lock = new object();
        private const string APP_STATE_FILE = "I:\\appState.json";

        private StateManager()
        {
            Init();
        }

        private void Init()
        {
            if (!File.Exists(APP_STATE_FILE))
            {                
                File.WriteAllText(APP_STATE_FILE, "{}");
            }
        }

        public static StateManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new StateManager();
                    }
                }
            }
            return _instance;
        }

        public void SetState(string key, string value)
        {
            using var fileStream = new FileStream(APP_STATE_FILE, FileMode.Open);
            var stored = (Hashtable)JsonConvert.DeserializeObject(fileStream, typeof(Hashtable)) ?? new Hashtable();

            if (stored.Contains(key))
            {
                stored[key] = value;
            }
            else
            {
                stored.Add(key, value);
            }
            
            var json = JsonSerializer.SerializeObject(stored);
            File.WriteAllText(APP_STATE_FILE, json);          
        }

        public string GetState(string key)
        {
            using var fileStream = new FileStream(APP_STATE_FILE, FileMode.Open);
            var stored = (Hashtable) JsonConvert.DeserializeObject(fileStream, typeof(Hashtable)) ?? new Hashtable();

            if (stored.Contains(key))
            {
                return (string) stored[key];
            }

            return string.Empty;
        }
    }
}
