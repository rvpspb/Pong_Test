using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace pong.core
{
    public class BonusView : MonoBehaviour
    {
        public BonusType BonusType { get; private set; }

        public event Action<BonusView> OnCollect;

        public void Construct(BonusType bonusType)
        {
            BonusType = bonusType;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BallView ballView))
            {
                OnCollect?.Invoke(this);
            }
        }

        public void Clear()
        {
            Destroy(gameObject);
        }
    }
}
