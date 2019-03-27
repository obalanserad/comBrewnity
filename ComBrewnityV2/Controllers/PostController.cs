using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ComBrewnityV2.DTOs;
using ComBrewnityV2.Exceptions;
using ComBrewnityV2.Services;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace ComBrewnityV2.Controllers
{
    [EnableCors]
    [Authorize(Policy = "User")]
    [Route("api/ver1/[controller]/[action]")]
    [ApiController]
    public class PostController : BaseApiController
    {
        private PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        public IActionResult GetAllSubscriptions(string userId)
        {
            try
            {
                return Ok(_postService.GetAllSubscriptions(userId));
            }
            catch (Exception e)
            {
                throw new ApiDataException(3000, e.Message, HttpStatusCode.NoContent);
            }
        }

        //api/ver1/Post/GetAllUserPosts
        public IActionResult GetAllUserPosts(string userId)
        {
            try
            {
                return Ok(_postService.GetAllUserPosts(userId));
            }
            catch (Exception e)
            {
                throw new ApiDataException(3000, e.Message, HttpStatusCode.NoContent);
            }
        }

        // api/ver1/Post/GetPost/6
        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult GetUserPost(int id, string guid)
        {
            try
            {
                return Ok(_postService.GetPost(id, guid));
            }
            catch(Exception e)
            {
                throw new ApiDataException(3000, e.Message, HttpStatusCode.NotFound);
            }
        }

        // POST: api/Post
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
        // PUT: api/Post/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // api/ver1/Post/DeletePost/6
        [HttpDelete("{id}", Name = "DeletePost")]
        public IActionResult DeletePost(int id, string userId)
        {

            if (_postService.DeletePost(id))
            { 
                List<PostDto> posts = _postService.GetAllUserPosts(userId);
                return Ok(posts);
            }
            else
                throw new ApiDataException(3000, "Gick inte att ta bort", HttpStatusCode.NoContent);
        }
    }
}
