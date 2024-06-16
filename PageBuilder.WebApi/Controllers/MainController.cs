using Microsoft.AspNetCore.Mvc;
using PageBuilder.Core.Contracts;
using PageBuilder.Core.Enums;
using PageBuilder.Core.Models;
using PageBuilder.Core.Models.ComponentModels;

namespace PageBuilder.WebApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IEngineFactory engineFactory;

        public MainController(IEngineFactory engineFactory)
        {
            this.engineFactory = engineFactory;
        }

        [HttpPost("generateLayout")]
        public async Task<IActionResult> GenerateLayoutAsync([FromBody] CreateLayoutModel inputs, [FromQuery] int engineType)
        {
            if (engineType < 0 || engineType > 1)
            {
                return BadRequest("Invalid Engine type");
            }

            try
            {
                EngineType en = (EngineType)engineType;

                var engine = engineFactory.GetEngine(en);
                var result = await engine.GenerateLayoutAsync(inputs);

                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("generateSection")]
        public async Task<IActionResult> GenerateSectionAsync([FromBody] AdintionalSectionModel sectionModel, [FromQuery] int engineType)
        {
            if (engineType < 0 || engineType > 1)
            {
                return BadRequest("Invalid Engine type");
            }

            try
            {
                EngineType en = (EngineType)engineType;

                var engine = engineFactory.GetEngine(en);
                var result = await engine.GenerateSectionAsync(sectionModel);

                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("updateImage")]
        public async Task<IActionResult> UpdateImage()
        {
            // To Do

            var result = string.Empty;

            return Ok(result);

        }

        [HttpPost("imageColorsExtract")]
        public async Task<IActionResult> ImageColorsExtract([FromBody] CreateLayoutModel input, [FromQuery] int engineType)
        {
            if (engineType < 0 || engineType > 1)
            {
                return BadRequest("Invalid Engine type");
            }

            try
            {
                EngineType en = (EngineType)engineType;

                var engine = engineFactory.GetEngine(en);
                var result = await engine.ImageColorExtractAsync(input);

                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("imageGenerator")]
        public async Task<IActionResult> ImageGenerator([FromBody] CreateLayoutModel input, [FromQuery] int engineType)
        {
            if (engineType < 0 || engineType > 1)
            {
                return BadRequest("Invalid Engine type");
            }

            try
            {
                EngineType en = (EngineType)engineType;

                var engine = engineFactory.GetEngine(en);
                var result = await engine.GenerateImageAsync(input);

                if (result == null)
                {
                    return BadRequest();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
