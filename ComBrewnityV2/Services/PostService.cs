using AutoMapper;
using ComBrewnityV2.DTOs;
using ComBrewnityV2.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComBrewnityV2.Services
{
    public class PostService : IPostService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PostService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PostDto> GetAll(string userId)
        {
            List<Post> posts = _context.Posts.Where(x=>x.User.Id == userId).ToList();
            List<PostDto> returnList = _mapper.Map<List<PostDto>>(posts);
            return returnList;
            

        }

        public PostDto GetPost(int id, string guid)
        {
            Post post = _context.Posts.First(x=>x.User.Id == guid && x.PostId == id);
            PostDto postDto = _mapper.Map<PostDto>(post);
            return postDto;
        }

        public bool DeletePost(int id)
        {
            Post post = _context.Posts.Where(x => x.PostId == id).FirstOrDefault();
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return true;
        }
    }
}
