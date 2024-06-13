using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Models;
using System.Drawing;

namespace PageBuilder.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IMainService mainService;

        public MainController(IMainService mainService)
        {
            this.mainService = mainService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] CreatePageModel jsonRequest)
        {
            if (jsonRequest == null)
            {
                return BadRequest("Invalid Data");
            }

            try
            {
                var result = await mainService.GeneratePageAsync(jsonRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpPost("update")]
        public async Task<IActionResult> Update([FromQuery] UpdatePageModel updateData, CreatePageModel currentData)
        {
            // To Do

            var result = await mainService.UpdatePageAsync(updateData, currentData);

            return Ok(result);

        }

        [HttpPost("updateImage")]
        public async Task<IActionResult> UpdateImage()
        {
            // To Do

            var result = string.Empty;

            return Ok(result);

        }

        [HttpPost("imageColorsExtract")]
        public async Task<IActionResult> ImageColorsExtract([FromBody] CreatePageModel jsonRequest)
        {
            if (jsonRequest == null)
            {
                return BadRequest("Invalid Data");
            }

            try
            {
                var result = await mainService.ImageColorExtractAsync(jsonRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("imageGenerator")]
        public async Task<IActionResult> ImageGenerator([FromBody] CreatePageModel jsonRequest)
        {
            if (jsonRequest == null)
            {
                return BadRequest("Invalid Data");
            }

            try
            {
                var result = await mainService.GenerateImageAsync(jsonRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
