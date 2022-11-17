using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthState : IState
{
    public void Tick()
    {
        Debug.Log("birth tick");
    }

    public void OnEnter()
    {
        Debug.Log("birth onenter");
    }

    public void OnExit()
    {
        Debug.Log("birth onexit");
    }
}
