// -----------------------------------------------------------------------
// <copyright file="HtmlExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Mvc.Properties;

    public static class HtmlExtensions
    {
        public static MvcHtmlString EnumListBoxFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata == null)
                throw new ArgumentException();

            if (metadata.ModelType == null)
                throw new ArgumentException();

            bool isArray;
            bool isNullable;
            Type enumType;
            if (!IsValidForEnum(metadata.ModelType, out isArray, out isNullable, out enumType))
                throw new ArgumentException();

            var @enum = GetValues(htmlHelper, expression, isArray, metadata);

            var selectList = GetSelectList(metadata.ModelType, isNullable, @enum);
            if (!string.IsNullOrEmpty(optionLabel) && selectList.Count != 0 && string.IsNullOrEmpty(selectList[0].Text))
            {
                selectList[0].Text = optionLabel;
            }

            return htmlHelper.ListBoxFor(expression, selectList, htmlAttributes);
        }

        internal static Enum[] GetValues<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, bool isArray, ModelMetadata metadata)
        {
            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);

            Enum[] @enum = null;
            if (!string.IsNullOrEmpty(fullHtmlFieldName))
            {
                ModelState modelState;
                if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Value != null)
                {
                    if (isArray)
                    {
                        @enum = modelState.Value.ConvertTo(metadata.ModelType, null) as Enum[];
                    }
                    else
                    {
                        @enum = new[] { modelState.Value.ConvertTo(metadata.ModelType, null) as Enum };

                    }
                }
            }

            if (@enum == null && !string.IsNullOrEmpty(expressionText))
                @enum = htmlHelper.ViewData.Eval(expressionText) as Enum[];

            if (@enum == null)
                @enum = metadata.Model as Enum[];

            return @enum;
        }

        internal static IList<SelectListItem> GetSelectList(Type enumType, bool nullable, params Enum[] values)
        {
            var selectList = GetSelectList(enumType, nullable);

            if ((values == null || values.Length == 0))
            {
                if (nullable)
                {
                    selectList[0].Selected = true;
                }
            }
            else
            {
                foreach (var value in values)
                {
                    var str = value == null ? "0" : value.ToString("d");
                    var found = false;
                    for (var i = 0; i < selectList.Count - 1; i++)
                    {
                        var selectListItem = selectList[i];
                        if (str == selectListItem.Value)
                        {
                            selectListItem.Selected = true;
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        selectList.Insert(0, new SelectListItem
                        {
                            Selected = true,
                            Text = string.Empty,
                            Value = str
                        });
                    }
                }
            }

            return selectList;
        }

        internal static IList<SelectListItem> GetSelectList(Type enumType, bool nullable)
        {
            var list = (IList<SelectListItem>)new List<SelectListItem>();

            if(nullable)
            {
                list.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });
            }

            foreach (var field in enumType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField))
            {
                var rawConstantValue = field.GetRawConstantValue();
                list.Add(new SelectListItem
                {
                    Text = GetDisplayName(field),
                    Value = rawConstantValue.ToString()
                });
            }
            return list;
        }

        internal static bool IsValidForEnum(Type type, out bool isArray, out bool isNullable, out Type enumType)
        {
            isArray = false;
            isNullable = false;
            enumType = null;

            if (type == null)
            {
                return false;
            }

            if (type.IsArray)
            {
                type = type.GetElementType();
                isArray = true;
            }

            isNullable = Nullable.GetUnderlyingType(type) != null;
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (!type.IsEnum || HasFlags(type))
            {
                return false;
            }

            enumType = type;
            return true;
        }

        private static string GetDisplayName(MemberInfo field)
        {
            var customAttribute = field.GetCustomAttribute<DisplayAttribute>(false);
            if (customAttribute == null)
            {
                return field.Name;
            }

            var name = customAttribute.GetName();

            return !string.IsNullOrEmpty(name) ? name : field.Name;
        }

        private static bool HasFlags(Type type)
        {
            return type.GetCustomAttribute<FlagsAttribute>(false) != null;
        }
    }
}