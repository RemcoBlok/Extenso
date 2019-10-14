﻿using Extenso.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Extenso.AspNetCore.Mvc.ExtensoUI.Providers
{
    public class Bootstrap4UIProvider : BaseUIProvider
    {
        private IAccordionProvider accordionProvider;
        private IModalProvider modalProvider;
        private IPanelProvider panelProvider;
        private ITabsProvider tabsProvider;

        #region IExtensoUIProvider Members

        #region General

        public override IHtmlContent ActionLink(IHtmlHelper html, string text, State state, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null)
        {
            string stateCss = GetButtonCssClass(state);

            var builder = new FluentTagBuilder("a")
                .MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes))
                .MergeAttribute("href", Internal.UrlHelper.Action(actionName, controllerName, routeValues))
                .AddCssClass(stateCss)
                .SetInnerHtml(text);

            return new HtmlString(builder.ToString());
        }

        public override IHtmlContent Button(string text, State state, string onClick = null, object htmlAttributes = null)
        {
            string stateCss = GetButtonCssClass(state);

            var builder = new FluentTagBuilder("button")
                .MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes))
                .MergeAttribute("type", "button")
                .AddCssClass(stateCss)
                .SetInnerHtml(text);

            if (!string.IsNullOrEmpty(onClick))
            {
                builder.MergeAttribute("onclick", onClick);
            }

            return new HtmlString(builder.ToString());
        }

        public override IHtmlContent SubmitButton(string text, State state, object htmlAttributes = null)
        {
            string stateCss = GetButtonCssClass(state);

            var builder = new FluentTagBuilder("button")
                .MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes))
                .MergeAttribute("type", "submit")
                .AddCssClass(stateCss)
                .SetInnerHtml(text);

            return new HtmlString(builder.ToString());
        }

        #endregion General

        #region Special

        public override IAccordionProvider AccordionProvider =>
            accordionProvider ?? (accordionProvider = new Bootstrap4AccordionProvider());

        public override IModalProvider ModalProvider =>
            modalProvider ?? (modalProvider = new Bootstrap4ModalProvider());

        public override IPanelProvider PanelProvider =>
            panelProvider ?? (panelProvider = new Bootstrap4PanelProvider());

        public override ITabsProvider TabsProvider =>
            tabsProvider ?? (tabsProvider = new Bootstrap4TabsProvider());

        #endregion Special

        #endregion IExtensoUIProvider Members

        protected override string GetButtonCssClass(State state)
        {
            switch (state)
            {
                case State.Primary: return "btn btn-primary";
                case State.Secondary: return "btn btn-secondary";
                case State.Success: return "btn btn-success";
                case State.Danger: return "btn btn-danger";
                case State.Warning: return "btn btn-warning";
                case State.Info: return "btn btn-info";
                case State.Light: return "btn btn-light";
                case State.Dark: return "btn btn-dark";
                default: return "btn btn-secondary";
            }
        }
    }
}