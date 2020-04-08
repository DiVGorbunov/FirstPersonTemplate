using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private static GameObject spawnedBluePortal = null;
    private static GameObject spawnedRedPortal = null;

    public bool isBlue = true;
    // Start is called before the first frame update
    void Start()
    {
        if (isBlue)
        {
            if (spawnedBluePortal)
            {
                Destroy(spawnedBluePortal);
            }
            spawnedBluePortal = this.gameObject;
            if (spawnedRedPortal)
            {
                Camera cam = spawnedRedPortal.GetComponentInChildren<Camera>();
                cam.transform.position = this.transform.position;
                cam.transform.forward = this.transform.up;

                cam = this.gameObject.GetComponentInChildren<Camera>();

                cam.transform.position = spawnedRedPortal.transform.position;
                cam.transform.forward = spawnedRedPortal.transform.up;
            }
        }
        else {
            if (spawnedRedPortal)
            {
                Destroy(spawnedRedPortal);
            }
            spawnedRedPortal = this.gameObject;
            if (spawnedBluePortal)
            {
                Camera cam = spawnedBluePortal.GetComponentInChildren<Camera>();
                cam.transform.position = this.transform.position;
                cam.transform.forward = this.transform.up;
                cam = this.gameObject.GetComponentInChildren<Camera>();

                cam.transform.position = spawnedBluePortal.transform.position;
                cam.transform.forward = spawnedBluePortal.transform.up;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider)
        {
            if (spawnedBluePortal && spawnedBluePortal)
            {
                Vector3 telep;
                Vector3 upVector;
                if (isBlue)
                {
                    telep = spawnedRedPortal.transform.position;
                    upVector = spawnedRedPortal.transform.up;
                    telep += upVector * 0.5f;
                }
                else {
                    telep = spawnedBluePortal.transform.position;
                    upVector = spawnedBluePortal.transform.up;
                    telep += upVector * 0.5f;
                }

                PlayerCharacterController controller = collider.gameObject.GetComponent<PlayerCharacterController>();

                if (controller)
                {
                    controller.teleportTo(telep, upVector);
                }
                else
                {
                    ProjectileStandard projectile = collider.gameObject.GetComponent<ProjectileStandard>();
                    if (projectile)
                    {
                        projectile.teleportTo(telep, upVector);
                    }
                }
            }
                     
        }
    }
}
