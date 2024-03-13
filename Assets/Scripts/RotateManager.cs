using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField] private char rotationAxys;
    public void RotateMirror()
    {
        transform.Rotate(rotationAxys == 'x' ? 90 : 0, rotationAxys == 'y' ? 90 : 0, rotationAxys == 'z' ? 90 : 0);
    }
}