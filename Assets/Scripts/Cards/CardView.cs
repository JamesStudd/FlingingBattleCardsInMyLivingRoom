using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descText;
        [SerializeField] private Image _cardImage;
        [SerializeField] private GameObject _isSelectedRoot;
        [SerializeField] private GameObject _backOfCard;
        [SerializeField] private GameObject _cardContent;

        public CardData CardData { get; private set; }

        public void Init(CardData cardData, PlayerHand playerHand, bool subscribeToOnClicks = true)
        {
            CardData = cardData;

            _titleText.text = cardData.Title;
            _descText.text = cardData.Desc;
            _cardImage.sprite = cardData.Image;

            _isSelectedRoot.SetActive(false);

            if (!subscribeToOnClicks)
            {
                return;
            }
            
            var local = playerHand.IsLocalPlayer;

            if (local)
            {
                playerHand.OnSelected += HandleNewCardSelected;
                playerHand.OnDeselected += () => _isSelectedRoot.SetActive(false);
                _backOfCard.SetActive(false);
                _cardContent.SetActive(true);
            }
            else
            {
                _backOfCard.SetActive(true);
                _cardContent.SetActive(false);
            }
            
            _selectButton.onClick.AddListener(() =>
            {
                if (_isSelectedRoot.activeSelf)
                {
                    playerHand.Deselect();
                }
                else
                {
                    playerHand.SelectCard(this);
                }
            });
        }

        private void HandleNewCardSelected(CardData newSelected)
        {
            _isSelectedRoot.SetActive(CardData == newSelected);
        }
    }
}