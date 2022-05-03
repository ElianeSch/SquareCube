using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndIntroduction : MonoBehaviour
{


    [SerializeField] private Animator animatorTransition;


    public void Update()
    {
        if (Dialogue.instance.endDialogue == true)
        {
            StartCoroutine(StartLevel1());
        }
    }


    public IEnumerator StartLevel1()
    {
        animatorTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Niveau1");

    }




}
