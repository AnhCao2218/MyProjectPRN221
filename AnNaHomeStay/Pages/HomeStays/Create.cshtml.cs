using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnNaHomeStay.Models;
using Microsoft.AspNetCore.Authorization;
using AnNaHomeStay.Contants;
using System.ComponentModel.DataAnnotations;

namespace AnNaHomeStay.Pages.HomeStays
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AnNaHomeStayContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(AnNaHomeStayContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Homestay Homestay { get; set; }

        [Required(ErrorMessage = "Please choose at least one file")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public List<IFormFile> FileUploads { get; set; }

        public List<string> UploadedFiles { get; set; }

        public IActionResult OnGet()
        {
            UploadedFiles = new List<string>();
            ViewData["HomeStayType"] = new SelectList(HomeStayType.Instance);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Homestay == null)
            {
                return Page();
            }

            UploadedFiles = new List<string>();

            _context.Homestays.Add(Homestay);
            await _context.SaveChangesAsync();

            if (FileUploads != null && FileUploads.Count > 0)
            {
                foreach (var fileUpload in FileUploads)
                {
                    if (fileUpload.Length > 0)
                    {
                        // Xác định đường dẫn thư mục lưu trữ file
                        var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "Images");
                        if (!Directory.Exists(uploadsFolderPath))
                        {
                            Directory.CreateDirectory(uploadsFolderPath);
                        }

                        // Tạo tên file duy nhất
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileUpload.FileName;
                        var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                        // Sao chép nội dung của file tải lên vào file mới
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await fileUpload.CopyToAsync(fileStream);
                        }

                        // Tạo đối tượng Image đại diện cho file và lưu vào cơ sở dữ liệu
                        Image image = new Image()
                        {
                            HomstayId = Homestay.HomestayId,
                            Link = Path.Combine("/Images", uniqueFileName)
                        };
                        _context.Images.Add(image);
                        await _context.SaveChangesAsync();

                        // Thêm đường dẫn của file vào danh sách UploadedFiles
                        UploadedFiles.Add(Path.Combine("/Images", uniqueFileName));
                    }
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
