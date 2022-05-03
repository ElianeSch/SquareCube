using UnityEngine;

public class Interrupteur : MonoBehaviour
{
    public GameObject objectToReveal;
    private bool interup = false;

    private void Start()
    {
        objectToReveal.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground")
        {
            objectToReveal.SetActive(true);

            if (other.tag == "ObjetInterrupteur" && interup == false)
            {
                print("coucou !");
                interup = true;
                gameObject.GetComponent<Collider>().enabled = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Ground" && other.tag != "ObjetInterrupteur" && interup == false)
        {
            print("Exit !");
            objectToReveal.SetActive(false);
        }

    }
}
