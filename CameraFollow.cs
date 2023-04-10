using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0, 10)][SerializeField] private float smoothCamera = 3.5f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Vector3 minimumCameraBounds, maximumCameraBounds;


    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 playerPosition = playerTransform.position + Offset;

        Vector3 originPosition = new Vector3(
            Mathf.Clamp(playerPosition.x, minimumCameraBounds.x, maximumCameraBounds.x),
            Mathf.Clamp(playerPosition.y, minimumCameraBounds.y, maximumCameraBounds.y),
            Mathf.Clamp(playerPosition.z, minimumCameraBounds.z, maximumCameraBounds.z));


        this.transform.position = Vector3.Lerp(transform.position, originPosition, smoothCamera * Time.fixedDeltaTime);
    }
}
