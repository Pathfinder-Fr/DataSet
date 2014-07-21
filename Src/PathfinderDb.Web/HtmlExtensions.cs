// -----------------------------------------------------------------------
// <copyright file="HtmlExtensions.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class HtmlExtensions
    {
        public static MvcHtmlString EnumListBoxFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata == null)
                throw new ArgumentException("expression");
            if (metadata.ModelType == null)
                throw new ArgumentException("expression");


            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);
            Enum @enum = null;
            if (!string.IsNullOrEmpty(fullHtmlFieldName))
            {
                ModelState modelState;
                if (htmlHelper.ViewData.ModelState.TryGetValue(fullHtmlFieldName, out modelState) && modelState.Value != null)
                    @enum = modelState.Value.ConvertTo(metadata.ModelType, null) as Enum;
            }

            if (@enum == null && !string.IsNullOrEmpty(expressionText))
                @enum = htmlHelper.ViewData.Eval(expressionText) as Enum;

            if (@enum == null)
                @enum = metadata.Model as Enum;

            var selectList = EnumHelper.GetSelectList(metadata.ModelType, @enum);
            if (!string.IsNullOrEmpty(optionLabel) && selectList.Count != 0 && string.IsNullOrEmpty(selectList[0].Text))
            {
                selectList[0].Text = optionLabel;
                optionLabel = null;
            }

            return SelectExtensions.ListBoxHelper(htmlHelper, metadata, expressionText, selectList, optionLabel, htmlAttributes);
        }
    }
}