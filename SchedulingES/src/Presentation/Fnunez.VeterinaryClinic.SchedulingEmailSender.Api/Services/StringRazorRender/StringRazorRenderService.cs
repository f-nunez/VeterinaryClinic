using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.StringRazorRender;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.StringRazorRender;

public class StringRazorRenderService : IStringRazorRenderService
{
    private readonly IRazorViewEngine _razorViewEngine;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITempDataProvider _tempDataProvider;

    public StringRazorRenderService(
        IRazorViewEngine razorViewEngine,
        IServiceProvider serviceProvider,
        ITempDataProvider tempDataProvider)
    {
        _razorViewEngine = razorViewEngine;
        _serviceProvider = serviceProvider;
        _tempDataProvider = tempDataProvider;
    }

    public async Task<string> RenderRazorToStringAsync(
        string viewName,
        object? model,
        bool isMainPage = true)
    {
        var actionContext = GetActionContext();

        var view = FindView(actionContext, viewName, isMainPage);

        await using var output = new StringWriter();
        var viewContext = new ViewContext(
            actionContext,
            view,
            new ViewDataDictionary(
                metadataProvider: new EmptyModelMetadataProvider(),
                modelState: new ModelStateDictionary())
            {
                Model = model
            },
            new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
            output,
            new HtmlHelperOptions()
        );

        await view.RenderAsync(viewContext);

        return output.ToString();
    }

    private IView FindView(
        ActionContext actionContext,
        string viewName,
        bool isMainPage)
    {
        var getViewResult = _razorViewEngine
            .GetView(null, viewName, isMainPage);

        if (getViewResult.Success)
            return getViewResult.View;

        var findViewResult = _razorViewEngine
            .FindView(actionContext, viewName, isMainPage);

        if (findViewResult.Success)
            return findViewResult.View;

        var searchedLocations = getViewResult
            .SearchedLocations
            .Concat(findViewResult.SearchedLocations);

        var errorMessage = string.Join(
            Environment.NewLine,
            new[]
            {
                $"Unable to find the view: '{viewName}'. Searched at followed paths: "
            }.Concat(searchedLocations)
        );

        throw new InvalidOperationException(errorMessage);
    }

    private ActionContext GetActionContext()
    {
        var httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProvider
        };

        return new ActionContext(
            httpContext,
            new RouteData(),
            new ActionDescriptor()
        );
    }
}