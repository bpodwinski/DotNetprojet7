using P7CreateRestApi.DTOs;

namespace P7CreateRestApi.Services
{
    public interface IBidListService
    {
        public List<BidListDTO> List();
        public BidListDTO? Create(BidListDTO inputModel);
        public BidListDTO? Get(int id);
        public BidListDTO? Update(int id, BidListDTO inputModel);
        public BidListDTO? Delete(int id);
    }
}