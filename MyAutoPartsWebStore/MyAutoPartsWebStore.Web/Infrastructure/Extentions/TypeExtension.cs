namespace MyAutoPartsWebStore.Web.Infrastructure.Extentions
{
    using Microsoft.AspNetCore.Mvc;
    using System;

    public static class TypeExtension
    {
        public static string GetControllerName(this Type controllerType)
            => controllerType.Name.Replace(nameof(Controller), string.Empty);
    }
}