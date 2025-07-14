using UnityEngine;

public class PortaX : MonoBehaviour
{
    [SerializeField] private Transform SalaEsq;
    [SerializeField] private Transform SalaDir;
    [SerializeField] private ControleCamera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                cam.MoverSalax(SalaDir);

            else
                cam.MoverSalax(SalaEsq);
        }
    }
}