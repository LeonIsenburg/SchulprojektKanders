using Microsoft.EntityFrameworkCore;
using Backend.Repository.Interfaces;

namespace Backend.Repository
{
    public class RequestRepository : iRequestRepository
    {
        public AppDbContext context;
        public RequestRepository(AppDbContext _context)
        {
            context = _context;
        }
        public "Klasse" Create("Klasse" name)
        {
            context.
        }
        public "Klasse" GetAll();
        public "Klasse" GetById();
        public "Klasse" Update("Klasse" name);
        public "Klasse" Delete("Klasse" name);
    }
}