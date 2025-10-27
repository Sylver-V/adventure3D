using UnityEngine;
using UnityEngine.UI;

public class UIWeaponDisplay : MonoBehaviour
{
    public Image weaponIcon;

    public void UpdateIcon(Sprite newIcon)
    {
        if (weaponIcon != null)
            weaponIcon.sprite = newIcon;
    }
}

