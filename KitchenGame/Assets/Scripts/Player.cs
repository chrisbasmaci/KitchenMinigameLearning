using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

using static DebugUtility;


public class Player : MonoBehaviour, IGameInput
{
    IGameInput GameInput => this;
    private Vector2 _inputVec;
    private float _playerSpeed = 7f;
    private Vector3 _currentMoveDir = new Vector3(0f,0f,0f);
    private Vector3 _lastMoveDir = new Vector3(0f,0f,0f);
    private float _rotateSpeed = 14f;
    private bool _isWalking = false;
    private bool _lockedWalk = false;
    private float _playerSize = .7f;
    private float _playerHeight = 2f;
    private float _interactionDistance = 2f;
    private bool _isHit;
    private RaycastHit _hitObject;
    private ClearCounter _selectedCounter;
    
    public KitchenObject HoldingObject { get; private set; }
    [SerializeField] private Transform holdPoint;
    public Transform HoldPoint => holdPoint;

    [SerializeField] private LayerMask countersLayerMask;
    
    void Start()
    {
        GameController.Instance.SetMainPlayer(this);
        GameInput.Initialize();
    }

    private void Update()
    {
        
        _inputVec = GameInput.UpdateInputVecNormalized();
        GameInput.ListenInteract();
        // Debug.Log("Input Vec Update: " + _inputVec);
        HandleInteractions();
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        MovePlayer();
    }
    
    private void MovePlayer()
    {
        _isWalking = _inputVec != Vector2.zero;


        float moveDistance = _playerSpeed * Time.deltaTime;
        
        //TODO MAYBE
        //Might need to remove using canmove and use ishit instead
        bool canMove = !_isHit;

        if (!canMove)
        { 
            Vector3 moveDirX = new Vector3(_currentMoveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position +Vector3.up * _playerHeight,
                _playerSize,
                moveDirX,
                moveDistance );
            if (canMove)
            {
                _currentMoveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, _currentMoveDir.z).normalized;
                canMove = !Physics.CapsuleCast(
                    transform.position,
                    transform.position +Vector3.up * _playerHeight,
                    _playerSize,
                    moveDirZ,
                    moveDistance );
                if (canMove)
                {
                    _currentMoveDir = moveDirZ;
                }
            }
        }
        
        if(canMove)
        {
            transform.position += _currentMoveDir * Time.deltaTime * _playerSpeed;
            // Debug.Log(_inputVec);
        }


        transform.forward = Vector3.Slerp(transform.forward, _currentMoveDir, Time.deltaTime * _rotateSpeed);
    }
    
    private void HandleInteractions()
    {
        float moveDistance = _playerSpeed * Time.deltaTime;
        _currentMoveDir = new Vector3(_inputVec.x, 0f, _inputVec.y);
        if (_currentMoveDir != Vector3.zero)
        {
            _lastMoveDir = _currentMoveDir;
        }

        _isHit = Physics.CapsuleCast(
            transform.position,
            transform.position +Vector3.up * _playerHeight,
            _playerSize,
            _lastMoveDir,
            out _hitObject,
            moveDistance);
        
        
        
    }

    //override
    bool IGameInput.Interact()
    {
        //Can only selected one thing at a time
        if (_selectedCounter)
        {
            return true;
        }
        
        if (_isHit && _hitObject.transform.TryGetComponent(out _selectedCounter))
        {
            _selectedCounter.Interact(this);
        }

        return true;    
    }

    bool IGameInput.InteractOut()
    {
        if (_selectedCounter)
        {
            _selectedCounter.InteractOut();
            _selectedCounter = null;
            return true;
        }
        return false;
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
    public void HoldObject(KitchenObject toHold)
    {
        if (HoldingObject != null)
        {
            Debug.LogWarning("Already holding: " + HoldingObject.name);
        }
        HoldingObject = toHold;
    }

    public void DropObject()
    {
        HoldingObject = null;
    }


}
