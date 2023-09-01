using Cysharp.Threading.Tasks;
using System;

namespace pong.helpers
{
    public class GameTimer
    {
        public float TickPeriod { get; private set; }
        public float CurrentTime { get; private set; }
        public float TargetTime { get; private set; }
        public bool IsRunning { get; private set; }

        public event Action OnTargetTime;

        public GameTimer(float targetTime, float tickPeriod)
        {
            TickPeriod = tickPeriod;
            TargetTime = targetTime;
        }

        public void SetTargetTime(float time)
        {
            TargetTime = time;
        }

        public void Start()
        {
            CurrentTime = 0;
            IsRunning = true;

            Tick();
        }

        public void Stop()
        {
            CurrentTime = 0;
            IsRunning = false;
        }

        private async UniTask Tick()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(TickPeriod));
            CurrentTime += TickPeriod;

            if (!IsRunning)
            {
                return;
            }

            if (CurrentTime >= TargetTime)
            {
                OnTargetTime?.Invoke();
                Stop();
                return;
            }

            Tick();
        }
    }
}
