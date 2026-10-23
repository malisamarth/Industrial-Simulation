using TMPro;
using UnityEngine;

public class ObjectInfoDisplay : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI objectInfoDisplay;

    [SerializeField] private GameObject scrollInfoDisplay;


    public void SetInfoDisplay(string info) {
        objectInfoDisplay.text = info;
        ShowScrollInfo();
    }

    public void DisplayNothing() {
        objectInfoDisplay.text = string.Empty;
        HideScrollInfo();
    }

    private void ShowScrollInfo() {
        scrollInfoDisplay.SetActive(true);
    }

    private void HideScrollInfo() {
        scrollInfoDisplay.SetActive(false);
    }

}