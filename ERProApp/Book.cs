using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERProApp
{
    /// <summary>
    /// Klasse zur Modelierung von Büchern
    /// </summary>
    class Book : IComparable
    {
        #region Membervariablen

        private String _id;
        private String _title;
        private String _author;
        private String _genre;
        private String _location;
        private int _year;
        private bool _blocked;

        #endregion // Membervariablen

        #region Properties (Eigenschaften)

        /// <summary>
        /// Ruft die Id des Buches ab oder legt diese fest.
        /// </summary>
        [XmlAttribute("ID")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Ruft den Buchtitel ab oder legt diese fest.
        /// </summary>
        [XmlElement("Title")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Ruft den Autor ab oder legt diese fest.
        /// </summary>
        [XmlElement("Author")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String Author
        {
            get { return _author; }
            set { _author = value; }
        }

        /// <summary>
        /// Ruft das Genre ab oder legt diese fest.
        /// </summary>
        [XmlElement("Genre")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        /// <summary>
        /// Ruft den Lagerort ab oder legt diese fest.
        /// </summary>
        [XmlElement("Location")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public String Location
        {
            get { return _location; }
            set { _location = value; }
        }

        /// <summary>
        /// Ruft Erscheinungsjahr ab oder legt diese fest.
        /// </summary>
        [XmlElement("Year")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        /// <summary>
        /// Ruft ab oder legt diese fest, ob das Exemplar für Ausleihen zur Verfügung steht.
        /// true: Ausleihen nicht möglich; false: Ausleihen möglich
        /// </summary>
        [XmlElement("Blocked")] // Wird für die Serialisierung und Deserialisierung benötigt.
        public bool Blocked
        {
            get { return _blocked; }
            set { _blocked = value; }
        }

        /// <summary>
        /// Vergleicht Bücher anhand des Titels
        /// </summary>
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion // Properties (Eigenschaften)

        #region Konstruktoren
        #endregion // Konstruktoren
    }
}
