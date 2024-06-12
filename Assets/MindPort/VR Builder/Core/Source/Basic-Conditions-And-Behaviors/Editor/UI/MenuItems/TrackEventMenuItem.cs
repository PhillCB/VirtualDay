using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

// This script makes the Track Event behavior appear in the behavior menu and makes it so that 
// when the behavior is added to a step, the TrackEventBehavior script is added to the step.

namespace VRBuilder.Editor.UI.Behaviors
{
    public class TrackEventMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "Guidance/Track Event";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new TrackEventBehavior();
        }
    }
}