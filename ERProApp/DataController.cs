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
        public static string CustomerFile = @"customers.xml";

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

        #endregion // Properties (Eigenschaften)

        #region Konstruktoren

        /// <summary>
        /// Statischer Standardkonstruktor
        /// </summary>
        static DataController()
        {
            App.Log.Debug("Lege Datenstrukturen an");
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
        /// Fuegt eine Ausleihe/Reservierung zur entsprechenden Liste hinzu.
        /// </summary>
        /// <param name="rental"></param> Hinzuzufuegende Ausleihe/Reservierung
        public static void AddRental(Rental rental)
        {
            // Fuegt das Objekt zur Liste hinzu
            if (rental != null)
            {
                Rentals.Add(rental);
                App.Log.Info("Neue Ausleihe hinzugefügt");
            }
        }

        /// <summary>
        /// Löscht eine Ausleihe/Reservierung aus der entsprechenden Liste.
        /// </summary>
        /// <param name="rental">Das zu löschende Ausleihe-Objekt</param>
        public static void DeleteRental(Rental rental)
        {
            // Entfernt das Objekt aus der Liste.
            if(rental != null)
            {
                Rentals.Remove(rental);
                App.Log.Info("Ausleihe gelöscht");
            } 
        }

        /// <summary>
        /// Lädt alle Kunden aus einer XML-Datei.
        /// </summary>
        /// <param name="filename">Name der Quelldatei incl. oder excl. Dateipfad</param>
        /// <returns></returns>
        public static void LoadCustomersFromFile(string filename)
        {
            App.Log.Info("Lade Kunden");

            // Prüfen ob die Datei Exisitiert.
            if (!File.Exists(filename))
            {
                // Keine Kunden gefunden.
                App.Log.Warn("Keine Kunden gefunden");
                return;
            }

            try
            {
                App.Log.Info("Lade Kunden aus Datei");

                // ein XmlSerializer initialisieren.
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Customer>), CustomersXmlRoot);
                App.Log.Debug("XmlSerializer initialisiert");

                // Objekt zum einlesen initialisieren
                using (var reader = new StreamReader(filename))
                {
                    // deserialisierer starten.
                    object obj = deserializer.Deserialize(reader);
                    // ergebnis casten.
                    Customers = (obj as ObservableCollection<Customer>);
                }
                App.Log.Info("Kunden geladen");
            }
            catch (Exception e) // Um eventuelle Fehler zu vermeiden.
            {
                // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                App.Log.Error("Fehler beim Laden der Kunden");
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
            App.Log.Info("Speichere Kunden");

            if (Customers.Any())
            {
                try
                {
                    // ein XmlSerializer initialisieren.
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Customer>), CustomersXmlRoot);
                    App.Log.Debug("XmlSerializer initialisiert");

                    // Objekt zum schreiben initialisieren
                    using (var writer = new StreamWriter(filename))
                    {
                        // Damit in der Xml-Datei kein lästiger "xmlns" Namespace steht, legen wir einen leeren Namespace an.
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add(string.Empty, string.Empty);

                        // serialisierer starten.
                        serializer.Serialize(writer, Customers.ToList(), ns);
                    }
                    App.Log.Info("Kunden gespeichert");
                }
                catch (Exception e) // Um eventuelle Fehler zu vermeiden.
                {
                    // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                    App.Log.Error("Fehler beim Speichern der Kunden");
                    string message = string.Format("Beim Speichern der Kunden ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                    MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                          // TODO: lokalisieren
                }
            }
            else
            {
                App.Log.Info("Keine Kunden vorhanden");
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
            App.Log.Info("Lade Bücher");

            // Prüfen ob die Datei Exisitiert.
            if (!File.Exists(filename))
            {
                // Keine Kunden gefunden.
                App.Log.Warn("Keine Bücher gefunden");
                return;
            }

            try
            {
                App.Log.Info("Lade Bücher aus Datei");

                // ein XmlSerializer initialisieren.
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Book>), BooksXmlRoot);
                App.Log.Debug("XmlSerializer initialisiert");

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
                App.Log.Error("Fehler beim Laden der Bücher");
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
            App.Log.Info("Speichere Bücher");

            if (Customers.Any())
            {
                try
                {
                    // ein XmlSerializer initialisieren.
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Book>), BooksXmlRoot);
                    App.Log.Debug("XmlSerializer initialisiert");

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
                    App.Log.Error("Fehler beim Speichern der Bücher");
                    string message = string.Format("Beim Speichern der Bücher ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                    MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                          // TODO: lokalisieren
                }
            }
            else
            {
                App.Log.Info("Keine Bücher vorhanden");
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
            App.Log.Info("Lade Ausleihen");

            // Prüfen ob die Datei Exisitiert.
            if (!File.Exists(filename))
            {
                // Keine Kunden gefunden.
                App.Log.Warn("Keine Ausleihen gefunden");
                return;
            }

            try
            {
                App.Log.Info("Lese Ausleihen aus Datei");

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
                App.Log.Info("Assoziiere Ausleihen mit Kunden und Büchern");
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
                        {
                            rental.Item = item;

                            // Setzt Statusfeld
                            if (rental.Reservation)
                            {
                                //item.ReservationCount += 1;
                                if (item.Status == "verfügbar")
                                {
                                    item.Status = "reserviert";
                                }
                            }
                            else
                            {
                                if (item.Status == "verfügbar" || item.Status == "reserviert")
                                {
                                    item.Status = "ausgeliehen";
                                }
                                
                            }
  
                        }    
                    }
                }
            }
            catch (Exception e) // Um eventuelle Fehler zu vermeiden.
            {
                // Dem Benutzer eine Fehlermeldung anzeigen falls es zu einer Exception kommt.
                App.Log.Error("Fehler beim Laden der Ausleihen");
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
            App.Log.Info("Speichere Ausleihen");
            if (Customers.Any())
            {
                try
                {
                    // ein XmlSerializer initialisieren.
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Rental>), RentalsXmlRoot);
                    App.Log.Debug("XmlSerializer initialisiert");

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
                    App.Log.Error("Fehler beim Speichern der Ausleihen");
                    string message = string.Format("Beim Speichern der Ausleihen ist ein Fehler aufgetreten.{0}{0}{1}", Environment.NewLine, e.Message); // TODO: lokalisieren
                    MessageBox.Show(message, "Fehler", MessageBoxButton.OK);                                                                             // TODO: lokalisieren
                }
            }
            else
            {
                App.Log.Info("Keine Ausleihen vorhanden");
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
        }

        /// <summary>
        /// Speichert Kunden-, Buch- und Ausleihdaten in den Standarddateien
        /// </summary>
        public static void SaveDataToFiles()
        {
            App.Log.Info("Speichere Daten");
            SaveRentalsToFile(RentalFile);
            SaveBooksToFile(BookFile);
            SaveCustomersToFile(CustomerFile);
        }

        /// <summary>
        /// Lädt Kunden-, Buch- und Ausleihdaten aus den Standarddateien
        /// </summary>
        public static void LoadDataFromFiles()
        {
            App.Log.Info("Lade Daten");
            LoadCustomersFromFile(CustomerFile);
            LoadBooksFromFile(BookFile);
            LoadRentalsFromFile(RentalFile);
        }

        /// <summary>
        /// Prüft für ein bestimmtes Ausleihobjekt ob zeitliche Überlappungen mit aktiven Ausleihen/Reservierungen gibt
        /// </summary>
        /// <param name="item"></param>Ausleihobjekt 
        /// <param name="start"></param>Anfang des Zeitintervalls
        /// <param name="end"></param>Ende des Zeitintervalls
        /// <returns></returns>
        public static bool TimeOverlapCheck(Book item, DateTime start, DateTime end)
        {
            App.Log.Debug("TimeOverlapCheck aufgerufen");

            // Hilfsvariable
            bool overlap = false;

            // Prüfe Ausleihen auf Zeitüberlappung
            foreach (var rental in Rentals)
            {
                if(rental.Item == item)
                {
                    if ( !(rental.StartDate > end || rental.EndDate < start) )
                    {
                        overlap = true;
                    }
                }
            }
            return overlap;
        }

        #endregion // Funktionen
    }
}
