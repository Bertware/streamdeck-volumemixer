using BarRaider.SdTools;

namespace streamdeck_volumemixer.Actions
{
    [PluginActionId("be.bertmarcelis.streamdeck.volumemixer.application.decrease")]
    public class DecreaseVolumeAction : DeltaVolumeAction
    {
        public DecreaseVolumeAction(ISDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            VolumeChangeStepSize = -2;
        }
    }
}