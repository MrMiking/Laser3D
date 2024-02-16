using UnityEngine;
using UnityEngine.VFX;

public class Laser : MonoBehaviour
{
    private bool active = false;

    private VisualEffect vfx;

    private float vfxSize;
    private float baseVfxLength = 1.0f;

    private bool batteryOn;

    private RaycastHit hit;
    private Laser currentHit;
    private float laserLength;
    
    private void Start()
    {
        if(transform.gameObject.CompareTag("Source")) active = true;

        vfx = GetComponentInChildren<VisualEffect>();

        vfxSize = vfx.GetFloat("Size");
    }
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(0, 1, 0)), out hit, Mathf.Infinity) && active)
        {
            laserLength = hit.distance / 3.0f;

            if (hit.collider.transform.gameObject.CompareTag("Battery"))
            {
                batteryOn = true;
                hit.transform.gameObject.GetComponent<CompleteLevel>().Complete();
            }

            if (hit.collider.transform.gameObject.CompareTag("Mirror"))
            {
                currentHit = hit.transform.gameObject.GetComponentInChildren<Laser>();
                if (currentHit != null) currentHit.active = true;
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
        gameObject.GetComponentInChildren<Transform>().Rotate(0,0,90);
    }
    private void PlayLaser(float length)
    {
        vfx.SetFloat("Length", length);
        vfx.SetFloat("Size", vfxSize);
    }
    private void StopLaser()
    {
        vfx.SetFloat("Size", 0);
    }
}
