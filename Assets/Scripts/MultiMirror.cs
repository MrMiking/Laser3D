using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class MultiMirror : MonoBehaviour
{
    [SerializeField] private List<Transform> lasers;

    public void PlayMultiLaser()
    {
        for (int i = 0; i < lasers.Count(); i++)
        {
            lasers[i].GetComponent<CastLaser>().PlayLaser();
        }
    }

    public void StopMultiLaser()
    {
        for (int i = 0; i < lasers.Count(); i++)
        {
            lasers[i].GetComponent<CastLaser>().StopLaser();
        }
    }
    public void CastMultiLaser()
    {
        for(int i = 0; i < lasers.Count(); i++)
        {
            lasers[i].GetComponent<CastLaser>().CastLaserRayCast(lasers[i].transform.position, lasers[i].transform.forward);
        }
    }
}
