using UnityEngine;

public class EnemyTriggerActivator : MonoBehaviour
{
    public GameObject enemyToActivate;

    private void Start()
    {
        if (enemyToActivate != null)
        {
            enemyToActivate.SetActive(false); // inimigo come�a desativado
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyToActivate != null)
        {
            enemyToActivate.SetActive(true);
            Destroy(gameObject); // remove o trigger ap�s ativar
        }
    }
}
