using UnityEngine;

public class SkillPanelController : MonoBehaviour
{
    public GameObject skillPanel;
    public MonoBehaviour cameraMovementScript; // Reference to your camera movement script

    void Start()
    {
        // Ensure the panel is disabled at the start
        skillPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            bool isActive = !skillPanel.activeSelf;
            skillPanel.SetActive(isActive);

            if (isActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cameraMovementScript.enabled = false; // Disable camera movement
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cameraMovementScript.enabled = true; // Enable camera movement
            }
        }
    }
}