using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    [Header("Configura las 4 palancas")]
    public LeverInteract[] levers = new LeverInteract[4];

    [Header("Direcciones correctas (Up, Down, Left, Right)")]
    public string[] correctDirections = new string[4];

    [Header("Puerta a abrir")]
    public DoorInteractV2 door;

    private string[] currentDirections = new string[4];

    void Start()
    {
        for (int i = 0; i < levers.Length; i++)
        {
            int index = i;
            levers[i].OnLeverMoved += (lever, dir) => OnLeverChanged(index, dir);
            currentDirections[i] = "Center";
        }
    }

    void OnLeverChanged(int index, string newDirection)
    {
        currentDirections[index] = newDirection;
        Debug.Log($"Palanca {index + 1} ? {newDirection}");

        CheckPuzzleSolved();
    }

    void CheckPuzzleSolved()
    {
        for (int i = 0; i < levers.Length; i++)
        {
            if (currentDirections[i] != correctDirections[i])
                return;
        }

        
        Debug.Log("? ¡Puzzle completado! Abriendo puerta...");
        if (door != null)
            door.OpenDoor();
    }
}
