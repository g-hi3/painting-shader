using UnityEngine;

public class ClickAndPoint : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        Application.targetFrameRate = 30;
    }

    private void FixedUpdate()
    {
        if (!Input.GetMouseButton(button: 0))
        {
            return;
        }

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, maxDistance: 100f)
            && hit.collider.TryGetComponent<Paintable>(out var paintable))
        {
            paintable.Paint(hit.point, Color.red, paintRadius: .4f);
        }
    }
}
