using System;
using System.Collections.Generic;
using Battle;
using Cards;
using UnityEngine;

namespace Player
{
    public class PlayerHand : MonoBehaviour
    {
        [SerializeField] private bool _isLocalPlayer;
        [SerializeField] private Transform _handRoot;
        [SerializeField] private CardView _cardViewPrefab;
        [SerializeField] private CardRepository _cardRepository;
        [SerializeField] private CardBattleManager _cardBattleManager;

        public event Action<CardData> OnSelected;
        public event Action OnDeselected;

        public bool IsLocalPlayer => _isLocalPlayer;

        private CardView _currentPlayerSelected;

        public List<CardView> _cards;
        
        private void Start()
        {
            foreach (var cardData in _cardRepository.AllCardData)
            {
                var instance = Instantiate(_cardViewPrefab, _handRoot, false);
                instance.Init(cardData, this);
                _cards.Add(instance);
            }
        }

        public void SelectCard(CardView cardView)
        {
            if (_isLocalPlayer)
            {
                OnSelected?.Invoke(cardView.CardData);
                _cardBattleManager.SelectPlayerCard(cardView);    
            }
            else
            {
                _cardBattleManager.SelectEnemyCard(cardView);
            }
        }

        public void Deselect()
        {
            OnDeselected?.Invoke();
            _cardBattleManager.DeselectPlayerCard();
        }
    }
}