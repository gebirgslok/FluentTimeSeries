using System;
using System.Linq;

namespace FluentTimeSeries.Internal.Signals.Composite
{
    internal class CompositeTimeSeries : ITimeSeries
    {
        private readonly IAccumulatorFunction[] _accumulatorFunctions;

        private bool _isInitialized;
        private DateTime _start;
        private DateTime _prev;

        public CompositeTimeSeries(IAccumulatorFunction[] accumulatorFunctions)
        {
            _accumulatorFunctions = accumulatorFunctions;
        }

        private double CalculateValue(DateTime timestampOrNow)
        {
            return _accumulatorFunctions.Aggregate(0.0, (current, f) => f.Next(current, timestampOrNow));
        }

        public DataPoint Next(DateTime? timestamp = null)
        {
            var timestampOrNow = timestamp ?? DateTime.Now;

            if (_isInitialized == false)
            {
                _start = timestampOrNow;
                _isInitialized = true;
            }

            var value = CalculateValue(timestampOrNow);

            var startOffset = timestampOrNow - _start;
            var prevOffset = timestampOrNow - _prev;
            _prev = timestampOrNow;
            return new DataPoint(value, timestampOrNow, startOffset, prevOffset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="samplingInterval">The <c>sampling interval</c> in seconds.</param>
        /// <param name="origin"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DataPoint[] Block(int length, double samplingInterval, DateTime? origin)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, $"{nameof(length)} must be > 0.");
            }

            var originOrNow = origin ?? DateTime.Now;

            var dataPoints = new DataPoint[length];

            var constantPrevOffset = TimeSpan.FromSeconds(samplingInterval);

            for (var i = 0; i < length; i++)
            {
                var elapsedSeconds = i * samplingInterval;
                var timestamp = originOrNow.AddSeconds(elapsedSeconds);
                var value = CalculateValue(timestamp);

                dataPoints[i] = new DataPoint(value,
                    timestamp, i == 0 ? TimeSpan.Zero : TimeSpan.FromSeconds(elapsedSeconds),
                    i == 0 ? TimeSpan.Zero : constantPrevOffset);
            }

            return dataPoints;
        }

        public DataPoint[] Block(TimeSpan length, double samplingInterval, DateTime? origin)
        {
            throw new NotImplementedException();
        }
    }
}