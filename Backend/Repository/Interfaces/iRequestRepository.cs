using Backend.Models.Event;
using Backend.Models.Requests;

namespace Backend.Repository.Interfaces
{
    public interface iRequestRepository
    {
        public Task<Event> Create(EventRequest request, Guid memberId);
        public Task<List<Event>> GetAll();
        public Task<Event?> GetById(Guid id);
        public Task<Event?> Update(Guid id, EventRequest request);
        public Task<Event?> UpdateStatus(Guid id, Status status);
        public Task<bool> Delete(Guid id);
    }
}
