using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.GetComponentInParent<RotateManager>() != null)
                {
                    hit.transform.gameObject.GetComponentInParent<RotateManager>().RotateObject();
                }
            }
        }
    }
}