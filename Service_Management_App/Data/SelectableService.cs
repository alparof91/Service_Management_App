namespace Service_Management_App.Data
{
    public class SelectableService
    {
        public bool IsSelected { get; set; }
        public Service Service { get; set; }

        public SelectableService(bool isSelected, Service service)
        {
            IsSelected = isSelected;
            Service = service;
        }
    }
}
