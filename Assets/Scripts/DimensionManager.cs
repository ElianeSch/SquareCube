using UnityEngine;
using System;

public class DimensionManager : MonoBehaviour
{

    private int numberOfDimensions; // le nombre de dimensions
    [SerializeField] private Color[] colorDimensions; // la couleur de chacune des dimensions
    private int indiceOfCurrentPlan;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject[] imagesVisu;

    [Serializable]
    public class DimArray
    {
        public GameObject[] dimension;

    }

    public DimArray[] allDimensions;  // contient tous les plans, array d'arrays

    //indiceOfCurrentPlan va de 0 à numberOfDimensions-1;

    private void Awake()
    {
        numberOfDimensions = allDimensions.Length;
        indiceOfCurrentPlan = 0;
        //print(numberOfDimensions);
        //print(allDimensions[0].dimension.Length);
    }

    private void Start()
    {
        ChangeCameraColor();
        for (int i = 0; i < allDimensions[0].dimension.Length; i++)
        {
            ManageObjectsDimension(allDimensions[0].dimension[i], true);
        }

        for (int j = 1; j < numberOfDimensions; j++)
        {
            for (int i = 0; i < allDimensions[j].dimension.Length; i++)
            {
                ManageObjectsDimension(allDimensions[j].dimension[i], false);
            }
        }

        if (imagesVisu.Length != 0)
        {
            foreach (GameObject element in imagesVisu)
                element.SetActive(false);
            imagesVisu[0].SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && numberOfDimensions > 1)
        {
            ChangeViewPlus();
            ChangeCameraColor();
        }

        else if (Input.GetKeyDown(KeyCode.E) && numberOfDimensions > 1)
        {
            ChangeViewMinus();
            ChangeCameraColor();
        }
    }

    private void ChangeViewPlus()
    {
        int indexOfPreviousDimension = indiceOfCurrentPlan;
        indiceOfCurrentPlan += 1;
        if (indiceOfCurrentPlan > numberOfDimensions - 1)
        {
            foreach (GameObject element in imagesVisu)
                element.SetActive(false);

            indiceOfCurrentPlan = 0;
        }
        imagesVisu[indiceOfCurrentPlan].SetActive(true);
        ManageDimensions(indexOfPreviousDimension, indiceOfCurrentPlan);

    }


    private void ChangeViewMinus()
    {
        imagesVisu[indiceOfCurrentPlan].SetActive(false);
        int indexOfPreviousDimension = indiceOfCurrentPlan;
        indiceOfCurrentPlan -= 1;
        if (indiceOfCurrentPlan < 0)
        {
            indiceOfCurrentPlan = numberOfDimensions - 1;
            for (int i = 0; i < indiceOfCurrentPlan + 1; i++)
                imagesVisu[i].SetActive(true);
        }
        ManageDimensions(indexOfPreviousDimension, indiceOfCurrentPlan);
    }

    private void ChangeCameraColor()
    {
        Color color = colorDimensions[indiceOfCurrentPlan];
        mainCamera.backgroundColor = color;

    }


    private void ManageDimensions(int indexOfPreviousDimension, int indexOfCurrentDimension)
    {
        DimArray previousDimension = allDimensions[indexOfPreviousDimension];
        DimArray currentDimension = allDimensions[indexOfCurrentDimension];

       for (int i=0; i< allDimensions[indexOfPreviousDimension].dimension.Length;i++)
        {
            ManageObjectsDimension(allDimensions[indexOfPreviousDimension].dimension[i], false);
            // On parcourt tous les élements de la dimension qu'on vient de quitter
            // On désactive le visuel et on garde le collider
        }


        for (int i = 0; i < allDimensions[indexOfCurrentDimension].dimension.Length; i++)
        {
            ManageObjectsDimension(allDimensions[indexOfCurrentDimension].dimension[i], true);
            // On parcourt tous les élements de la dimension actuelle
            // On active le visuel
        }

    }

    private void ManageObjectsDimension(GameObject element, bool isInCurrentDimension)
    {

        if (element.CompareTag("UniqueDimension"))
        {
            // l'objet n'est présent que dans une dimension, son collider disparaît également quand on quitte sa dimension
            if (isInCurrentDimension)
            {
                element.GetComponent<MeshRenderer>().enabled = true;
                element.GetComponent<Collider>().enabled = true;
            }

            else
            {
                element.GetComponent<MeshRenderer>().enabled = false;
                element.GetComponent<Collider>().enabled = false;
            }
        }

        else
        {
            if (isInCurrentDimension)
            {
                element.GetComponent<MeshRenderer>().enabled = true;
                element.GetComponent<Collider>().enabled = true;
            }

            else
            {
                element.GetComponent<MeshRenderer>().enabled = false;
                element.GetComponent<Collider>().enabled = true;
            }


        }

        
    }


}
