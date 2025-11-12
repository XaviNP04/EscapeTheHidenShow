using UnityEngine;

public class LeverPuzzleManager : MonoBehaviour
{
    [Header("Configura las 4 palancas")]
    public LeverInteractV2[] levers = new LeverInteractV2[4];

    [Header("Direcciones correctas (Up, Down, Left, Right)")]
    public string[] correctDirections = new string[4];

    [Header("Puerta a abrir")]
    public ResolutionAction obj;

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
            Debug.Log($"Palanca {i + 1}: actual={currentDirections[i]} | correcta={correctDirections[i]}");
            if (currentDirections[i] != correctDirections[i])
                return;
        }

        
        Debug.Log("? ¡Puzzle completado! Abriendo puerta...");
        if (obj != null)
            obj.Action();
    }
}
