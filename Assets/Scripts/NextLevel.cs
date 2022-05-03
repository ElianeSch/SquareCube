using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string LevelToLoad;
    [SerializeField] private Animator animatorTransition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            CharacterMovement.instance.isFreezed = true;
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        animatorTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(LevelToLoad);
        CharacterMovement.instance.isFreezed = false;

    }
}
