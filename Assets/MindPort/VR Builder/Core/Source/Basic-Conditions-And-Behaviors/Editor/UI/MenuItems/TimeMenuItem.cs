using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;

namespace VRBuilder.Editor.UI.Conditions
{
    public class TimeMenuItem : MenuItem<ICondition>
    {
        public override string DisplayedName { get; } = "Timing/Time Passed";

        public override ICondition GetNewItem()
        {
            return new TimeCondition();
        }
    }
}
