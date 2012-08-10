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
        public static HtmlString ToJsonDumpFromItems<TItem>(this HtmlHelper html, IEnumerable<TItem> items, bool useCamelCase = true)
        {
            return html.ToJsonDump(items, useCamelCase);
        }

        public static HtmlString ToJsonDumpFromItems<TItem, TTransformedItem>(this HtmlHelper html, 
            IEnumerable<TItem> items, 
            Func<TItem, TTransformedItem> transform, bool useCamelCase = true)
        {
            var transformedItems = items.Select(e=> transform(e)).ToArray();

            return html.ToJsonDump(transformedItems, useCamelCase);
        }



        public static HtmlString ToJsonDump<TModel>(this HtmlHelper html, 
            TModel model,
            bool useCamelCase = true)
        {
            return new HtmlString(model.ToJson(useCamelCase));
        }

        public static HtmlString ToJsonDump<TModel, TTransform>(this HtmlHelper html, TModel model,
            Func<TModel, TTransform> transform, bool useCamelCase = true)
        {
            var transformedItem = transform(model);

            return new HtmlString(transformedItem.ToJson());
        }

        private static string ToJson<TModel>(this TModel model, bool useCamelCase = true) 
        {
            JsonSerializerSettings settings = useCamelCase ?
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() } :
                new JsonSerializerSettings() { };

            return JsonConvert.SerializeObject(model, Formatting.Indented, settings);
        }
    }
}