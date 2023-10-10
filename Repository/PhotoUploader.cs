using AutoMapper;
using Backend_Task.Data;
using Backend_Task.Entities;
using Backend_Task.Models.Product;
using Backend_Task.Repository.IRepository;

namespace Backend_Task.Repository
{
    public class PhotoUploader:IPhotoUploader
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoUploader(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment) 
        {
            this._context = context;
            this._mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> DeleteImage(BaseProduct product)
        {
            Product p = _mapper.Map<Product>(product);
            var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, p.FileUrl.TrimStart('\\'));
            if (File.Exists(oldImage))
            {
                File.Delete(oldImage);
            }
            return "";
            
        }

        public async Task<String> UploadImage(BaseProduct product)
        {
            Product p = _mapper.Map<Product>(product);
            if (p.File != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(p.File.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images");

                if (!string.IsNullOrEmpty(p.FileUrl))
                {
                    var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, p.FileUrl.TrimStart('\\'));
                    if (File.Exists(oldImage))
                    {
                        File.Delete(oldImage);
                    }
                }

                using (var filestream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create))
                {
                    p.File.CopyTo(filestream);
                    p.FileUrl = @"\Images\" + fileName;
                    _context.Update(p);
                    await _context.SaveChangesAsync();
                }
                return p.FileUrl;
            }
            return "";
        }
    }
}

