using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class AverageIntervalCalculator
    {
        private readonly List<float> _receivedTimeInterval = new List<float> {0, 0, 0, 0 , 0};
        private float _lastReceivedTime = 0;
        
        public float GetAverageInterval()
        {
            var receivedTimeIntervalCount = _receivedTimeInterval.Count;
            float sum = 0;
            for (var i = 0; i < receivedTimeIntervalCount; i++)
            {
                sum += _receivedTimeInterval[i];
            }
            
            return sum / receivedTimeIntervalCount;
        }
        
        public void SaveReceivedTime()
        {
            var interval = Time.time - _lastReceivedTime;
            _lastReceivedTime = Time.time;
            _receivedTimeInterval.Add(interval);
            _receivedTimeInterval.RemoveAt(0);
        }
    }
    
    
}