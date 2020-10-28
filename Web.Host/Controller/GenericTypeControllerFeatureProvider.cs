using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Web.Application.Controllers;
using Web.Core.Helpers;

namespace Web.Host.Controllers
{
  public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
  {
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
      var currentAssemblyxxx = typeof(GenericTypeControllerFeatureProvider).Assembly;
      var currentAssembly = AppDomain.CurrentDomain.GetAssemblies()
        .Where(x => x.FullName.Contains("Web.Core"))
        .First();

      var candidates = currentAssembly.GetExportedTypes().Where(x =>
        x.GetCustomAttributes<GeneratedAttribute>().Any()
      );

      foreach (var candidate in candidates)
      {
        feature.Controllers.Add(
            typeof(GenericBaseController<>).MakeGenericType(candidate).GetTypeInfo()
        );
      }
    }
  }
}