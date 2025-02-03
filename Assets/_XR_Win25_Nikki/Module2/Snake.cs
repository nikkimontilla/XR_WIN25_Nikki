using UnityEngine;

public class Snake : Animal
{
    private Color Color = Color.green;

    private void Start()
    {
        Name = "Snake";
    }
    public override void MakeSound()
    {
        Debug.Log("Snake hisses");
    }
}
