namespace LineLoginService.Models
{
    public class HomeIndexViewModel
    {
        public string AccessToken { get; set; }

        public bool IsAuthenticated { get; set; }

        public string ProviderID { get; set; }

        public string ProviderKey { get; set; }

        public string UserID { get; set; }

        public string Email { get; set; }

        public string ReturnUrl { get; set; }

    }

}
