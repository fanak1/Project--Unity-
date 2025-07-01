using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProjectileHitBox : MonoBehaviour
{
    public float scale;

    public UnitBase source;

    public float dmgInterval = 1f;

    private List<UnitBase> hitUnits = new();

    public Action OnHit;

    public ParticlesType particles;

    public void Init(UnitBase source) 
    {
        this.source = source;
    }
    public float Damage()
    {
        return source.stats.atk * this.scale;
    }

    private bool IsInHitCooldown(Collider2D collision)
    {
        var hit = collision.gameObject.GetComponent<UnitBase>();
        if (hit != null) {
            
            if (hitUnits.Contains(hit)) 
                return true;
            else 
                return false;
        }
        hitUnits.RemoveAll(u => u == null);
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (source == null) return;
        if (collision.gameObject.CompareTag(source.tag)) return;
        if (CheckOpponent(collision.gameObject) && !IsInHitCooldown(collision))
        {
            UnitBase cUnit = collision.gameObject.GetComponent<UnitBase>();

            if (cUnit != null)
            {
                CoroutineManager.Instance.StartNewCoroutine(AddDamageInterval(cUnit));
                cUnit.damagePosition = cUnit.transform.position;
                source.Hitting(cUnit, Damage());
                SoundManager.Instance.PlaySFX(SoundManager.Instance.hitEnemy);
                OnHit?.Invoke();
                if (particles != ParticlesType.None)
                {
                    Registry.CreateParticle(particles, cUnit.transform.position);
                }
                
            }
        }
        else
        {

        }
    }

    IEnumerator AddDamageInterval(UnitBase cUnit)
    {
        hitUnits.Add(cUnit);

        yield return new WaitForSeconds(dmgInterval);

        if(cUnit != null) hitUnits.Remove(cUnit);
        else hitUnits.RemoveAll(c => c == null);
    }

    private bool CheckOpponent(GameObject collider)
    { //Check if we hit opponent instead of wall...
        if (!source.CompareTag(collider.tag))
        {
            if (!collider.CompareTag("Player") && !collider.CompareTag("Enemy")) return false;
            return true;
        }
        return false;
    }

    public void End()
    {
        Destroy(gameObject);
    }


}
