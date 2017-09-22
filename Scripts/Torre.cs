using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Torre : MonoBehaviour {

    float posY;

    public GameObject tower;

    public List<GameObject> listOfTowers;

    void Start()
    {
        instantiateTowers();
    }

	void Update () {
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + posY, transform.position.z), Color.red);
    }

    public void instantiateTowers()
    {
        CreateCylinders script = GameObject.Find("Spawner").GetComponent<CreateCylinders>();
        if (script.qnt > 10)
        {
            posY = (((float)script.qnt - 10.0f) / 10.0f) + 1.0f;
        }
        else
        {
            posY = (float)script.qnt / 10.0f;
        }

        GameObject go = Instantiate(tower, this.transform.position, Quaternion.identity) as GameObject;
        go.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.y + posY, go.transform.localScale.z);
        go.name = this.name + " tower";
    }

}
