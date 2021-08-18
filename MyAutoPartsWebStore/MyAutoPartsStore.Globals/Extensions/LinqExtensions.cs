namespace MyAutoPartsWebStore.Globals.Extensions
{
    using MyAutoPartsWebStore.Web.Base;
    using System.Collections.Generic;
    using System.Linq;

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

        public static IEnumerable<T> Paging<T>(this IEnumerable<T> query, int page = 1, int quantity = 10)
            => query.Skip((page - 1) * quantity)
                .Take(quantity);
    }
}