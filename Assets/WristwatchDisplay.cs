using TMPro;
using UnityEngine;
using System;

public class WristwatchDisplay : MonoBehaviour {
    [SerializeField] private TextMeshPro timeDisplay;
    private InGameTimeTracker inGameTimeTracker;

    private void Start() {
        inGameTimeTracker = FindObjectOfType<InGameTimeTracker>();
    }

    private void Update() {
        UpdateTimeDisplay();
    }

    private void UpdateTimeDisplay() {
        DateTime currentTime = inGameTimeTracker.CurrentTime;
        timeDisplay.text = currentTime.ToString("HH:mm");
    }
}
