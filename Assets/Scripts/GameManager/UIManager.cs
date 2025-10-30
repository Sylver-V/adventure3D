using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Life")]
    public UIFillUpdater healthBar;
    public TMPro.TextMeshProUGUI lifeText;

    [Header("Gun")]
    public UIFillUpdater ammoBar;

    private void Awake()
    {
        Instance = this;
    }
}

