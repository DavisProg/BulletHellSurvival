using UnityEngine;
public class CustomCursor : MonoBehaviour
{
    public GameObject currentObject;
    private void Update()
    {
        if (currentObject)
        {
            currentObject.transform.position = Input.mousePosition;
        }
    }
}
