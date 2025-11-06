using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilityShot : PlayerAbilityBase
{

    public List<UIFillUpdater> uIFillUpdaters;
    public List<GunBase> availableGuns;

    public GunBase gunBase;
    public Transform gunPosition;

    public UIWeaponDisplay uiWeaponDisplay;

    public FlashColor _flashColor;

    private int _currentGunIndex = 0;
    private GunBase _currentGun;

    [Header("VFX")]
    public ParticleSystem gunSmoke;


    protected override void Init()
    {
        base.Init();

        CreateGun(_currentGunIndex);

        if (uiWeaponDisplay != null && availableGuns[_currentGunIndex].gunIcon != null)
            uiWeaponDisplay.UpdateIcon(availableGuns[_currentGunIndex].gunIcon);

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

        inputs.Gameplay.Weapon1.performed += ctx => SwitchGun(0);
        inputs.Gameplay.Weapon2.performed += ctx => SwitchGun(1);
    }

    private void CreateGun(int index)
    {

        if (_currentGun != null)
            Destroy(_currentGun.gameObject);
        _currentGun = Instantiate(availableGuns[index], gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShot();
        _flashColor.Flash();
        Debug.Log("Start Shoot");

        if (gunSmoke != null)
        {
            gunSmoke.Play();
        }
    }



    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }

    private void SwitchGun(int index)
    {
        if (index >= 0 && index < availableGuns.Count)
        {
            _currentGunIndex = index;
            CreateGun(_currentGunIndex);

            foreach (var ui in uIFillUpdaters)
            {
                ui.UpdateIcon(availableGuns[index].gunIcon);
            }
            
            uiWeaponDisplay.UpdateIcon(availableGuns[index].gunIcon);


            Debug.Log($"Switched to gun {index + 1}");
        }
    }

}
