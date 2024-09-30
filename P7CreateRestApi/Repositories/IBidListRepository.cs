using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IBidListRepository
    {
        public List<BidList> List();
        public void Create(BidList bidList);
        public BidList? Get(int id);
        public BidList? Update(BidList bidList);
        public BidList? Delete(int id);
    }
}