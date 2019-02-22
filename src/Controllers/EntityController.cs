using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SouliCool.Tutorials.Contracts;
using SouliCool.Tutorials.Entities;
using SouliCool.Tutorials.Entities.Models;

namespace SouliCool.Tutorials.Controllers
{
    [Route("api/entity")]
    public class EntityController : Controller
    {
        //private EntitiesDbContext _context;
        private IRepositoryWrapper _repoWrapper;
        private ILoggerManager _logger;

        public EntityController(IRepositoryWrapper repoWrapper, ILoggerManager logger)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            if (_repoWrapper.Entity.FindAll().Count() == 0)
            {
                _repoWrapper.Entity.Create(new Entity
                {
                    Id = 1,
                    Value = "Entity 1"
                });
                _repoWrapper.Entity.Save();
            }
        }       

        [HttpGet]
        public IActionResult Get()
        {            
            _logger.LogInfo("Get executed");
            return Ok(_repoWrapper.Entity.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInfo("Get by Id executed");
            var entity = _repoWrapper.Entity.FindByCondition(x=> x.Id == id);
            if (!entity.Any())
            {
                return NotFound("Entity not found");
            }
            else
            {
                return Ok(entity);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Entity newEntity)
        {
            _logger.LogInfo("Post executed");
            if (newEntity == null)
            {
                return BadRequest("Entity object is null");
            }

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("Invalid object model");
            //}

            _repoWrapper.Entity.Create(newEntity);
            _repoWrapper.Entity.Save();
            return Ok();
        }
    }
}