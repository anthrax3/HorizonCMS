﻿using System;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Horizon.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ValidateAntiForgeryTokenOnAllPosts : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext != null)
            {
                var request = filterContext.HttpContext.Request;

                //  Only validate POSTs
                if (request.HttpMethod == WebRequestMethods.Http.Post)
                {
                    //  Ajax POSTs and normal form posts have to be treated differently when it comes
                    //  to validating the AntiForgeryToken
                    if (request.IsAjaxRequest())
                    {
                        var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

                        var cookieValue = antiForgeryCookie != null
                            ? antiForgeryCookie.Value
                            : null;

                        AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                    }
                    else
                    {
                        new ValidateAntiForgeryTokenAttribute()
                            .OnAuthorization(filterContext);
                    }
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public sealed class ValidateAntiForgeryTokenOnAllPostsNoAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext != null)
            {
                var request = filterContext.HttpContext.Request;

                //  Only validate POSTs
                if (request.HttpMethod == WebRequestMethods.Http.Post)
                {
                    //  Ajax POSTs and normal form posts have to be treated differently when it comes
                    //  to validating the AntiForgeryToken
                    if (request.IsAjaxRequest())
                    {
                        var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

                        var cookieValue = antiForgeryCookie != null
                            ? antiForgeryCookie.Value
                            : null;

                        AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                    }

                }

            }
        }
    }
}