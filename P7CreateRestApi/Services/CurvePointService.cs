using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {
        private readonly ICurvePointRepository _curvePointRepository;
        private readonly LocalDbContext _dbContext;

        public CurvePointService(
            ICurvePointRepository curvePointRepository,
            LocalDbContext dbContext
        )
        {
            _curvePointRepository = curvePointRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieves all CurvePoint entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of CurvePointDTOs</returns>
        public async Task<List<CurvePointDTO>> GetAll()
        {
            try
            {
                var curvePoints = await _curvePointRepository.GetAll();
                return curvePoints.Select(ToCurvePointDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all CurvePoints.", ex);
            }
        }

        /// <summary>
        /// Creates a new CurvePoint entity based on the provided dto.
        /// </summary>
        /// <param name="dto">The CurvePointDTO object containing the data for the new entity</param>
        /// <returns>The created CurvePointDTO</returns>
        public async Task<CurvePointDTO?> Create(CurvePointDTO dto)
        {
            try
            {
                var curvePoint = ToCurvePoint(dto);
                await _curvePointRepository.Create(curvePoint);
                return ToCurvePointDTO(curvePoint);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating a new CurvePoint.", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to retrieve</param>
        /// <returns>The CurvePointDTO or null if not found</returns>
        public async Task<CurvePointDTO?> GetById(int id)
        {
            try
            {
                var curvePoint = await _curvePointRepository.GetById(id);
                return curvePoint != null ? ToCurvePointDTO(curvePoint) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving CurvePoint with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Updates a specific CurvePoint entity.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to update</param>
        /// <param name="dto">The CurvePointDTO with updated values</param>
        /// <returns>The updated CurvePointDTO or null if not found</returns>
        public async Task<CurvePointDTO?> Update(int id, CurvePointDTO dto)
        {
            try
            {
                var existingCurvePoint = await _curvePointRepository.GetById(id) ?? throw new Exception($"CurvePoint with ID {id} not found.");
                var curvePoint = ToCurvePoint(dto);

                var updatedCurvePoint = await _curvePointRepository.Update(existingCurvePoint);
                return updatedCurvePoint != null ? ToCurvePointDTO(updatedCurvePoint) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating CurvePoint with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Deletes a specific CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to delete</param>
        /// <returns>The deleted CurvePointDTO or null if not found</returns>
        public async Task<CurvePointDTO?> DeleteById(int id)
        {
            try
            {
                var existingCurvePoint = await _curvePointRepository.GetById(id);
                if (existingCurvePoint == null)
                {
                    return null;
                }

                _dbContext.Entry(existingCurvePoint).State = EntityState.Detached;

                var deletedCurvePoint = await _curvePointRepository.DeleteById(id);
                return deletedCurvePoint != null ? ToCurvePointDTO(deletedCurvePoint) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting CurvePoint with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Converts a BidListDTO to a BidList entity.
        /// </summary>
        /// <param name="dto">The BidListDTO containing data.</param>
        /// <returns>The corresponding BidList entity.</returns>
        private static CurvePoint ToCurvePoint(CurvePointDTO dto) => new()
        {
            Id = dto.Id,
            CurveId = dto.CurveId,
            AsOfDate = dto.AsOfDate,
            CurvePointValue = dto.CurvePointValue
        };

        /// <summary>
        /// Converts a BidList entity to a BidListDTO.
        /// </summary>
        /// <param name="bidList">The BidList entity to convert.</param>
        /// <returns>The corresponding BidListDTO.</returns>
        private static CurvePointDTO ToCurvePointDTO(CurvePoint curvePoint) => new()
        {
            Id = curvePoint.Id,
            CurveId = curvePoint.CurveId,
            AsOfDate = curvePoint.AsOfDate,
            CurvePointValue = curvePoint.CurvePointValue
        };
    }
}
