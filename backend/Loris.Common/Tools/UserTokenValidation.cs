using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loris.Common.Tools
{
    public static class UserTokenValidation
    {
        public static string GetUserKey => Guid.NewGuid().ToString();

        public static string GenerateToken(string loginId, string userKey = null)
        {
            var bTime = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] bUserKey = { };
            if (!string.IsNullOrEmpty(userKey?.Trim()))
            {
                bUserKey = Guid.Parse(userKey).ToByteArray();
            }
            var bLoginId = Encoding.UTF8.GetBytes(loginId);
            var data = new byte[bTime.Length + bUserKey.Length + bLoginId.Length];

            Buffer.BlockCopy(bTime, 0, data, 0, bTime.Length);
            Buffer.BlockCopy(bUserKey, 0, data, bTime.Length, bUserKey.Length);
            Buffer.BlockCopy(bLoginId, 0, data, (bTime.Length + bUserKey.Length), bLoginId.Length);

            return Convert.ToBase64String(data.ToArray());
        }

        public static List<TokenValidationStatus> ValidateToken(string token, string loginId, string userKey = null)
        {
            var result = new List<TokenValidationStatus>();
            var data = Convert.FromBase64String(token);
            var bTime = data.Take(8).ToArray();
            byte[] bUserKey = { };
            if (!string.IsNullOrEmpty(userKey?.Trim()))
            {
                bUserKey = data.Skip(bTime.Length).Take(bTime.Length + 8).ToArray();
            }
            var bLoginId = data.Skip(bTime.Length + bUserKey.Length).ToArray();

            var time = DateTime.FromBinary(BitConverter.ToInt64(bTime, 0));
            if (time < DateTime.UtcNow.AddHours(-24))
            {
                result.Add(TokenValidationStatus.Expired);
            }

            if (!string.IsNullOrEmpty(userKey?.Trim()))
            {
                var userKeyIn = new Guid(bUserKey).ToString();
                if (!userKeyIn.Equals(userKey))
                {
                    result.Add(TokenValidationStatus.WrongGuid);
                }
            }

            var loginIdIn = Encoding.UTF8.GetString(bLoginId);
            if (!loginIdIn.Equals(loginId))
            {
                result.Add(TokenValidationStatus.WrongUser);
            }

            return result;
        }
    }

    public enum TokenValidationStatus
    {
        Expired,
        WrongUser,
        WrongGuid
    }
}
