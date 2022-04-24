using UnityEngine;

public class Movement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.forward;
        }
    }
}
