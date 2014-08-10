// -----------------------------------------------------------------------
// <copyright file="HtmlExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Views
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlExtensions
    {
        public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return DescriptionFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes = null)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata == null)
                throw new ArgumentException();

            if (metadata.ModelType == null)
                throw new ArgumentException();

            var description = metadata.Description;

            if (string.IsNullOrEmpty(description))
            {
                return MvcHtmlString.Empty;
            }

            var builder = new TagBuilder("span");
            builder.MergeAttributes(htmlAttributes);
            builder.SetInnerText(metadata.Description);
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString EnumDropDownListGroupedFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            return EnumDropDownListGroupedFor(htmlHelper, expression, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString EnumDropDownListGroupedFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel = null, IDictionary<string, object> htmlAttributes = null)
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

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
        }

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

        private static MvcHtmlString SelectInternal(this HtmlHelper htmlHelper, ModelMetadata metadata, string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple, IDictionary<string, object> htmlAttributes)
        {
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentNullException("name");
            }

            var usedViewData = false;

            // If we got a null selectList, try to use ViewData to get the list of items.
            if (selectList == null)
            {
                selectList = (IEnumerable<SelectListItem>)htmlHelper.ViewData.Eval(name);
                ;
                usedViewData = true;
            }

            var defaultValue = (allowMultiple) ? GetModelStateValue(htmlHelper, fullName, typeof(string[])) : GetModelStateValue(htmlHelper, fullName, typeof(string));

            // If we haven't already used ViewData to get the entire list of items then we need to
            // use the ViewData-supplied value before using the parameter-supplied value.
            if (defaultValue == null && !String.IsNullOrEmpty(name))
            {
                if (!usedViewData)
                {
                    defaultValue = htmlHelper.ViewData.Eval(name);
                }
                else if (metadata != null)
                {
                    defaultValue = metadata.Model;
                }
            }

            if (defaultValue != null)
            {
                selectList = GetSelectListWithDefaultValue(selectList, defaultValue, allowMultiple);
            }

            // Convert each ListItem to an <option> tag and wrap them with <optgroup> if requested.
            var listItemBuilder = BuildItems(optionLabel, selectList);

            var tagBuilder = new TagBuilder("select")
            {
                InnerHtml = listItemBuilder.ToString()
            };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", fullName, true /* replaceExisting */);
            tagBuilder.GenerateId(fullName);
            if (allowMultiple)
            {
                tagBuilder.MergeAttribute("multiple", "multiple");
            }

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
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
                else
                {
                    object modelValue;
                    if (htmlHelper.ViewData.TryGetValue(fullHtmlFieldName, out modelValue))
                    {
                        if (isArray)
                        {
                            @enum = modelValue as Enum[];
                        }
                        else
                        {
                            @enum = new[] { modelValue as Enum };
                        }
                    }
                }
            }

            if(@enum == null)
            {
                if (isArray)
                {
                    @enum = htmlHelper.ViewData.Model as Enum[];
                }
                else
                {
                    @enum = new[] { htmlHelper.ViewData.Model as Enum };
                }
            }


            if (@enum == null && !string.IsNullOrEmpty(expressionText))
                @enum = htmlHelper.ViewData.Eval(expressionText) as Enum[];

            if (@enum == null)
                @enum = metadata.Model as Enum[];

            return @enum;
        }


        private static
            IEnumerable<SelectListItem> GetSelectListWithDefaultValue
            (IEnumerable<SelectListItem> selectList, object defaultValue, bool allowMultiple)
        {
            IEnumerable defaultValues;

            if (allowMultiple)
            {
                defaultValues = defaultValue as IEnumerable;
                if (defaultValues == null || defaultValues is string)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            CultureInfo.CurrentCulture,
                            "MvcResources.HtmlHelper_SelectExpressionNotEnumerable",
                            "expression"));
                }
            }
            else
            {
                defaultValues = new[] { defaultValue };
            }

            var values = defaultValues.Cast<object>().Select(value => Convert.ToString(value, CultureInfo.CurrentCulture));

            // ToString() by default returns an enum value's name.  But selectList may use numeric values.
            var enumValues = defaultValues.OfType<Enum>().Select(value => value.ToString("d"));

            values = values.Concat(enumValues);

            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();

            foreach (var item in selectList)
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            }
            return newSelectList;
        }

        internal static
            IList<SelectListItem> GetSelectList
            (Type enumType, bool nullable, params Enum[] values)
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

        internal static
            IList<SelectListItem> GetSelectList
            (Type enumType, bool nullable)
        {
            var list = (IList<SelectListItem>)new List<SelectListItem>();

            if (nullable)
            {
                list.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });
            }

            var groups = new List<SelectListGroup>();

            var items = enumType
                .GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField)
                .Select(f => new SelectListItem { Text = GetDisplayName(f), Group = GetGroup(f, groups), Value = f.GetRawConstantValue().ToString() })
                .OrderBy(i => i.Group != null ? i.Group.Name : string.Empty)
                .ThenBy(i => i.Text)
                ;

            foreach (var item in items)
            {
                list.Add(item);
            }

            return list;
        }

        internal static
            bool IsValidForEnum
            (Type type, out bool isArray, out bool isNullable, out Type enumType)
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

        internal static
            string ListItemToOption
            (SelectListItem
                item)
        {
            var builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            if (item.Disabled)
            {
                builder.Attributes["disabled"] = "disabled";
            }
            return builder.ToString(TagRenderMode.Normal);
        }

        private static
            StringBuilder BuildItems
            (string optionLabel, IEnumerable<SelectListItem> selectList)
        {
            var listItemBuilder = new StringBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem
                {
                    Text = optionLabel,
                    Value = String.Empty,
                    Selected = false
                }));
            }

            // Group items in the SelectList if requested.
            // Treat each item with Group == null as a member of a unique group
            // so they are added according to the original order.
            var groupedSelectList = selectList.GroupBy(
                i => (i.Group == null) ? i.GetHashCode() : i.Group.GetHashCode());
            foreach (var group in groupedSelectList)
            {
                var optGroup = group.First().Group;

                // Wrap if requested.
                TagBuilder groupBuilder = null;
                if (optGroup != null)
                {
                    groupBuilder = new TagBuilder("optgroup");
                    if (optGroup.Name != null)
                    {
                        groupBuilder.MergeAttribute("label", optGroup.Name);
                    }
                    if (optGroup.Disabled)
                    {
                        groupBuilder.MergeAttribute("disabled", "disabled");
                    }
                    listItemBuilder.AppendLine(groupBuilder.ToString(TagRenderMode.StartTag));
                }

                foreach (var item in group)
                {
                    listItemBuilder.AppendLine(ListItemToOption(item));
                }

                if (optGroup != null)
                {
                    listItemBuilder.AppendLine(groupBuilder.ToString(TagRenderMode.EndTag));
                }
            }

            return listItemBuilder;
        }

        private static
            object GetModelStateValue
            (HtmlHelper htmlHelper, string key, Type destinationType)
        {
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }

        private static
            SelectListGroup GetGroup
            (MemberInfo field, List<SelectListGroup> groups)
        {
            var groupName = GetGroupName(field);

            if (string.IsNullOrEmpty(groupName))
            {
                return null;
            }

            var group = groups.FirstOrDefault(g => g.Name.Equals(groupName));

            if (group == null)
            {
                group = new SelectListGroup { Name = groupName };
                groups.Add(group);
            }

            return group;
        }

        private static
            string GetGroupName
            (MemberInfo
                field)
        {
            var customAttribute = field.GetCustomAttribute<DisplayAttribute>(false);
            if (customAttribute == null)
            {
                return null;
            }

            var groupName = customAttribute.GetGroupName();

            return !string.IsNullOrEmpty(groupName) ? groupName : null;
        }

        private static
            string GetDisplayName
            (MemberInfo
                field)
        {
            var customAttribute = field.GetCustomAttribute<DisplayAttribute>(false);
            if (customAttribute == null)
            {
                return field.Name;
            }

            var name = customAttribute.GetName();

            return !string.IsNullOrEmpty(name) ? name : field.Name;
        }

        private static
            bool HasFlags
            (Type
                type)
        {
            return type.GetCustomAttribute<FlagsAttribute>(false) != null;
        }
    }
}