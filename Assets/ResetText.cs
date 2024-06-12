using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResetText : MonoBehaviour
{
    // We'll keep a reference to the button component and the message display
    private Button clearButton;
    private TimedMessagesDisplay messageDisplay;

    private void Start()
    {
        // Get the button component on this GameObject
        clearButton = GetComponent<Button>();
        
        // Use the tag to find the message display
        GameObject messageDisplayObject = GameObject.FindGameObjectWithTag("Messages");
        messageDisplay = messageDisplayObject.GetComponent<TimedMessagesDisplay>();

        // Set up the button's click event to clear the message
        clearButton.onClick.AddListener(messageDisplay.ClearMessage);
    }

}