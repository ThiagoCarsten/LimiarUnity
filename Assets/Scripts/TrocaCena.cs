using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void ChangeScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Brasil")
        {
            SceneManager.LoadScene("CasaLis");
        }
        else if (currentScene == "CasaLis")
        {
            SceneManager.LoadScene("Brasil");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Z))
        {
            ChangeScene();
        }
    }
}
