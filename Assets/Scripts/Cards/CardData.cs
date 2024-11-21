using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = nameof(CardData), menuName = "Data/" + nameof(CardData), order = 0)]
    public class CardData : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private string _desc;
        [SerializeField] private Sprite _image;

        public string Title => _title;
        public string Desc => _desc;
        public Sprite Image => _image;
    }
}