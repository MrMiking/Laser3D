using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Mirror : MonoBehaviour
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
        activeLaser.SetActive(false);
    }

    public void CastMirrorLaser(Vector3 position, Vector3 direction)
    {
        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            laserLength = hit.distance / 2.0f;

            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

            if (hit.transform.CompareTag("Mirror"))
            {
                if (currentHit != null && currentHit != hit.transform.gameObject)
                {
                    currentHit.GetComponent<Mirror>().StopLaser();
                }

                currentHit = hit.transform.gameObject;

                currentHit.GetComponent<Mirror>().PlayLaser();
            }
            if (hit.transform.CompareTag("Border"))
            {
                if (currentHit != null && currentHit.transform.CompareTag("Mirror"))
                {
                    currentHit.GetComponent<Mirror>().StopLaser();
                }
                currentHit = null;
            }
        }
        else
        {
            if (currentHit != null && currentHit.transform.CompareTag("Mirror"))
            {
                currentHit.GetComponent<Mirror>().StopLaser();
                currentHit = null;
            }
            laserLength = 1f;
            position += direction * 100;
        }

        Debug.DrawLine(startingPosition, position, Color.blue);

        activeLaser.GetComponentInChildren<VisualEffect>().SetFloat("Length", laserLength);
        activeLaser.transform.position = startingPosition;
        activeLaser.transform.LookAt(position);

        if (currentHit != null && currentHit.transform.CompareTag("Mirror"))
        {
            currentHit.GetComponent<Mirror>().CastMirrorLaser(position, direction);
        }
    }

    public void PlayLaser()
    {
        activeLaser.SetActive(true);
    }

    public void StopLaser()
    {
        activeLaser.SetActive(false);
        if (currentHit != null)
        {
            currentHit.GetComponent<Mirror>().StopLaser();
        }
    }
}
