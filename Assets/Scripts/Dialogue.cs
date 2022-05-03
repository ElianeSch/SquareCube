using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    public static Dialogue instance;
    public GameObject dialogueBox;

    public bool dialogueHasStarted = false;
    public bool canMoveWhileTalking = true;
    public bool endDialogue = false;

    public Camera cam;


    [Header("Position of the dialogue Box")]
    public GameObject player;
    public Rigidbody rb;
    public Vector3 posDialogueBox;
    public Vector3 offset;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("Plus d'une instance de Dialogue dans la scène !");
        else
            instance = this;


    }

    private void Start()
    {
        posDialogueBox = cam.WorldToScreenPoint(player.transform.position);
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (dialogueHasStarted && canMoveWhileTalking && (Input.GetAxisRaw("Horizontal") != 0 || rb.velocity.y != 0))

        {
            posDialogueBox = cam.WorldToScreenPoint(player.transform.position);
            dialogueBox.transform.position = posDialogueBox + offset;

        }
        
        if (Input.GetKeyDown(KeyCode.A) && dialogueHasStarted)
        {
            if (textComponent.text == lines[index])
                NextLine();
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    
    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        dialogueHasStarted = true;
        textComponent.text = "";
        index = 0;
        if (canMoveWhileTalking == false) CharacterMovement.instance.isFreezed = true;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else
        {
            dialogueBox.SetActive(false);
            dialogueHasStarted = false;
            CharacterMovement.instance.isFreezed = false;
            endDialogue = true;
        }
    }


    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

}