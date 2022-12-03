using UnityEngine;

public class KeyCodeTestTemplate : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Press A Done");   
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Press S Done");
        }
    }
}
