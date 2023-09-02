using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.input;
using pong.helpers;
using System;
using pong.config;
using System.Linq;

namespace pong.core
{
    public class BonusController 
    {
        private readonly Paddle _leftPaddle;
        private readonly Paddle _rightPaddle;
        private readonly IInput _input;
        private readonly Level _level;
        private List<BonusView> _bonusViews;
        private GameTimer _gameTimer;
        private readonly BonusConfig _bonusConfig;
        private List<Bonus> _bonuses;
        private BonusType _lastBonus;

        public event Action<BonusType> OnApplyBonus;

        public BonusController(BonusConfig bonusConfig, Paddle leftPaddle, Paddle rightPaddle, Level level, IInput input)
        {
            _bonusConfig = bonusConfig;            
            _leftPaddle = leftPaddle;
            _rightPaddle = rightPaddle;
            _input = input;
            _level = level;

            _bonusViews = new List<BonusView>();
            AddBonuses();
            _lastBonus = GetRandomBonus();

            _gameTimer = new GameTimer(_bonusConfig.SpawnPeriod);
        }

        private void AddBonuses()
        {
            _bonuses = new List<Bonus>();

            _bonuses.Add(new InvertControlBonus(BonusType.InvertedControls, _input));
            _bonuses.Add(new PaddleSizeBonus(BonusType.UpPaddleSize, _leftPaddle, _rightPaddle, 1f, _bonusConfig.UpPaddleSizeMult));
            _bonuses.Add(new PaddleSizeBonus(BonusType.DownPaddleSize, _leftPaddle, _rightPaddle, 1f, _bonusConfig.DownPaddleSizeMult));
        }

        public void StartSpawnBonuses()
        {            
            _gameTimer.Start();
            _gameTimer.OnTargetTime += SpawnBonus;
        }

        public void Stop()
        {
            _gameTimer.OnTargetTime -= SpawnBonus;
            _gameTimer.Stop();

            ClearViews();            
        }

        private void SpawnBonus()
        {
            ClearViews();

            BonusView bonusView = _level.SpawnBonus();
            _lastBonus = GetRandomBonus(_lastBonus);
            bonusView.Construct(_lastBonus);
            bonusView.OnCollect += ApplyBonus;

            _bonusViews.Add(bonusView);            
            _gameTimer.Start();
        }

        private BonusType GetRandomBonus()
        {
            int num = UnityEngine.Random.Range(0, _bonusConfig.BonusTypes.Count);
            return _bonusConfig.BonusTypes[num];
        }

        private BonusType GetRandomBonus(BonusType exeption)
        {
            int num = 0;

            while(_bonusConfig.BonusTypes[num] == exeption)
            {
                num = UnityEngine.Random.Range(0, _bonusConfig.BonusTypes.Count);
            }

            return _bonusConfig.BonusTypes[num];
        }

        public void ClearViews()
        {
            if (_bonusViews.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _bonusViews.Count; i++)
            {
                ClearView(_bonusViews[i]);
            }

            _bonusViews.Clear();            
        }

        private void ClearView(BonusView bonusView)
        {
            bonusView.OnCollect -= ApplyBonus;
            bonusView.Clear();            
        }

        public void ApplyBonus(BonusView bonusView)
        {           
            ClearBonuses();
            OnApplyBonus?.Invoke(bonusView.BonusType);
            Bonus bonus = _bonuses.FirstOrDefault(bonus => bonus.BonusType == bonusView.BonusType);

            if (bonus != null)
            {
                bonus.SwitchBonus(true);
            }
                        
            ClearView(bonusView);
            _bonusViews.Remove(bonusView);            
        }
        
        public void ClearBonuses()
        {
            if (_bonuses == null || _bonuses.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _bonuses.Count; i++)
            {
                _bonuses[i].SwitchBonus(false);
            }
        }        
    }
}

