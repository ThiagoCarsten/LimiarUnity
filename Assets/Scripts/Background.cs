using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private GameObject cam;

    void Start()
    {
    cam = Camera.main.gameObject; 
    }

    void LateUpdate()
    { 
        transform.position = new Vector3(
            cam.transform.position.x,
            cam.transform.position.y,
            transform.position.z
        );
    }
}