using System;
using UnityEngine;

[Serializable]
public class TimeSensitiveData
{
    public GameObject obj;
    public string startTime;
    public string endTime;
}

public class TimeSensitiveObject : MonoBehaviour
{
    [SerializeField] private TimeSensitiveData[] timeSensitiveObjects;

    private InGameTimeTracker inGameTimeTracker;

    private void Start()
    {
        inGameTimeTracker = FindObjectOfType<InGameTimeTracker>();
    }

    private void FixedUpdate()
    {
        UpdateObjectStatus();
    }

    private DateTime ParseTimeString(string timeString)
    {
        string[] timeParts = timeString.Split(':');
        int hour = int.Parse(timeParts[0]);
        int minute = int.Parse(timeParts[1]);
        return new DateTime(1, 1, 1, hour, minute, 0);
    }

    private void UpdateObjectStatus()
    {
        DateTime currentTime = inGameTimeTracker.CurrentTime;

        foreach (TimeSensitiveData timeSensitiveData in timeSensitiveObjects)
        {
            DateTime startTime = ParseTimeString(timeSensitiveData.startTime);
            DateTime endTime = ParseTimeString(timeSensitiveData.endTime);
            bool shouldBeActive = currentTime >= startTime && currentTime < endTime;
            if (timeSensitiveData.obj.activeSelf != shouldBeActive)
            {
                timeSensitiveData.obj.SetActive(shouldBeActive);
            }
        }
    }
}
