using UnityEngine;

public class Pivot : MonoBehaviour
{
    public static Pivot Instance { get; private set; }

    public float RotationX { get => transform.rotation.eulerAngles.x; set => UpdateRotation(value, true, false, false); }
    public float RotationY {  get => transform.rotation.eulerAngles.y; set => UpdateRotation(value, false, true, false); }
    public float RotationZ { get => transform.rotation.eulerAngles.z; set => UpdateRotation(value, false, false, true); }

    private void UpdateRotation(float angle, bool x_axis, bool y_axis, bool z_axis)
    {
        Vector3 rotation = transform.eulerAngles;
        if (x_axis)
        {
            rotation.x = angle;
        }
        if (y_axis)
        {
            rotation.y = angle;
        }
        if (z_axis)
        {
            rotation.z = angle;
        }
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void Start()
    {
        Instance = this;
    }
}
