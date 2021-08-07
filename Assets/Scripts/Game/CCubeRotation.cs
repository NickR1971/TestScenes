using UnityEngine;

public class CCubeRotation : MonoBehaviour
{
    private int angle;

    void Start()
    {
        angle = 0;
    }

    private void FixedUpdate()
    {
        if (++angle == 360) angle = 0;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(angle, angle, 0);
    }

}
