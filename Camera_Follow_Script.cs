using UnityEngine;


public class CameraFollow : MonoBehaviour
{

    public float cameraHeight_y = 15.0f;
    public float cameraDistance_z = 0f;
    public float cameraOffset_X = 0f;
    public float cameraSmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private GameObject CameraFollowTarget;

    [Header("1 Smooth Damp. 0 Lerp")] [SerializeField] private bool _smoothDamp = true;

    private void Start()
    {
        CameraFollowTarget = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector3 targetposition = CameraFollowTarget.transform.position;

        targetposition.y += cameraHeight_y;
        targetposition.z -= cameraDistance_z;
        targetposition.x += cameraOffset_X;

        if (_smoothDamp) transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, cameraSmoothTime);
        else transform.position = Vector3.Lerp(transform.position, targetposition, cameraSmoothTime);
    }
}
