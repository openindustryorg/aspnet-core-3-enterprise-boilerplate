using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Web.Core.Helpers;

namespace Web.Host.Controllers
{
  public class GenericControllerRouteConvention : IControllerModelConvention
  {
    public void Apply(ControllerModel controller)
    {
      if (controller.ControllerType.IsGenericType)
      {
        var genericType = controller.ControllerType.GenericTypeArguments[0];
        var customNameAttribute = genericType.GetCustomAttribute<GeneratedAttribute>();

        if (customNameAttribute?.Route != null)
        {
          controller.Selectors.Add(new SelectorModel
          {
            AttributeRouteModel = new AttributeRouteModel( new Microsoft.AspNetCore.Mvc.RouteAttribute(customNameAttribute.Route){Name=customNameAttribute.Route}
            ),
          });
        }
      }
    }
  }
}