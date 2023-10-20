using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Services.Interfaces;
using Services.Service;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{
    public class MembersController : BaseController
    {
        private readonly IMemberService _memberService;
        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        /// <summary>
        /// Get a list of all members.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var members = await _memberService.GetAllMembers();

            if (members == null)
            {
                return NotFound();
            }

            return Ok(members);
        }

        /// <summary>
        /// Get member by member ID.
        /// </summary>
        [Authorize]
        [HttpGet("{id}", Name = "MemberDetails")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        /// <summary>
        /// Create member.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MemberCreateView memberDto)
        {
            if (memberDto == null)
            {
                return BadRequest("Invalid member data");
            }

            await _memberService.CreateMember(memberDto);

            return Ok(await _memberService.GetMemberById(memberDto.MemberId));
        }

        /// <summary>
        /// Update member.
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MemberUpdateView memberDto)
        {
            var existingMember = await _memberService.GetMemberById(id);
            if (existingMember == null)
            {
                return NotFound("Member not found");
            }

            await _memberService.UpdateMember(id, memberDto);

            return Ok(await _memberService.GetMemberById(id));
        }

        /// <summary>
        /// Delete member.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingMember = await _memberService.GetMemberById(id);
            if (existingMember == null)
            {
                return NotFound("Member not found");
            }

            await _memberService.Delete(id);

            return Ok("Delete successful");
        }


    }
}
