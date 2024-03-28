using UnityEngine;

public class PortalMirror : MonoBehaviour
{
    [SerializeField] private Transform linkedPortal;

    public void PlayLinkedLaser()
    {
        linkedPortal.GetComponent<CastLaser>().PlayLaser();
    }

    public void StopLinkedLaser()
    {
        linkedPortal.GetComponent<CastLaser>().StopLaser();
    }

    public void CastLinkedLaser()
    {
        linkedPortal.GetComponent<CastLaser>().CastLaserRayCast(linkedPortal.position, linkedPortal.forward);
    }

    public bool IsActive()
    {
        return linkedPortal.GetComponent<CastLaser>().isActive;
    }
}
