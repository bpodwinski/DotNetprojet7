using Dot.Net.WebApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class BidListService : IBidListService
    {
        private readonly IBidListRepository _bidListRepository;
        public BidListService(IBidListRepository bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }

        public BidListDTO? Create(BidListDTO inputModel)
        {
            var bidList = new BidList
            {
                Account = inputModel.Account,
                BidType = inputModel.BidType,
                BidQuantity = inputModel.BidQuantity,
                AskQuantity = inputModel.AskQuantity,
                Bid = inputModel.Bid,
                Ask = inputModel.Ask,
                Benchmark = inputModel.Benchmark,
                BidListDate = inputModel.BidListDate,
                Commentary = inputModel.Commentary,
                BidSecurity = inputModel.BidSecurity,
                BidStatus = inputModel.BidStatus,
                Trader = inputModel.Trader,
                Book = inputModel.Book,
                CreationName = inputModel.CreationName,
                CreationDate = DateTime.Now,
                RevisionName = inputModel.RevisionName,
                RevisionDate = inputModel.RevisionDate,
                DealName = inputModel.DealName,
                DealType = inputModel.DealType,
                SourceListId = inputModel.SourceListId,
                Side = inputModel.Side
            };
            _bidListRepository.Create(bidList);
            return ToOutputModel(bidList);
        }

        public BidListDTO? Delete(int id)
        {
            var bidList = _bidListRepository.Delete(id);
            if (bidList is not null)
            {
                return ToOutputModel(bidList);
            }
            return null;
        }

        public BidListDTO? Get(int id)
        {
            var bidList = _bidListRepository.Get(id);
            if (bidList is not null)
            {
                return ToOutputModel(bidList);
            }
            return null;
        }

        public List<BidListDTO> List()
        {
            var list = new List<BidListDTO>();
            var bidLists = _bidListRepository.List();
            foreach (var bidList in bidLists)
            {
                list.Add(ToOutputModel(bidList));
            }
            return list;
        }

        public BidListDTO? Update(int id, BidListDTO inputModel)
        {
            var bidList = _bidListRepository.Update(new BidList
            {
                BidListId = id,
                Account = inputModel.Account,
                BidType = inputModel.BidType,
                BidQuantity = inputModel.BidQuantity,
                AskQuantity = inputModel.AskQuantity,
                Bid = inputModel.Bid,
                Ask = inputModel.Ask,
                Benchmark = inputModel.Benchmark,
                BidListDate = inputModel.BidListDate,
                Commentary = inputModel.Commentary,
                BidSecurity = inputModel.BidSecurity,
                BidStatus = inputModel.BidStatus,
                Trader = inputModel.Trader,
                Book = inputModel.Book,
                CreationName = inputModel.CreationName,
                RevisionName = inputModel.RevisionName,
                RevisionDate = inputModel.RevisionDate,
                DealName = inputModel.DealName,
                DealType = inputModel.DealType,
                SourceListId = inputModel.SourceListId,
                Side = inputModel.Side
            });
            if (bidList is not null)
            {
                return ToOutputModel(bidList);
            }
            return null;
        }

        private BidListDTO ToOutputModel(BidList bidList) => new BidListDTO
        {
            BidListId = bidList.BidListId,
            Account = bidList.Account,
            BidType = bidList.BidType,
            BidQuantity = bidList.BidQuantity,
            AskQuantity = bidList.AskQuantity,
            Bid = bidList.Bid,
            Ask = bidList.Ask,
            Benchmark = bidList.Benchmark,
            BidListDate = bidList.BidListDate,
            Commentary = bidList.Commentary,
            BidSecurity = bidList.BidSecurity,
            BidStatus = bidList.BidStatus,
            Trader = bidList.Trader,
            Book = bidList.Book,
            CreationName = bidList.CreationName,
            CreationDate = bidList.CreationDate,
            RevisionName = bidList.RevisionName,
            RevisionDate = bidList.RevisionDate,
            DealName = bidList.DealName,
            DealType = bidList.DealType,
            SourceListId = bidList.SourceListId,
            Side = bidList.Side
        };
    }
}