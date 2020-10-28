using System;

namespace Web.Core.Helpers
{

  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class GeneratedAttribute : Attribute
  {
    public GeneratedAttribute(string route)
    {
      Route = route;
    }

    public string Route { get; set; }
  }
}