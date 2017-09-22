using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour {

    GameObject[] menuItems;

    void Start()
    {
        menuItems = GameObject.FindGameObjectsWithTag("Menu item");
        showHideMenuItems();
    }

	public void addZoom()
    {
        Transform mainC = GameObject.Find("Main Camera").GetComponent<Transform>();
        mainC.localPosition = new Vector3(mainC.localPosition.x - 0.5f, mainC.localPosition.y, mainC.localPosition.z);
    }
    public void minusZoom()
    {
        Transform mainC = GameObject.Find("Main Camera").GetComponent<Transform>();
        mainC.localPosition = new Vector3(mainC.localPosition.x + 0.5f, mainC.localPosition.y, mainC.localPosition.z);
    }
    public void showHideMenuItems()
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            if(menuItems[i].name.Equals("text quantidade aneis"))
                menuItems[i].GetComponent<Text>().text = "Quantidade de aneis: " + GameObject.Find("Spawner").GetComponent<CreateCylinders>().qnt.ToString();
            menuItems[i].active = !menuItems[i].activeSelf;
        }
    }
    public void addSpaceBetweenTower()
    {
        GameObject ori = GameObject.Find("Origem tower");
        GameObject dest = GameObject.Find("Destino tower");
        ori.transform.localPosition = new Vector3(ori.transform.localPosition.x, ori.transform.localPosition.y, ori.transform.localPosition.z - 0.1f);
        dest.transform.localPosition = new Vector3(dest.transform.localPosition.x, dest.transform.localPosition.y, dest.transform.localPosition.z + 0.1f);
        rePosAneis();
        GameObject.Find("Origem").transform.position = ori.transform.position;
        GameObject.Find("Destino").transform.position = dest.transform.position;
    }
    public void minusSpaceBetweenTower()
    {
        GameObject ori = GameObject.Find("Origem tower");
        GameObject dest = GameObject.Find("Destino tower");
        ori.transform.localPosition = new Vector3(ori.transform.localPosition.x, ori.transform.localPosition.y, ori.transform.localPosition.z + 0.1f);
        dest.transform.localPosition = new Vector3(dest.transform.localPosition.x, dest.transform.localPosition.y, dest.transform.localPosition.z - 0.1f);
        rePosAneis();
        GameObject.Find("Origem").transform.position = ori.transform.position;
        GameObject.Find("Destino").transform.position = dest.transform.position;
    }
    public void addAneis()
    {
        int qnt = GameObject.Find("Spawner").GetComponent<CreateCylinders>().qnt++;
        GameObject.Find("text quantidade aneis").GetComponent<Text>().text = "Quantidade de aneis: " + GameObject.Find("Spawner").GetComponent<CreateCylinders>().qnt.ToString();
        reSpawnAneis(qnt, "add");
    }
    public void minusAneis()
    {
        if (GameObject.Find("Spawner").GetComponent<CreateCylinders>().qnt > 3)
        {
            int qnt = GameObject.Find("Spawner").GetComponent<CreateCylinders>().qnt--;
            GameObject.Find("text quantidade aneis").GetComponent<Text>().text = "Quantidade de aneis: " + GameObject.Find("Spawner").GetComponent<CreateCylinders>().qnt.ToString();
            reSpawnAneis(qnt, "minus");
        }
    }
    public void automaticStep()
    {
        Movements movObj = GameObject.Find("Movements").GetComponent<Movements>();

        GameObject[] aneis = GameObject.FindGameObjectsWithTag("Anel");
        if (aneis.Length == 0)
            spawnAneis();
        try {
            GameObject[] menuItems = GameObject.FindGameObjectsWithTag("Menu item");
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].SetActive(false);
            }
            GameObject.Find("Menu").SetActive(false);
            GameObject.Find("Spawn").SetActive(false);
        }
        catch (System.Exception e) {
            
        }
        movObj.start = !movObj.start;
    }
    public void nextStep()
    {
        Movements movObj = GameObject.Find("Movements").GetComponent<Movements>();

        GameObject[] aneis = GameObject.FindGameObjectsWithTag("Anel");
        if (aneis.Length == 0)
            spawnAneis();
        try
        {
            GameObject[] menuItems = GameObject.FindGameObjectsWithTag("Menu item");
            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].SetActive(false);
            }
            GameObject.Find("Menu").SetActive(false);
            GameObject.Find("Spawn").SetActive(false);
        }
        catch (System.Exception e)
        {

        }

        movObj.moveDisco(movObj.listOfSteps[0].disco, movObj.listOfSteps[0].torre, movObj.listOfSteps[0].removeFromThatList, movObj.listOfSteps[0].addToThisList);
        movObj.listOfSteps.Remove(movObj.listOfSteps[0]);
    }
    public void restartScene()
    {
        SceneManager.LoadScene("Scene");
    }

    private void reSpawnAneis(int qnt, string addOrMinus)
    {
        // Deleta todos aneis existentes
        GameObject[] aneis = GameObject.FindGameObjectsWithTag("Anel");
        for (int i = 0; i < aneis.Length; i++)
        {
            Destroy(aneis[i].gameObject);
        }

        // Aumenta tamanho das torres
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        for (int i = 0; i < towers.Length; i++)
        {
            if(addOrMinus.Equals("add"))
                towers[i].transform.localScale = new Vector3(towers[i].transform.localScale.x, towers[i].transform.localScale.y + 0.1f, towers[i].transform.localScale.z);
            else
                towers[i].transform.localScale = new Vector3(towers[i].transform.localScale.x, towers[i].transform.localScale.y - 0.1f, towers[i].transform.localScale.z);
        }

        // Recria aneis
        spawnAneis();
    }
    private void rePosAneis()
    {
        Transform oriTower = GameObject.Find("Origem tower").transform;
        GameObject[] aneis = GameObject.FindGameObjectsWithTag("Anel");
        for (int i = 0; i < aneis.Length; i++)
        {
            aneis[i].transform.localPosition = new Vector3(aneis[i].transform.localPosition.x, aneis[i].transform.localPosition.y, oriTower.position.z);
        }
    }
    public void spawnAneis()
    {
        CreateCylinders obj = GameObject.Find("Spawner").GetComponent<CreateCylinders>();
        obj.setCylinderScale();
        obj.spawnCylinders(obj.cylinder, obj.qnt);

        Movements objHanoi = GameObject.Find("Movements").GetComponent<Movements>();
        objHanoi.listOfSteps.Clear();
        objHanoi.hanoi(obj.qnt, objHanoi.origem.transform, objHanoi.auxiliar.transform, objHanoi.destino.transform);
        objHanoi.resetList();
    }
}
