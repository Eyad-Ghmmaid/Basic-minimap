using UnityEngine;
using UnityEngine.UI;

public class BigMapUIController : MonoBehaviour
{
    [SerializeField] private bigMapManager mapManager;
    [SerializeField] private Button centerOnPlayerButton;

    void Start()
    {
        if (centerOnPlayerButton != null && mapManager != null)
        {
            centerOnPlayerButton.onClick.AddListener(OnCenterButtonClicked);
        }
    }

    private void OnCenterButtonClicked()
    {
        mapManager.CenterOnPlayer();
    }
}
