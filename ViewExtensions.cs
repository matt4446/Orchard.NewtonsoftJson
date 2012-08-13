using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Orchard.NewtonsoftJson
{
    public static class ViewExtensions
    {
        public static HtmlString ToJsonDumpFromItems<TItem>(this HtmlHelper html, 
            IEnumerable<TItem> items, 
            bool useCamelCase = true,
            bool formatJson = true)
        {
            return html.ToJsonDump(items, useCamelCase, formatJson);
        }

        public static HtmlString ToJsonDumpFromItems<TItem, TTransformedItem>(this HtmlHelper html, 
            IEnumerable<TItem> items, 
            Func<TItem, TTransformedItem> transform, 
            bool useCamelCase = true, 
            bool formatJson = true)
        {
            var transformedItems = items.Select(e=> transform(e)).ToArray();

            return html.ToJsonDump(transformedItems, useCamelCase, formatJson);
        }

        public static HtmlString ToJsonDump<TModel>(this HtmlHelper html, 
            TModel model,
            bool useCamelCase = true,
            bool formatJson = true)
        {
            return new HtmlString(model.ToJson(useCamelCase));
        }

        public static HtmlString ToJsonDump<TModel, TTransform>(this HtmlHelper html, TModel model,
            Func<TModel, TTransform> transform, bool useCamelCase = true, bool formatJson = true)
        {
            var transformedItem = transform(model);

            return new HtmlString(transformedItem.ToJson(useCamelCase: useCamelCase, formatJson: formatJson));
        }

        public static string ToJson<TModel>(this TModel model, bool useCamelCase = true, bool formatJson = true) 
        {
            JsonSerializerSettings settings = useCamelCase ?
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() } :
                new JsonSerializerSettings() { };

            return 
                formatJson ?
                JsonConvert.SerializeObject(model, Formatting.Indented, settings) : 
                JsonConvert.SerializeObject(model, Formatting.None, settings);
        }
    }
}