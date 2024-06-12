using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using UnityEngine;

namespace Vday {
    public class Tracker : MonoBehaviour 
    {
        // Struct to hold task data
        public struct TaskData {
            public bool IsComplete { get; set; }
            public DateTime RealWorldTimeCompleted { get; set; }
            public TimeSpan InGameTimeCompleted { get; set; }
        }

        // Dictionary to store completed tasks
        public Dictionary<string, TaskData> completedTasks = new Dictionary<string, TaskData>();

        // For tracking the start of the game session
        private DateTime sessionStartTime;

        // Initialize Dictionary values
        private void Start() {
            sessionStartTime = DateTime.Now;

            string[] taskNames = { "Burger", "Park", "Cake", "Milk and Cereals", "Inhaler1", "Inhaler2", "Garbage", "Laundry", "Parcel", 
                                    "Salmon", "Teeth", "Water Plants1", "Water Plants2", "Sweep Floor", "Set Table","Buy Tickets", "Lightbulb", "Laundry Pickup", "Hospital", "Coffee"};
            foreach (string task in taskNames) {
                completedTasks.Add(task, new TaskData { IsComplete = false});
            }
        }

        // Mark the task as completed
        public void CompleteTask(string taskName, DateTime inGameTime) {
            TaskData data = new TaskData {
                IsComplete = true,
                RealWorldTimeCompleted = DateTime.Now,
                InGameTimeCompleted = inGameTime.TimeOfDay
            };
            completedTasks[taskName] = data;
        }
        
        public bool IsTaskCompleted(string taskName)
        {
            if (completedTasks.TryGetValue(taskName, out TaskData taskData))
            {
                return taskData.IsComplete;
            }
            return false;
        }

        // Save completed tasks to a csv file when the application is closed
        private void OnApplicationQuit() {
            string directoryPath = Application.dataPath + "/Records";
            string fileName = "Tracker_" + sessionStartTime.ToString("yyyyMMdd_HHmmss") + ".csv";
            string filePath = Path.Combine(directoryPath, fileName);

            try {
                // Create directory if it does not exist
                if (!Directory.Exists(directoryPath)) {
                    Directory.CreateDirectory(directoryPath);
                }

                // Create and open the file
                using (StreamWriter writer = new StreamWriter(filePath)) {
                    // Write headers
                    writer.WriteLine("Task,Completed,RealWorldTimeCompleted,InGameTimeCompleted");

                    // Write data for each task
                    foreach (KeyValuePair<string, TaskData> entry in completedTasks) {
                        writer.WriteLine("{0},{1},{2},{3}", 
                            entry.Key, 
                            entry.Value.IsComplete, 
                            entry.Value.RealWorldTimeCompleted.ToString("O"), // ISO 8601 datetime format
                            entry.Value.InGameTimeCompleted.ToString("c")); // constant (invariant) time format
                    }

                    // Write session end timestamp
                    writer.WriteLine("\nSession End: {0}", DateTime.Now.ToString("O"));
                }
            } catch (IOException e) {
                Debug.LogError("Error writing to file: " + e.Message);
            }
        }
    } 
}
