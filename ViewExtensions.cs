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
        public static HtmlString ToJsonDump<TModel, TItem>(HtmlHelper<TModel> html, Func<TModel, TItem> modelItem)
        {
            var item = modelItem(html.ViewData.Model);

            return new HtmlString(item.ToJson());
        }

        public static HtmlString ToJson<TModel, TItem, TTransform>(HtmlHelper<TModel> html, Func<TModel, TItem> modelItem, Func<TItem, TTransform> transform)
        {
            var item = modelItem(html.ViewData.Model);
            var transformedItem = transform(item);

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