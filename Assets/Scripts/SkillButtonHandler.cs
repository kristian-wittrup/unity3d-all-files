using UnityEngine;
using UnityEngine.UI; // Add this line to include the UI namespace
using TMPro; // Import TextMeshPro namespace

public class SkillButtonHandler : MonoBehaviour
{
    public Button[] skillButtons; // Ensure Button type is recognized
    public TMP_Text skillPointsText; // Use TMP_Text for TextMeshPro

    public int totalSkillPoints = 250;
    private int currentSkillIndex = 0;
    private bool[] isSelected;// Track which buttons are selected
    private int[] skillCosts = { 2, 3, 4, 5 }; // Adjust costs as needed

    void Start()
    {
        isSelected = new bool[skillButtons.Length];
        UpdateSkillPointsText();
        UpdateButtonInteractivity();
    }

    // Left-click: select the next skill in sequence
    public void OnSkillButtonLeftClick(int skillIndex)
    {
        // Only proceed if this button is the next in sequence
        if (skillIndex != currentSkillIndex || isSelected[skillIndex]) return;
        if (totalSkillPoints < skillCosts[skillIndex]) return; // Not enough points

        // Deduct cost, highlight, move to next skill
        isSelected[skillIndex] = true;
        totalSkillPoints -= skillCosts[skillIndex];
        currentSkillIndex++;
        HighlightButtons();
        UpdateSkillPointsText();
        UpdateButtonInteractivity();
          
          // Log the updated totalSkillPoints
        Debug.Log($"Skrill {skillIndex + 1} selected. Remain skill points: {totalSkillPoints}");
 
    }

    // Right-click: deselect a previously selected skill - no worky yet
    public void OnSkillButtonRightClick(int skillIndex)
    {
        // Only allow deselection if button is already selected
        if (!isSelected[skillIndex]) return;

        // Add back cost, unhighlight, set currentSkillIndex if needed
        isSelected[skillIndex] = false;
        totalSkillPoints += skillCosts[skillIndex];

        // If we deselect the highest unlocked skill, move back the index
        if (skillIndex == currentSkillIndex - 1)
            currentSkillIndex--;

        HighlightButtons();
        UpdateSkillPointsText();
        UpdateButtonInteractivity();

        Debug.Log($"Skrill {skillIndex + 1} deselected. Remain skill points: {totalSkillPoints}");
   
    }

    // Highlight all selected buttons
    private void HighlightButtons()
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            var colors = skillButtons[i].colors;
            if (isSelected[i])
            {
                colors.normalColor = Color.green;
                colors.highlightedColor = Color.green;
                colors.pressedColor = Color.green;
                colors.selectedColor = Color.green;
            }
            else
            {
                colors.normalColor = Color.white;
                colors.highlightedColor = Color.white;
                colors.pressedColor = Color.white;
                colors.selectedColor = Color.white;
            }
            skillButtons[i].colors = colors;
        }
    }

    // Update displayed skill points
    private void UpdateSkillPointsText()
    {
        if (skillPointsText == null)
        {
            Debug.LogError("skillPointsText is not assigned!");
            return;
        }

        skillPointsText.text = $"{totalSkillPoints} / 250";
    }

    // Make only the next skill clickable
    private void UpdateButtonInteractivity()
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            // Already selected? Keep it interactable for right-click - i have not set this up correct in unity. It doesnt work as i want it to
            if (isSelected[i])
                skillButtons[i].interactable = true;
            else
                // Only the current skill index is selectable
                skillButtons[i].interactable = (i == currentSkillIndex);
        }
    }
}