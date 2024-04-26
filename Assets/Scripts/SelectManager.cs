using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    private RaycastHit hit;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("hello");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 3)
                {
                    hit.transform.gameObject.GetComponentInParent<RotateManager>().RotateMirror();
                }
            }
        }
    }
}