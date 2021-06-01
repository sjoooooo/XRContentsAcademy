using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

[Serializable]
public class FurnitureInfo
{
    public float x;
    public float y;
    public float z;
    public float rx;
    public float ry;
    public float rz;
    public float rw;
    public float sx;
    public float sy;
    public float sz;

    public string name;
}
public class FurnitureManager : MonoBehaviour
{
    public Transform placementTarget;
    public GameObject selectedUI;
    public GameObject curSelected;

    private Camera cam;
    public GameObject[] furnifuresPrefabs;

    GameObject[] furnitures;
    public FurnitureInfo[] furnitureInfos;

    private void Start()
    {
        cam = Camera.main;
        selectedUI.SetActive(false);
    }
    public void PlaceFurniture(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, placementTarget.position,
            Quaternion.identity);
        Select(obj);
    }
    void ToggleSelectionVisual(GameObject obj, bool toggle)
    {
        obj.transform.Find("Selected").gameObject.SetActive(toggle);
    }

    void Select(GameObject selected)
    {
        selectedUI.SetActive(true);
        if (curSelected != null)
        {
            ToggleSelectionVisual(curSelected, false);
        }
        curSelected = selected;
        ToggleSelectionVisual(curSelected, true);
        furntureInfoUpLoad();
    }
    void DeSelect()
    {
        if (curSelected != null)
        {
            ToggleSelectionVisual(curSelected, false);
        }
        curSelected = null;
        selectedUI.SetActive(false);
    }
    public void ScaleSelected(float rate)
    {
        curSelected.transform.localScale += Vector3.one * rate;
    }
    public void RotateSelected(float rate)
    {
        curSelected.transform.eulerAngles += Vector3.up * rate;
    }
    public void MoveXSelected(float rate)
    {
        curSelected.transform.Translate(0, 0, rate);
    }
    public void MoveYSelected(float rate)
    {
        curSelected.transform.Translate(0, rate, 0); ;
    }
    public void MoveZSelected(float rate)
    {
        curSelected.transform.Translate(rate, 0, 0);
    }
    public void DeleteSelected()
    {
        Destroy(curSelected);
        DeSelect();
    }
    public void SetColor(Image buttonImage)
    {
        MeshRenderer[] meshRenderers =
            curSelected.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in meshRenderers)
        {
            if (mr.gameObject.name == "Selected")
                continue;
            mr.material.color = buttonImage.color;
        }
    }

    private void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit " + raycastHit.collider.name);
                if (raycastHit.collider.tag == "Furniture")
                {
                    Select(raycastHit.collider.gameObject);
                }

            }
        }
    }

    void furntureInfoUpLoad()
    {
        furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        furnitureInfos = new FurnitureInfo[furnitures.Length];
        for (int i = 0; i < furnitureInfos.Length; i++)
            furnitureInfos[i] = new FurnitureInfo();
        Debug.Log(furnitureInfos.Length);
        Debug.Log(furnitureInfos[0]);
        if (furnitures != null)
            for (int index = 0; index < furnitures.Length; index++)
            {
                furnitureInfos[index].x = furnitures[index].transform.position.x;
                Debug.Log(furnitures[index].transform.position.x);
                furnitureInfos[index].y = furnitures[index].transform.position.y;
                furnitureInfos[index].z = furnitures[index].transform.position.z;
                furnitureInfos[index].rx = furnitures[index].transform.rotation.x;
                furnitureInfos[index].ry = furnitures[index].transform.rotation.y;
                furnitureInfos[index].rz = furnitures[index].transform.rotation.z;
                furnitureInfos[index].rw = furnitures[index].transform.rotation.w;
                furnitureInfos[index].sx = furnitures[index].transform.localScale.x;
                furnitureInfos[index].sy = furnitures[index].transform.localScale.y;
                furnitureInfos[index].sz = furnitures[index].transform.localScale.z;
                furnitureInfos[index].name = furnitures[index].name.Replace("(Clone)", "");
            }

    }
    public void Save()
    {
        furntureInfoUpLoad();

        string toJson = JsonHelper.ToJson(furnitureInfos);

        File.WriteAllText(Application.persistentDataPath + "/data.json", toJson);
    }

    public void Load()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + "/data.json");
        var data = JsonHelper.FromJson<FurnitureInfo>(jsonString);
        foreach (var furnitureInfo in data)
        {
            GameObject furnifuresPrefab;
            foreach (GameObject ele in furnifuresPrefabs)
            {
                if (furnitureInfo.name == ele.name)
                {
                    furnifuresPrefab = ele;
                    GameObject furniture = Instantiate(furnifuresPrefab, new Vector3(furnitureInfo.x, furnitureInfo.y, furnitureInfo.z),
                    Quaternion.identity);
                    furniture.transform.rotation = new Quaternion(furnitureInfo.rx, furnitureInfo.ry, furnitureInfo.rz, furnitureInfo.rw);
                    furniture.transform.localScale = new Vector3(furnitureInfo.sx, furnitureInfo.sy, furnitureInfo.sz);
                }
            }
        }


    }
}
