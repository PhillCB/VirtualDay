using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.SceneObjects;
using System;
using VRBuilder.Core.Utils;
using VRBuilder.Core.Properties;
using VRBuilder.Core.Validation;
using VRBuilder.Core.ProcessUtils;
using Newtonsoft.Json;
using UnityEngine.Scripting;
using VRBuilder.Core.Behaviors;
using UnityEngine;
using Vday;

namespace VRBuilder.Core.Conditions
{
    /// <summary>
    /// A condition that checks if an event has not been completed yet, by looking at the tracker dictionary.
    /// </summary>
    [DataContract(IsReference = true)]
    public class NotCompletedCondition : Condition<NotCompletedCondition.EntityData>
    {        
        /// <summary>
        /// The data for a not completed condition.
        /// </summary>
        [DisplayName("Not Completed")]
        public class EntityData : IConditionData
        {
            [DataMember]
            public string EventName { get; set; }

            /// <inheritdoc />
            public bool IsCompleted { get; set; }

            /// <inheritdoc />
            public Metadata Metadata { get; set; }

            /// <inheritdoc />
            [DataMember]
            [HideInProcessInspector]
            public string Name { get; set; }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            public Tracker tracker;

            public ActiveProcess(EntityData data) : base(data)
            {
            }

            // Check the tracker dictionary to see if the event has been completed.
            public override void Start() {
                // Find game object with the Tracker script using findobjectswithtag
                GameObject rig = GameObject.FindGameObjectWithTag("XR Rig");

                // filling in missing references
                if (rig == null) {
                    rig = GameObject.Find("XR Rig");
                }

                // Set tracker to the Tracker script on the game object
                tracker = rig.GetComponent<Tracker>();

                if (tracker.IsTaskCompleted(Data.EventName) == true) {
                    Data.IsCompleted = false;
                }
                else {
                    Data.IsCompleted = true;
                }
            }

            /// <inheritdoc />
            protected override bool CheckIfCompleted()
            {
                return Data.IsCompleted;
            }
        }

        [JsonConstructor, Preserve]
        public NotCompletedCondition()
        {
        }        


        /// <inheritdoc />
        public override IStageProcess GetActiveProcess()
        {
            return new ActiveProcess(Data);
        }
    }
}
