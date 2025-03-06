﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace InternalJobPortalMVC.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IModelMetadataProvider mmdp;
        public CustomExceptionFilter(IModelMetadataProvider metadataProvider)
        {
            mmdp = metadataProvider;
        }   
        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "Error" };
            result.ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(mmdp, context.ModelState);
            result.ViewData.Add("ErrorMessage", context.Exception.Message);
            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}
