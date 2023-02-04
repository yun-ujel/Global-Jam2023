using UnityEngine;

public class CursorPosition : MonoBehaviour
{
    public Vector2 position;
    public Camera mainCamera;
    public Transform ransform;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }

    private void Update()
    {
        position = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (ransform != null)
        {
            ransform.position = position;
        }
    }
}
