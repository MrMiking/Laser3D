using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class LaserManager : MonoBehaviour
{
    [SerializeField] private GameObject laserTrailRender;

    public GameObject CreateLaser()
    {
        return Instantiate(laserTrailRender);
    }
}