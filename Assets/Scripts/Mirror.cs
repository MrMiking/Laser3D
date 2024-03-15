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

            if(currentHit != null && currentHit != hit.transform.gameObject)
            {
                currentHit.GetComponent<Mirror>().StopLaser();
            }

            currentHit = hit.transform.gameObject;
        }
        else
        {
            if (currentHit != null)
            {
                currentHit.GetComponent<Mirror>().StopLaser();
                currentHit = null;
            }
            position += direction * 100;
        }

        Debug.DrawLine(startingPosition, position, Color.blue);

        UpdateLaser(startingPosition, position);

        if (currentHit != null)
        {
            currentHit.GetComponent<Mirror>().CastMirrorLaser(position, direction);
            currentHit.GetComponent<Mirror>().PlayLaser();
        }
    }

    private void UpdateLaser(Vector3 startingPosition, Vector3 position)
    {
        activeLaser.GetComponentInChildren<VisualEffect>().SetFloat("Length", currentHit == null ? 1 : laserLength);
        activeLaser.transform.position = startingPosition;
        activeLaser.transform.LookAt(position);
    }

    public void PlayLaser()
    {
        activeLaser.SetActive(true);
    }

    public void StopLaser()
    {
        activeLaser.SetActive(false);
    }
}
