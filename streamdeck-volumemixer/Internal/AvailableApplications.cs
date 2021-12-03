using Newtonsoft.Json;

namespace streamdeck_volumemixer.Internal
{
    public class AudioApplication
    {
        public AudioApplication(string executablePath)
        {
            ExecutablePath = executablePath;
        }

        [JsonProperty(PropertyName = "ExecutablePath")]
        public string ExecutablePath { get; set; }

        [JsonProperty(PropertyName = "ApplicationName")]
        public string ApplicationName
        {
            get { return ExecutablePath.Substring(ExecutablePath.LastIndexOf('\\') + 1); }
            set
            {
                // Do nothing, but have a setter in case sdtools needs it
            }
        }
    }
}