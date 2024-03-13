using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleMirror : MonoBehaviour
{
    [SerializeField] private CastLaser mirror_01;
    [SerializeField] private CastLaser mirror_02;

    public void ResetDoubleMirror()
    {
        mirror_01.GetComponent<CastLaser>().StopLaser();
        mirror_02.GetComponent<CastLaser>().StopLaser();
    }
}
