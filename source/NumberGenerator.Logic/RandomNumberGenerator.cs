﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Implementiert einen Nummern-Generator, welcher Zufallszahlen generiert.
    /// Es können sich Beobachter registrieren, welche über eine neu generierte Zufallszahl benachrichtigt werden.
    /// Zwischen der Generierung der einzelnen Zufallsnzahlen erfolgt jeweils eine Pause.
    /// Die Generierung erfolgt so lange, solange Beobachter registriert sind.
    /// </summary>
    public class RandomNumberGenerator : IObservable
    {
        #region Constants

        private static readonly int DEFAULT_SEED = DateTime.Now.Millisecond;
        private const int DEFAULT_DELAY = 500;

        private const int RANDOM_MIN_VALUE = 1;
        private const int RANDOM_MAX_VALUE = 1000;

        #endregion

        #region Fields
        private Random _random;
        private int _delay = 1000;
        private int newNumber;
        private IList<IObserver> _observers = new List<IObserver>();


        #endregion

        #region Constructors
        
        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        public RandomNumberGenerator() : this(DEFAULT_DELAY, DEFAULT_SEED)
        {

        }

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        /// <param name="delay">Enthält die Zeit zw. zwei Generierungen in Millisekunden.</param>
        public RandomNumberGenerator(int delay) : this(delay, DEFAULT_SEED)
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        /// <param name="delay">Enthält die Zeit zw. zwei Generierungen in Millisekunden.</param>
        /// <param name="seed">Enthält die Initialisierung der Zufallszahlengenerierung.</param>
        public RandomNumberGenerator(int delay, int seed)
        {
            _delay = delay;
            _random = new Random(seed);
        }

        #endregion

        #region Methods

        #region IObservable Members

        /// <summary>
        /// Fügt einen Beobachter hinzu.
        /// </summary>
        /// <param name="observer">Der Beobachter, welcher benachricht werden möchte.</param>
        public void Attach(IObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (_observers.Contains(observer))
            {
                throw new InvalidOperationException("Observer is already attached!");
            }

            _observers.Add(observer);
        }

        /// <summary>
        /// Entfernt einen Beobachter.
        /// </summary>
        /// <param name="observer">Der Beobachter, welcher nicht mehr benachrichtigt werden möchte</param>
        public void Detach(IObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (_observers.Contains(observer) == false)
            {
                throw new InvalidOperationException("Observer was not attached!");
            }

            _observers.Remove(observer);
 
        }

        /// <summary>
        /// Benachrichtigt die registrierten Beobachter, dass eine neue Zahl generiert wurde.
        /// </summary>
        /// <param name="number">Die generierte Zahl.</param>
        public void NotifyObservers(int number)
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].OnNextNumber(newNumber);
            }
            
            //foreach (var observer in _observers)
            //{
            //    observer.OnNextNumber(newNumber);
            //}
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (var observer in _observers)
            {
                sb.AppendLine(observer.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Started the Nummer-Generierung.
        /// Diese läuft so lange, solange interessierte Beobachter registriert sind (=>Attach()).
        /// </summary>
        public void StartNumberGeneration()
        {

            while (_observers.Count > 0)
            {
                newNumber = _random.Next(RANDOM_MIN_VALUE, RANDOM_MAX_VALUE);
                Console.WriteLine($">> {nameof(RandomNumberGenerator)}: Number generated: '{newNumber}'");
                NotifyObservers(newNumber);
                Task.Delay(_delay).Wait();
            }
        }

        #endregion
    }

}
