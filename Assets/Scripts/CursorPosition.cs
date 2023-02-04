using UnityEngine;

public class CursorPosition : MonoBehaviour
{
    private Vector3 screenPosition;
    public Vector3 worldPosition;
    public Transform myTransform;

    bool freezePosition = false;
    private void Update()
    {
        if (!freezePosition)
        {
            screenPosition = Input.mousePosition;
            screenPosition.z = CalculateDistance();

            worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            if (myTransform != null)
            {
                myTransform.position = worldPosition;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            freezePosition = true;
        }
    }

    RaycastHit hit;
    float CalculateDistance()
    {
        Vector3 direction = Vector3.Normalize(worldPosition - Camera.main.transform.position);

        Ray ray = new Ray(Camera.main.transform.position, direction);

        Physics.Raycast(ray, out hit, 100f, 4);

        return Vector3.Distance(Camera.main.transform.position, hit.point);
    }
}
