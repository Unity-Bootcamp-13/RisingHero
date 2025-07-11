using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
    [SerializeField] private Button targetButton;
    [SerializeField] private GameObject targetPanel;

    private void Start()
    {
        targetButton.onClick.AddListener(() =>
        {
            if (targetPanel != null)
                targetPanel.SetActive(true);
        });
    }
}
