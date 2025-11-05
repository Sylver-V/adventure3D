using System.Collections;
using UnityEngine;

public class ProjectileBaseHandler : MonoBehaviour
{
    public float attackMultiplier = 1f;
    private Coroutine _resetRoutine;

    public void SetAttackMultiplier(float multiplier, float duration)
    {
        attackMultiplier = multiplier;

        if (_resetRoutine != null)
            StopCoroutine(_resetRoutine);

        _resetRoutine = StartCoroutine(ResetAfter(duration));
    }
    private IEnumerator ResetAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        attackMultiplier = 1f;
    }
}
