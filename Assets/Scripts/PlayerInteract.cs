using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private Animator m_Animator;
    private Camera _camera;
    [SerializeField]
    private float distance = 6f;
    [SerializeField]
    private LayerMask _layerMask;
    private PlayerUI _playerUI;
    private InputManager _inputManager;
    void Start()
    {
        _camera = GetComponent<PlayerLook>().camera;
        _playerUI = GetComponent<PlayerUI>();
        _inputManager = GetComponent<InputManager>();
        m_Animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        _playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, _layerMask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                _playerUI.UpdateText(interactable.promptMes);
                if(_inputManager.onFootActions.Interact.triggered)
                {
                    Debug.Log("Attack");
                    interactable.BaseInteract();
                    _inputManager.TriggerAttack();
                }
            }
        }
    }

}
