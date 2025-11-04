using Animation;
using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HealthBase : MonoBehaviour, IDamageble
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    public float destroyDelay = 3f;


    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uIFillUpdater;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
        UpdateUI();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }


    protected virtual void Kill()
    {
        if(destroyOnKill)
            Destroy(gameObject, destroyDelay);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float f)
    {
        _currentLife -= f;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        float lifePercent = _currentLife / startLife;

        if (CompareTag("Player"))
        {
            if (UIManager.Instance != null && UIManager.Instance.healthBar != null)
            {
                UIManager.Instance.healthBar.UpdateValue(lifePercent);
            }

            if (UIManager.Instance != null && UIManager.Instance.lifeText != null)
            {
                UIManager.Instance.lifeText.text = $"{Mathf.CeilToInt(_currentLife)} / {Mathf.CeilToInt(startLife)}";
            }

            if (UIManager.Instance != null && UIManager.Instance.lifePackKeyText != null)
            {
                bool showKey = _currentLife < startLife * 0.5f;
                UIManager.Instance.lifePackKeyText.gameObject.SetActive(showKey);

                if (showKey)
                {
                    var action = FindObjectOfType<ActionLifePack>();
                    if (action != null)
                    {
                        var key = action.keyCode;
                        UIManager.Instance.lifePackKeyText.text = $"Pressione [{key}] para curar";
                    }
                }
            }
        }

    }



}
