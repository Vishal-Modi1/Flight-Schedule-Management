namespace ViewModels.VM
{
    public class DatatableParams
    {
        public string SearchText { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public string SortOrderColumn { get; set; }

        public string OrderType { get; set; }
    }
}
