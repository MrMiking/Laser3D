using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    [SerializeField] private char rotationAxys;
    [SerializeField] private bool isRelief;

    [SerializeField] private List<int> bannedRotation;
    public void RotateMirror()
    {
        transform.Rotate(0, 0, 90);
    }

    private void Update()
    {
        if (bannedRotation.Count == 2 && transform.rotation.z >= bannedRotation[1] && transform.rotation.z <= bannedRotation[0])
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}