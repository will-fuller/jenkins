using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using ResearchTHM.Core;
using ResearchTHM.Core.Models;
using ResearchTHM.Core.Services;
using ResearchTHM.API.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace ResearchTHM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMktUserService _userService;
        private readonly IMktUserGroupAccessService _userGroupAccessService;
        public IConfiguration Configuration { get; }
        public UserController(IMktUserService userService, IConfiguration configuration, IMktUserGroupAccessService userGroupAccessService)
        {
            this._userService = userService;
            this._userGroupAccessService = userGroupAccessService;
            this.Configuration = configuration;           
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<List<MktContributor>>> GetUserGroupAccess(Guid groupid)
        {
            var groupaccess = await _userService.GetUserGroupAccess(groupid);
            return Ok(groupaccess);
        }

        [Authorize]
        [HttpPut("prefer")]
        public async Task<IActionResult> UpdatePrefContributor([FromQuery] string userid,string username,string language, [FromBody] List<string> prefContributor)
        {           
            if (await _userService.UpdatePrefContributor(userid,username,language, prefContributor))
                return Ok();
            else
                return Problem();
        }

        //[Authorize]
        //[HttpGet("all")]
        //[EnableQuery]
        //public async Task<ActionResult<IEnumerable<UserResource>>> GetUserById(string userid)
        //{
        //    if (userid == "0")
        //        return Ok(_mapper.Map<IEnumerable<MktUser>, IEnumerable<UserResource>>(_userService.Find(usr => (usr.Status == true && usr.IsDeleted == 0))));
        //    else
        //        return Ok(_mapper.Map<MktUser, UserResource>(await _userService.GetByIdAsync(new Guid(userid))));
        //}

        [HttpPost("login")]
        public async Task<ActionResult<IEnumerable<MktUser>>> Login(LoginData loginData)
        {

            VwUserInfoDeveloper data = new VwUserInfoDeveloper();
            if (!string.IsNullOrEmpty(loginData.userId)) {
                //data = (await _userService.GetUserGroupById(loginData.userId)).First();
                data = (await _userService.GetUserGroupById(loginData.userId));
            }
            if ((loginData.appId == Configuration["AppAuth:ApplicationId"].Trim()) && (loginData.skey == Configuration["AppAuth:SecretKey"].Trim()))
            {
                var secretKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
                var signinCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                if(!string.IsNullOrEmpty(loginData.userId) && data != null) {
                    var role = data.UserId.ToString().ToUpper();
                    var tempguid = data.GroupId.ToString().ToUpper();
                    var dev = Configuration["AppAuth:Developer"].ToUpper().Split(";");
                    var ad = Configuration["AppAuth:Admin"].ToUpper().Split(";");
                    if (Array.Exists(dev, ele => ele == tempguid))
                    {
                        role = "94D858DF-1857-4795-B7C3-81B004183CA09";
                    }
                    else if (Array.Exists(ad, ele => ele == tempguid))
                    {
                        role = tempguid;
                    }
                    var claims = new[] {
                        new Claim("userId",Convert.ToString(data.UserId)),
                        new Claim("emailId",Convert.ToString(data.EmailId)),
                        new Claim("groupId",Convert.ToString(data.GroupId)),
                        new Claim("groupName",Convert.ToString(data.GroupName)),
                        //new Claim("employeeId",Convert.ToString(data.Employeeid)),
                        new Claim("displayName",Convert.ToString(data.DisplayName)),
                        new Claim("firstName", Convert.ToString(data.FirstName)),
                        new Claim("lastName", Convert.ToString(data.LastName)),
                        new Claim("upn", Convert.ToString(data.userprincipalname)),
                        new Claim("location", (!string.IsNullOrEmpty(data.Location))? Convert.ToString(data.Location):""),
                        new Claim("oid",role),                        
                    };
                    var tokeOptions = new JwtSecurityToken(
                    issuer: Configuration["Jwt:Issuer"],
                    audience: Configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Configuration.GetValue<int>("AppAuth:TokenTimeout")),
                    signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
                else
                {
                    var tokeOptions = new JwtSecurityToken(
                        issuer: Configuration["Jwt:Issuer"],
                        audience: Configuration["Jwt:Audience"],
                        expires: DateTime.Now.AddMinutes(Configuration.GetValue<int>("AppAuth:TokenTimeout")),
                        signingCredentials: signinCredentials);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
            }
            else
            {
                return Ok("Invalid applicationId/SecretKey");
            }

        }

        [HttpPost("validate")]
        public async Task<ActionResult<IEnumerable<MktUser>>> Validate(LoginData loginData)
        {

            VwUserInfoDeveloper data = new VwUserInfoDeveloper();
            if (!string.IsNullOrEmpty(loginData.userId))
            {
                data = (await _userService.GetUserInfoByUPN(loginData.userId));
            }
            if ((loginData.appId == Configuration["AppAuth:ApplicationId"].Trim()) && (loginData.skey == Configuration["AppAuth:SecretKey"].Trim()))
            {
                var secretKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
                var signinCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                if (!string.IsNullOrEmpty(loginData.userId) && data != null)
                {
                    var role = data.UserId.ToString().ToUpper();
                    var tempguid = data.GroupId.ToString().ToUpper();
                    var dev = Configuration["AppAuth:Developer"].ToUpper().Split(";");
                    var ad = Configuration["AppAuth:Admin"].ToUpper().Split(";");
                    if (Array.Exists(dev, ele => ele == tempguid))
                    {
                        role = "94D858DF-1857-4795-B7C3-81B004183CA09";
                    }
                    else if(Array.Exists(ad, ele => ele == tempguid))
                    {
                        role = tempguid;
                    }
                    var claims = new[] {
                        new Claim("userId",Convert.ToString(data.UserId)),
                        new Claim("emailId",Convert.ToString(data.EmailId)),
                        new Claim("groupId",Convert.ToString(data.GroupId)),
                        new Claim("groupName",Convert.ToString(data.GroupName)),
                        //new Claim("employeeId",Convert.ToString(data.Employeeid)),
                        new Claim("displayName",Convert.ToString(data.DisplayName)),
                        new Claim("firstName", Convert.ToString(data.FirstName)),
                        new Claim("lastName", Convert.ToString(data.LastName)),
                        new Claim("upn", Convert.ToString(data.userprincipalname)),
                        new Claim("location", (!string.IsNullOrEmpty(data.Location))? Convert.ToString(data.Location):""),
                        new Claim("oid",role),                       
                    };
                    var tokeOptions = new JwtSecurityToken(
                    issuer: Configuration["Jwt:Issuer"],
                    audience: Configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Configuration.GetValue<int>("AppAuth:TokenTimeout")),
                    signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
                else
                {
                    var tokeOptions = new JwtSecurityToken(
                        issuer: Configuration["Jwt:Issuer"],
                        audience: Configuration["Jwt:Audience"],
                        expires: DateTime.Now.AddMinutes(Configuration.GetValue<int>("AppAuth:TokenTimeout")),
                        signingCredentials: signinCredentials);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
            }
            else
            {
                return Ok("Invalid applicationId/SecretKey");
            }

        }

        [HttpPost("sso")]
        public async Task<ActionResult<IEnumerable<MktUser>>> SSO(LoginData loginData)
        {

            VwUserInfoDeveloper data = new VwUserInfoDeveloper();
            if (!string.IsNullOrEmpty(loginData.userId))
            {
                data = (await _userService.GetUserInfoByUPN(loginData.userId));
            }
            if ((loginData.appId == Configuration["AppAuth:ApplicationId"].Trim()) && (loginData.skey == Configuration["AppAuth:SecretKey"].Trim()))
            {
                var secretKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
                var signinCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                if (!string.IsNullOrEmpty(loginData.userId) && data != null)
                {
                    var role = data.UserId.ToString().ToUpper();
                    var tempguid = data.GroupId.ToString().ToUpper();
                    var dev = Configuration["AppAuth:Developer"].ToUpper().Split(";");
                    var ad = Configuration["AppAuth:Admin"].ToUpper().Split(";");
                    if (Array.Exists(dev, ele => ele == tempguid))
                    {
                        role = "94D858DF-1857-4795-B7C3-81B004183CA09";
                    }
                    else if (Array.Exists(ad, ele => ele == tempguid))
                    {
                        role = tempguid;
                    }
                    var claims = new[] {
                        new Claim("userId",Convert.ToString(data.UserId)),
                        new Claim("emailId",Convert.ToString(data.EmailId)),
                        new Claim("groupId",Convert.ToString(data.GroupId)),
                        new Claim("groupName",Convert.ToString(data.GroupName)),
                        //new Claim("employeeId",Convert.ToString(data.Employeeid)),
                        new Claim("displayName",Convert.ToString(data.DisplayName)),
                        new Claim("firstName", Convert.ToString(data.FirstName)),
                        new Claim("lastName", Convert.ToString(data.LastName)),
                        new Claim("upn", Convert.ToString(data.userprincipalname)),
                        new Claim("location", (!string.IsNullOrEmpty(data.Location))? Convert.ToString(data.Location):""),
                        new Claim("oid",role),                        
                    };
                    var tokeOptions = new JwtSecurityToken(
                        issuer: Configuration["Jwt:Issuer"],
                        audience: Configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(10800),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new { Token = tokenString });
                }
                else
                {
                    return Ok(new { AccessDenied = true });
                }
            }
            else
            {
                return Ok("Invalid applicationId/SecretKey");
            }

        }


        [Authorize]
        [HttpGet("preferences")]
        [EnableQuery]
        public async Task<ActionResult<object>> GetUser(string userid)
        {
            var x = await _userService.GetUserGroupById(userid);
            if(x!=null)
                return Ok(x);
            else
                return Problem();
        }

        [Authorize]
        [HttpGet("languages")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<MktLanguages>>> GetLanguages()
        {
            var x = await _userService.GetLanguagesList();
            if(x!=null)
                return Ok(x);
            else
                return Problem();
        }

        [Authorize]
        [HttpGet("all")]
        [EnableQuery]
        public async Task<ActionResult<object>> GetUsers(string userid, bool IsDeveloper = false)
        {

            //if (userid == "0")
            //    return Ok(await _userService.GetUserGroupById(userid));
            //else
            //    return Ok(await _userService.GetUserGroupById(userid));

            var viewUser = await _userService.GetViewUserInfo(IsDeveloper);
            return viewUser;
        }

        [Authorize]
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(string userid, bool status)
        {
            if (await _userService.UpdateStatus(userid, status))
                return Ok();
            else
                return Problem();
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<MktUser>> CreateUser([FromBody] MktUser mktUser)
        {                      
            await _userService.AddAsync(mktUser);
            await _userService.SaveChangesAsync();

            if (mktUser.GroupId != null || mktUser.GroupId != "undefined")
            {
                var grpId = mktUser.GroupId.Split(';');
                List<MktUserGroupAccess> UserGroupList = new List<MktUserGroupAccess>();
                foreach (var item in grpId)
                {
                    UserGroupList.Add(new MktUserGroupAccess
                    {
                        UserId = mktUser.UserId,
                        GroupId = Guid.Parse(item)
                    });
                }

                return Ok(await _userGroupAccessService.CreateUserGroupAccess(null, UserGroupList));
            }

            return Ok(false);
        }

        //[Authorize]
        //[HttpPut("update")]
        //public async Task<ActionResult> UpdateUser(string userId, [FromBody] UserResource saveUserResources)
        //{
        //    if (saveUserResources.UserId.ToString() != userId)
        //        return BadRequest();

        //    var user = _mapper.Map<UserResource, MktUser>(saveUserResources);
        //    await _userService.UpdateUser(user, saveUserResources.UserId.ToString());
        //    var result = _userService.SaveChangesAsync();
        //    if (result.Status.ToString() == "RanToCompletion")
        //    {
        //        if (saveUserResources.GroupId != null || saveUserResources.GroupId != "undefined")
        //        {
        //            var grpId = saveUserResources.GroupId.Split(';');
        //            List<MktUserGroupAccess> UserGroupList = new List<MktUserGroupAccess>();
        //            foreach (var item in grpId)
        //            {
        //                UserGroupList.Add(new MktUserGroupAccess
        //                {
        //                    UserId = saveUserResources.UserId,
        //                    GroupId = Guid.Parse(item)
        //                });
        //            }

        //            var newUserGroupAccess = await _userGroupAccessService.CreateUserGroupAccess(userId, UserGroupList);
        //        }
        //    }
        //    return Ok();
        //}

    }

    public class LoginData
    {
        [Required]
        public string appId { get; set; }
        [Required]
        public string skey { get; set; }
        [DataType(DataType.Text)]
        public string userId { get; set; }
    }

}