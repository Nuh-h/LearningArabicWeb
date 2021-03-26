using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArabicLearning.Models;
using ArabicLearning.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ArabicLearning.Controllers
{
    [Route("Images")]
    public class ImagesController : Controller
    {
        private readonly IImagesRepository imrepo;
        [BindProperty] Image image { get; set; }
        public ImagesController(IImagesRepository imagerepo)
        {
            imrepo = imagerepo;
        }
        public IActionResult Images(int? id)
        {
            image = new Image();
            if (id == null)
            {
                image = imrepo.GetImage(0);
                return View(image);
            }
            
            image = imrepo.GetImage((int)id);

            if (image == null) return NotFound();

            return View(image);
        }
    }
}
