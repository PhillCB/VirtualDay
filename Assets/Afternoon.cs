using UnityEngine;
using UnityEngine.UI;
using System;

public class Afternoon : MonoBehaviour
{
    private InGameTimeTracker inGameTimeTracker;

    private void Start()
    {
        inGameTimeTracker = FindObjectOfType<InGameTimeTracker>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAfternoon);
    }

    private void OnClickAfternoon()
    {
        if (inGameTimeTracker != null)
        {
            inGameTimeTracker.CurrentTime = new DateTime(1, 1, 1, 12, 0, 0);
        }
        else
        {
            Debug.LogError("InGameTimeTracker component not found.");
        }
    }
}
