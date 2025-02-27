using System;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    [SerializeField] StoneSocket fireStoneSocket;
    [SerializeField] StoneSocket waterStoneSocket;
    [SerializeField] StoneSocket forestStoneSocket;


    private void OnEnable()
    {
        //Subscribing to events 
        StoneSocket.OnAllStonesPlaced += Disappear;
        //waterStoneSocket.OnAllStonesPlaced += Disappear;
        //forestStoneSocket.OnAllStonesPlaced += Disappear;

    }

    private void OnDisable()
    {
        //Unsubscribing to events 
        StoneSocket.OnAllStonesPlaced -= Disappear;
        //waterStoneSocket.OnAllStonesPlaced -= Disappear;
        //forestStoneSocket.OnAllStonesPlaced -= Disappear;

    }

    private void Disappear(StoneSocket socket)
    {
        gameObject.SetActive(false);
    }
}
