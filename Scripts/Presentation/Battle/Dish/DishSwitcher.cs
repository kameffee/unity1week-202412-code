using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202412.Battle
{
    public class DishSwitcher : MonoBehaviour
    {
        [Serializable]
        public class DishData
        {
            public int DishId => _dishId;
            public GameObject DishObject => _dishObject;

            [SerializeField]
            private int _dishId;

            [SerializeField]
            private GameObject _dishObject;
        }

        [SerializeField]
        private List<DishData> _dishDatas;

        public void Switch(int dishId)
        {
            foreach (var dishData in _dishDatas)
            {
                var isActive = dishData.DishId == dishId;
                if (dishData.DishObject.activeSelf == isActive)
                    continue;
                dishData.DishObject.SetActive(isActive);
            }
        }
    }
}