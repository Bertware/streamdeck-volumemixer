using BarRaider.SdTools;

namespace streamdeck_volumemixer.Actions
{
    [PluginActionId("be.bertmarcelis.streamdeck.volumemixer.application.increase")]
    public class IncreaseVolumeAction : DeltaVolumeAction
    {
        public IncreaseVolumeAction(ISDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            VolumeChangeStepSize = 2;
        }
    }
}