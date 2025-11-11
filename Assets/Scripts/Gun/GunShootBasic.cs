using UnityEngine;

public class GunShootBasic : GunShootLimit
{
    [Header("Audio")]
    public SFXType shootSFX;

    public override void Shoot()
    {
        // Instancia um único projétil
        var projectile = Instantiate(prefabProjectile, positionToShoot.position, positionToShoot.rotation);
        projectile.speed = speed;

        // Som do disparo
        if (SFXPool.Instance != null)
            SFXPool.Instance.Play(shootSFX);
    }
}
