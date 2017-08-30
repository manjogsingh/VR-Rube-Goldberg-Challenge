using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{
    public GameObject emptyCanvas;
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
        try
        {
            objectList[currentObject].SetActive(false);
            currentObject--;
            if (currentObject < 0)
            {
                currentObject = objectList.Count - 1;
            }
            objectList[currentObject].SetActive(true);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            emptyCanvas.SetActive(true);
        }
    }
    public void MenuRight()
    {
        try
        {
            emptyCanvas.SetActive(false);
            objectList[currentObject].SetActive(false);
            currentObject++;
            if (currentObject > objectList.Count - 1)
            {
                currentObject = 0;
            }
            objectList[currentObject].SetActive(true);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            emptyCanvas.SetActive(true);
        }
    }

    public void SpawnCurrentObject()
    {
        if (currentObject > -1)
        {
            try
            {
                Instantiate(objectPrefabList[currentObject], objectList[currentObject].transform.position, objectPrefabList[currentObject].transform.rotation);
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(currentObject).gameObject);
                }
                objectList.Remove(objectList[currentObject]);
                objectPrefabList.Remove(objectPrefabList[currentObject]);
                currentObject--;
                if (currentObject < 0)
                    currentObject = 0;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                emptyCanvas.SetActive(true);
            }
        }
    }
}
