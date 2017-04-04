﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Invector;
public class vOnDeadTrigger : MonoBehaviour {

    public UnityEvent OnDead;
	void Start () {
        vCharacter character = GetComponent<vCharacter>();
        if (character)
            character.onDead.AddListener(OnDeadHandle);
	}
	
	public void OnDeadHandle(GameObject target)
    {
        OnDead.Invoke();
    }
}