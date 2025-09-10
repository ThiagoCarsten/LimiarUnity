using UnityEngine;

public class MenuPausar : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
           pauseMenu.SetActive(true); 
        }
        
    }
}
