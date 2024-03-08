using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.UI.GridLayoutGroup;

public class Laser : MonoBehaviour
{
    public List<int> bannedRotation;

    private bool active = false;

    private VisualEffect vfx;
    private Transform mesh;
    private Transform mirrorCorner;

    private float vfxSize;
    private float baseVfxLength = 1.0f;

    private bool batteryOn;

    private RaycastHit hit;
    private Laser currentHit;
    private float laserLength;

    private void Start()
    {
        if (transform.gameObject.CompareTag("Source"))
        {
            active = true;
        }
        else
        {
            mirrorCorner = transform.Find("MirrorCorner");
        }

        vfx = GetComponentInChildren<VisualEffect>();
        mesh = transform.Find("Mesh");

        vfxSize = vfx.GetFloat("Size");
    }
    private void Update()
    {
        if(bannedRotation.Count == 3 || )
        {
            
        }

        Debug.DrawRay(vfx.transform.position, vfx.transform.TransformDirection(new Vector3(0, 1, 0)) * 1000, Color.yellow);

        if (Physics.Raycast(vfx.transform.position, vfx.transform.TransformDirection(new Vector3(0, 1, 0)), out hit, Mathf.Infinity) && active)
        {
            
            laserLength = (hit.distance + 0.2f) / 3.0f;

            if (hit.collider.transform.gameObject.CompareTag("MirrorCollider"))
            {
                currentHit = hit.transform.gameObject.GetComponentInParent<Laser>();
                StartCoroutine(Wait());
            }
            else
            {
                if (currentHit != null) currentHit.active = false; currentHit = null;
            }
        }            
        else
        {
            batteryOn = false;
            if (currentHit != null) currentHit.active = false; currentHit = null;
        }

        if (active) PlayLaser(currentHit != null ? laserLength : batteryOn ? laserLength : baseVfxLength); else StopLaser();

    }
    public void RotateMirror()
    {
        mesh.Rotate(0, 0, 90);
        mirrorCorner.Rotate(0, 0, 90);
    }
    void PlayLaser(float length)
    {
        vfx.SetFloat("Length", length);
        vfx.SetFloat("Size", vfxSize);
    }
    private void StopLaser()
    {
        vfx.SetFloat("Size", 0);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.05f);

        if (currentHit != null) currentHit.active = true;
    }
}
