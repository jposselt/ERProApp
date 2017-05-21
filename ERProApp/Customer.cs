﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERProApp
{
    /// <summary>
    /// Klasse zur Modelierung von Kunden
    /// </summary>
    class Customer : IComparable
    {

        #region Membervariablen

        private String _id;
        private String _surName;
        private String _lastName;
        private String _city;
        private String _street;
        private int _zip;
        
        #endregion // Membervariablen

        #region Properties (Eigenschaften)

        /// <summary>
        /// Ruft die Id des Kunden ab oder legt diese fest.
        /// </summary>
        [XmlAttribute("ID")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Ruft Vornamen des Kunden ab oder legt diese fest.
        /// </summary>
        [XmlElement("Surname")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public string SurName
        {
            get { return _surName; }
            set { _surName = value; }
        }

        /// <summary>
        /// Ruft Nachnamen des Kunden ab oder legt diese fest.
        /// </summary>
        [XmlElement("Lastname")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        /// <summary>
        /// Ruft den Wohnort des Kunden ab oder legt diese fest.
        /// </summary>
        [XmlElement("City")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        /// <summary>
        /// Ruft die Strasse des Kunden ab oder legt diese fest.
        /// </summary>
        [XmlElement("Street")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        /// <summary>
        /// Ruft die PLZ des Kunden ab oder legt diese fest.
        /// </summary>
        [XmlElement("ZIP")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public int Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        /// <summary>
        /// Vergleicht Kunden anhand des Namens in der Reihenfolge Nachname, Vorname
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Customer otherCustomer = obj as Customer;
            if (otherCustomer != null)
            {
                if (_lastName.CompareTo(otherCustomer._lastName) == 0)
                    return _surName.CompareTo(otherCustomer._surName);
                else
                    return _lastName.CompareTo(otherCustomer._lastName);
            }
            else
                throw new ArgumentException("Object is not a Customer");
        }

        #endregion // Properties (Eigenschaften)

        #region Konstruktoren

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        //public Customer(){}

        #endregion // Konstruktoren

    }
}