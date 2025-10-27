using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public List<Transform> patrolPoints;
    public float patrolDuration = 2f;
    public float lookDuration = 0.5f;
    public Ease patrolEase = Ease.Linear;

    private int _currentIndex = 0;

    private Tween _patrolTween;

    private void Start()
    {
        if (patrolPoints.Count > 0)
            MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        Transform target = patrolPoints[_currentIndex];

        _patrolTween = transform.DOLookAt(target.position, lookDuration).OnComplete(() =>
        {
            _patrolTween = transform.DOMove(target.position, patrolDuration)
                .SetEase(patrolEase)
                .OnComplete(() =>
                {
                    _currentIndex = (_currentIndex + 1) % patrolPoints.Count;
                    MoveToNextPoint();
                });
        });
    }

    public void StopPatrol()
    {
        if (_patrolTween != null && _patrolTween.IsActive())
            _patrolTween.Kill();
    }


}

