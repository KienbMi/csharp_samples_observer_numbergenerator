using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher auf einen vollständigen Quick-Tipp wartet: 6 unterschiedliche Zahlen zw. 1 und 45.
    /// </summary>
    public class QuickTippObserver : IObserver
    {
        #region Fields

        private IObservable _numberGenerator;

        #endregion

        #region Properties

        public List<int> QuickTippNumbers { get; private set; }
        public int CountOfNumbersReceived { get; private set; }

        #endregion

        #region Constructor

        public QuickTippObserver(IObservable numberGenerator)
        {
            _numberGenerator = numberGenerator;
            QuickTippNumbers = new List<int>();

            _numberGenerator.Attach(this);
        }

        #endregion

        #region Methods

        public void OnNextNumber(int number)
        {
            CountOfNumbersReceived++;

            if (number >= 1 && number <= 45)
            {
                if (QuickTippNumbers.Contains(number) == false)
                {
                    QuickTippNumbers.Add(number);
                }
            }

            if (QuickTippNumbers.Count >= 6)
            {
                DetachFromNumberGenerator();
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append($"{base.ToString()} => ");
            sb.Append($"{nameof(QuickTippObserver)} ");
            sb.Append($"[{nameof(CountOfNumbersReceived)}='{CountOfNumbersReceived}'");
            for (int i = 0; i < QuickTippNumbers.Count; i++)
            {
                sb.Append($", Number_{i+1}='{QuickTippNumbers[i]}'");
            }
            sb.Append($"]");

            return sb.ToString();
        }

        private void DetachFromNumberGenerator()
        {
            _numberGenerator.Detach(this);
        }

        #endregion
    }
}
