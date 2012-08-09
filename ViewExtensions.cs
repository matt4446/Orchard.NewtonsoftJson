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
        public static HtmlString ToJsonDumpFromItems<TItem>(this HtmlHelper html, IEnumerable<TItem> items)
        {
            return html.ToJsonDump(items);
        }
        public static HtmlString ToJsonDumpFromItems<TItem, TTransformedItem>(this HtmlHelper html, IEnumerable<TItem> items, Func<TItem, TTransformedItem> transform)
        {
            var transformedItems = items.Select(e=> transform(e)).ToArray();

            return html.ToJsonDump(transformedItems);
        }



        public static HtmlString ToJsonDump<TModel>(this HtmlHelper html, TModel model)
        {
            return new HtmlString(model.ToJson());
        }

        public static HtmlString ToJsonDump<TModel, TTransform>(this HtmlHelper html, TModel model, 
            Func<TModel, TTransform> transform)
        {
            var transformedItem = transform(model);

            return new HtmlString(transformedItem.ToJson());
        }

        private static string ToJson<TModel>(this TModel model) 
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(model, Formatting.Indented, settings);
        }
    }
}