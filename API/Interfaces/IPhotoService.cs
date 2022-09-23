using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    // Photo service for CRUD photo functionality
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}