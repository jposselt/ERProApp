using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;


namespace ERProApp
{
    /// <summary>
    /// Der DataController dient als Verwaltungsklasse für die Komponenten des Datenmodels.
    /// 
    /// Die statische Klasse stellt Funktionen bereit um Komponenten zu speichern und laden 
    /// 
    /// sowie zum Erstellen und Löschen von Ausleihen/Reservierungen.
    /// </summary>
    class DataController
    {
        #region Klassenvariablen

        /// <summary>
        /// Pfad + Name der Datei in der Kunden gespeichert werden.
        /// </summary>
        public static string NoteFile = @"customers.xml";

        /// <summary>
        /// Xml-Stammelement für die Serialisierung und Deserialisierung.
        /// </summary>
        public static XmlRootAttribute CustomersXmlRoot = new XmlRootAttribute("Customers");

        /// <summary>
        /// Pfad + Name der Datei in der Bücher gespeichert werden.
        /// </summary>
        public static string BookFile = @"books.xml";

        /// <summary>
        /// Xml-Stammelement für die Serialisierung und Deserialisierung.
        /// </summary>
        public static XmlRootAttribute BooksXmlRoot = new XmlRootAttribute("Books");

        /// <summary>
        /// Pfad + Name der Datei in der Ausleihen gespeichert werden.
        /// </summary>
        public static string RentalFile = @"rentals.xml";

        /// <summary>
        /// Xml-Stammelement für die Serialisierung und Deserialisierung.
        /// </summary>
        public static XmlRootAttribute RentalsXmlRoot = new XmlRootAttribute("Rentals");

        #endregion // Klassenvariablen

        #region Properties (Eigenschaften)

        /// <summary>
        /// Eine Liste aller Ausleihen/Rservierungen
        /// </summary>
        public static ObservableCollection<Rental> Rentals { get; set; }

        /// <summary>
        /// Eine Liste aller Kunden
        /// </summary>
        public static ObservableCollection<Customer> Customers { get; set; }

        /// <summary>
        /// Eine Liste aller Bücher
        /// </summary>
        public static ObservableCollection<Book> Books { get; set; }

        /// <summary>
        /// Gibt an ob eine Ausleihe/Rservierung sofort gelöscht werden soll oder ob erst eine Meldung angezeigt wird.
        /// </summary>
        public static bool DeleteRentalImmediately { get; set; }

        #endregion // Properties (Eigenschaften)

        #region Konstruktoren

        /// <summary>
        /// Statischer Standardkonstruktor
        /// </summary>
        static DataController()
        {
            if (Customers == null)
                Customers = new ObservableCollection<Customer>();
            if (Books == null)
                Books = new ObservableCollection<Book>();
            if (Rentals == null)
                Rentals = new ObservableCollection<Rental>();
        }

        #endregion // Konstruktoren

        #region Funktionen
        /// <summary>
        /// 
        /// </summary>
        public static void CreateRental()
        {
            // TODO
        }

        /// <summary>
        /// Löscht eine Ausleihe/Reservierung aus der entsprechenden Liste.
        /// </summary>
        /// <param name="rental">Das zu löschende Ausleihe-Objekt</param>
        public static void DeleteRental(Rental rental)
        {
            // Entfernt das Objekt aus der Liste.
            Rentals.Remove(rental);
        }

        /// <summary>
        /// Lädt alle Kunden aus einer XML-Datei.
        /// </summary>
        /// <param name="filename">Name der Quelldatei incl. oder excl. Dateipfad</param>
        /// <returns></returns>
        public static void LoadCustomersFromFile(string filename)
        {
            // Prüfen ob die Datei Exisitiert.
            if (!File.Exists(filename))
            {
                // Keine Kunden gefunden.             
                return;
            }

            try
            {
                // ein XmlSerializer initialisieren.
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Customer>), CustomersXmlRoot);

