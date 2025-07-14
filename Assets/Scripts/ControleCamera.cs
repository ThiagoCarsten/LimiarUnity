using UnityEngine;

public class ControleCamera : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.3f;
    private float currentPosX;
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        currentPosX = transform.position.x;
        currentPosY = transform.position.y;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(currentPosX, currentPosY, transform.position.z),
            ref velocity, smoothTime);
    }

    public void MoverSalax(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }

    public void MoverSalay(Transform _newRoom)
    {
        currentPosY = _newRoom.position.y;
    }
}
