using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public TMP_Text speechText;
    public TMP_Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;

    private Dialogue currentDialogue; 

    public void Speech(Sprite p, string[] txt, string actorName, Dialogue npcRef)
    {
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        speechText.text = "";
        index = 0;
        currentDialogue = npcRef; 
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                // acabou o diálogo
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);

                // avisa o NPC que terminou
                if (currentDialogue != null)
                {
                    currentDialogue.OnDialogueFinished();
                }

                currentDialogue = null; // limpa referência
            }
        }
    }
}
