using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;

namespace VRBuilder.Editor.UI.Conditions
{
    /// <inheritdoc />
    public class NotCompletedMenuItem : MenuItem<ICondition>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "Environment/Event Completed";

        /// <inheritdoc />
        public override ICondition GetNewItem()
        {
            return new NotCompletedCondition();
        }
    }
}