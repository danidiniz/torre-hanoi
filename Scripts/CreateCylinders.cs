using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateCylinders : MonoBehaviour {

    public GameObject cylinder;
    public int qnt;
    public GameObject origem;

    void Start()
    {
        //spawnCylinders(cylinder, qnt);
    }

    public void spawnCylinders(GameObject obj, int qnt)
    {
        origem.GetComponent<Torre>().listOfTowers.Clear();
        Transform oriTower = GameObject.Find("Origem tower").GetComponent<Transform>();
        float posIni = 0.0f;
        float tamIni = 0.0f;
        List<Color> colorsUsed = new List<Color>();
        for (int i = 1; i <= qnt; i++)
        {
            GameObject cylinder = Instantiate(obj, new Vector3(oriTower.position.x, posIni, oriTower.position.z), Quaternion.identity) as GameObject;
            cylinder.transform.localScale = new Vector3(cylinder.transform.localScale.x - tamIni, 0.01f, cylinder.transform.localScale.z - tamIni);
            cylinder.gameObject.name = "Cylinder " + i.ToString();
            posIni += 0.1f;
            tamIni += 0.1f;
            origem.GetComponent<Torre>().listOfTowers.Add(cylinder);
            Color randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            while (colorsUsed.Contains(randomColor))
            {
                randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            }
            colorsUsed.Add(randomColor);
            cylinder.GetComponent<Renderer>().material.color = randomColor;
        }
    }

    public void setCylinderScale()
    {
        if (qnt > 10)
        {
            float soma = ((float)qnt - 10.0f) / 10.0f;
            cylinder.transform.localScale = new Vector3(1.0f + soma, 0.01f, 1.0f + soma);
        }
    }
}
