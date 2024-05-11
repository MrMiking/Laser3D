using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class CastLaser : MonoBehaviour
{
    private LaserManager laserManager;

    private GameObject currentHit;
    private GameObject activeLaser;

    private LayerMask layerMask;

    private float laserLength;

    public bool isActive = false;

    private void Awake()
    {
        laserManager = GameObject.Find("SceneManager").GetComponent<LaserManager>();
        layerMask = LayerMask.GetMask("Default", "Border");
    }

    private void Start()
    {
        activeLaser = laserManager.CreateLaser();
        if (transform.CompareTag("Source"))
        {
            Application.targetFrameRate = 60;
            PlayLaser();
        }
        else
        {
            StopLaser();
        }
    }

    private void Update()
    {
        if (transform.CompareTag("Source"))
        {
            CastLaserRayCast(transform.position, transform.forward * 0.75f);
        }
    }

    public void CastLaserRayCast(Vector3 position, Vector3 direction)
    {
        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            laserLength = hit.distance;

            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

            if (currentHit != hit.transform.gameObject)
            {
                StopAllLaser();
            }
            if(!IsActive(hit.transform.gameObject))
            {
                if (hit.transform.CompareTag("Mirror"))
                {
                    currentHit = hit.transform.gameObject;

                    currentHit.GetComponent<CastLaser>().PlayLaser();
                }
                else if (hit.transform.CompareTag("MultiMirror"))
                {
                    currentHit = hit.transform.gameObject;

                    currentHit.GetComponent<MultiMirror>().PlayMultiLaser();
                }
                else if (hit.transform.CompareTag("Border"))
                {
                    currentHit = null;
                }
                else if (hit.transform.CompareTag("Portal"))
                {
                    currentHit = hit.transform.gameObject;

                    currentHit.GetComponent<PortalMirror>().PlayLinkedLaser();
                }
                else if (hit.transform.CompareTag("Battery"))
                {
                    currentHit = hit.transform.gameObject;

                    currentHit.GetComponent<Battery>().ActiveBattery();
                }
            }
        }
        else
        {
            StopAllLaser();
            position += direction * 100;
            laserLength = 2f;
        }

        activeLaser.transform.Find("EndVFX").position = activeLaser.transform.position + activeLaser.transform.forward * laserLength;

        activeLaser.transform.LookAt(position);
        activeLaser.transform.position = startingPosition;

        activeLaser.GetComponentInChildren<LineRenderer>().SetPosition(0, startingPosition);
        activeLaser.GetComponentInChildren<LineRenderer>().SetPosition(1, activeLaser.transform.position + activeLaser.transform.forward * laserLength);

        if(currentHit != null)
        {
            if (currentHit.transform.CompareTag("Mirror"))
            {
                currentHit.GetComponent<CastLaser>().CastLaserRayCast(position, direction);
            }

            if (currentHit.transform.CompareTag("MultiMirror"))
            {
                currentHit.GetComponent<MultiMirror>().CastMultiLaser();
            }
            if (currentHit.transform.CompareTag("Portal"))
            {
                currentHit.GetComponent<PortalMirror>().CastLinkedLaser();
            }
        }
    }

    public bool IsActive(GameObject hit)
    {
        if (hit != null)
        {
            if (hit.transform.CompareTag("Mirror"))
            {
                if (hit.GetComponent<CastLaser>().isActive)
                {
                    return true;
                }
            }
            if (hit.transform.CompareTag("MultiMirror"))
            {
                if (hit.GetComponent<MultiMirror>().IsActive())
                {
                    return true;
                }
            }
            if (hit.transform.CompareTag("Portal"))
            {
                if (hit.GetComponent<PortalMirror>().IsActive())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void PlayLaser()
    {
        activeLaser.SetActive(true);
        isActive = true;
    }

    public void StopLaser()
    {
        activeLaser.SetActive(false);
        isActive = false;
        StopAllLaser();
    }

    public void StopAllLaser()
    {
        if (currentHit != null)
        {
            if (currentHit.transform.CompareTag("Mirror"))
            {
                currentHit.GetComponent<CastLaser>().StopLaser();
            }
            if (currentHit.transform.CompareTag("MultiMirror"))
            {
                currentHit.GetComponent<MultiMirror>().StopMultiLaser();

            }
            if (currentHit.transform.CompareTag("Portal"))
            {
                currentHit.GetComponent<PortalMirror>().StopLinkedLaser();
            }
            if (currentHit.transform.CompareTag("Battery"))
            {
                currentHit.GetComponent<Battery>().DesactiveBattery();
            }
            if (currentHit.transform.CompareTag("Border"))
            {
                currentHit.GetComponent<CastLaser>().StopLaser();
            }
            currentHit = null;
        }
    }
}
