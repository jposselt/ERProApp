using System;
using System.IO;
using System.Reflection;

namespace ERProApp
{
    /// <summary>
    /// Diese Klasse bietet Zugriff auf informationen der Applikation.
    /// </summary>
    public static class ApplicationInfo
    {
        private static string _productName;
        private static bool _productNameCached;

        private static string _version;
        private static bool _versionCached;

        private static string _company;
        private static bool _companyCached;

        private static string _applicationPath;
        private static bool _applicationPathCached;

        private static DateTime? _compileDate;
        private static bool _compileDateCached;

        private static DateTime _startTime = DateTime.UtcNow;

        /// <summary>
        /// Name der Applikation.
        /// </summary>
        public static string ProductName
        {
            get
            {
                if (!_productNameCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        AssemblyProductAttribute attribute = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyProductAttribute)));
                        _productName = (attribute != null) ? attribute.Product : string.Empty;
                    }
                    else
                    {
                        _productName = string.Empty;
                    }
                    _productNameCached = true;
                }
                return _productName;
            }
        }

        /// <summary>
        /// Versionsnummer der Applikation.
        /// </summary>
        public static string Version
        {
            get
            {
                if (!_versionCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        _version = entryAssembly.GetName().Version.ToString();
                    }
                    else
                    {
                        _version = string.Empty;
                    }
                    _versionCached = true;
                }
                return _version;
            }
        }

        /// <summary>
        /// Hersteller der Applikation.
        /// </summary>
        public static string Company
        {
            get
            {
                if (!_companyCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        AssemblyCompanyAttribute attribute = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyCompanyAttribute)));
                        _company = (attribute != null) ? attribute.Company : string.Empty;
                    }
                    else
                    {
                        _company = string.Empty;
                    }
                    _companyCached = true;
                }
                return _company;
            }
        }

        /// <summary>
        /// Pfad der Applikation ohne den Applikationsnamen.
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                if (!_applicationPathCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        _applicationPath = Path.GetDirectoryName(entryAssembly.Location);
                    }
                    else
                    {
                        _applicationPath = string.Empty;
                    }
                    _applicationPathCached = true;
                }
                return _applicationPath;
            }
        }

        /// <summary>
        /// Datum und Uhrzeit des Builds.
        /// </summary>
        public static DateTime CompileDate
        {
            get
            {
                if (!_compileDateCached)
                {
                    Assembly entryAssembly = Assembly.GetEntryAssembly();
                    if (entryAssembly != null)
                    {
                        _compileDate = RetrieveLinkerTimestamp(entryAssembly.Location);
                    }
                    _compileDateCached = true;
                }
                return _compileDate ?? new DateTime();
            }
        }

        /// <summary>
        /// Initialisierung der Applikationsinformationen.
        /// </summary>
        public static void Initialize()
        {
            _startTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Retrieves the linker timestamp.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        /// <remarks>http://www.codinghorror.com/blog/2005/04/determining-build-date-the-hard-way.html</remarks>
        private static DateTime RetrieveLinkerTimestamp(string filePath)
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            var b = new byte[2048];

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.Read(b, 0, 2048);
                }
            }
            catch
            {
                //verschlucken...
            }

            var dt = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(b, BitConverter.ToInt32(b, peHeaderOffset) + linkerTimestampOffset));

            return dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
        }
    }
}
