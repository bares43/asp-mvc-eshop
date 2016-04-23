using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Eshop.Class
{
    public class JsonResponseHelper
    {
        public List<JsonResponseHelperSection> Sections = new List<JsonResponseHelperSection>(); 

        public enum Actions
        {
            Noty_Success,
            Noty_Warning,
            Js,
            Refresh
        };

        public JsonResponseHelperSection[] JsonObject => Sections.ToArray();

        public JsonResponseHelper AddNotySuccess(string message)
        {
            return AddAction(Actions.Noty_Success.ToString(), nameof(message), message);
        }

        public JsonResponseHelper AddNotyWarning(string message)
        {
            return AddAction(Actions.Noty_Warning.ToString(), nameof(message), message);
        }

        public JsonResponseHelper AddJs(string js)
        {
            return AddAction(Actions.Js.ToString(), nameof(js), js);
        }

        public JsonResponseHelper AddRefresh()
        {
            return AddAction(Actions.Refresh.ToString()); 
        }

        public JsonResponseHelper AddAction(string action, string param = null, string value = null)
        {
            var parameters = new Dictionary<string, string>();
            if (param != null && value != null)
            {
                parameters.Add(param, value);
            }
            Sections.Add(new JsonResponseHelperSection() { Action = action, Params = parameters });
            return this;
        }


    }

    public class JsonResponseHelperSection
    {
        public string Action { get; set; }

        private Dictionary<string, string> paramsDictionary;

        public Dictionary<string, string> Params
        {
            get
            {
                if (paramsDictionary == null)
                {
                    paramsDictionary = new Dictionary<string, string>();
                }
                return paramsDictionary;
            }
            set { paramsDictionary = value; }
        }
    }
}