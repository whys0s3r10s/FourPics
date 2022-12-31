using System;
using System.Collections.Generic;
using UnityEngine;

namespace FourPics
{
    [Serializable]
    [CreateAssetMenu(fileName = "Shop", menuName = "Content Data/Shop")]
    public class ShopContentData : ScriptableObject
    {   
        public List<ProductData> Products;        
    }
}