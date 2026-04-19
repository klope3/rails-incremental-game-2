using UnityEngine;

public interface IProjectileImpactable
{
    public void Impact(Projectile projectile, RaycastHit hitInfo);
}
