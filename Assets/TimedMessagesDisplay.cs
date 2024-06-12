using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class TimedMessage
{
    public string time;
    public string message;
}

public class TimedMessagesDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageDisplay;
    private InGameTimeTracker inGameTimeTracker;

    [SerializeField] public List<TimedMessage> timedMessages;
    private DateTime lastDisplayedTime;

    private void Start()
    {
        inGameTimeTracker = FindObjectOfType<InGameTimeTracker>();
        messageDisplay = GetComponent<TextMeshProUGUI>();
        lastDisplayedTime = DateTime.MinValue;
    }

    private void Update()
    {
        DisplayTimedMessages();
    }

    private DateTime ParseTimeString(string timeString)
    {
        string[] timeParts = timeString.Split(':');
        int hour = int.Parse(timeParts[0]);
        int minute = int.Parse(timeParts[1]);
        return new DateTime(1, 1, 1, hour, minute, 0);
    }

    private void DisplayTimedMessages()
    {
        DateTime currentTime = inGameTimeTracker.CurrentTime;

        foreach (var timedMessage in timedMessages)
        {
            DateTime messageTime = ParseTimeString(timedMessage.time);
            if (currentTime >= messageTime && lastDisplayedTime < messageTime)
            {
                DisplayMessage(timedMessage.message);
                lastDisplayedTime = messageTime;
            }
        }
    }

    private void DisplayMessage(string message)
    {
        messageDisplay.text = message;
    }

    // This function can be called by a button to clear the displayed message.
    public void ClearMessage()
    {
        messageDisplay.text = "";
    }
}
