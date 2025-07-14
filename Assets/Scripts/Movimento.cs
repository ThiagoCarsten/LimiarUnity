using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D corpo;
    bool noChao;
    bool naParede;
    [SerializeField] int velocidade = 350;
    [SerializeField] float pulo = 4;
    private Animator playerAnim;

    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        float Andar = Input.GetAxis("Horizontal");

        // Movimentação
        corpo.linearVelocity = new Vector2(Andar * velocidade * Time.deltaTime, corpo.linearVelocity.y);

        // Animação Update
        playerAnim.SetFloat("xVelocity", Math.Abs(Andar));
        playerAnim.SetFloat("yVelocity", corpo.linearVelocity.y);

        // Animação de cair
        if (!noChao && corpo.linearVelocity.y < 0)
        {
            playerAnim.SetBool("Pulando", true);
        }

        // Virar personagem
        if (Andar > 0.01f)
            transform.localScale = Vector3.one;
        else if (Andar < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Pular
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            Pular();
        }
    }

    private void Pular()
    {
        corpo.linearVelocity = new Vector2(corpo.linearVelocity.x, pulo);
        noChao = false;
        playerAnim.SetBool("Pulando", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            noChao = true;
            playerAnim.SetBool("Pulando", false);
        }
    }
}
