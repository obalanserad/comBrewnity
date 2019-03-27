using AutoMapper;
using ComBrewnityV2.DTOs;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComBrewnityV2.Services
{
    public class PostService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PostService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PostDto> GetAllUserPosts(string userId)
        {
            List<Post> posts = _context.Posts.Where(x=>x.User.Id == userId).ToList();
            List<PostDto> returnList = _mapper.Map<List<PostDto>>(posts);
            return returnList;
        }

        public List<PostDto> GetAllSubscriptions(string userId)
        {
            List<Post> _subs = new List<Post>();
            List<UserSubscription> ids = _context.Users.Where(x => x.Id == userId).FirstOrDefault().UserSubscriptions.ToList();
            //foreach (var id in ids)
            //{
            //    List<Post> temp = _context.Posts.Where(x => x.User.Id == id).ToList();
            //    _subs.AddRange(temp);
            //}
            //_subs.OrderBy(x => x.Updated).ToList();

            List<PostDto> subsPosts = _mapper.Map<List<PostDto>>(_subs);
            return subsPosts;
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
