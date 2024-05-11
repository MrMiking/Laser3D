using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
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