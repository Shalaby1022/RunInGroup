
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroopWebApp.Helpers;
using RunInGroup.Data.Interface;

namespace RunGroopWebApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloundinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloundinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhototAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloundinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloundinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}





















//using CloudinaryDotNet.Actions;
//using CloudinaryDotNet;
//using Microsoft.Extensions.Options;
//using RunInGroup.Data.Interface;
//using RunInGroup.Helpers;

//public class PhotoService : IPhotoService
//{
//    private readonly Cloudinary _cloundinary;

//    public PhotoService(IOptions<CloudinarySettings> config)
//    {
//        var acc = new Account(
//            config.Value.CloudName,
//            config.Value.CloudApi,
//            config.Value.ApiSecret
//            );

//        _cloundinary = new Cloudinary(acc);
//    }

//    public async Task<ImageUploadResult> AddPhototAsync(IFormFile file)
//    {
//        var uploadResult = new ImageUploadResult();

//        if (file.Length > 0)
//        {
//            using var stream = file.OpenReadStream();
//            var uploadParams = new ImageUploadParams
//            {
//                File = new FileDescription(file.FileName, stream),
//                Transformation = new Transformation()
//            };
//            uploadResult = await _cloundinary.UploadAsync(uploadParams);
//        }
//        return uploadResult;
//    }

//    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
//    {
//        var deleteParams = new DeletionParams(publicId);
//        return await _cloundinary.DestroyAsync(deleteParams);
//    }
//}

