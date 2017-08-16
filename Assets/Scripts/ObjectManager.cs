using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{

    public List<GameObject> objectList;
    public List<GameObject> objectPrefabList;
    public int currentObject = 0;

    //public Text message;
    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            objectList.Add(child.gameObject);
        }
    }

    public void MenuLeft()
    {
        objectList[currentObject].SetActive(false);
        currentObject--;
        if (currentObject < 0)
        {
            currentObject = objectList.Count - 1;
        }
        objectList[currentObject].SetActive(true);
    }
    public void MenuRight()
    {
        objectList[currentObject].SetActive(false);
        currentObject++;
        if (currentObject > objectList.Count - 1)
        {
            currentObject = 0;
        }
        objectList[currentObject].SetActive(true);
    }

    public void SpawnCurrentObject()
    {
        if (currentObject > -1)
        {
            Instantiate(objectPrefabList[currentObject], objectList[currentObject].transform.position, objectList[currentObject].transform.rotation);
            // foreach (Transform child in transform)
            // {
            //     if (child.name.Equals(objectList[currentObject].name))
            //     {
            //         Destroy(child.gameObject);
            //     }
            // }
            for(int i=0;i<transform.childCount;i++)
            {
                Destroy(transform.GetChild(currentObject).gameObject);
            }
            objectList.Remove(objectList[currentObject]);
            objectPrefabList.Remove(objectPrefabList[currentObject]);
            currentObject--;
            if(currentObject<0)
                currentObject=0;
        }
    }
}
