using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;




namespace RunInGroup.Data.Interface
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhototAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(String publicId);


    }
}
