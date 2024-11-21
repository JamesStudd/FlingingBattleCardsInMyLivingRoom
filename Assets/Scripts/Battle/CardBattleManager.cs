using Cards;
using DG.Tweening;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class CardBattleManager : MonoBehaviour
    {
        [SerializeField] private Ease _moveXEase;
        [SerializeField] private float _moveXDuration;
        [SerializeField] private float _xRandomOffsetRange;
        [SerializeField] private Ease _moveYEase;
        [SerializeField] private float _moveYDuration;
        [SerializeField] private float _yRandomOffsetRange;
        [SerializeField] private Ease _scaleEase;
        [SerializeField] private float _scaleDuration;

        [SerializeField] private TMP_Text _indicatorText;

        [SerializeField] private PlayerHand _enemyHand;
        [SerializeField] private PlayerHand _playerHand;
        
        private CardView _playerCard;
        private bool _isPlayersTurn = true;

        private void Start()
        {
            _indicatorText.text = "Players Turn!";
        }

        public void SelectPlayerCard(CardView cardView)
        {
            _playerCard = cardView;
        }

        public void DeselectPlayerCard()
        {
            _playerCard = null;
        }

        public void SelectEnemyCard(CardView enemyCard)
        {
            if (_playerCard != null && _isPlayersTurn)
            {
                FlingCard(_playerCard, enemyCard);

                _isPlayersTurn = false;
                
                _indicatorText.text = "Enemies Turn!";
                Invoke(nameof(EnemyTurn), 0.5f);
            }
        }

        private void EnemyTurn()
        {
            var randomChoice = _enemyHand._cards[Random.Range(0, _enemyHand._cards.Count)];
            var randomTarget = _playerHand._cards[Random.Range(0, _enemyHand._cards.Count)];

            FlingCard(randomChoice, randomTarget);
            
            _isPlayersTurn = true;
            
            _indicatorText.text = "Players Turn!"; 
        }

        private void FlingCard(CardView aggressor, Component defender)
        {
            var instance = Instantiate(aggressor, aggressor.transform.parent);
            instance.GetComponent<LayoutElement>().ignoreLayout = true;
            instance.transform.position = aggressor.transform.position;
            instance.GetComponent<RectTransform>().sizeDelta = aggressor.GetComponent<RectTransform>().sizeDelta;
            instance.Init(aggressor.CardData, null, false);
            
            var position = defender.transform.position;
            
            instance.transform.DOMoveX(position.x, _moveXDuration + Random.Range(-_xRandomOffsetRange, _xRandomOffsetRange)).SetEase(_moveXEase);
            instance.transform.DOMoveY(position.y, _moveYDuration + Random.Range(-_yRandomOffsetRange, _yRandomOffsetRange)).SetEase(_moveYEase);
            instance.transform.DOScale(Vector3.zero, _scaleDuration).SetEase(_scaleEase);
        }
    }
}