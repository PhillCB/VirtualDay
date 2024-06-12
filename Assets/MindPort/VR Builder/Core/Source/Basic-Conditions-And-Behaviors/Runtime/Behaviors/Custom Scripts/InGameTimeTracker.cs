using UnityEngine;
using System;
using Vday;

// This script keeps track of in-game time based on the number of completed tasks.
// Each task moves the time forward by half an hour, starting at 7am.
// The script is attached to the player.
public class InGameTimeTracker : MonoBehaviour
{
    public DateTime CurrentTime { get; set; }
    private int completedTaskCount;
    private Tracker Tracker;

    private bool pastLunch = false;

    private void Start()
    {
        CurrentTime = new DateTime(1, 1, 1, 7, 30, 0); // Start time: 7:30am
        completedTaskCount = 0;
        Tracker = GetComponent<Tracker>();
    }

    private void Update()
    {
        int currentCompletedTaskCount = GetNumberOfCompletedTasks();

        if (currentCompletedTaskCount > completedTaskCount)
        {
            completedTaskCount = currentCompletedTaskCount;
            UpdateInGameTime(currentCompletedTaskCount);
        }

        Debug.Log(currentCompletedTaskCount);
    }

    private int GetNumberOfCompletedTasks()
    {
        int completedTasks = 0;

        foreach (var task in Tracker.completedTasks)
        {
            if (task.Value.IsComplete)
            {
                completedTasks++;
            }
        }

        return completedTasks;
    }

    private void UpdateInGameTime(int tc)
    {
        // Check if "Burger" task is completed and it's not past lunch
        if (tc <= 6 && Tracker.completedTasks.TryGetValue("Burger", out Tracker.TaskData burgerTask) && burgerTask.IsComplete && !pastLunch)
        {
            CurrentTime = CurrentTime.AddMinutes(90); // add 90 minutes
            pastLunch = true; // Set this to true after handling the "Burger" task
            Debug.Log("Burger task completed! In-game time: " + CurrentTime.ToString("HH:mm"));
        }
        
        else if (tc <= 6 && Tracker.completedTasks.TryGetValue("Hospital", out Tracker.TaskData hospitalTask) && hospitalTask.IsComplete)
        {
            CurrentTime = CurrentTime.AddMinutes(90); // add 90 minutes
            Debug.Log("Hospial task completed! In-game time: " + CurrentTime.ToString("HH:mm"));

        }

        else if (tc <= 6 && Tracker.completedTasks.TryGetValue("Friend", out Tracker.TaskData friendTask) && friendTask.IsComplete)
        {
            CurrentTime = CurrentTime.AddMinutes(90); // add 90 minutes
            Debug.Log("Friend task completed! In-game time: " + CurrentTime.ToString("HH:mm"));

        }

        else
        {
            CurrentTime = CurrentTime.AddMinutes(30); // add 30 minutes
            Debug.Log("In-game time: " + CurrentTime.ToString("HH:mm"));
        }
    }
}
