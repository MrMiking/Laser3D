using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class CastLaser : MonoBehaviour
{
    [SerializeField] private GameObject laser;

    public Color raycastColor;

    private GameObject currentLaser;
    private CastLaser currentHit;
    private LinkMirror linkMirror;

    private Vector3 startingPosition;

    private float laserLength;

    private void Awake()
    {
        currentLaser = Instantiate(laser);
    }

    void Update()
    {
        if(transform.CompareTag("Source")) DrawRaycast(transform.position, transform.forward, this);
    }

    private void DrawRaycast(Vector3 position, Vector3 direction, CastLaser pastHit)
    {
        startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            laserLength = hit.distance / 2.0f;

            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

            if (hit.collider.transform.CompareTag("Mirror"))
            {
                currentHit = hit.transform.GetComponent<CastLaser>();
            }
            if (hit.collider.transform.CompareTag("LaserEntry"))
            {
                linkMirror = hit.transform.GetComponent<LinkMirror>();
                currentHit = hit.transform.GetComponent<LinkMirror>().linkedMirror.GetComponent<CastLaser>();
            }

            if (currentHit != null && pastHit != currentHit) currentHit.PlayLaser();
        }
        else
        {
            position += direction * 100;

            if (currentHit != null && pastHit != currentHit) currentHit.StopLaser();
        }

        Debug.DrawLine(startingPosition, position, raycastColor);

        currentLaser.transform.position = startingPosition;
        currentLaser.transform.LookAt(position);

        if (transform.CompareTag("Source")) PlayLaser();

        if (currentHit != null && pastHit != currentHit)
        {
            if (linkMirror != null)
            {
                currentHit.GetComponent<CastLaser>().DrawRaycast(position + currentHit.transform.position - linkMirror.transform.position, direction, this);
            }
            else 
            {
                currentHit.GetComponent<CastLaser>().DrawRaycast(position, direction, this);
            }
        }
    }

    private void PlayLaser()
    {
        currentLaser.GetComponentInChildren<VisualEffect>().SetFloat("Length", laserLength == 0 ? 1 : laserLength);
        currentLaser.SetActive(true);
    }

    private void StopLaser()
    {
        currentLaser.SetActive(false);
    }
}