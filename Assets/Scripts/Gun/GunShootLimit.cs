using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> uIFillUpdaters;

    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    private float _currentShoots;
    private bool _recharging = false;

    private UIFillUpdater ammoBar;

    //private void Awake()
    //{
    //    GetAllUIs();
    //}

    private void Awake()
    {
        ammoBar = UIManager.Instance?.ammoBar;
    }


    protected override IEnumerator ShootCoroutine()
    {

        if (_recharging) yield break;

        while(true)
        {
            if(_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }


        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }
    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    private void UpdateUI()
    {
        if (ammoBar != null)
            ammoBar.UpdateValue(maxShoot, _currentShoots);
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            if (ammoBar != null)
                ammoBar.UpdateValue(time / timeToRecharge);
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _recharging = false;
    }


    //private void GetAllUIs()
    //{
    //    uIFillUpdaters = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();
    //}


}
