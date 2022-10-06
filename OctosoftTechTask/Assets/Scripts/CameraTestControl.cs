using UnityEngine;

public class CameraTestControl : MonoBehaviour
{
    Transform cubeTransform;
    private void Start()
    {
        cubeTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(-1, 0, 0);
        }
    }
}
