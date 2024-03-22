using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class CastLaser : MonoBehaviour
{
    [SerializeField] private LaserManager laserManager;

    [SerializeField] private GameObject currentHit;
    [SerializeField] private GameObject activeLaser;

    [SerializeField] private float laserLength;

    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        laserManager = GameObject.Find("LaserManager").GetComponent<LaserManager>();
    }

    private void Start()
    {
        activeLaser = laserManager.CreateLaser();
        if (transform.CompareTag("Source"))
        {
            activeLaser.SetActive(true);
        }
        else
        {
            activeLaser.SetActive(false);
        }
    }

    private void Update()
    {
        if (transform.CompareTag("Source"))
        {
            CastLaserRayCast(transform.position, transform.forward);
        }
    }

    public void CastLaserRayCast(Vector3 position, Vector3 direction)
    {
        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            laserLength = hit.distance / 2.0f;

            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

            StopAllLaser();

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
        else
        {
            StopAllLaser();

            laserLength = 1f;
            position += direction * 100;
        }

        Debug.DrawLine(startingPosition, position, Color.blue);

        activeLaser.GetComponentInChildren<VisualEffect>().SetFloat("Length", laserLength);
        activeLaser.transform.position = startingPosition;
        activeLaser.transform.LookAt(position);

        if (currentHit != null && currentHit.transform.CompareTag("Mirror"))
        {
            currentHit.GetComponent<CastLaser>().CastLaserRayCast(position, direction);
        }

        if (currentHit != null && currentHit.transform.CompareTag("MultiMirror"))
        {
            currentHit.GetComponent<MultiMirror>().CastMultiLaser();
        }
        if(currentHit != null && currentHit.transform.CompareTag("Portal"))
        {
            currentHit.GetComponent<PortalMirror>().CastLinkedLaser();
        }
    }

    public void PlayLaser()
    {
        activeLaser.SetActive(true);
    }

    public void StopLaser()
    {
        activeLaser.SetActive(false);
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
