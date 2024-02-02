using UnityEngine;
using UnityEngine.VFX;

public class MirrorRotationManager : MonoBehaviour
{
    [SerializeField] private VisualEffect vfx;

    private RaycastHit hit;

    public bool active = true;

    private float vfxSize;
    private void Start()
    {
        vfx = GetComponentInChildren<VisualEffect>();

        vfxSize = vfx.GetFloat("Size");
    }
    private void Update()
    {
        active = false;

        if (active)
        {
            PlayLaser();
        }
        if(!active)
        {
            StopLaser();
        }

        Vector3 direction = new Vector3(0,1,0);

        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity))
        {
            
            vfx.SetFloat("Length", 1.5f);
            hit.transform.gameObject.GetComponent<MirrorRotationManager>().PlayLaser();
            hit.transform.gameObject.GetComponent<MirrorRotationManager>().active = true;
        }
        else
        {
            vfx.SetFloat("Length", 1);
        }

    }
    public void RotateMirror()
    {
        transform.Rotate(0,0,90);
    }
    private void PlayLaser()
    {
        vfx.SetFloat("Size", vfxSize);
    }
    private void StopLaser()
    {
        vfx.SetFloat("Size", 0);
    }
}
