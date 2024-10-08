﻿using Microsoft.AspNetCore.Mvc;
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
            if (!Enum.IsDefined(typeof(EngineType), engineType))
            {
                return BadRequest("Invalid Engine type");
            }

            if (string.IsNullOrWhiteSpace(inputs.Inputs))
            {
                return BadRequest("Wrong data provided.");
            }

            try
            {
                EngineType en = (EngineType)engineType;

                var engine = engineFactory.GetEngine(en);
                var result = await engine.GenerateLayoutAsync(inputs);

                if (result == null)
                {
                    return BadRequest("Layout generation failed.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("generateSection")]
        public async Task<IActionResult> GenerateSectionAsync([FromBody] AdditionalSectionModel sectionModel, [FromQuery] int engineType)
        {
            if (!Enum.IsDefined(typeof(EngineType), engineType))
            {
                return BadRequest("Invalid Engine type.");
            }

            if (string.IsNullOrWhiteSpace(sectionModel.InitialInputs))
            {
                return BadRequest("Wrong data provided.");
            }

            try
            {
                EngineType en = (EngineType)engineType;

                var engine = engineFactory.GetEngine(en);
                var result = await engine.GenerateSectionAsync(sectionModel);

                if (result == null)
                {
                    return BadRequest("Section generation failed.");
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
