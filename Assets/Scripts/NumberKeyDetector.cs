using Unity.VisualScripting;
using UnityEngine;
using Utilities.Events;

public class NumberKeyDetector : MonoBehaviour
{
    [SerializeField]
    GameEvent numberKeyPressed; 
    void Update()
    {
        // Loop through the number keys (0-9)
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                numberKeyPressed.Raise(i);
            }
        }
    }
}
