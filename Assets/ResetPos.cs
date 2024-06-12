using UnityEngine;
using UnityEngine.UI;

public class ResetPos : MonoBehaviour
{
    public GameObject player; // Assign your player GameObject in the Inspector

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickResetPlayerPosition);
    }

    private void OnClickResetPlayerPosition()
    {
        player.transform.position = new Vector3(43.3549957f,3.56447172f,83.2865143f);
    }
}
