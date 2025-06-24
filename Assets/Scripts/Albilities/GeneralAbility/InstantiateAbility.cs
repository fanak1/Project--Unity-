using System.Data.Common;
using UnityEngine;

public class InstantiateAbility : Abilities
{
    public bool setToFirePoint = false;

    public InstantiateAbilityObject prefabs;

    GameObject current;
    
    public override void Action()
    {
        var obj = Instantiate(prefabs, source.transform.position, Quaternion.identity);
        if (setToFirePoint)
        {
            var firePoint = source.transform.Find("FirePoint");
            if (firePoint != null)
            {
                obj.follow = firePoint;
            }
            else
            {
                Debug.LogWarning("FirePoint not found, using source position instead.");
                obj.follow = source.transform;
            }
        }
        else
        {
            if(source is PlayerUnit)
                obj.follow = ((PlayerUnit)source).GetTransformPosition();
            else
                obj.follow = source.transform;
        }
        obj.Init(source);
        current = obj.gameObject;
    }

    protected override void CleanUpOnDetach()
    {
        if(current != null)
        {
            Destroy(current);
        }
    }
}
