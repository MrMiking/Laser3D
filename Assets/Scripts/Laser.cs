using UnityEngine;
using UnityEngine.VFX;

public class Laser : MonoBehaviour
{
    private VisualEffect vfx;

    private bool active = false;

    private RaycastHit hit;
    private Laser currentHit;

    private float vfxSize;
    private void Start()
    {
        if(transform.gameObject.CompareTag("Source")) active = true;

        vfx = GetComponentInChildren<VisualEffect>();

        vfxSize = vfx.GetFloat("Size");
    }
    private void Update()
    {
        if (active) PlayLaser(); else StopLaser();

        Vector3 direction = new Vector3(0,1,0);

        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity) && active)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * 1000);

            if (hit.collider.transform.gameObject.CompareTag("Battery")){
                hit.transform.gameObject.GetComponent<CompleteLevel>().Complete();
            }

            currentHit = hit.transform.gameObject.GetComponent<Laser>();

            if (currentHit != null)
            {
                currentHit.active = true;
            }
        }            
        else
        {
            if (currentHit != null)
            {
                currentHit.active = false;
            }
        }

    }
    public void RotateMirror()
    {
        transform.Rotate(0,0,90);
    }
    public void PlayLaser()
    {
        vfx.SetFloat("Size", vfxSize);
    }
    public void StopLaser()
    {
        vfx.SetFloat("Size", 0);
    }
}
