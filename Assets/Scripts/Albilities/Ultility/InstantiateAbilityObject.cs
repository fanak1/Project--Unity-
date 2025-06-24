using UnityEngine;

public class InstantiateAbilityObject : ProjectileHitBox
{
    public Transform follow;

    private void Update()
    {
        if(follow != null)
        {
            this.transform.position = follow.position;
            this.transform.rotation = follow.rotation;
        } else
        {
            Destroy(gameObject);
        }
    }
}
