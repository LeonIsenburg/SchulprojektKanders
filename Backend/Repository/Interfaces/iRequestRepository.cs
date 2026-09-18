namespace Backend.Repository.Interfaces
{
    public interface iRequestRepository
    {
        public "Klasse" Create("Klasse" name);
        public "Klasse" GetAll();
        public "Klasse" GetById();
        public "Klasse" Update("Klasse" name);
        public "Klasse" Delete("Klasse" name);

    }
}