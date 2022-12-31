using System;
using System.Collections.Generic;
using UnityEngine;

namespace FourPics
{
    public class HintsControl : MonoBehaviour
    {
        [SerializeField]
        private List<HintItem> hintItems;

        public Action<HintType> OnHintClicked;

        public void UpdateRender(Dictionary<HintType, int> hintPrices)
        {
            if (hintPrices == null)
                throw new ArgumentNullException(nameof(hintPrices));

            foreach (HintItem hintItem in hintItems)
            {
                hintItem.Initialize(OnHintClicked, hintPrices[hintItem.HintType]);
            }
        }
    }
}