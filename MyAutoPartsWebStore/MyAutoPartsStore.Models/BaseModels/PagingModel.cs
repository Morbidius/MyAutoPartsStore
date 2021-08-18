namespace MyAutoPartsStore.Models.BaseModels
{
    public interface IPagingModel
    {
        public int CurrentPage { get; set; }

        public int? NextPage { get; set; }

        public int? PreviousPage { get; set; }

        public int MaxPages { get; set; }
    }
}