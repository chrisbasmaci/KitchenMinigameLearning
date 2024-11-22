using System;
using UnityEngine;


    public interface IGameInput
    {
        void Initialize()
        {
            InitializeDirectionalMovement();
            InitializeKeys();
        }
        void InitializeDirectionalMovement()
        {
            EInputs<Vector2>.Up.SetAction(MoveUp);
            EInputs<Vector2>.Down.SetAction(MoveBack); 
            EInputs<Vector2>.Left.SetAction(MoveLeft);
            EInputs<Vector2>.Right.SetAction(MoveRight);
        }

        void InitializeKeys()
        {
            EInputs<bool>.Interact.SetAction(Interact);
            EInputs<bool>.Interact.SetReleaseAction(InteractOut);
        }

        Vector2 UpdateInputVecNormalized()
        {
            Vector2 inputVec = new Vector2();
            var upInput = EInputs<Vector2>.Up;
            var downInput = EInputs<Vector2>.Down;
            var leftInput = EInputs<Vector2>.Left;
            var rightInput = EInputs<Vector2>.Right;

            inputVec = UpdateInput(inputVec, upInput);
            inputVec = UpdateInput(inputVec, downInput);
            inputVec = UpdateInput(inputVec, leftInput);
            inputVec = UpdateInput(inputVec, rightInput);
            
            return inputVec.normalized;
        }
        Vector2 UpdateInput(Vector2 inputVec, EInputs<Vector2> input)
        {
            if (Input.GetKey(input.Key))
            {
                // DebugUtility.LogWithColor("");
                var vec = input.GetAction().Invoke();
                inputVec.x = vec.x == 0 ? inputVec.x : vec.x;
                inputVec.y = vec.y == 0 ? inputVec.y : vec.y;
                return inputVec;
            }
            return inputVec;
        }
        
        bool ListenInteract()
        {
            var interact = EInputs<bool>.Interact;
            return interact.ListenAction();
        }
        
        Vector2 MoveUp()
        {
            return new Vector2(0, 1);
        }

        Vector2 MoveLeft()
        {
            return new Vector2(-1, 0);
        }

        Vector2 MoveRight()
        {
            return new Vector2(1, 0);
        }

        Vector2 MoveBack()
        {
            return new Vector2(0, -1);
        }

        void Jump()
        {
            DebugUtility.LogSoftAssert("Not Implemented");
        }

        protected virtual bool Interact()
        {
            DebugUtility.LogSoftAssert("Not Implemented");
            return false;
        }
        protected virtual bool InteractOut()
        {
            DebugUtility.LogSoftAssert("Not Implemented");
            return false;
        }
    }
