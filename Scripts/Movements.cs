using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Movements : MonoBehaviour
{

    public GameObject origem, auxiliar, destino;
    Torre oriTower, auxTower, destTower;
    public List<Steps> listOfSteps;
    public float cooldown, coolDownTimer, currTime;
    public bool start  = false;

    float tempoQueIraDemorar;

    Text tempoToFinish;

    void Start()
    {
        oriTower = origem.GetComponent<Torre>();
        auxTower = auxiliar.GetComponent<Torre>();
        destTower = destino.GetComponent<Torre>();

        listOfSteps = new List<Steps>();

        float numberOfSteps = (Mathf.Pow(2, GameObject.Find("Spawner").GetComponent<CreateCylinders>().qnt) - 1.0f);
        tempoQueIraDemorar = numberOfSteps * cooldown + (Time.deltaTime * numberOfSteps);

        tempoToFinish = GameObject.Find("Tempo").GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hanoi(GameObject.Find("Spawner").GetComponent<CreateCylinders>().qnt, origem.transform, auxiliar.transform, destino.transform);
            resetList();
        }
        if (Input.GetKeyDown(KeyCode.Space) && listOfSteps.Count > 0)
        {
            moveDisco(listOfSteps[0].disco, listOfSteps[0].torre, listOfSteps[0].removeFromThatList, listOfSteps[0].addToThisList);
            listOfSteps.Remove(listOfSteps[0]);
            //listarSteps();
        }

        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            start = true;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            start = false;
        }

        if (coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0)
            coolDownTimer = 0;
        if (coolDownTimer == 0 && start)
        {
            if (listOfSteps.Count > 0)
            {
                moveDisco(listOfSteps[0].disco, listOfSteps[0].torre, listOfSteps[0].removeFromThatList, listOfSteps[0].addToThisList);
                listOfSteps.Remove(listOfSteps[0]);
                coolDownTimer = cooldown;
            }
            else
                start = false;
        }

        if (start)
            currTime = Time.time;

        //tempoToFinish.text = "Time to finish: " + ( Mathf.Round((tempoQueIraDemorar - currTime) * 10) / 10 ).ToString() + " seconds.";
        
    }

    public void hanoi(int n, Transform ori, Transform aux, Transform dest)
    {
        if (n > 0)
        {
            // origem para auxiliar
            hanoi(n - 1, ori, dest, aux);
            createStep(ori.GetComponent<Torre>().listOfTowers[ori.GetComponent<Torre>().listOfTowers.Count - 1], dest.transform, ori.GetComponent<Torre>().listOfTowers, dest.GetComponent<Torre>().listOfTowers);
            //Debug.Log("Mover de " + ori.gameObject.name + " para " + dest.gameObject.name);

            // auxiliar para destino
            hanoi(n - 1, aux, ori, dest);
        }
    }

    public void moveDisco(GameObject disco, Transform torre, List<GameObject> removeFromThatList, List<GameObject> addToThisList)
    {
        Vector3 targetPos = new Vector3(torre.position.x, torre.GetComponent<Torre>().listOfTowers.Count * 0.1f, torre.position.z);
        disco.transform.position = Vector3.Lerp(disco.transform.position, targetPos, 1.0f);
        removeFromThatList.Remove(disco);
        addToThisList.Add(disco);
    }

    void createStep(GameObject disco, Transform torre, List<GameObject> removeFromThatList, List<GameObject> addToThisList)
    {
        Steps step = new Steps(disco, torre, removeFromThatList, addToThisList);
        listOfSteps.Add(step);
        removeFromThatList.Remove(disco);
        addToThisList.Add(disco);
    }

    public void resetList()
    {
        for (int i = 0; i < destTower.listOfTowers.Count; i++)
        {
            oriTower.listOfTowers.Add(destTower.listOfTowers[i]);
        }
        destTower.listOfTowers.Clear();
    }

    void listarSteps()
    {
        for (int i = 0; i < listOfSteps.Count; i++)
        {
            Debug.Log("Step " + i + "\nDisco: " + listOfSteps[i].disco.name + " | Torre: " + listOfSteps[i].torre.name);
        }
    }
}
