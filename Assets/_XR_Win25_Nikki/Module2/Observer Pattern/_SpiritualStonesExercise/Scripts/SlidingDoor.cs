using System.Collections;
using System.Net.Sockets;
using UnityEngine;


public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Transform openDooraPosition;
    [SerializeField] private Transform closeDoorPosition;
    [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    [Tooltip("Slide movement duration in seconds")]
    [SerializeField] private float slideDuration = 5.0f;


    [SerializeField] StoneSocket fireStoneSocket;
    [SerializeField] StoneSocket waterStoneSocket;
    [SerializeField] StoneSocket forestStoneSocket;

    private Coroutine doorSlideCoroutine;


    private void OnEnable()
    {
        //Subscribing to events 
        StoneSocket.OnAllStonesPlaced += Open;
        //waterStoneSocket.OnAllStonesPlaced += Open;
        //forestStoneSocket.OnAllStonesPlaced += Open;
                               
    }

    private void OnDisable()
    {
        //Unsubscribing to events 
        StoneSocket.OnAllStonesPlaced -= Open;
        //waterStoneSocket.OnAllStonesPlaced -= Open;
        //forestStoneSocket.OnAllStonesPlaced -= Open;

    }
    void Start()
        {
            Close();
        }

        IEnumerator SlideDoor(Vector3 endPosition)
        {
            Vector3 startPosition=transform.position;
            float elapsedTime=0;

            while (elapsedTime<slideDuration)
            {
                elapsedTime+=Time.deltaTime;
                float percentageComplete = elapsedTime / slideDuration;

                transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percentageComplete));
                yield return null;
            }
            transform.position = endPosition; // Ensure the exact target position is reached
        }

        [ContextMenu("Open")] // This allows running the function from the Editor to test it (dotStack Menu next to Component Name). Only works for functions with no parameters.
        private void Open(StoneSocket socket)
        {
            StopDoorSlideCoroutine(); // Stop any existing coroutine to avoid conflicts
            doorSlideCoroutine = StartCoroutine(SlideDoor(openDooraPosition.position));
            Debug.Log("Object that triggered the event is " + socket.gameObject.name);
        }

    [ContextMenu("Close")] 
        public void Close()
        {
            StopDoorSlideCoroutine();
            doorSlideCoroutine = StartCoroutine(SlideDoor(closeDoorPosition.position));
        }

        void StopDoorSlideCoroutine()
        {
            if (doorSlideCoroutine != null)
            {
                StopCoroutine(doorSlideCoroutine);
                doorSlideCoroutine = null;
            }
        }
}


