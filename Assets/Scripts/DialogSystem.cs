using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public Button nextButton;
    public Button finishButton;

    [Header("Highlight Targets")]
    public GameObject healthBar;
    public GameObject collectible;
    public GameObject exitDoor;

    public GameObject healthBarHighlight;
    public GameObject collectibleHighlight;
    public GameObject exitDoorHighlight;

    [Header("Dialog Content")]
    [TextArea(2, 5)]
    public string[] dialogLines = new string[]
    {
        "This is your Health Bar. If it reaches zero, you're dead. Stay alert.",
        "Collect glowing orbs to unlock secret paths. They may save your life.",
        "Find the Exit Door while avoiding enemies. You cannot fight — only hide."
    };

    private int currentLine = 0;

    void Start()
    {
        // Initial setup
        dialogPanel.SetActive(true);
        dialogText.text = dialogLines[0];
        nextButton.gameObject.SetActive(true);
        finishButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(NextLine);
        finishButton.onClick.AddListener(CloseDialog);

        UpdateHighlights();
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLine];
            UpdateHighlights();

            if (currentLine == dialogLines.Length - 1)
            {
                nextButton.gameObject.SetActive(false);
                finishButton.gameObject.SetActive(true);
            }
        }
    }

    void CloseDialog()
    {
        SceneManager.LoadScene("Level1");
    }

    void UpdateHighlights()
    {
        DisableAllHighlights();

        switch (currentLine)
        {
            case 0:
                healthBar.SetActive(true);
                healthBarHighlight.SetActive(true);
                break;
            case 1:
                collectible.SetActive(true);
                collectibleHighlight.SetActive(true);
                break;
            case 2:
                exitDoor.SetActive(true);
                exitDoorHighlight.SetActive(true);
                break;
        }
    }

    void DisableAllHighlights()
    {
        healthBar.SetActive(false);
        healthBarHighlight.SetActive(false);
        collectible.SetActive(false);
        collectibleHighlight.SetActive(false);
        exitDoor.SetActive(false);
        exitDoorHighlight.SetActive(false);
    }
}
