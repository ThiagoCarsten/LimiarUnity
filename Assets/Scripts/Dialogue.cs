using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;
    public float radius;

    public LayerMask playerLayer;

    private DialogueControl dc;
    private bool isPlayerNear = false;
    private bool hasStartedDialogue = false;

    [Header("Character")]
    public Animator animator;           
    public string exitTrigger = "Exit";

    private void Start()
    {
        dc = Object.FindFirstObjectByType<DialogueControl>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CheckPlayerProximity();
    }

    private void CheckPlayerProximity()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        isPlayerNear = hit != null;
    }

    private void Update()
    {
        if (isPlayerNear && !hasStartedDialogue && Input.GetKeyDown(KeyCode.Z))
        {
            dc.Speech(profile, speechText, actorName, this); 
            hasStartedDialogue = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnDialogueFinished()
    {
        if (animator != null)
        {
            animator.SetTrigger(exitTrigger);
        }
    }
}
