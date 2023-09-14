using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKINCOUNT : MonoBehaviour 
{
    public void IncrementIndex(int index) 
    {
        GameController.instance.ChangeSkin(index);
    }

    public void ChangeScene(int Scene) 
    {
        GameController.instance.ChangeScene(Scene);
    }

}
