using Backend_Task.Models.Product;

namespace Backend_Task.Repository.IRepository
{
    public interface IPhotoUploader
    {
        Task<string> UploadImage(BaseProduct p);
        Task<string> DeleteImage(BaseProduct p);
    }
}
