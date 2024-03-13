using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class CastLaser : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject laser;

    public Color raycastColor;

    private GameObject currentLaser;
    private CastLaser currentHit;

    private LinkMirror linkMirror;
    private LinkMirror linkPortal;

    private Vector3 startingPosition;

    private float laserLength;

    private void Awake()
    {
        currentLaser = Instantiate(laser);
    }

    void Update()
    {
        if (transform.CompareTag("Source"))
        {
            PlayLaser();
            DrawRaycast(transform.position, transform.forward, this);
        }
    }

    private void DrawRaycast(Vector3 position, Vector3 direction, CastLaser pastHit)
    {
        startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            laserLength = hit.distance / 2.0f;

            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

            if (hit.collider.transform.CompareTag("Mirror"))
            {
                if(hit.transform.GetComponent<CastLaser>() != currentHit)
                {
                    if (currentHit != null && pastHit != currentHit) 
                    {
                        currentHit.StopLaser();
                    }
                }
                currentHit = hit.transform.GetComponent<CastLaser>();
            }
            if (hit.collider.transform.CompareTag("ReliefMirror"))
            {
                linkMirror = hit.transform.GetComponent<LinkMirror>();
                currentHit = linkMirror.linkedMirror.GetComponent<CastLaser>();
            }
            if (hit.collider.transform.CompareTag("Portal"))
            {
                linkPortal = hit.transform.GetComponent<LinkMirror>();
                currentHit = linkPortal.linkedMirror.GetComponent<CastLaser>();
            }

            currentHit.transform.GetComponent<CastLaser>().PlayLaser();
        }
        else
        {
            position += direction * 100;

            if (currentHit != null) currentHit.StopLaser();
        }

        Debug.DrawLine(startingPosition, position, raycastColor);

        currentLaser.transform.position = startingPosition;
        currentLaser.transform.LookAt(position);

        if (currentHit != null && pastHit != currentHit)
        {
            if (linkMirror != null)
            {
                currentHit.DrawRaycast(
                    position + currentHit.transform.position - linkMirror.transform.position, direction, this);
            }
            if(linkPortal != null)
            {
                currentHit.DrawRaycast(
                    position + currentHit.transform.position - linkPortal.transform.position, currentHit.transform.forward, this);
            }
            else 
            {
                currentHit.DrawRaycast(position, direction, this);
            }
        }
    }

    private void PlayLaser()
    {
        currentLaser.GetComponentInChildren<VisualEffect>().SetFloat("Length", laserLength == 0 ? 1 : laserLength);
        currentLaser.SetActive(true);
    }

    public void StopLaser()
    {
        currentLaser.SetActive(false);
    }
}