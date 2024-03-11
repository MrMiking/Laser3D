using Unity.VisualScripting;
using UnityEngine;

public class LinkMirror : MonoBehaviour
{
    public GameObject linkedMirror;

    private Vector3 linkPosition;
    private Vector3 linkedMirrorPosition;

    private void Awake()
    {
        linkPosition = transform.position;
        linkedMirrorPosition = linkedMirror.transform.position;

        if (linkedMirror != null) this.AddComponent<BoxCollider>();
        transform.localScale = linkedMirror.transform.localScale;
        transform.rotation = linkedMirror.transform.rotation;
    }

    private void Update()
    {
        transform.localScale = linkedMirror.transform.localScale;
        transform.rotation = linkedMirror.transform.rotation;
        transform.position = linkPosition + linkedMirror.transform.position - linkedMirrorPosition;
    }
}
