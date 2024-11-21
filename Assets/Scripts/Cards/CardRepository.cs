using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = nameof(CardRepository), menuName = "Data/" + nameof(CardRepository), order = 0)]
    public class CardRepository : ScriptableObject
    {
        [SerializeField] private CardData[] _allCardData;
        public IEnumerable<CardData> AllCardData => _allCardData;
    }
}