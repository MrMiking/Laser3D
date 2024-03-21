using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class MultiLaser : MonoBehaviour
{
    public List<Transform> lasers;

    public void PlayMultiLaser()
    {
        for (int i = 0; i < lasers.Count(); i++)
        {
            lasers[i].GetComponent<Mirror>().PlayLaser();
        }
    }

    public void StopMultiLaser()
    {
        for (int i = 0; i < lasers.Count(); i++)
        {
            lasers[i].GetComponent<Mirror>().StopLaser();
        }
    }
    public void CastMultiLaser()
    {
        for(int i = 0; i < lasers.Count(); i++)
        {
            lasers[i].GetComponent<Mirror>().CastMirrorLaser(lasers[i].transform.position, lasers[i].transform.forward);
        }
    }
}
