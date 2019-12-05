using System;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields
        private int _counter = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; }

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; } = int.MinValue;

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg { get; private set; }

        #endregion

        #region Constructors

        public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override void OnNextNumber(int number)
        {
            _counter++;

            if (number < Min)
                Min = number;
            else if (number > Max)
                Max = number;

            Sum += number;

            Avg = Sum / _counter;
            
        }

        #endregion
    }
}
