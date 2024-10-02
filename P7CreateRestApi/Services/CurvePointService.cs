using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class CurvePointService : ICurvePointService
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointService(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        /// <summary>
        /// Retrieves all CurvePoint entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of CurvePointDTOs</returns>
        public async Task<List<CurvePointDTO>> ListAsync()
        {
            var curvePoints = await _curvePointRepository.List();
            return curvePoints.Select(ToDTO).ToList();
        }

        /// <summary>
        /// Creates a new CurvePoint entity based on the provided model.
        /// </summary>
        /// <param name="model">The CurvePointModel object containing the data for the new entity</param>
        /// <returns>The created CurvePointDTO</returns>
        public async Task<CurvePointDTO?> CreateAsync(CurvePointModel model)
        {
            var curvePoint = new CurvePoint
            {
                CurveId = model.CurveId,
                AsOfDate = model.AsOfDate,
                Term = model.Term,
                CurvePointValue = model.CurvePointValue,
                CreationDate = DateTime.Now
            };
            await _curvePointRepository.Create(curvePoint);
            return ToDTO(curvePoint);
        }

        /// <summary>
        /// Retrieves a specific CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to retrieve</param>
        /// <returns>The CurvePointDTO or null if not found</returns>
        public async Task<CurvePointDTO?> GetByIdAsync(int id)
        {
            var curvePoint = await _curvePointRepository.GetById(id);
            return curvePoint != null ? ToDTO(curvePoint) : null;
        }

        /// <summary>
        /// Updates a specific CurvePoint entity.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to update</param>
        /// <param name="model">The CurvePointModel with updated values</param>
        /// <returns>The updated CurvePointDTO or null if not found</returns>
        public async Task<CurvePointDTO?> UpdateByIdAsync(int id, CurvePointModel model)
        {
            var existingCurvePoint = await _curvePointRepository.GetById(id);
            if (existingCurvePoint == null)
            {
                return null;
            }

            // Mise à jour de l'entité
            existingCurvePoint.CurveId = model.CurveId;
            existingCurvePoint.AsOfDate = model.AsOfDate;
            existingCurvePoint.Term = model.Term;
            existingCurvePoint.CurvePointValue = model.CurvePointValue;

            var updatedCurvePoint = await _curvePointRepository.Update(existingCurvePoint);
            return ToDTO(updatedCurvePoint);
        }

        /// <summary>
        /// Deletes a specific CurvePoint by ID.
        /// </summary>
        /// <param name="id">The ID of the CurvePoint to delete</param>
        /// <returns>The deleted CurvePointDTO or null if not found</returns>
        public async Task<CurvePointDTO?> DeleteByIdAsync(int id)
        {
            var existingCurvePoint = await _curvePointRepository.GetById(id);
            if (existingCurvePoint == null)
            {
                return null;
            }

            var deletedCurvePoint = await _curvePointRepository.DeleteById(id);
            return deletedCurvePoint != null ? ToDTO(deletedCurvePoint) : null;
        }

        /// <summary>
        /// Maps a CurvePoint entity to a CurvePointDTO.
        /// </summary>
        /// <param name="curvePoint">The CurvePoint entity</param>
        /// <returns>The corresponding CurvePointDTO</returns>
        private CurvePointDTO ToDTO(CurvePoint curvePoint) =>
            new CurvePointDTO
            {
                Id = curvePoint.Id,
                CurveId = curvePoint.CurveId,
                AsOfDate = curvePoint.AsOfDate,
                CurvePointValue = curvePoint.CurvePointValue
            };
    }
}
