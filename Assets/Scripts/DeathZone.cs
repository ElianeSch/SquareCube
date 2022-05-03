using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
