using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using UnityEngine.Scripting;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using UnityEngine;
using Vday;

// This behavior takes the name of the event it is attached to and uses it to switch the 
// bool with the same name in the Tracker script to true. This is used to keep track of
// which tasks the player has completed.
// The behavior is executed after the step it's attached to. 

namespace VRBuilder.Core.Behaviors {

    [DataContract(IsReference = true)]
    public class TrackEventBehavior : Behavior<TrackEventBehavior.EntityData> {

        /// <summary>
        /// The "track event" behavior's data.
        /// </summary>
        [DisplayName("Track Event")]
        [DataContract(IsReference = true)]
        public class EntityData : IBehaviorData {
            /// <summary>
            /// The name of the event to track.
            /// </summary>
            [DataMember]
            [DisplayName("Event Name")]
            public string EventName { get; set; }
            public Metadata Metadata { get; set; }
            public string Name { get; set; }

            [DataMember]
            public BehaviorExecutionStages ExecutionStages { get; set; }
        }

        [JsonConstructor, Preserve]
        public TrackEventBehavior() : this("Event") { }

        public TrackEventBehavior(string eventName) {
            Data.EventName = eventName;
        }

        private class ActivatingProcess : InstantProcess<EntityData>
        {
            public ActivatingProcess(EntityData data) : base(data)
            {
            }

            /// <inheritdoc />
            public override void Start()
            {
            }
        }

        private class DeactivatingProcess : InstantProcess<EntityData> {
            
            public DeactivatingProcess(EntityData data) : base(data) { }

            public Tracker tracker;

            /// <inheritdoc />
            public override void Start()
            {
                // Find game object with the Tracker script using findobjectswithtag
                GameObject rig = GameObject.FindGameObjectWithTag("XR Rig");

                // filling in missing references
                if (rig == null)
                {
                    rig = GameObject.Find("XR Rig");
                }

                // Get Tracker and InGameTimeTracker scripts on the game object
                Tracker tracker = rig.GetComponent<Tracker>();
                InGameTimeTracker inGameTimeTracker = rig.GetComponent<InGameTimeTracker>();

                // Get current in-game time
                DateTime inGameTime = inGameTimeTracker.CurrentTime;

                // Set the bool with the same name as the event to true, pass in-game time to CompleteTask
                tracker.CompleteTask(Data.EventName, inGameTime);

                // Log to console that the event has been tracked
                Debug.Log("Event " + Data.EventName + " has been tracked at " + inGameTime.ToString("HH:mm") + " in-game time.");
            }


            /// <inheritdoc />
            public override void End() {
                
            }

        }
        


        /// <inheritdoc />
        public override IStageProcess GetDeactivatingProcess()
        {
            return new DeactivatingProcess(Data);
        }
    
    }
    
}
