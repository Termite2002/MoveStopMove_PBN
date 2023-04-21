using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private Character charater;
    
    public void Nem()
    {
        charater.Throw();
    }
}
