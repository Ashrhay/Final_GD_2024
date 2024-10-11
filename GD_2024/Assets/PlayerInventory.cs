using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
   public float numberOfShrooms { get; private set; }

    public UnityEvent<PlayerInventory> OnshroomCollected;

    public void shroomCollected()
    {
        numberOfShrooms++;
        OnshroomCollected.Invoke(this);
    }
}
