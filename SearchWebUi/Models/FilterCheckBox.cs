namespace SearchWebUi.Models
{
    public class FilterCheckBox
    {

        public string FilterName { get; set; }
        public bool Selected { get; set; }
        public FilterType Type { get; set; }
    }

    public enum FilterType
    {
        Product = 1, 
        OrgLocation = 2, 
        Status = 3, 
        Date = 4
    }
}