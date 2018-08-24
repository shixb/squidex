﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschränkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSwag.Annotations;
using Squidex.Areas.Api.Controllers.UI.Models;
using Squidex.Config;
using Squidex.Domain.Apps.Core.Rules.Actions;
using Squidex.Infrastructure.Commands;
using Squidex.Pipeline;

namespace Squidex.Areas.Api.Controllers.UI
{
    /// <summary>
    /// Manages ui settings and configs.
    /// </summary>
    [ApiExceptionFilter]
    [SwaggerTag(nameof(UI))]
    public sealed class UIController : ApiController
    {
        private readonly MyUIOptions uiOptions;
        private readonly TwitterOptions twitterOptions;

        public UIController(ICommandBus commandBus, IOptions<MyUIOptions> uiOptions, IOptions<TwitterOptions> twitterOptions)
            : base(commandBus)
        {
            this.twitterOptions = twitterOptions.Value;

            this.uiOptions = uiOptions.Value;
        }

        /// <summary>
        /// Get ui settings.
        /// </summary>
        [HttpGet]
        [Route("ui/settings/")]
        [ProducesResponseType(typeof(UISettingsDto), 200)]
        [ApiCosts(0)]
        public IActionResult GetSettings()
        {
            var dto = new UISettingsDto
            {
                MapType = uiOptions.Map?.Type ?? "OSM",
                MapKey = uiOptions.Map?.GoogleMaps?.Key,
                SupportsTwitterActions = twitterOptions.IsConfigured()
            };

            return Ok(dto);
        }
    }
}
