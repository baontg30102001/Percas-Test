using System;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _widthInput;
    [SerializeField]
    private TMP_InputField _heightInput;
    [SerializeField]
    private Toggle _npcToggle;
    [SerializeField]
    private Toggle _goalToggle;
    [SerializeField]
    private Button _generateButton;
    [SerializeField]
    private Button _pathButton;
    [SerializeField]
    private Button _clearButton;
    [SerializeField] 
    private BoardManager _boardManager;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _generateButton.onClick.AddListener(OnGenerate);
        _pathButton.onClick.AddListener(_boardManager.FindPath);
        _clearButton.onClick.AddListener(_boardManager.ClearEntityAndPath);
        
        _npcToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
                _boardManager.CurrentEditType = CellType.NPC;
        });
        _goalToggle.onValueChanged.AddListener(isOn =>
        {
            if(isOn)
                _boardManager.CurrentEditType = CellType.Goal;
        });
    }

    private void OnGenerate()
    {
        int w = int.Parse(_widthInput.text);
        int h = int.Parse(_heightInput.text);
        
        _boardManager.Width = w;
        _boardManager.Height = h;
        
        _boardManager.GenerateBoard();
    }

    private void OnDestroy()
    {
        _generateButton.onClick.RemoveListener(OnGenerate);
        _pathButton.onClick.RemoveListener(_boardManager.FindPath);
        _clearButton.onClick.RemoveListener(_boardManager.ClearEntityAndPath);
        
        _npcToggle.onValueChanged.RemoveAllListeners();
        _goalToggle.onValueChanged.RemoveAllListeners();
    }
}
