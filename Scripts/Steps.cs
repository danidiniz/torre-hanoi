using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Steps {

    public GameObject disco;
    public Transform torre;
    public List<GameObject> removeFromThatList;
    public List<GameObject> addToThisList;

    public Steps(GameObject _disco, Transform _torre, List<GameObject> _removeFromThat, List<GameObject> _addToThis)
    {
        disco = _disco;
        torre = _torre;
        removeFromThatList = _removeFromThat;
        addToThisList = _addToThis;
    }
}
