namespace MinimalCrud.Models
{
    public class User
    {
        // Id пользователя
        public Guid Id{get;set;}

        // Имя пользователя
        public string Name{get;set;}

        // Email пользователя
        public string Email{get;set;}
    }
}