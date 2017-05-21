using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERProApp
{
    /// <summary>
    /// Klasse zur Modelierung Ausleihen und Reservierungen
    /// </summary>
    public class Rental
    {
        #region Klassenvariablen
        private DateTime defaultDate = new DateTime(1970, 1, 1);
        #endregion // Klassenvariablen

        #region Membervariablen

        // Diese Variablen werden Serialisiert
        private String _id;
        private String _customerID;
        private String _itemID;
        private DateTime _startDate;
        private DateTime _endDate;
        private bool _reservation;
        private bool _overdue;

        // Diese Variablen werden nicht Serialisiert
        private Customer _customer;
        private Book _item;

        #endregion // Membervariablen

        #region Properties (Eigenschaften)

        /// <summary>
        /// Ruft die Id der Ausleihe/Reservierung ab oder legt diese fest.
        /// </summary>
        [XmlAttribute("ID")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Ruft Kunden ID ab oder legt diese fest.
        /// </summary>
        [XmlElement("CustomerID")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        /// <summary>
        /// Ruft Gegenstand ID ab oder legt diese fest.
        /// </summary>
        [XmlElement("ItemID")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        /// <summary>
        /// Ruft das Anfangsdatum der Ausleihe/Reservierung ab oder legt diese fest.
        /// </summary>
        [XmlElement(DataType = "date")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        /// <summary>
        /// Ruft das Enddatum der Ausleihe/Reservierung ab oder legt diese fest.
        /// </summary>
        [XmlElement(DataType = "date")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        /// <summary>
        /// Ruft ab oder legt fest ob es sich um eine Reservierung(true) oder Ausleihe(false) handelt.
        /// </summary>
        [XmlElement("Reservation")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public bool Reservation
        {
            get { return _reservation; }
            set { _reservation = value; }
        }

        /// <summary>
        /// Ruft ab oder legt fest ob die Ausleihe/Reservierung überfällig ist.
        /// </summary>
        [XmlElement("Overdue")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public bool Overdue
        {
            get { return _overdue; }
            set { _overdue = value; }
        }

        /// <summary>
        /// Ruft den ausleihenden Kunden ab oder legt diese fest.
        /// </summary>
        [XmlIgnore] // Bei der Serialisierung und Deserialisierung ignorieren
        public Customer Taker
        {
            get { return _customer; }
            set { _customer = value; }
        }

        /// <summary>
        /// Ruft das ausgeliehene Objekt ab oder legt diese fest.
        /// </summary>
        [XmlIgnore] // Bei der Serialisierung und Deserialisierung ignorieren
        public Book Item
        {
            get { return _item; }
            set { _item = value; }
        }

        #endregion // Properties (Eigenschaften)

        #region Konstruktoren

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        public Rental(){}

        /// <summary>
        /// Konstruktor für neue Ausleihe
        /// </summary>
        /// <param name="customer"></param>Der ausleihende Kunde
        /// <param name="item"></param>Das auszuleihende Objekt
        /// <param name="start"></param>Startdatum der Ausleihe
        /// <param name="end"></param>Enddatum der Ausleihe
        /// <param name="reservation"></param>
        public Rental(Customer customer, Book item, DateTime start, DateTime end, bool reservation)
        {
            _id = Guid.NewGuid().ToString();

            _customer = customer;
            if (customer == null)
                _customerID = "";
            else
                _customerID = customer.ID;

            _item = item;
            if (item == null)
                _itemID = "";
            else
                _itemID = item.ID;

            if (start == null)
                _startDate = defaultDate;
            else
                _startDate = start;

            if (end == null)
                _endDate = defaultDate;
            else
                _endDate = start;

            // Vertausche Start/Enddatum, wenn Enddatum vor Startdatum
            if (_endDate < _startDate)
            {
                DateTime tmp = _endDate;
                _endDate = _startDate;
                _startDate = tmp;
            }

            _reservation = reservation;
            _overdue = false;
        }

        #endregion // Konstruktoren

    }
}
