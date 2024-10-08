using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Service class for managing operations on BidList entities.
    /// </summary>
    public class BidListService : IBidListService
    {
        private readonly IBidListRepository _bidListRepository;

        public BidListService(IBidListRepository bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }

        /// <summary>
        /// Asynchronously creates a new BidList entity based on the provided DTO.
        /// </summary>
        /// <param name="dto">The DTO containing data for the new BidList.</param>
        /// <returns>The created BidListDTO.</returns>
        public async Task<BidListDTO?> Create(BidListDTO dto)
        {
            try
            {
                var bidList = ToBidList(dto);
                bidList.CreationDate = DateTime.Now;

                await _bidListRepository.Create(bidList);
                return ToBidListDTO(bidList);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the BidList.", ex);
            }
        }

        /// <summary>
        /// Asynchronously deletes a BidList entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BidList to delete.</param>
        /// <returns>The deleted BidListDTO, or null if not found.</returns>
        public async Task<BidListDTO?> DeleteById(int id)
        {
            try
            {
                var bidList = await _bidListRepository.GetById(id) ?? throw new Exception($"BidList with ID {id} does not exist.");
                var deletedBidList = await _bidListRepository.DeleteById(id);
                return deletedBidList is not null ? ToBidListDTO(deletedBidList) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the BidList with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Asynchronously retrieves a BidList entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BidList to retrieve.</param>
        /// <returns>The BidListDTO, or null if not found.</returns>
        public async Task<BidListDTO?> GetById(int id)
        {
            var bidList = await _bidListRepository.GetById(id);
            return bidList is not null ? ToBidListDTO(bidList) : null;
        }

        /// <summary>
        /// Retrieves all BidList entities and converts them to BidListDTOs.
        /// </summary>
        /// <returns>A list of BidListDTOs.</returns>
        public async Task<List<BidListDTO>> GetAll()
        {
            var bidLists = await _bidListRepository.GetAll().ToListAsync();
            return bidLists.Select(ToBidListDTO).ToList();
        }

        /// <summary>
        /// Asynchronously updates a BidList entity with the provided data.
        /// </summary>
        /// <param name="id">The ID of the BidList to update.</param>
        /// <param name="dto">The DTO containing the updated values.</param>
        /// <returns>The updated BidListDTO, or null if not found.</returns>
        public async Task<BidListDTO?> Update(int id, BidListDTO dto)
        {
            _ = await _bidListRepository.GetById(id)
                ?? throw new Exception($"BidList with ID {id} not found.");

            var bidList = ToBidList(dto);
            bidList.BidListId = id;

            var updatedBidList = await _bidListRepository.Update(bidList);
            return updatedBidList != null ? ToBidListDTO(updatedBidList) : null;
        }

        /// <summary>
        /// Converts a BidListDTO to a BidList entity.
        /// </summary>
        /// <param name="dto">The BidListDTO containing data.</param>
        /// <returns>The corresponding BidList entity.</returns>
        private static BidList ToBidList(BidListDTO dto) => new()
        {
            Account = dto.Account,
            BidType = dto.BidType,
            BidQuantity = dto.BidQuantity,
            AskQuantity = dto.AskQuantity,
            Bid = dto.Bid,
            Ask = dto.Ask,
            Benchmark = dto.Benchmark,
            BidListDate = dto.BidListDate,
            Commentary = dto.Commentary,
            BidSecurity = dto.BidSecurity,
            BidStatus = dto.BidStatus,
            Trader = dto.Trader,
            Book = dto.Book,
            CreationName = dto.CreationName,
            RevisionName = dto.RevisionName,
            RevisionDate = dto.RevisionDate,
            DealName = dto.DealName,
            DealType = dto.DealType,
            SourceListId = dto.SourceListId,
            Side = dto.Side
        };

        /// <summary>
        /// Converts a BidList entity to a BidListDTO.
        /// </summary>
        /// <param name="bidList">The BidList entity to convert.</param>
        /// <returns>The corresponding BidListDTO.</returns>
        private static BidListDTO ToBidListDTO(BidList bidList) => new()
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
