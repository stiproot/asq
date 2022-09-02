using System;
using System.Security.Cryptography;
using ZoomClient.Providers;

namespace ZoomClient.Builder
{
    public class SignatureBuilder : ISignatureBuilder
    {
        private readonly IZoomSettingProvider _settingProvider;
        private readonly char[] _padding = { '=' };
        private string _role;
        private string _meetingNo;

        public SignatureBuilder(IZoomSettingProvider settingProvider)
        {
            this._settingProvider = settingProvider;
        }
        
        public ISignatureBuilder SetRole(string role)
        {
            this._role= role;
            return this;
        }
        public ISignatureBuilder SetMeetingNo(string meetingNo)
        {
            this._meetingNo = meetingNo;
            return this;
        }

        public string Build()
        {
            return this.GenerateToken();
        }

        private string GenerateToken()
        {
            String ts = (this.ToTimestamp(DateTime.UtcNow.ToUniversalTime()) - 30000).ToString();
            string msg = $"{this._settingProvider.GetApiKey()}{this._meetingNo}{ts}{this._role}";

            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(this._settingProvider.GetApiSecret());
            byte[] msgByteTest = encoding.GetBytes(msg);
            string msgHashPreHmac = System.Convert.ToBase64String(msgByteTest);
            byte[] msgBytes = encoding.GetBytes(msgHashPreHmac);
            using(var hmacSha256 = new HMACSHA256(keyByte))
            {
                byte[] hashMsg = hmacSha256.ComputeHash(msgBytes);
                string msgHash = System.Convert.ToBase64String(hashMsg);
                string token = $"{this._settingProvider.GetApiKey()}.{this._meetingNo}.{ts}.{this._role}.{msgHash}";
                var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
                return System.Convert.ToBase64String(tokenBytes).TrimEnd(this._padding);
            }
        }

        private long ToTimestamp(DateTime dt)
        {
            long epoch = (dt.Ticks - this._settingProvider.GetEpochConstant()) / 10000;
            return epoch;
        }
    }
}