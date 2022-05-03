using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueBox;
    public string[] dialogueText;
    public TextMeshProUGUI textComponent;
    public bool canMoveWhileTalking;

    private void Awake()
    {
        dialogueBox.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Dialogue.instance.lines = dialogueText;
            Dialogue.instance.dialogueBox = dialogueBox;
            Dialogue.instance.textComponent = textComponent;
            Dialogue.instance.textComponent.text = string.Empty;
            Dialogue.instance.canMoveWhileTalking = canMoveWhileTalking;
            Dialogue.instance.StartDialogue();
            Dialogue.instance.dialogueBox.transform.position = Dialogue.instance.cam.WorldToScreenPoint(Dialogue.instance.player.transform.position) + Dialogue.instance.offset;
            gameObject.GetComponent<Collider>().enabled = false;
           
        }
    }

}
