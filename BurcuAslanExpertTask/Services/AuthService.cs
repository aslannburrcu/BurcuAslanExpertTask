using BurcuAslanExpertTask.Database;
using BurcuAslanExpertTask.Helpers;
using BurcuAslanExpertTask.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace BurcuAslanExpertTask.Services
{
    public class AuthService
    {
        protected ApplicationDbContext _dbContext;

        public AuthService()
        {
            _dbContext = new ApplicationDbContext();
        }

        /// <summary>
        /// Authenticate User Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public async Task<ResultModel> Authenticate(LoginModel model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(i => i.UserName == model.Username);

            if(user == null)
            {
                return new ResultModel
                {
                    IsSuccessful = false,
                    Message = "kullanıcı adınız yada şifreniz yanlış!",
                    Code = (int)HttpStatusCode.Unauthorized
                };
            }
            
            if(!HashManager.VerifyPassword(model.Password, user.Password))
            {
                return new ResultModel
                {
                    IsSuccessful = false,
                    Message = "şifreniz yada kullanıcı adınız hatalı!",
                    Code = (int)HttpStatusCode.Unauthorized
                };
            }

            return new ResultModel
            {
                IsSuccessful = true,
                Data = user,
                Message = "User logged in successfully!",
                Code = (int)HttpStatusCode.OK
            };
        }

        public async Task<ResultModel>Register(RegisterModel model)
        {
            var isUserExist = _dbContext.Users.Any(i => i.UserName == model.UserName);

            if (!isUserExist)
            {
                var password = HashManager.HashPassword(model.Password);
                var user = _dbContext.Users.Add(new Entities.User
                {
                    UserName = model.UserName,
                    Password = password,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                });
                await _dbContext.SaveChangesAsync();

                return new ResultModel
                {
                    IsSuccessful = true,
                    Data = user,
                    Message = "Kullanıcı Başarılı Şekilde Oluşturuldu!",
                    Code = (int)HttpStatusCode.OK
                };
            }
            else
            {
                return new ResultModel
                {
                    IsSuccessful = false,
                    Data = null,
                    Message = "Kullanıcı adı zaten var!",
                    Code = (int)HttpStatusCode.BadRequest
                };
            }
        }

    }
}