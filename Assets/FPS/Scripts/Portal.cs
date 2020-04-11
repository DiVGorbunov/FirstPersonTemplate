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
                cam.transform.forward = this.transform.forward;

                cam = this.gameObject.GetComponentInChildren<Camera>();

                cam.transform.position = spawnedRedPortal.transform.position;
                cam.transform.forward = spawnedRedPortal.transform.forward;
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
                cam.transform.forward = this.transform.forward;
                cam = this.gameObject.GetComponentInChildren<Camera>();

                cam.transform.position = spawnedBluePortal.transform.position;
                cam.transform.forward = spawnedBluePortal.transform.forward;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 orientation = Camera.main.transform.forward;
        if (spawnedBluePortal)
        {
            Camera cam = spawnedBluePortal.GetComponentInChildren<Camera>();
            cam.transform.forward = -orientation;
        }
        if (spawnedRedPortal)
        {
            Camera cam = spawnedRedPortal.GetComponentInChildren<Camera>();
            cam.transform.forward = -orientation;
        }*/
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider)
        {
            if (spawnedBluePortal && spawnedBluePortal)
            {
                Vector3 telep;
                Vector3 forwardVector;
                if (isBlue)
                {
                    telep = spawnedRedPortal.transform.position;
                    forwardVector = spawnedRedPortal.transform.forward;
                    telep += forwardVector * 0.5f;
                }
                else {
                    telep = spawnedBluePortal.transform.position;
                    forwardVector = spawnedBluePortal.transform.forward;
                    telep += forwardVector * 0.5f;
                }

                PlayerCharacterController controller = collider.gameObject.GetComponent<PlayerCharacterController>();

                if (controller)
                {
                    controller.teleportTo(telep, forwardVector);
                }
                else
                {
                    ProjectileStandard projectile = collider.gameObject.GetComponent<ProjectileStandard>();
                    if (projectile)
                    {
                        projectile.teleportTo(telep, forwardVector);
                    }
                }
            }
                     
        }
    }
}
