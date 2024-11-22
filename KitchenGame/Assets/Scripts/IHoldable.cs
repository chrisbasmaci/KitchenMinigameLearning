using System;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IHoldable<T>
    {
        public void HoldObject(T toHold);
        public Transform HoldPoint();
        public void DropObject();
        public T HoldingObject();

        public bool HasObjectOnTop => HoldingObject() != null;

        public String Name();

    }

    
}