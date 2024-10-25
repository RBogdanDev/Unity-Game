using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance;
    public delegate void XPChangeHandler(int amount);
    public event XPChangeHandler onXPChange;

    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }

    public void AddXP(int amount){
        onXPChange?.Invoke(amount);
    }
}