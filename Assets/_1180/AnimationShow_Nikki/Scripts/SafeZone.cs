using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private Animator parasiteAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parasiteAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        parasiteAnim.SetTrigger("Player Hide");
    }

}
