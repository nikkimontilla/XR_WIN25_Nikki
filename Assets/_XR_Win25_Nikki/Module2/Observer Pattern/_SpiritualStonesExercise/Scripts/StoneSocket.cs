using System;
using UnityEngine;


    public class StoneSocket : MonoBehaviour
    {
        [Tooltip("Drag one of the three spiritual stones in here")] //This allows us to display some text when hovering over the variable name in the editor.
        [SerializeField] private GameObject stoneReference;
        [SerializeField] private string stoneTag;

        private bool isOccupied = false;

        static int numberOfStonePlace = 0;

        [SerializeField] private SlidingDoor slidingDoor;
        [SerializeField] private AudioSource audioSource;   

    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(stoneTag))
            {
                isOccupied = true;
                numberOfStonePlace++;

                Debug.Log($"{numberOfStonePlace} number of stone placed");

                Debug.Log($"{other.gameObject.name} gameobject entered the trigger");
            }

            if (numberOfStonePlace == 3)
            {
                slidingDoor.Open();
                audioSource.Play();
             }
        }

        private void OnTriggerExit(Collider other)
        {
        if (other.gameObject.CompareTag(stoneTag))
        {
            isOccupied = false;
            numberOfStonePlace--;

            Debug.Log($"{numberOfStonePlace} number of stone placed");

            Debug.Log($"{other.gameObject.name} gameobject exited the trigger");
        }
    }
}


