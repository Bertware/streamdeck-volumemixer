using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using BarRaider.SdTools;
using Newtonsoft.Json.Linq;
using streamdeck_volumemixer.Internal;

namespace streamdeck_volumemixer.Actions
{
    public abstract class DeltaVolumeAction : PluginBase
    {
        public int VolumeChangeStepSize { get; set; }
        public int VolumeChangeInterval { get; set; } = 50;

        private ISimpleAudioVolume VolumeHandle { get; set; }

        // Create this constructor in your plugin and pass the objects to the PluginBase class
        public DeltaVolumeAction(ISDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            if (payload.Settings == null || payload.Settings.Count == 0)
            {
                settings = VolumeActionSettings.CreateDefaultSettings();
                initSettings();
            }
            else
            {
                settings = payload.Settings.ToObject<VolumeActionSettings>();
            }

            // Other relevant settings in the payload include the actual position of the plugin on the Stream Deck

            Connection.StreamDeckConnection.OnPropertyInspectorDidAppear +=
                StreamDeckConnection_OnPropertyInspectorAppeared;
        }

        #region StreamDeck-Tools listeners

        public override void KeyPressed(KeyPayload payload)
        {
            Console.WriteLine("Changing volume for " + settings.SelectedApplicationExecutablePath);
            VolumeHandle = WindowsCoreAudioWrapper.GetVolumeObject(settings.SelectedApplicationExecutablePath);
            keyDownTimer = new Timer(VolumeChangeInterval);
            keyDownTimer.Elapsed += OnKeyHeldDown;
            keyDownTimer.Start();
        }

        public override void KeyReleased(KeyPayload payload)
        {
            keyDownTimer.Stop();
            keyDownTimer.Dispose();
            VolumeHandle = null;
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            Tools.AutoPopulateSettings(settings, payload.Settings);
            SaveSettings();
        }


        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload)
        {
        }

        public override void OnTick()
        {
            // Called every second for UI updates
        }

        public override void Dispose()
        {
            Connection.StreamDeckConnection.OnPropertyInspectorDidAppear -=
                StreamDeckConnection_OnPropertyInspectorAppeared;
        }

        #endregion

        #region Private Members

        private readonly VolumeActionSettings settings;

        private Timer keyDownTimer;

        private void OnKeyHeldDown(object source, ElapsedEventArgs e)
        {
            WindowsCoreAudioWrapper.SetApplicationVolumeDelta(VolumeHandle, VolumeChangeStepSize);
        }

        private void StreamDeckConnection_OnPropertyInspectorAppeared(object sender,
            streamdeck_client_csharp.StreamDeckEventReceivedEventArgs<
                streamdeck_client_csharp.Events.PropertyInspectorDidAppearEvent> e)
        {
            initSettings();
            SaveSettings();
        }

        private void initSettings()
        {
            settings.AllAudioApplications =
            (
                from executablePath in WindowsCoreAudioWrapper.EnumerateApplications()
                select new AudioApplication(executablePath)
            ).ToList();
            if (string.IsNullOrEmpty(settings.SelectedApplicationExecutablePath))
            {
                settings.SelectedApplicationExecutablePath = settings.AllAudioApplications[0].ExecutablePath;
            }
        }

        private void SaveSettings()
        {
            Connection.SetSettingsAsync(JObject.FromObject(settings));
        }

        #endregion
    }
}