                // Objekt zum einlesen initialisieren
                using (var reader = new StreamReader(filename))
                {
                    // deserialisierer starten.
                    object obj = deserializer.Deserialize(reader);
                    // ergebnis casten.
                    Customers = (obj as ObservableCollection<Customer>);
                }
            }
            catch (Exception e) // Um eventuelle Fehler zu vermeiden.
            {
                // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                string message = string.Format("Beim Laden der Kunden ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                      // TODO: lokalisieren
            }
        }

        /// <summary>
        /// Speichert alle Kunden in eine Xml-Datei.
        /// </summary>
        /// <param name="filename">Name der Zieldatei incl. oder excl. Dateipfad</param>
        public static void SaveCustomersToFile(string filename)
        {
            if (Customers.Any())
            {
                try
                {
                    // ein XmlSerializer initialisieren.
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>), CustomersXmlRoot);

                    // Objekt zum schreiben initialisieren
                    using (var writer = new StreamWriter(filename))
                    {
                        // Damit in der Xml-Datei kein lästiger "xmlns" Namespace steht, legen wir einen leeren Namespace an.
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add(string.Empty, string.Empty);

                        // serialisierer starten.
                        serializer.Serialize(writer, Customers.ToList(), ns);
                    }
                }
                catch (Exception e) // Um eventuelle Fehler zu vermeiden.
                {
                    // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                    string message = string.Format("Beim Speichern der Kunden ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                    MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                          // TODO: lokalisieren
                }
            }
            else
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
        }

        /// <summary>
        /// Lädt alle Bücher aus einer XML-Datei.
        /// </summary>
        /// <param name="filename">Name der Quelldatei incl. oder excl. Dateipfad</param>
        /// <returns></returns>
        public static void LoadBooksFromFile(string filename)
        {
            // Prüfen ob die Datei Exisitiert.
            if (!File.Exists(filename))
            {
                // Keine Kunden gefunden.             
                return;
            }

            try
            {
                // ein XmlSerializer initialisieren.
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Book>), BooksXmlRoot);

                // Objekt zum einlesen initialisieren
                using (var reader = new StreamReader(filename))
                {
                    // deserialisierer starten.
                    object obj = deserializer.Deserialize(reader);
                    // ergebnis casten.
                    Books = (obj as ObservableCollection<Book>);
                }
            }
            catch (Exception e) // Um eventuelle Fehler zu vermeiden.
            {
                // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                string message = string.Format("Beim Laden der Bücher ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                      // TODO: lokalisieren
            }
        }

        /// <summary>
        /// Speichert alle Bücher in eine Xml-Datei.
        /// </summary>
        /// <param name="filename">Name der Zieldatei incl. oder excl. Dateipfad</param>
        public static void SaveBooksToFile(string filename)
        {
            if (Customers.Any())
            {
                try
                {
                    // ein XmlSerializer initialisieren.
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Book>), BooksXmlRoot);

                    // Objekt zum schreiben initialisieren
                    using (var writer = new StreamWriter(filename))
                    {
                        // Damit in der Xml-Datei kein lästiger "xmlns" Namespace steht, legen wir einen leeren Namespace an.
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add(string.Empty, string.Empty);

                        // serialisierer starten.
                        serializer.Serialize(writer, Books.ToList(), ns);
                    }
                }
                catch (Exception e) // Um eventuelle Fehler zu vermeiden.
                {
                    // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                    string message = string.Format("Beim Speichern der Bücher ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                    MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                          // TODO: lokalisieren
                }
            }
            else
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
        }

        /// <summary>
        /// Lädt alle Ausleihen aus einer XML-Datei.
        /// </summary>
        /// <param name="filename">Name der Quelldatei incl. oder excl. Dateipfad</param>
        /// <returns></returns>
        public static void LoadRentalsFromFile(string filename)
        {
            // Prüfen ob die Datei Exisitiert.
            if (!File.Exists(filename))
            {
                // Keine Kunden gefunden.             
                return;
            }

            try
            {
                // ein XmlSerializer initialisieren.
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Rental>), RentalsXmlRoot);

                // Objekt zum einlesen initialisieren
                using (var reader = new StreamReader(filename))
                {
                    // deserialisierer starten.
                    object obj = deserializer.Deserialize(reader);
                    // ergebnis casten.
                    Rentals = (obj as ObservableCollection<Rental>);
                }

                // Assoziere Ausleihen mit Kunden- und Buch-Objekten
                foreach (var rental in Rentals)
                {
                    foreach(var customer in Customers)
                    {
                        if (customer.ID.Equals(rental.CustomerID))
                            rental.Taker = customer;
                    }

                    foreach(var item in Books)
                    {
                        if (item.ID.Equals(rental.ItemID))
                            rental.Item = item;
                    }
                }
            }
            catch (Exception e) // Um eventuelle Fehler zu vermeiden.
            {
                // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                string message = string.Format("Beim Laden der Ausleihen ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                         // TODO: lokalisieren
            }
        }

        /// <summary>
        /// Speichert alle Ausleihen in eine Xml-Datei.
        /// </summary>
        /// <param name="filename">Name der Zieldatei incl. oder excl. Dateipfad</param>
        public static void SaveRentalsToFile(string filename)
        {
            if (Customers.Any())
            {
                try
                {
                    // ein XmlSerializer initialisieren.
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Rental>), RentalsXmlRoot);

                    // Objekt zum schreiben initialisieren
                    using (var writer = new StreamWriter(filename))
                    {
                        // Damit in der Xml-Datei kein lästiger "xmlns" Namespace steht, legen wir einen leeren Namespace an.
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add(string.Empty, string.Empty);

                        // serialisierer starten.
                        serializer.Serialize(writer, Rentals.ToList(), ns);
                    }
                }
                catch (Exception e) // Um eventuelle Fehler zu vermeiden.
                {
                    // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                    string message = string.Format("Beim Speichern der Ausleihen ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                    MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                             // TODO: lokalisieren
                }
            }
            else
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
        }

        public static bool ConsistencyCheck(Book item, DateTime start, DateTime end)
        {
            // TODO
            return true; // Dummy
        }

        #endregion // Funktionen
    }
}
