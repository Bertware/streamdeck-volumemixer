using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace streamdeck_volumemixer.Internal
{
    public class VolumeActionSettings
    {
        public static VolumeActionSettings CreateDefaultSettings()
        {
            VolumeActionSettings instance = new VolumeActionSettings
            {
                SelectedApplicationExecutablePath = String.Empty,
                AllAudioApplications = null 
            };
            return instance;
        }

        
        [JsonProperty(PropertyName = "AllAudioApplications")]
        public List<AudioApplication> AllAudioApplications { get; set; }

        
        [JsonProperty(PropertyName = "SelectedApplicationExecutablePath")]
        public string SelectedApplicationExecutablePath { get; set; }
    }
}