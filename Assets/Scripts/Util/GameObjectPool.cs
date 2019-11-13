﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    public GameObject BaseObject {get; private set;}
    public Transform Parent {get;private set;}
    private Stack<GameObject> _inactive = new Stack<GameObject>();
    private List<GameObject> _active = new List<GameObject>();

    public GameObjectPool(GameObject baseObject,Transform parent){
        BaseObject = baseObject;
        Parent = parent;
    }

    public void Retire(GameObject gameObject){
        if(!gameObject.activeSelf)
            Debug.LogWarning("Retiring inactive object");

        _active.Remove(gameObject);
        _inactive.Push(gameObject);
    }

    public void RetireAll(){
        foreach(var obj  in _active){
            obj.SetActive(false);
            _inactive.Push(obj);
        }

        _active.Clear();
    }

    public GameObject Request(){
        if(_inactive.Count == 0){
            var newObj = UnityEngine.Transform.Instantiate(BaseObject,Parent);
            _active.Add(newObj);
            return newObj;
        }

        var obj = _inactive.Pop();
        _active.Add(obj);
        obj.SetActive(true);
        return obj;
    }

}
