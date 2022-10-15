using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;

    [SerializeField] private List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>(); // Lista que contiene todos los niveles que se han creado
    [SerializeField] private List<LevelBlock> currentLevelBlocks = new List<LevelBlock>(); // Lista de los bloques que tenemos ahora mismo en pantalla    
    [SerializeField] Transform levelInitialPoint;

    private bool isGeneratingInitialBlocks = false;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateInitialBlocks();
    }

    public void GenerateInitialBlocks()
    {
        isGeneratingInitialBlocks = true;
        for (int i = 0; i < 3; i++)
        {
            AddNewBlock();
        }
        isGeneratingInitialBlocks = false;
    }

    public void AddNewBlock()
    {
        // Seleccionar un bloque aleatorio entre los que tenemos disponibles
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

        if (isGeneratingInitialBlocks)
        {
            randomIndex = 0;
        }

        LevelBlock block = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
        block.transform.SetParent(this.transform, false);

        // Posición del bloque
        Vector3 blockPosition = Vector3.zero;

        if (currentLevelBlocks.Count == 0)
        {
            // Colocamos el primer bloque en pantalla
            blockPosition = levelInitialPoint.position;
        }
        else
        {
            // Ya hay bloques en pantalla, se empalma en el último disponible
            blockPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }

        block.transform.position = blockPosition;
        currentLevelBlocks.Add(block);
    }

    public void RemoveOldBlock()
    {
        LevelBlock block = currentLevelBlocks[0];
        currentLevelBlocks.Remove(block);
        Destroy(block.gameObject);
    }

    public void RemoveAllTheBlocks()
    {
        while (currentLevelBlocks.Count > 0)
        {
            RemoveOldBlock();
        }
    }
}
