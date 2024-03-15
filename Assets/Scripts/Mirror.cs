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

        if (Physics.Raycast(ray, out hit, 100))
        {
            laserLength = hit.distance / 2.0f;

            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;

            currentHit = hit.transform.gameObject;
        }
        else
        {
            position += direction * 100;
            if (currentHit != null) currentHit.GetComponent<Mirror>().StopLaser();
        }

        Debug.DrawLine(startingPosition, position, Color.blue);

        activeLaser.transform.position = startingPosition;
        activeLaser.transform.LookAt(position);

        if (currentHit != null) StartCoroutine(CastDelay(position, direction));
    }

    IEnumerator CastDelay(Vector3 position, Vector3 direction)
    {
        yield return new WaitForSeconds(0.25f);

        currentHit.GetComponent<Mirror>().CastMirrorLaser(position, direction);
        currentHit.GetComponent<Mirror>().PlayLaser();
    }

    public void PlayLaser()
    {
        activeLaser.GetComponentInChildren<VisualEffect>().SetFloat("Length", laserLength == 0 ? 1 : laserLength);
        activeLaser.SetActive(true);
    }

    public void StopLaser()
    {
        activeLaser.SetActive(false);
    }
}
