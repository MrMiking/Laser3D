using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    private RaycastHit hit;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject()
            && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Mirror"))
            {
                hit.transform.gameObject.GetComponent<RotateManager>().RotateMirror();
            }
            else return;
        }
    }
}