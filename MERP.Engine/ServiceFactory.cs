using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MERP.Engine
{
    public class Services
    {
        public Services() { }

        public static ServiceFactory _factory;
        public static ServiceFactory Factory
        {
            get { return _factory; }
        }

        static Services()
        {
            _factory = new ServiceFactory("ICS.Core.ServiceFactory");
        }
    }
    public class ServiceFactory
    {
        private enum CreateMode
        {
            Local, Remote
        }

        private const string DefaultAppSettingsKey = "ICS.ServiceFactory";
        private const string KeyMode = "mode";
        private const string KeyBaseUrl = "baseUrl";
        private const string KeyAssembly = "assembly";
        private CreateMode _mode;
        private Assembly _assembly;
        private string _baseUrl;
        private Hashtable _cachedTypes = new Hashtable();
        public ServiceFactory(string configurationKey)
        {
            #region Read Configuration and Create Cache

            // Check if the configuration key specified by the Component exists 
            // ... if not, use the default one "ICS.ServiceFactory"
            string configurationString = "";//System.Configuration.ConfigurationManager.AppSettings[configurationKey];
            if (configurationString == null)
            {
                //configurationString = System.Configuration.ConfigurationManager.AppSettings[DefaultAppSettingsKey];
            }

            string helpString = string.Format("Please add either of the keys: '{0}' or '{1}' in config file with the Service Settings in following format:"
                + Environment.NewLine + "For Remote: \"mode=Remote; baseUrl=<base-service-url>, eg. tcp://www.domain.com:1234/ServiceAssembly/\""
                + Environment.NewLine + "For Local: \"mode=Local; assembly=<your-assembly-name>\"", configurationKey, DefaultAppSettingsKey);

            if (configurationString == null)
            {
                return;
            }

            SettingsReader reader = new SettingsReader(configurationString);
            if (!reader.Contains(KeyMode))
            {
                throw new Exception(string.Format("ServiceFactory configuraion setting is missing or invalid. " + helpString));
            }

            try
            {
                _mode = (CreateMode)Enum.Parse(typeof(CreateMode), reader.GetSetting(KeyMode));
            }
            catch (InvalidCastException)
            {
                throw new Exception("The specified mode in serviceFactory setting is invalid. " + helpString);
            }

            switch (_mode)
            {
                case CreateMode.Local:
                    if (!reader.Contains(KeyAssembly))
                    {
                        throw new Exception("Missing attribute 'assembly'. " + helpString);
                    }

                    string assemblyName = reader.GetSetting(KeyAssembly);
                    try
                    {
                        _assembly = Assembly.Load(assemblyName);
                    }
                    catch (Exception)
                    {
                        throw new Exception("The assembly specified in SrviceFactory setting could not be Loaded.");
                    }

                    foreach (Type type in _assembly.GetTypes())
                    {
                        // Cache all types that are MarshalByRef
                        if (type.IsMarshalByRef)
                        {
                            string key = _assembly + "/" + type.Name;
                            if (!_cachedTypes.Contains(key))
                                _cachedTypes.Add(key, type);
                        }
                    }

                    break;
                case CreateMode.Remote:
                    if (!reader.Contains(KeyBaseUrl))
                    {
                        throw new Exception("Missing attribute 'url' in ServiceFactory Setting" + helpString);
                    }

                    _baseUrl = reader.GetSetting(KeyBaseUrl);
                    break;
            }
            #endregion
        }

        public object CreateService(Type type)
        {
            object service = null;
            try
            {
                string serviceName = type.Name.StartsWith("I") ? type.Name.Substring(1) : type.Name;
                switch (_mode)
                {
                    case CreateMode.Remote:
                        service = Activator.GetObject(type, _baseUrl + "." + serviceName);
                        break;
                    case CreateMode.Local:
                        Type implType = (Type)_cachedTypes[_assembly + "/" + serviceName];
                        service = implType.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, null, null);
                        //service = Activator.GetObject(type, "" + "." + serviceName);
                        break;
                }
            }
            catch (Exception exp)
            {
                throw new Exception(string.Format("Error Creating Service for type: {0}", type.Name), exp);
            }

            if (service == null)
                throw new Exception(string.Format("Error Creating Service for type: {0}", type.Name));

            return service;
        }
    }
    #region Framework: Helper Class
    public class SettingsReader
    {
        private Hashtable _settings = new Hashtable();

        public bool Contains(string key)
        {
            return _settings.ContainsKey(key);
        }

        public string GetSetting(string key)
        {
            return (string)_settings[key];
        }

        public SettingsReader(string settings)
        {
            string[] segments = settings.Split(';');
            foreach (string segment in segments)
            {
                string[] keyAndValue = segment.Split('=');
                if (keyAndValue.Length == 2)
                {
                    _settings.Add(keyAndValue[0].Trim(), keyAndValue[1].Trim());
                }
            }
        }
    }
    #endregion
}