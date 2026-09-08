
// این کلاس مسئولیت تولید توکن JWT برای کاربران احراز هویت شده
// را بر عهده دارد.
// 
// ساختار توکن JWT
// Header: الگوریتم امضا و نوع توکن
// Payload: Claimهااطلاعات کاربر و metadata
// Signature: امضای دیجیتال برای اعتبارسنجی
// 
// Claimهای شامل:
// NameIdentifier: شناسه یکتای کاربر UserId
// Email: آدرس ایمیل کاربر
// Name: نام کامل کاربر FirstName + LastName
// 
// تنظیمات امنیتی:
//  SecretKey: کلید محرمانه برای امضای توکن باید قوی و طولانی باشد
// Issuer: صادرکننده توکن برای اعتبارسنجی
// Audience: مخاطب توکن برای اعتبارسنجی
// Expiration: مدت اعتبار توکن 7 روز
// 
// نکات مهم
// SecretKey باید در appsettings.json ذخیره شود نه hardcode
// Encoding باید در تولید و اعتبارسنجی یکسان باشد (UTF8)
// DateTime.UtcNow برای جلوگیری از مشکلات timezone
// ══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Application.Common
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(ApplicationUser user, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDays = Convert.ToDouble(jwtSettings["ExpireDays"]);
            var expires = DateTime.UtcNow.AddDays(expireDays);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}