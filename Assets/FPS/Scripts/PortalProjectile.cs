using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalProjectile : ProjectileStandard
{
    public GameObject PortalPrefab;
    override public void OnHit(Vector3 point, Vector3 normal, Collider collider)
    {
        //Quaternion quat = new Quaternion(normal.x, normal.y, normal.z, 1.0f);

        GameObject portal = GameObject.Instantiate<GameObject>(PortalPrefab);
        Transform portalTransform = portal.GetComponent<Transform>();
        portalTransform.position = point;
        portalTransform.up = normal;

        // impact vfx
        if (impactVFX)
        {
            GameObject impactVFXInstance = Instantiate(impactVFX, point + (normal * impactVFXSpawnOffset), Quaternion.LookRotation(normal));
            if (impactVFXLifetime > 0)
            {
                Destroy(impactVFXInstance.gameObject, impactVFXLifetime);
            }
        }

        // impact sfx
        if (impactSFXClip)
        {
            AudioUtility.CreateSFX(impactSFXClip, point, AudioUtility.AudioGroups.Impact, 1f, 3f);
        }

        // Self Destruct
        Destroy(this.gameObject);
    }
}
