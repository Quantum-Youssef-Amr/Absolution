using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GetGridPos : MonoBehaviour
{
    [SerializeField] private Tilemap Map;
    [SerializeField] private Base_AI _AI;
    private Camera _mainCamera;

    public Action<Vector2> OnGridClickEvent;

    #region inputs
    private newInputSystem _inputs;
    void OnEnable() => _inputs.Enable();
    void OnDisable() => _inputs.Disable();
    #endregion

    void Awake()
    {
        _inputs = new();
        _mainCamera = Camera.main;
    }


    void Start()
    {
        _inputs.Player.MousePosIfClick.started += mousePos =>
        {
            Vector3 m_WorldPos = GetMouseGridPos(_mainCamera.ScreenToWorldPoint(mousePos.ReadValue<Vector2>()));
            _AI.GetToTile(m_WorldPos);
        };
    }

    private Vector3 GetMouseGridPos(Vector3 mouseWorldPos)
    {
        return Map.GetCellCenterWorld(Map.WorldToCell(mouseWorldPos));
    }
}
