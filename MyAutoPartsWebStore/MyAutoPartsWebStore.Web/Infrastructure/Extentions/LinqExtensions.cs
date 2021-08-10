namespace MyAutoPartsWebStore.Web.Infrastructure.Extentions
{
    using System.Collections.Generic;
    using System.Linq;
    using MyAutoPartsWebStore.Web.Base;

    public static class LinqExtensions
    {
        public static IEnumerable<TSource> CategoriesOrder<TSource>(this IEnumerable<TSource> enumerable, string categoryName)
            where TSource : BaseCategoryModel
        {
            var newlyCollection = new List<TSource>();

            var neededCategories = enumerable.Where(x => x.Category == categoryName).ToList();

            newlyCollection.AddRange(neededCategories);

            var leftCategories = enumerable.Where(x => x.Category != categoryName).ToList();

            newlyCollection.AddRange(leftCategories);

            return newlyCollection;
        }
    }
}