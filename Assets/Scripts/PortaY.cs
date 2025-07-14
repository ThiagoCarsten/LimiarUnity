using UnityEngine;

public class PortaY : MonoBehaviour
{
    [SerializeField] private Transform SalaAcima;
    [SerializeField] private Transform SalaAbaixo;
    [SerializeField] private ControleCamera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.y > transform.position.y)
                cam.MoverSalay(SalaAbaixo);

            else
                cam.MoverSalay(SalaAcima);
        }
    }
}