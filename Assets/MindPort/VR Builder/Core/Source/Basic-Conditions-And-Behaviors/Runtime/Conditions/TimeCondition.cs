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
    [DataContract(IsReference = true)]
    public class TimeCondition : Condition<TimeCondition.EntityData>
    {
        [DisplayName("Time Condition")]
        public class EntityData : IConditionData
        {
            [DataMember]
            public string TargetTimeString { get; set; }

            public bool IsCompleted { get; set; }

            public Metadata Metadata { get; set; }

            [DataMember]
            [HideInProcessInspector]
            public string Name { get; set; }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            public InGameTimeTracker timeTracker;

            public DateTime GetTargetTimeAsDateTime()
            {
                string[] timeParts = Data.TargetTimeString.Split(':');
                int hour = int.Parse(timeParts[0]);
                int minute = int.Parse(timeParts[1]);
                return new DateTime(1, 1, 1, hour, minute, 0);
            }

            public ActiveProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                GameObject rig = GameObject.FindGameObjectWithTag("XR Rig");
                DateTime targetTime = GetTargetTimeAsDateTime();

                if (rig == null)
                {
                    rig = GameObject.Find("XR Rig");
                }

                timeTracker = rig.GetComponent<InGameTimeTracker>();

                if (timeTracker.CurrentTime >= targetTime)
                {
                    Data.IsCompleted = true;
                }
                else
                {
                    Data.IsCompleted = false;
                }
            }

            protected override bool CheckIfCompleted()
            {
                DateTime targetTime = GetTargetTimeAsDateTime();
                if (timeTracker.CurrentTime >= targetTime)
                {
                    Data.IsCompleted = true;
                }
                else
                {
                    Data.IsCompleted = false;
                }
                return Data.IsCompleted;
            }

        }

        [JsonConstructor, Preserve]
        public TimeCondition()
        {
        }

        public override IStageProcess GetActiveProcess()
        {
            return new ActiveProcess(Data);
        }
    }
}
