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
        if(transform.CompareTag("Source")) DrawRaycast(transform.position, transform.forward);
    }

    private void DrawRaycast(Vector3 position, Vector3 direction)
    {
        startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
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
        }
        else
        {
            position += direction * 100;
        }

        Debug.DrawLine(startingPosition, position, raycastColor);

        if (linkMirror != null && currentHit != null)
        {
            currentHit.GetComponent<CastLaser>().DrawRaycast(position + currentHit.transform.position - linkMirror.transform.position, direction);
        }
        else if(currentHit != null)
        {
            currentHit.GetComponent<CastLaser>().DrawRaycast(position, direction);
        }

        /*currentLaser.transform.position = startingPosition;
        currentLaser.transform.LookAt(position);*/
    }

    /*private void PlayLaser()
    {
        currentLaser.GetComponentInChildren<VisualEffect>().SetFloat("Length", laserLength <= 1.5f ? 1.5f : laserLength);
        currentLaser.SetActive(true);
    }

    private void StopLaser()
    {
        currentLaser.SetActive(false);
    }*/
}